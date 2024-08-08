﻿using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SportingEvents.API.Application;

namespace SportingEvents.API.Endpoints
{
    public class IncreaseAvailableTickets : EndpointBaseAsync
        .WithRequest<IncreaseAvailableTicketsRequest>
        .WithActionResult
    {
        private readonly SportingEventService _sportingEventService;

        public IncreaseAvailableTickets(SportingEventService sportingEventService)
        {
            _sportingEventService = sportingEventService;
        }
        [HttpPut("api/v1/sporting-events/{id}/available-tickets/increase")]
        public override async Task<ActionResult> HandleAsync([FromRoute]IncreaseAvailableTicketsRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                await _sportingEventService.IncreaseAvailableTickets(request.Id, request.Amount);
            }
            catch (Exception)
            {

                return BadRequest();
            }

            return Ok();
        }
    }

    public class IncreaseAvailableTicketsRequest
    {
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }

        [FromQuery]
        public int Amount { get; set; }
    }
}