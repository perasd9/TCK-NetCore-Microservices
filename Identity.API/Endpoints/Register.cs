using Ardalis.ApiEndpoints;
using Identity.API.Application;
using Identity.API.Core;
using Identity.API.DTOs;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Endpoints
{
    public class Register : EndpointBaseAsync
        .WithRequest<RegisterUserDTO>
        .WithActionResult
    {
        private readonly IMapper _mapper;
        private readonly AuthenticationService _authenticationService;

        public Register(IMapper mapper, AuthenticationService authenticationService)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        [HttpPost("api/v1/users/register")]
        public override async Task<ActionResult> HandleAsync(RegisterUserDTO request, CancellationToken cancellationToken = default)
        {
            User user = _mapper.Map<User>(request);
            await _authenticationService.Register(user);

            return Ok();
        }
    }
}
