using Microsoft.EntityFrameworkCore;
using Reservations.API.Core;
using Reservations.API.Core.Abstractions;
using Reservations.API.Core.Interfaces.UnitOfWork;
using Reservations.API.Core.Pagination;
using Reservations.API.Core.Protos;
using Reservations.API.Endpoints.QueryParameters;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Transactions;

namespace Users.API.Application
{
    public class ReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ReservationService(IUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
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

            var httpResponse = await http.PutAsync($"{_configuration.GetSection("Routes")["UserRoute"]}/{reservation.UserId}/loyalty-points/increase?Amount={reservation.ReservationComponents?.Count * 10}", new StringContent(
                    JsonSerializer.Serialize(new object()),
                    Encoding.UTF8,
                    "application/json"));

            if (httpResponse.StatusCode != HttpStatusCode.OK)
                return Result.Failure(ReservationErrors.IncreaseLoyaltyPoints(httpResponse.ToString()));

            foreach (var item in reservation.ReservationComponents!)
            {
                httpResponse = await http.PutAsync($"{_configuration.GetSection("Routes")["SportingEventRoute"]}/{item.SportingEventId}/available-tickets/decrease?Amount={item.NumberOfTickets}", new StringContent(
                        JsonSerializer.Serialize(new object()),
                        Encoding.UTF8,
                        "application/json"));

                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    httpResponse = await http.PutAsync($"{_configuration.GetSection("Routes")["UserRoute"]}/{reservation.UserId}/loyalty-points/decrease?Amount={reservation.ReservationComponents?.Count * 10}", new StringContent(
                    JsonSerializer.Serialize(new object()),
                    Encoding.UTF8,
                    "application/json"));

                    foreach (var item1 in reservation.ReservationComponents!)
                    {
                        httpResponse = await http.PutAsync($"{_configuration.GetSection("Routes")["SportingEventRoute"]}/{item.SportingEventId}/available-tickets/increase?Amount={item.NumberOfTickets}", new StringContent(
                                JsonSerializer.Serialize(new object()),
                                Encoding.UTF8,
                                "application/json"));
                        if (item1 == item) return Result.Failure(ReservationErrors.IncreaseAvailableTickets(httpResponse.ToString()));
                    }
                }
            }

            await _unitOfWork.SaveChanges();

            transaction.Complete();

            return Result.Success();
        }

        public async Task Delete(Guid id)
        {

        }
    }
}
