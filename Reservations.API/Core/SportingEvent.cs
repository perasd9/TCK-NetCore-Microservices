using ProtoBuf;

namespace Reservations.API.Core
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
        //public TypeOfSportingEvent? TypeOfSportingEvent { get; set; }
        [ProtoMember(7)]
        public Guid UserId { get; set; }
        [ProtoMember(8)]
        public User? User { get; set; }
    }
}
