namespace SportingEvents.API.Core
{
    public class SportingEvent
    {
        public Guid SportingEventId { get; set; }
        public string SportingEventName { get; set; } = "";
        public string SportingEventDescription { get; set; } = "";
        public double SportingEventTicketPrice { get; set; }
        public DateTime DateOfSportingEvent { get; set; }
        public Guid TypeOfSportingEventId { get; set; }
        public TypeOfSportingEvent? TypeOfSportingEvent { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public int AvailableTickets { get; set; }
    }
}
