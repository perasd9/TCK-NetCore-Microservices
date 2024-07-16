using Ardalis.ApiEndpoints;
using Identity.API.Application;
using Identity.API.Core;
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

            user = await _authenticationService.LoginUser(user);

            if (user == null) return Unauthorized();

            return user == null ? BadRequest("Wrong credentials!") : Ok(_authenticationService.GenerateToken(user));
        }
    }
}
