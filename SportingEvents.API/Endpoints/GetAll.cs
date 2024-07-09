using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using SportingEvents.API.Application;
using SportingEvents.API.Core;

namespace SportingEvents.API.Endpoints
{
    public class GetAll : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult
    {
        private SportingEventService _sportingEventService;
        private readonly IHttpClientFactory _httpClientFactory;

        public GetAll(SportingEventService sportingEventService, IHttpClientFactory httpClientFactory)
        {
            _sportingEventService = sportingEventService;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("api/v1/events")]
        public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
        {
            HttpClient http = _httpClientFactory.CreateClient();

            //HttpResponseMessage response = await http.GetAsync("https://localhost:9201/api/v1/types", cancellationToken);
            var events = await _sportingEventService.GetAll();
            //var types = JsonSerializer.Deserialize<List<TypeOfSportingEvent>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});

            //events[0].Place = places?[0];

            return Ok(events);
        }
    }
}
