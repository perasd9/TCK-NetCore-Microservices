namespace Reservations.API.DTOs
{
    public class CreateReservationDTO
    {
        public Guid ReservationId { get; set; }
        public double SumPrice { get; set; }
        public DateTime DateOfReservation { get; set; }
        public Guid UserId { get; set; }
        public IList<CreateReservationComponentDTO>? ReservationComponents { get; set; }
    }
}
