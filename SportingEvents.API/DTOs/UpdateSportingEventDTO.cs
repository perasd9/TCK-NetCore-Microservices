namespace SportingEvents.API.DTOs
{
    public class UpdateSportingEventDTO
    {
        public Guid SportingEventId { get; set; }
        public string SportingEventName { get; set; } = "";
        public string SportingEventDescription { get; set; } = "";
        public double SportingEventTicketPrice { get; set; }
        public DateTime DateOfSportingEvent { get; set; }
        public Guid TypeOfSportingEventId { get; set; }
        public Guid UserId { get; set; }
        public int AvailableTickets { get; set; }
    }
}
