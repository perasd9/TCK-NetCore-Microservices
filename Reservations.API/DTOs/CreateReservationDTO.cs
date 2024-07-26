using Reservations.API.Core;

namespace Reservations.API.DTOs
{
    public class CreateReservationDTO
    {
        public double SumPrice { get; set; }
        public DateTime DateOfReservation { get; set; }
        public Guid UserId { get; set; }
        public IList<ReservationComponent>? ReservationComponents { get; set; }
    }
}
