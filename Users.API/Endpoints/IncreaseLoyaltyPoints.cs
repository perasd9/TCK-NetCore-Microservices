﻿using Ardalis.ApiEndpoints;
using Identity.API.Application;
using Identity.API.Core.Abstractions;
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
        public override async Task<ActionResult> HandleAsync([FromRoute] IncreaseLoyaltyPointsRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _userService.IncreaseLoyaltyPoints(request.Id, request.Amount);

            return result.IsSuccess ? Ok("Loyalty points increased!") : ApiResults.Problem(result);
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
