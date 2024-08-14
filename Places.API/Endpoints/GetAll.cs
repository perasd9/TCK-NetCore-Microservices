using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Places.API.Application;
using Places.API.Core;
using Places.API.Core.Abstractions;
using Places.API.Core.Pagination;
using Places.API.Core.Protos;
using Places.API.Endpoints.QueryParameters;

namespace Places.API.Endpoints
{
    public class GetAll : EndpointBaseAsync
        .WithRequest<PlaceQueryParameters>
        .WithActionResult<PaginationList<GetPlaceDTO>>
    {
        private readonly PlaceService _placeService;

        public GetAll(PlaceService placeService)
        {
            _placeService = placeService;
        }

        [HttpGet("api/v1/places")]
        public override async Task<ActionResult<PaginationList<GetPlaceDTO>>> HandleAsync([FromQuery]PlaceQueryParameters queryParameters, CancellationToken cancellationToken = default)
        {
            var result = await _placeService.GetAll(queryParameters);

            PaginationList<GetPlaceDTO> values = new PaginationList<GetPlaceDTO>
            {
                Items = result.Value.Items.Select(p => new GetPlaceDTO()
                {
                    PlaceId = p.PlaceId,
                    PlaceName = p.PlaceName,
                    PlaceName22 = new Core.Field(),
                    PlaceName23 = new Core.Field(),
                    PlaceName24 = new Core.Field(),
                    PlaceName25 = new Core.Field(),
                    PlaceName26 = new Core.Field(),
                    PlaceName27 = new List<Core.Field>() { new Core.Field() },
                    PlaceName67 = new List<Core.Enumeration>(){ new Core.Enumeration() },
                })
            };

            return result.IsSuccess ? Ok(values) : ApiResults.Problem(result);
        }
    }
}
