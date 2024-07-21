using ProtoBuf;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reservations.API.Core
{
    [ProtoContract]
    public class Reservation
    {
        [ProtoMember(1)]
        public Guid ReservationId { get; set; }
        [ProtoMember(2)]
        public double SumPrice { get; set; }
        [ProtoMember(3)]
        public DateTime DateOfReservation { get; set; }
        [ProtoMember(4)]
        public Guid UserId { get; set; }
        [ProtoMember(5)]
        public User? User { get; set; }
        [ProtoMember(6)]
        public IList<ReservationComponent>? ReservationComponents { get; set; }
    }
}
