using ProtoBuf;

namespace Reservations.API.Core
{
    [ProtoContract]
    public class User
    {
        [ProtoMember(1)]
        public Guid UserId { get; set; }
        [ProtoMember(2)]
        public string JMBG { get; set; } = "";
        [ProtoMember(3)]
        public string Name { get; set; } = "";
        [ProtoMember(4)]
        public string Surname { get; set; } = "";
        [ProtoMember(5)]
        public DateTime DateOfBirth { get; set; }
        [ProtoMember(6)]
        public string Email { get; set; } = "";
        [ProtoMember(7)]
        public string Password { get; set; } = "";
        [ProtoMember(8)]
        public Guid PlaceId { get; set; }
        //public Reservation? Place { get; set; }
    }
}
