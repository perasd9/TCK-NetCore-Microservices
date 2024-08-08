using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TypesOfSportingEvents.API.Application;
using TypesOfSportingEvents.API.Core;
using TypesOfSportingEvents.API.Core.Abstractions;

namespace TypesOfSportingEvents.API.Endpoints
{
    public class GetById : EndpointBaseAsync
        .WithRequest<Guid>
        .WithActionResult<TypeOfSportingEvent>
    {
        private TypeOfSportingEventService _typeOfSportingEventsService;

        public GetById(TypeOfSportingEventService typeOfSportingEventsService)
        {
            _typeOfSportingEventsService = typeOfSportingEventsService;
        }

        [HttpGet("api/v1/types-of-sporting-events/{id}")]
        [Authorize]
        public override async Task<ActionResult<TypeOfSportingEvent>> HandleAsync([FromRoute(Name = "id")]Guid request, CancellationToken cancellationToken = default)
        {
            var result = await _typeOfSportingEventsService.GetById(request);

            return result.IsSuccess ? Ok(result.Value) : ApiResults.Problem(result);
        }
    }
}
