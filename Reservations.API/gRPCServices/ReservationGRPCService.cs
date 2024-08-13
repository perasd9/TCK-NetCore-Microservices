using Google.Protobuf;
using Grpc.Core;
using MapsterMapper;
using Reservations.API.Application;
using Reservations.API.Core;
using Reservations.API.Core.Protos;

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
            request.PageNumber = 1;
            request.PageSize = int.MaxValue;
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

        public async override Task<Empty> Add(CreateReservationGrpc request, ServerCallContext context)
        {
            var reservation = _mapper.Map<Reservation>(request);

            var result = await _reservationService.Add(reservation);

            context.Status = result.IsSuccess ? new Status(StatusCode.InvalidArgument, "Bad request!")
                : new Status(StatusCode.OK, "Reservation created!");

            return new();
        }
    }
}
