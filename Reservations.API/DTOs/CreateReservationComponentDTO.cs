namespace Reservations.API.DTOs
{
    public class CreateReservationComponentDTO
    {
        public Guid ReservationId { get; set; }
        public int SerialNumber { get; set; }
        public double Price { get; set; }
        public int NumberOfTickets { get; set; }
        public double SumComponentPrice { get; set; }
        public Guid SportingEventId { get; set; }
    }
}
