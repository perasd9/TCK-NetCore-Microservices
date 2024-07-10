namespace Reservations.API.Core
{
    public class ReservationComponent
    {
        public Guid ReservationId { get; set; }
        public Reservation? Reservation { get; set; }
        public int SerialNumber { get; set; }
        public double Price { get; set; }
        public int NumberOfTickets { get; set; }
        public double SumComponentPrice { get; set; }
        public Guid SportingEventId { get; set; }
        public SportingEvent SportingEvent { get; set; }
    }
}
