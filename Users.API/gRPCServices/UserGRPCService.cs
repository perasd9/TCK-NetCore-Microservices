using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Identity.API.Application;
using Identity.API.Core;
using MapsterMapper;
using Users.API.Core.Protos;
using Empty = Users.API.Core.Protos.Empty;

namespace Identity.API.gRPCServices
{
    public class UserGRPCService : gRPCUserService.gRPCUserServiceBase
    {
        private readonly UserService _userService;
        private readonly AuthenticationService _authervice;
        private readonly IMapper _mapper;

        public UserGRPCService(UserService userService, IMapper mapper, AuthenticationService authervice)
        {
            _userService = userService;
            _mapper = mapper;
            _authervice = authervice;
        }

        public async override Task GetAll(Empty request, IServerStreamWriter<UserGrpc> responseStream, ServerCallContext context)
        {
            //var httpHandler = new HttpClientHandler();
            //httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            //var channel = GrpcChannel.ForAddress("https://localhost:9501", new GrpcChannelOptions
            //{
            //    HttpHandler = httpHandler,
            //});

            //var client = new gRPCPlaceService.gRPCPlaceServiceClient(channel);


            var result = await _userService.GetAll();
            List<User> users;

            if (result.IsSuccess)
                users = result.Value;
            else
                return;

            foreach (var user in users)
            {
                var userMessage = new UserGrpc
                {
                    UserId = new UUID { Id = user.UserId.ToString() },
                    Name = user.Name,
                    Email = user.Email,
                    JMBG = user.JMBG,
                    DateOfBirth = Timestamp.FromDateTime(DateTime.SpecifyKind(user.DateOfBirth, DateTimeKind.Utc)),
                    LoyaltyPoints = user.LoyaltyPoints,
                    Password = user.Password,
                    Surname = user.Surname,

                };

                await responseStream.WriteAsync(userMessage);
            }
        }

        public override async Task<Empty> Login(LoginRequestGRPC request, ServerCallContext context)
        {
            User? user = _mapper.Map<User>(request);

            var result = await _authervice.LoginUser(user);

            context.Status = result.IsSuccess ? new Status(StatusCode.Unauthenticated, "Invalid Credentials!")
                : new Status(StatusCode.OK, _authervice.GenerateToken(user));

            return new();
        }

        public async override Task<Empty> Register(RegisterRequestGRPC request, ServerCallContext context)
        {
            User? user = _mapper.Map<User>(request);

            await _authervice.Register(user);

            return new();
        }

        public async override Task<Empty> IncreaseLoyaltyPoints(IncreaseLoyaltyPointsRequestGRPC request, ServerCallContext context)
        {
            try
            {
                await _userService.IncreaseLoyaltyPoints(new Guid(request.UserId.Id), request.Amount);
                return new();

            }
            catch (Exception)
            {
                context.Status = new Status(StatusCode.InvalidArgument, "Bad request!");
                return new();

            }
        }

        public async override Task<Empty> DecreaseLoyaltyPoints(DecreaseLoyaltyPointsRequestGRPC request, ServerCallContext context)
        {
            try
            {
                await _userService.DecreaseLoyaltyPoints(new Guid(request.UserId.Id), request.Amount);
                return new();

            }
            catch (Exception)
            {
                context.Status = new Status(StatusCode.InvalidArgument, "Bad request!");
                return new();

            }
        }
    }
}
