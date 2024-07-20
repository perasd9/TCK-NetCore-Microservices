using ProtoBuf;

namespace SportingEvents.API.Core
{
    [ProtoContract]
    public class TypeOfSportingEvent
    {
        [ProtoMember(1)]
        public Guid PlaceId { get; set; }
        [ProtoMember(2)]
        public string PlaceName { get; set; } = "";
    }
}
