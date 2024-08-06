using Ardalis.ApiEndpoints;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Users.API.Application;
using Reservations.API.Core;
using Reservations.API.Endpoints.QueryParameters;
using Reservations.API.Core.Pagination;

namespace Reservations.API.Endpoints
{
    public class GetAll : EndpointBaseAsync
        .WithRequest<ReservationQueryParameters>
        .WithActionResult<PaginationList<Reservation>>
    {
        private readonly ReservationService _reservationService;
        private readonly IHttpClientFactory _httpClientFactory;

        public GetAll(ReservationService reservationService, IHttpClientFactory httpClientFactory)
        {
            _reservationService = reservationService;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("api/v1/reservations")]
        public override async Task<ActionResult<PaginationList<Reservation>>> HandleAsync([FromQuery] ReservationQueryParameters queryParameters, CancellationToken cancellationToken = default)
        {
            HttpClient http = _httpClientFactory.CreateClient();

            http.DefaultRequestVersion = HttpVersion.Version20;

            //HttpResponseMessage response = await http.GetAsync("https://localhost:9401/api/v1/sportingevents", cancellationToken);
            var reservations = await _reservationService.GetAll(queryParameters);
            //var events = JsonSerializer.Deserialize<List<SportingEvent>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});


            return Ok(reservations);
        }
    }
}
