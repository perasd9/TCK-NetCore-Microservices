using Grpc.Net.Client;
using Identity.API.Core;
using Identity.API.Core.Abstractions;
using Identity.API.Core.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using ProtoBuf;
using System.Net;
using System.Text.Json;
using System.Threading;
using Users.API.Core.Protos;

namespace Identity.API.Application
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;

        public UserService(IUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory)
        {
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
        }

        //Rest application logic
        public async Task<Result<List<User>>> GetAll(CancellationToken cancellationToken = default)
        {
            HttpClient http = _httpClientFactory.CreateClient();

            http.DefaultRequestVersion = HttpVersion.Version11;

            var users = await _unitOfWork.UserRepository.GetAll().ToListAsync(cancellationToken: cancellationToken);

            HttpResponseMessage response = await http.GetAsync("https://localhost:9501/api/v1/places", cancellationToken);

            var places = JsonSerializer.Deserialize<List<Place>>(await response.Content.ReadAsStringAsync(cancellationToken),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            foreach(var user in users)
                user.Place = places?.FirstOrDefault(p => p.PlaceId == user.PlaceId);

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

            var users = await _unitOfWork.UserRepository.GetAll().ToListAsync(cancellationToken: cancellationToken);

            var reply = await client.GetAllAsync(new(), cancellationToken: cancellationToken);

            using var memoryStream = new MemoryStream(reply.Places.ToByteArray());

            var places = Serializer.Deserialize<List<Place>>(memoryStream);
            
            foreach (var user in users)
                user.Place = places?.FirstOrDefault(p => p.PlaceId == user.PlaceId);

            return Result.Success(users);
        }

        public async Task<Result> IncreaseLoyaltyPoints(Guid id, double loyaltyPoints)
        {
            var user  = await _unitOfWork.UserRepository.GetById(id);

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
