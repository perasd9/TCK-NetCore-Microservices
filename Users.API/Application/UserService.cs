using Azure;
using Grpc.Core;
using Grpc.Net.Client;
using Identity.API.Core;
using Identity.API.Core.Abstractions;
using Identity.API.Core.Interfaces.UnitOfWork;
using Identity.API.Core.Pagination;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Wrap;
using ProtoBuf;
using System.Net;
using System.Text.Json;
using Users.API.Core.Protos;

namespace Identity.API.Application
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AsyncPolicyWrap<HttpResponseMessage> _policyWrap;
        private readonly AsyncPolicyWrap _policyWrapGrpc;

        public UserService(IUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory)
        {
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;

            var retryPolicy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .Or<Exception>()
                .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                (outcome, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Retry attempt {retryCount}");
                });

            var retryPolicyGrpc = Policy.Handle<Exception>()
                .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                (outcome, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Retry attempt {retryCount}");
                });

            var circuitBreakerPolicy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .Or<Exception>()
                .CircuitBreakerAsync(2, TimeSpan.FromMinutes(2),
                onBreak: (outcome, timespan) =>
                {
                    Console.WriteLine("Circuit breaker triggered");
                },
                onReset: () =>
                {
                    Console.WriteLine("Circuit breaker reset");
                },
                onHalfOpen: () =>
                {
                    Console.WriteLine("Circuit breaker half-open");
                });

            var circuitBreakerPolicyGrpc = Policy.Handle<Exception>()
                .CircuitBreakerAsync(2, TimeSpan.FromMinutes(2),
                onBreak: (outcome, timespan) =>
                {
                    Console.WriteLine("Circuit breaker triggered");
                },
                onReset: () =>
                {
                    Console.WriteLine("Circuit breaker reset");
                },
                onHalfOpen: () =>
                {
                    Console.WriteLine("Circuit breaker half-open");
                });


            _policyWrap = Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);
            _policyWrapGrpc = Policy.WrapAsync(retryPolicyGrpc, circuitBreakerPolicyGrpc);
        }

        //Rest application logic
        public async Task<Result<List<User>>> GetAll(CancellationToken cancellationToken = default)
        {
            HttpClient http = _httpClientFactory.CreateClient();

            http.DefaultRequestVersion = HttpVersion.Version11;


            var users = await _unitOfWork.UserRepository.GetAll("Role").ToListAsync(cancellationToken: cancellationToken);

            HttpResponseMessage response = await _policyWrap.ExecuteAsync(async () =>
                await http.GetAsync("https://localhost:9501/api/v1/places", cancellationToken));

            if (!response.IsSuccessStatusCode)
                return Result.Failure<List<User>>(Error.Failure("Users.PlacesFailure", "Failed to retrieve place"));

            string test = await response.Content.ReadAsStringAsync(cancellationToken);

            var places = JsonSerializer.Deserialize<PaginationList<Place>>(await response.Content.ReadAsStringAsync(cancellationToken),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            foreach (var user in users)
                user.Place = places?.Items.FirstOrDefault(p => p.PlaceId == user.PlaceId);

            return Result.Success(users);
        }

        //gRPC application logic
        public async Task<Result<List<User>>> GetAllGrpc(CancellationToken cancellationToken = default)
        {
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            var channel = GrpcChannel.ForAddress("https://localhost:9501", new GrpcChannelOptions
            {
                HttpHandler = httpHandler,
            });

            var client = new gRPCPlaceService.gRPCPlaceServiceClient(channel);

            var users = await _unitOfWork.UserRepository.GetAll("Role").ToListAsync(cancellationToken: cancellationToken);

            try
            {
                var reply = await _policyWrapGrpc.ExecuteAsync(async () => await client.GetAllAsync(new(), cancellationToken: cancellationToken));

                using var memoryStream = new MemoryStream(reply.Places.ToByteArray());

                var places = Serializer.Deserialize<List<Place>>(memoryStream);

                foreach (var user in users)
                    user.Place = places?.FirstOrDefault(p => p.PlaceId == user.PlaceId);

                return Result.Success(users);
            }
            catch (Exception)
            {
                return Result.Failure<List<User>>(Error.Failure("Users.PlacesFailure", "Failed to retrieve place"));
            }
        }

        public async Task<Result> IncreaseLoyaltyPoints(Guid id, double loyaltyPoints)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);

            if (user == null) return Result.Failure(UserErrors.NotFound);

            await _unitOfWork.UserRepository.IncreaseLoyaltyPoints(id, loyaltyPoints);

            await _unitOfWork.SaveChanges();

            return Result.Success();
        }

        public async Task<Result> DecreaseLoyaltyPoints(Guid id, double loyaltyPoints)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);

            if (user == null) return Result.Failure(UserErrors.NotFound);
            if (user.LoyaltyPoints < loyaltyPoints) return Result.Failure(UserErrors.DecreaseLessThanZero);

            await _unitOfWork.UserRepository.DecreaseLoyaltyPoints(id, loyaltyPoints);

            await _unitOfWork.SaveChanges();

            return Result.Success();
        }
    }
}
