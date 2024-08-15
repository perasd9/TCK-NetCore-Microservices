using Ardalis.ApiEndpoints;
using Identity.API.Application;
using Identity.API.Core;
using Identity.API.Core.Abstractions;
using Identity.API.DTOs;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Endpoints
{
    public class Login : EndpointBaseAsync
        .WithRequest<LoginUserDTO>
        .WithActionResult
    {
        private readonly IMapper _mapper;
        private readonly AuthenticationService _authenticationService;

        public Login(AuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        [HttpPost("api/v1/users/login")]
        public async override Task<ActionResult> HandleAsync([FromBody] LoginUserDTO request, CancellationToken cancellationToken = default)
        {
            User? user = _mapper.Map<User>(request);

            var result = await _authenticationService.LoginUser(user);

            return result.IsSuccess ? Ok(_authenticationService.GenerateToken(result.Value)) : ApiResults.Problem(result);
        }
    }
}
