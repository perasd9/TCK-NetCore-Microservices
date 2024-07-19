namespace SportingEvents.API.DTOs
{
    public class DeleteSportingEventDTO
    {
        public Guid SportingEventId { get; set; }
        public Guid TypeOfSportingEventId { get; set; }
        public Guid UserId { get; set; }
        public int AvailableTickets { get; set; }
    }
}
