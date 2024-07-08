using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Users.API.Application;

namespace Users.API.Endpoints
{
    public class GetAll : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult
    {
        private UserService _userService;

        public GetAll(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("api/v1/users")]
        public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }
    }
}
