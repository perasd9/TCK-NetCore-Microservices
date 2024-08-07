using Google.Protobuf;
using Grpc.Core;
using MapsterMapper;
using Reservations.API.Core;
using Reservations.API.Core.Protos;
using Users.API.Application;

namespace Reservations.API.gRPCServices
{
    public class ReservationGRPCService : gRPCReservationService.gRPCReservationServiceBase
    {
        private readonly ReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationGRPCService(ReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        public async override Task<PaginationList> GetAll(QueryParameters request, ServerCallContext context)
        {
            var reservations = (await _reservationService.GetAll(request)).Value;

            var pagination = new PaginationList
            {
                HasNext = reservations.HasNext,
                HasPrevious = reservations.HasPrevious,
                PageIndex = reservations.PageIndex,
                PageSize = reservations.PageSize,
                TotalPages = reservations.TotalPages,
                Reservations = ByteString.CopyFrom(SerializeListToBytes(reservations.Items))
            };

            return pagination;
        }


        //helper method for serializing list
        private static byte[] SerializeListToBytes(IEnumerable<Reservation> reservations)
        {
            using var memoryStream = new MemoryStream();

            ProtoBuf.Serializer.Serialize(memoryStream, reservations);
            return memoryStream.ToArray();
        }

        public override Task<ReservationGrpc> GetById(UUID request, ServerCallContext context)
        {
            return base.GetById(request, context);
        }

        public async override Task<Empty> Add(CreateReservationGrpc request, ServerCallContext context)
        {
            var reservation = _mapper.Map<Reservation>(request);

            try
            {
                await _reservationService.Add(reservation);
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
