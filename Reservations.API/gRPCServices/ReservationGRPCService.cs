using Google.Protobuf;
using Grpc.Core;
using Reservations.API.Core;
using Reservations.API.Core.Protos;
using Users.API.Application;

namespace Reservations.API.gRPCServices
{
    public class ReservationGRPCService : gRPCReservationService.gRPCReservationServiceBase
    {
        private readonly ReservationService _reservationService;

        public ReservationGRPCService(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async override Task<PaginationList> GetAll(QueryParameters request, ServerCallContext context)
        {
            var reservations = await _reservationService.GetAll(request);

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
    }
}
