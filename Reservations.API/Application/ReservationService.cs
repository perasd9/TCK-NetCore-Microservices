using Grpc.Net.Client;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Wrap;
using Reservations.API.Core;
using Reservations.API.Core.Abstractions;
using Reservations.API.Core.Interfaces.UnitOfWork;
using Reservations.API.Core.Pagination;
using Reservations.API.Core.Protos;
using Reservations.API.Endpoints.QueryParameters;
using System.Data.Common;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Transactions;

namespace Reservations.API.Application
{
    public class ReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly AsyncPolicyWrap<HttpResponseMessage> _policyWrap;
        private readonly AsyncPolicyWrap _policyWrapGrpc;

        public ReservationService(IUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

            var retryPolicy = Policy.HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.InternalServerError || r.StatusCode == HttpStatusCode.BadGateway)
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

            var circuitBreakerPolicy = Policy.HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.InternalServerError || r.StatusCode == HttpStatusCode.BadGateway)
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

            var fallbackPolicy = Policy.HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.InternalServerError || r.StatusCode == HttpStatusCode.BadGateway)
                .Or<Exception>()
                .FallbackAsync(
                fallbackAction: (result, context, cancellationToken) =>
                {
                    Console.WriteLine("Fallback triggered, manual intervention needed.");
                    return Task.FromResult(new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                    {
                        Content = new StringContent("Fallback response triggered")
                    });
                },
                onFallbackAsync: (outcome, context) =>
                {
                    Console.WriteLine("Fallback log triggered");
                    return Task.CompletedTask;
                });

            var fallbackPolicyGrpc = Policy.Handle<Exception>()
                .FallbackAsync((ct) =>
                {
                    Console.WriteLine("Fallback triggered, manual intervention needed.");

                    return Task.CompletedTask;
                });



            _policyWrap = Policy.WrapAsync(fallbackPolicy, circuitBreakerPolicy, retryPolicy);
            _policyWrapGrpc = Policy.WrapAsync(fallbackPolicyGrpc, circuitBreakerPolicyGrpc, retryPolicyGrpc);
        }

        //REST METHOD
        public async Task<Result<PaginationList<Reservation>>> GetAll(ReservationQueryParameters queryParameters)
        {
            var items = await _unitOfWork.ReservationRepository.GetAll(queryParameters).ToListAsync();

            return Result.Success(new PaginationList<Reservation>(items, items.Count, queryParameters.PageNumber, queryParameters.PageSize));
        }

        //GRPC METHOD
        public async Task<Result<PaginationList<Reservation>>> GetAll(QueryParameters queryParameters)
        {
            var items = await _unitOfWork.ReservationRepository.GetAll(queryParameters).ToListAsync();

            return Result.Success(new PaginationList<Reservation>(items, items.Count, queryParameters.PageNumber, queryParameters.PageSize));
        }

        public async Task<Result> Add(Reservation reservation)
        {
            var http = _httpClientFactory.CreateClient();

            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _unitOfWork.ReservationRepository.Save(reservation);

            var httpResponse = await _policyWrap.ExecuteAsync(() =>
        http.PutAsync($"{_configuration.GetSection("Routes")["UserRoute"]}/{reservation.UserId}/loyalty-points/increase?Amount={reservation.ReservationComponents?.Count * 10}", new StringContent(
            JsonSerializer.Serialize(new object()),
            Encoding.UTF8,
            "application/json")));


            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                return Result.Failure(ReservationErrors.IncreaseLoyaltyPoints("User " + httpResponse.ReasonPhrase!));
            }

            foreach (var item in reservation.ReservationComponents!)
            {
                httpResponse = await _policyWrap.ExecuteAsync(() =>
                    http.PutAsync($"{_configuration.GetSection("Routes")["SportingEventRoute"]}/{item.SportingEventId}/available-tickets/decrease?Amount={item.NumberOfTickets}", new StringContent(
                        JsonSerializer.Serialize(new object()),
                        Encoding.UTF8,
                        "application/json")));

                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    var sportingEventResponse = httpResponse;

                    httpResponse = await _policyWrap.ExecuteAsync(() =>
                        http.PutAsync($"{_configuration.GetSection("Routes")["UserRoute"]}/{reservation.UserId}/loyalty-points/decrease?Amount={reservation.ReservationComponents?.Count * 10}", new StringContent(
                            JsonSerializer.Serialize(new object()),
                            Encoding.UTF8,
                            "application/json")));

                    foreach (var item1 in reservation.ReservationComponents!)
                    {
                        httpResponse = await _policyWrap.ExecuteAsync(() =>
                                http.PutAsync($"{_configuration.GetSection("Routes")["SportingEventRoute"]}/{item.SportingEventId}/available-tickets/increase?Amount={item.NumberOfTickets}", new StringContent(
                                JsonSerializer.Serialize(new object()),
                                Encoding.UTF8,
                                "application/json")));

                        if (item1 == item) return Result.Failure(ReservationErrors.IncreaseAvailableTickets("Sporting Event " + sportingEventResponse.ReasonPhrase!));
                    }
                }
            }

            try
            {
                await _unitOfWork.SaveChanges();
            }
            catch (DbException)
            {
                httpResponse = await _policyWrap.ExecuteAsync(() =>
                        http.PutAsync($"{_configuration.GetSection("Routes")["UserRoute"]}/{reservation.UserId}/loyalty-points/decrease?Amount={reservation.ReservationComponents?.Count * 10}", new StringContent(
                        JsonSerializer.Serialize(new object()),
                        Encoding.UTF8,
                        "application/json")));

                foreach (var item in reservation.ReservationComponents!)
                {
                    httpResponse = await _policyWrap.ExecuteAsync(() =>
                            http.PutAsync($"{_configuration.GetSection("Routes")["SportingEventRoute"]}/{item.SportingEventId}/available-tickets/increase?Amount={item.NumberOfTickets}", new StringContent(
                            JsonSerializer.Serialize(new object()),
                            Encoding.UTF8,
                            "application/json")));
                }
                return Result.Failure(Error.Validation("DbError", "Reservation didn't succedeed"));
            }

            transaction.Complete();

            return Result.Success();
        }

        public async Task<Result> AddGrpc(Reservation reservation)
        {
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            var channelUser = GrpcChannel.ForAddress("https://localhost:9102", new GrpcChannelOptions
            {
                HttpHandler = httpHandler,
            });

            var channelSportingEvent = GrpcChannel.ForAddress("https://localhost:9301", new GrpcChannelOptions
            {
                HttpHandler = httpHandler,
            });

            var clientUser = new gRPCUserService.gRPCUserServiceClient(channelUser);
            var clientSportingEvent = new gRPCSportingEventService.gRPCSportingEventServiceClient(channelUser);

            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _unitOfWork.ReservationRepository.Save(reservation);

            try
            {
                var reply = await _policyWrapGrpc.ExecuteAsync(async () =>
                await clientUser.IncreaseLoyaltyPointsAsync(
                new IncreaseLoyaltyPointsRequestGRPC()
                {
                    UserId = new UUID() { Id = reservation.UserId.ToString() },
                    Amount = double.Parse((reservation.ReservationComponents!.Count * 10).ToString())
                }));
            }
            catch (Exception ex)
            {
                return Result.Failure(ReservationErrors.IncreaseLoyaltyPoints(ex.Message));
            }


            foreach (var item in reservation.ReservationComponents!)
            {
                try
                {
                    var reply = await _policyWrapGrpc.ExecuteAsync(async () =>
                    await clientSportingEvent.DecreaseAvailableTicketsAsync(
                    new DecreaseAvailableTicketsRequestGRPC()
                    {
                        SportingEventId = new UUID() { Id = item.SportingEventId.ToString() },
                        Amount = reservation.ReservationComponents!.Count * 10
                    }));
                }
                catch (Exception ex)
                {
                    var reply = await _policyWrapGrpc.ExecuteAsync(async () =>
                    await clientUser.DecreaseLoyaltyPointsAsync(
                    new DecreaseLoyaltyPointsRequestGRPC()
                    {
                        UserId = new UUID() { Id = reservation.UserId.ToString() },
                        Amount = double.Parse((reservation.ReservationComponents!.Count * 10).ToString())
                    }));

                    foreach (var item1 in reservation.ReservationComponents!)
                    {
                        reply = await _policyWrapGrpc.ExecuteAsync(async () =>
                        await clientSportingEvent.IncreaseAvailableTicketsAsync(
                        new IncreaseAvailableTicketsRequestGRPC()
                        {
                            SportingEventId = new UUID() { Id = item.SportingEventId.ToString() },
                            Amount = reservation.ReservationComponents!.Count * 10
                        }));

                        if (item1 == item) return Result.Failure(ReservationErrors.IncreaseAvailableTickets(ex.Message));
                    }
                }
            }

            try
            {
                await _unitOfWork.SaveChanges();
            }
            catch (DbException)
            {
                var reply = await _policyWrapGrpc.ExecuteAsync(async () =>
                await clientUser.DecreaseLoyaltyPointsAsync(
                new DecreaseLoyaltyPointsRequestGRPC()
                {
                    UserId = new UUID() { Id = reservation.UserId.ToString() },
                    Amount = double.Parse((reservation.ReservationComponents!.Count * 10).ToString())
                }));

                foreach (var item in reservation.ReservationComponents!)
                {
                    reply = await _policyWrapGrpc.ExecuteAsync(async () =>
                    await clientSportingEvent.IncreaseAvailableTicketsAsync(
                    new IncreaseAvailableTicketsRequestGRPC()
                    {
                        SportingEventId = new UUID() { Id = item.SportingEventId.ToString() },
                        Amount = reservation.ReservationComponents!.Count * 10
                    }));
                }
                return Result.Failure(Error.Validation("DbError", "Reservation didn't succedeed"));
            }

            transaction.Complete();

            return Result.Success();
        }

        public async Task Delete(Guid id)
        {

        }
    }
}
