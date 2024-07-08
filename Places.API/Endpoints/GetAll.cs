using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Places.API.Application;

namespace Places.API.Endpoints
{
    public class GetAll : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult
    {
        private PlaceService _placeService;

        public GetAll(PlaceService placeService)
        {
            _placeService = placeService;
        }

        [HttpGet("api/v1/places")]
        public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
        {
            var places = await _placeService.GetAll();
            return Ok(places);
        }
    }
}
