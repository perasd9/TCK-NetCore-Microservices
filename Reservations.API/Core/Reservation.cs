using System.ComponentModel.DataAnnotations.Schema;

namespace Reservations.API.Core
{
    public class Reservation
    {
        public Guid ReservationId { get; set; }
        public double SumPrice { get; set; }
        public DateTime DateOfReservation { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public IList<ReservationComponent> ReservationComponents { get; set; }
    }
}
