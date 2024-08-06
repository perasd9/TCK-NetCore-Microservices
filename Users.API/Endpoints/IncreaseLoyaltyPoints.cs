using Ardalis.ApiEndpoints;
using Identity.API.Application;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Endpoints
{
    public class IncreaseLoyaltyPoints : EndpointBaseAsync
        .WithRequest<IncreaseLoyaltyPointsRequest>
        .WithActionResult
    {
        private readonly UserService _userService;

        public IncreaseLoyaltyPoints(UserService userService)
        {
            _userService = userService;
        }
        [HttpPut("api/v1/users/{id}/loyalty-points/increase")]
        public override async Task<ActionResult> HandleAsync([FromRoute]IncreaseLoyaltyPointsRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                await _userService.IncreaseLoyaltyPoints(request.Id, request.Amount);
            }
            catch (Exception)
            {

                return BadRequest();
            }

            return Ok();
        }
    }

    public class IncreaseLoyaltyPointsRequest
    {
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }

        [FromQuery]
        public double Amount { get; set; }
    }
}
