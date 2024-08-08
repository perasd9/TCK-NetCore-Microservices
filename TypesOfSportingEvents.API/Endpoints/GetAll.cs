using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TypesOfSportingEvents.API.Application;
using TypesOfSportingEvents.API.Core;
using TypesOfSportingEvents.API.Core.Abstractions;
using TypesOfSportingEvents.API.Core.Pagination;
using TypesOfSportingEvents.API.Endpoints.QueryParameters;

namespace TypesOfSportingEvents.API.Endpoints
{
    public class GetAll : EndpointBaseAsync
        .WithRequest<TypeOfSportingEventQueryParameters>
        .WithActionResult<PaginationList<TypeOfSportingEvent>>
    {
        private readonly TypeOfSportingEventService _typeOfSportingEventsService;

        public GetAll(TypeOfSportingEventService typeOfSportingEventsService)
        {
            _typeOfSportingEventsService = typeOfSportingEventsService;
        }

        [HttpGet("api/v1/types-of-sporting-events")]
        public override async Task<ActionResult<PaginationList<TypeOfSportingEvent>>> HandleAsync([FromQuery]TypeOfSportingEventQueryParameters queryParameters, CancellationToken cancellationToken = default)
        {
            var result = await _typeOfSportingEventsService.GetAll(queryParameters);
            
            return result.IsSuccess ? Ok(result.Value) : ApiResults.Problem(result);
        }
    }
}
