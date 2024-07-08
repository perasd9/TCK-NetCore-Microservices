using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using Users.API.Application;
using Users.API.Core;

namespace Users.API.Endpoints
{
    public class GetAll : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult
    {
        private UserService _userService;
        private readonly IHttpClientFactory _httpClientFactory;

        public GetAll(UserService userService, IHttpClientFactory httpClientFactory)
        {
            _userService = userService;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("api/v1/users")]
        public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
        {
            HttpClient http = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await http.GetAsync("https://localhost:9501/api/v1/places", cancellationToken);
            var users = await _userService.GetAll();
            var places = JsonSerializer.Deserialize<List<Place>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});

            users[0].Place = places?[0];

            return Ok(users);
        }
    }
}
