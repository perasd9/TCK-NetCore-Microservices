using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SportingEvents.API.Application;

namespace SportingEvents.API.Endpoints
{
    public class DecreaseAvailableTickets : EndpointBaseAsync
        .WithRequest<DecreaseAvailableTicketsRequest>
        .WithActionResult
    {
        private readonly SportingEventService _sportingEventService;

        public DecreaseAvailableTickets(SportingEventService sportingEventService)
        {
            _sportingEventService = sportingEventService;
        }
        [HttpPut("api/v1/sporting-events/{id}/available-tickets/decrease")]
        public override async Task<ActionResult> HandleAsync([FromRoute]DecreaseAvailableTicketsRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                await _sportingEventService.DecreaseAvailableTickets(request.Id, request.Amount);
            }
            catch (Exception)
            {

                return BadRequest();
            }

            return Ok();
        }
    }

    public class DecreaseAvailableTicketsRequest
    {
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }

        [FromQuery]
        public int Amount { get; set; }
    }
}
