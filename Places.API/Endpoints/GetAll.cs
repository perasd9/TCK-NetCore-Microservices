using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Places.API.Application;
using Places.API.Core;
using Places.API.Core.Pagination;
using Places.API.Endpoints.QueryParameters;

namespace Places.API.Endpoints
{
    public class GetAll : EndpointBaseAsync
        .WithRequest<PlaceQueryParameters>
        .WithActionResult<PaginationList<Place>>
    {
        private readonly PlaceService _placeService;

        public GetAll(PlaceService placeService)
        {
            _placeService = placeService;
        }

        [HttpGet("api/v1/places")]
        public override async Task<ActionResult<PaginationList<Place>>> HandleAsync([FromQuery]PlaceQueryParameters queryParameters, CancellationToken cancellationToken = default)
        {
            var places = await _placeService.GetAll(queryParameters);

            return Ok(places);
        }
    }
}
