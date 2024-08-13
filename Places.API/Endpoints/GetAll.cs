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

            var values  = result.Value.Items.Select(p => new GetPlaceDTO()
            {
                PlaceId = p.PlaceId,
                PlaceName = p.PlaceName,
                PlaceName22 = new Core.Field() { PlaceName3 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" },
                PlaceName23 = new Core.Field() { PlaceName3 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" },
                PlaceName24 = new Core.Field() { PlaceName3 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" },
                PlaceName25 = new Core.Field() { PlaceName3 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" },
                PlaceName26 = new Core.Field() { PlaceName3 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" },
                PlaceName27 = { new Core.Field() { PlaceName3 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" }, },
                PlaceName2 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                PlaceName3 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                PlaceName4 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                PlaceName5 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                PlaceName6 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                PlaceName7 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                PlaceName8 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                PlaceName9 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                PlaceName11 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                PlaceName12 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                PlaceName13 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                PlaceName14 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                PlaceName15 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                PlaceName16 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                PlaceName21 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                PlaceName17 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                PlaceName18 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                PlaceName19 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                PlaceName20 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                PlaceName28 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
            })!;

            return result.IsSuccess ? Ok(values) : ApiResults.Problem(result);
        }
    }
}
