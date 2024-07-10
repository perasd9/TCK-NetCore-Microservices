using Ardalis.ApiEndpoints;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Users.API.Application;
using Users.API.Core;
using Users.API.Core.Protos;

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

            http.DefaultRequestVersion = HttpVersion.Version20;

            HttpResponseMessage response = await http.GetAsync("https://localhost:9501/api/v1/places", cancellationToken);
            var users = await _userService.GetAll();
            var places = JsonSerializer.Deserialize<List<Place>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});

            users[0].Place = places?[0];

            return Ok(users);
        }

        [HttpGet("api/v2/users")]
        public async Task<ActionResult> Handle(CancellationToken cancellationToken = default)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var channel = GrpcChannel.ForAddress("https://localhost:9501", new GrpcChannelOptions
            {
                HttpHandler = httpHandler
            });

            var client = new PlaceService.PlaceServiceClient(channel);

            var request = new Empty();

            var reply = await client.GetAllAsync(request);
            

            var users = await _userService.GetAll();
            var places = reply.Places.Select(placeGrpc => new Place()
            {
                PlaceId = new Guid(placeGrpc.PlaceId),
                PlaceName = placeGrpc.PlaceName,
            }).ToList();

            users[0].Place = places?[0];

            return Ok(users);
        }
    }
}
