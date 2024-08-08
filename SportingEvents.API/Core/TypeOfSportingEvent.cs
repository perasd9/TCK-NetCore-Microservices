using ProtoBuf;

namespace SportingEvents.API.Core
{
    [ProtoContract]
    public class TypeOfSportingEvent
    {
        [ProtoMember(1)]
        public Guid TypeOfSportingEventId { get; set; }
        [ProtoMember(2)]
        public string TypeOfSportingEventName { get; set; } = "";
    }
}
