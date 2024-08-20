using ProtoBuf;
using System.Text.Json.Serialization;

namespace Reservations.API.Core
{
    [ProtoContract]
    public class ReservationComponent
    {
        [ProtoMember(1)]
        public Guid ReservationId { get; set; }
        [JsonIgnore]
        public Reservation? Reservation { get; set; }
        [ProtoMember(2)]
        public int SerialNumber { get; set; }
        [ProtoMember(3)]
        public double Price { get; set; }
        [ProtoMember(4)]
        public int NumberOfTickets { get; set; }
        [ProtoMember(5)]
        public double SumComponentPrice { get; set; }
        [ProtoMember(6)]
        public Guid SportingEventId { get; set; }
        [ProtoMember(7)]
        public SportingEvent? SportingEvent { get; set; }
    }
}
