using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Places.API.Application;
using Places.API.Core;

namespace Places.API.Endpoints
{
    public class GetAll : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult
    {
        private readonly PlaceService _placeService;

        public GetAll(PlaceService placeService)
        {
            _placeService = placeService;
        }

        [HttpGet("api/v1/places")]
        [Authorize(Roles = "User")]
        public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
        {
            //var places = await _placeService.GetAll();
            var places = new List<Place>
            {
                new Place
                {
                    PlaceId = new Guid(),
                    PlaceName = "p"
                },
                new Place
                {
                    PlaceId = new Guid(),
                    PlaceName = "p"
                },
                new Place
                {
                    PlaceId = new Guid(),
                    PlaceName = "p"
                },
                new Place
                {
                    PlaceId = new Guid(),
                    PlaceName = "p"
                },
            };
            return Ok(places);
        }
    }
}
