using Mapster;
using Reservations.API.Core;
using Reservations.API.Core.Protos;
using Reservations.API.DTOs;

namespace Reservations.API.Endpoints.Mapster
{
    public static class MapsterConfig
    {
        public static void Configure()
        {
            //CreateReservationDTO to Reservation
            TypeAdapterConfig<CreateReservationDTO, Reservation>.NewConfig()
                .Map(dest => dest.SumPrice, src => src.SumPrice)
                .Map(dest => dest.DateOfReservation, src => src.DateOfReservation)
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.ReservationComponents, src => src.ReservationComponents);

            //GRPC MAPPINGS
            //ResevationGRPC to Reservation
            TypeAdapterConfig<CreateReservationGrpc, Reservation>.NewConfig()
                .Map(dest => dest.SumPrice, src => src.SumPrice)
                .Map(dest => dest.DateOfReservation, src => src.DateOfReservation)
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.ReservationComponents, src => src.ReservationComponents);
        }
    }
}
