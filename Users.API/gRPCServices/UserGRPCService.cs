using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Identity.API.Application;
using Identity.API.Core;
using MapsterMapper;
using Microsoft.AspNetCore.OutputCaching;
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

        [OutputCache]
        public async override Task GetAll(Empty request, IServerStreamWriter<UserList> responseStream, ServerCallContext context)
        {
            var result = await _userService.GetAllGrpc();
            List<User> users;

            if (result.IsSuccess)
                users = result.Value;
            else
                return;

            var userMessage = users.Select(user => new UserGrpc
            {
                UserId = new UUID() { Id = user.UserId.ToString() },
                Name = user.Name,
                Email = user.Email,
                JMBG = user.JMBG,
                DateOfBirth = Timestamp.FromDateTime(DateTime.SpecifyKind(user.DateOfBirth, DateTimeKind.Utc)),
                LoyaltyPoints = user.LoyaltyPoints,
                Password = user.Password,
                Surname = user.Surname,
                RoleId = new UUID { Id = user.RoleId.ToString() },
                Role = new RoleGrpc { RoleId = new UUID { Id = user.RoleId.ToString() }, RoleName = user.Role!.RoleName },
                PlaceId = new UUID { Id = user.PlaceId.ToString() },
                Place = new PlaceGrpc { PlaceId = user.PlaceId.ToString(), PlaceName = user.Place!.PlaceName }
            }).ToList();

            await responseStream.WriteAsync(new UserList { Users = { userMessage } });
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
            User user = _mapper.Map<User>(request);

            var result = await _authervice.Register(user);

            context.Status = result.IsSuccess ? new Status(StatusCode.InvalidArgument, "Bad Request!")
                : new Status(StatusCode.OK, "User successfully registered!");

            return new();
        }

        public async override Task<Empty> IncreaseLoyaltyPoints(IncreaseLoyaltyPointsRequestGRPC request, ServerCallContext context)
        {
            var result = await _userService.IncreaseLoyaltyPoints(new Guid(request.UserId.Id), request.Amount);

            context.Status = result.IsSuccess != true ? new Status(StatusCode.InvalidArgument, "Bad request!")
                : new Status(StatusCode.OK, "Loyalty points increased!");

            return new();
        }

        public async override Task<Empty> DecreaseLoyaltyPoints(DecreaseLoyaltyPointsRequestGRPC request, ServerCallContext context)
        {
            var result = await _userService.DecreaseLoyaltyPoints(new Guid(request.UserId.Id), request.Amount);

            context.Status = result.IsSuccess != true ? new Status(StatusCode.InvalidArgument, "Bad request!")
                : new Status(StatusCode.OK, "Loyalty points decreased!");

            return new();
        }
    }
}
