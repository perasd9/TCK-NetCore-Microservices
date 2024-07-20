using ProtoBuf;

namespace Places.API.Core
{
    [ProtoContract]
    public class Place
    {
        [ProtoMember(1)]
        public Guid PlaceId { get; set; }
        [ProtoMember(2)]
        public string PlaceName { get; set; } = "";
    }
}
