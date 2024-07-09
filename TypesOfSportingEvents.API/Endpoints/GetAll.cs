using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using TypesOfSportingEvents.API.Application;
using TypesOfSportingEvents.API.Core;

namespace TypesOfSportingEvents.API.Endpoints
{
    public class GetAll : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult
    {
        private TypeOfSportingEventService _typeOfSportingEventsService;
        private readonly IHttpClientFactory _httpClientFactory;

        public GetAll(TypeOfSportingEventService typeOfSportingEventsService, IHttpClientFactory httpClientFactory)
        {
            _typeOfSportingEventsService = typeOfSportingEventsService;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("api/v1/typesofsportingevents")]
        public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
        {
            HttpClient http = _httpClientFactory.CreateClient();

            //HttpResponseMessage response = await http.GetAsync("https://localhost:9201/api/v1/types", cancellationToken);
            var types = await _typeOfSportingEventsService.GetAll();
            //var events = JsonSerializer.Deserialize<List<SportingEvent>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});

            //events[0].Place = places?[0];

            return Ok(types);
        }
    }
}
