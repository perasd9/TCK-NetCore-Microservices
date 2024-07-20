using ProtoBuf;

namespace SportingEvents.API.Core
{
    [ProtoContract]
    public class SportingEvent
    {
        [ProtoMember(1)]
        public Guid SportingEventId { get; set; }
        [ProtoMember(2)]
        public string SportingEventName { get; set; } = "";
        [ProtoMember(3)]
        public string SportingEventDescription { get; set; } = "";
        [ProtoMember(4)]
        public double SportingEventTicketPrice { get; set; }
        [ProtoMember(5)]
        public DateTime DateOfSportingEvent { get; set; }
        [ProtoMember(6)]
        public Guid TypeOfSportingEventId { get; set; }
        [ProtoMember(7)]
        public TypeOfSportingEvent? TypeOfSportingEvent { get; set; }
        [ProtoMember(8)]
        public Guid UserId { get; set; }
        [ProtoMember(9)]
        public User? User { get; set; }
        [ProtoMember(10)]
        public int AvailableTickets { get; set; }
    }
}
