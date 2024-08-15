using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Places.API.Application;
using Places.API.Core;
using Places.API.Core.Abstractions;

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

        [HttpGet("api/v1/places/{id}")]
        //[Authorize]
        public override async Task<ActionResult<Place>> HandleAsync([FromRoute(Name = "id")] Guid request, CancellationToken cancellationToken = default)
        {
            var result = await _placeService.GetById(request);

            return result.IsSuccess ? Ok(result.Value) : ApiResults.Problem(result);
        }
    }
}
