﻿using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SportingEvents.API.Application;
using SportingEvents.API.Core;
using SportingEvents.API.Endpoints.QueryParameters;
using SportingEvents.API.Core.Pagination;
using SportingEvents.API.Core.Abstractions;

namespace SportingEvents.API.Endpoints
{
    public class GetAll : EndpointBaseAsync
        .WithRequest<SportingEventQueryParameters>
        .WithActionResult<PaginationList<SportingEvent>>
    {
        private readonly SportingEventService _sportingEventService;

        public GetAll(SportingEventService sportingEventService)
        {
            _sportingEventService = sportingEventService;
        }

        [HttpGet("api/v1/sporting-events")]
        public override async Task<ActionResult<PaginationList<SportingEvent>>> HandleAsync([FromQuery] SportingEventQueryParameters queryParameters, CancellationToken cancellationToken = default)
        {
            //HttpResponseMessage response = await http.GetAsync("https://localhost:9201/api/v1/types", cancellationToken);
            var result = await _sportingEventService.GetAll(queryParameters);
            //var types = JsonSerializer.Deserialize<List<TypeOfSportingEvent>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});

            //events[0].Place = places?[0];

            return result.IsSuccess ? Ok(result.Value) : ApiResults.Problem(result);
        }
    }
}
