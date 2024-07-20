using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Places.API.Application;
using Places.API.Core;

namespace Places.API.Endpoints
{
    public class GetById : EndpointBaseAsync
        .WithRequest<Guid>
        .WithActionResult<Place>
    {
        private PlaceService _placeService;

        public GetById(PlaceService placeService)
        {
            _placeService = placeService;
        }

        [HttpGet("api/v1/types-of-sporting-events/{id}")]
        [Authorize]
        public override async Task<ActionResult<Place>> HandleAsync([FromRoute(Name = "id")] Guid request, CancellationToken cancellationToken = default)
        {
            var place = await _placeService.GetById(request);

            return Ok(place);
        }
    }
}
