using Ardalis.ApiEndpoints;
using Grpc.Net.Client;
using Identity.API.Application;
using Identity.API.Core;
using Identity.API.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Users.API.Core.Protos;

namespace Identity.API.Endpoints
{
    public class GetAll : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<IEnumerable<User>>
    {
        private readonly UserService _userService;

        public GetAll(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("api/v1/users")]
        [OutputCache]
        public override async Task<ActionResult<IEnumerable<User>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var result = await _userService.GetAll(cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : ApiResults.Problem(result);
        }
    }
}
