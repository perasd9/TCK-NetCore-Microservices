using Ardalis.ApiEndpoints;
using Identity.API.Application;
using Identity.API.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Endpoints
{
    public class DecreaseLoyaltyPoints : EndpointBaseAsync
        .WithRequest<DecreaseLoyaltyPointsRequest>
        .WithActionResult
    {
        private readonly UserService _userService;

        public DecreaseLoyaltyPoints(UserService userService)
        {
            _userService = userService;
        }
        [HttpPut("api/v1/users/{id}/loyalty-points/decrease")]
        public override async Task<ActionResult> HandleAsync([FromRoute] DecreaseLoyaltyPointsRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _userService.DecreaseLoyaltyPoints(request.Id, request.Amount);

            return result.IsSuccess ? Ok("Loyalty points decreased!") : ApiResults.Problem(result);
        }
    }
    public class DecreaseLoyaltyPointsRequest
    {
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }

        [FromQuery]
        public double Amount { get; set; }
    }
}
