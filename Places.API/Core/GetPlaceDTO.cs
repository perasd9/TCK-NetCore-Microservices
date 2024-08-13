using ProtoBuf;
using System.ComponentModel.DataAnnotations.Schema;

namespace Places.API.Core
{
    //JUST FOR TESTING PURPOSE OF MAPPING
    [ProtoContract]
    public class GetPlaceDTO
    {
        [ProtoMember(1)]
        public Guid PlaceId { get; set; }
        [ProtoMember(2)]
        public string PlaceName { get; set; } = "";
        [ProtoMember(3)]
        [NotMapped]
        public string PlaceName1 { get; set; } = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        [NotMapped]
        [ProtoMember(4)]
        public string PlaceName2 { get; set; } = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        [NotMapped]
        [ProtoMember(5)]
        public string PlaceName3 { get; set; } = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        [NotMapped]
        [ProtoMember(6)]
        public string PlaceName4 { get; set; } = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        [NotMapped]
        [ProtoMember(7)]
        public string PlaceName5 { get; set; } = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        [NotMapped]
        [ProtoMember(8)]
        public string PlaceName6 { get; set; } = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        [NotMapped]
        [ProtoMember(9)]
        public string PlaceName7 { get; set; } = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        [NotMapped]
        [ProtoMember(10)]
        public string PlaceName8 { get; set; } = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        [NotMapped]
        [ProtoMember(11)]
        public string PlaceName9 { get; set; } = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        [NotMapped]
        [ProtoMember(12)]
        public string PlaceName11 { get; set; } = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        [NotMapped]
        [ProtoMember(13)]
        public string PlaceName12 { get; set; } = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        [NotMapped]
        [ProtoMember(14)]
        public string PlaceName13 { get; set; } = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        [NotMapped]
        [ProtoMember(15)]
        public string PlaceName14 { get; set; } = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        [NotMapped]
        [ProtoMember(16)]
        public string PlaceName15 { get; set; } = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        [NotMapped]
        [ProtoMember(17)]
        public string PlaceName16 { get; set; } = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        [NotMapped]
        [ProtoMember(18)]
        public string PlaceName17 { get; set; } = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        [NotMapped]
        [ProtoMember(19)]
        public string PlaceName18 { get; set; } = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        [NotMapped]
        [ProtoMember(20)]
        public string PlaceName19 { get; set; } = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        [NotMapped]
        [ProtoMember(21)]
        public string PlaceName20 { get; set; } = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        [NotMapped]
        [ProtoMember(22)]
        public string PlaceName21 { get; set; } = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        [NotMapped]
        [ProtoMember(23)]
        public Field PlaceName22 { get; set; } = new Field();
        [NotMapped]
        [ProtoMember(24)]
        public Field PlaceName23 { get; set; } = new Field();
        [NotMapped]
        [ProtoMember(25)]
        public Field PlaceName24 { get; set; } = new Field();
        [NotMapped]
        [ProtoMember(26)]
        public Field PlaceName25 { get; set; } = new Field();
        [NotMapped]
        [ProtoMember(27)]
        public Field PlaceName26 { get; set; } = new Field();
        [NotMapped]
        [ProtoMember(28)]
        public List<Field> PlaceName27 { get; set; } = new List<Field>() { new(), new(), new(), new() };
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName28 { get; set; } = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
    }
}
