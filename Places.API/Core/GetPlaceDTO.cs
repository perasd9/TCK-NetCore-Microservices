﻿using ProtoBuf;
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
        public string PlaceName1 { get; set; } = "";
        [NotMapped]
        [ProtoMember(4)]
        public string PlaceName2 { get; set; } = "";
        [NotMapped]
        [ProtoMember(5)]
        public string PlaceName3 { get; set; } = "";
        [NotMapped]
        [ProtoMember(6)]
        public string PlaceName4 { get; set; } = "";
        [NotMapped]
        [ProtoMember(7)]
        public string PlaceName5 { get; set; } = "";
        [NotMapped]
        [ProtoMember(8)]
        public string PlaceName6 { get; set; } = "";
        [NotMapped]
        [ProtoMember(9)]
        public string PlaceName7 { get; set; } = "";
        [NotMapped]
        [ProtoMember(10)]
        public string PlaceName8 { get; set; } = "";
        [NotMapped]
        [ProtoMember(11)]
        public string PlaceName9 { get; set; } = "";
        [NotMapped]
        [ProtoMember(12)]
        public string PlaceName11 { get; set; } = "";
        [NotMapped]
        [ProtoMember(13)]
        public string PlaceName12 { get; set; } = "";
        [NotMapped]
        [ProtoMember(14)]
        public string PlaceName13 { get; set; } = "";
        [NotMapped]
        [ProtoMember(15)]
        public string PlaceName14 { get; set; } = "";
        [NotMapped]
        [ProtoMember(16)]
        public string PlaceName15 { get; set; } = "";
        [NotMapped]
        [ProtoMember(17)]
        public string PlaceName16 { get; set; } = "";
        [NotMapped]
        [ProtoMember(18)]
        public string PlaceName17 { get; set; } = "";
        [NotMapped]
        [ProtoMember(19)]
        public string PlaceName18 { get; set; } = "";
        [NotMapped]
        [ProtoMember(20)]
        public string PlaceName19 { get; set; } = "";
        [NotMapped]
        [ProtoMember(21)]
        public string PlaceName20 { get; set; } = "";
        [NotMapped]
        [ProtoMember(22)]
        public string PlaceName21 { get; set; } = "";
        [NotMapped]
        [ProtoMember(23)]
        public Field PlaceName22 { get; set; }
        [NotMapped]
        [ProtoMember(24)]
        public Field PlaceName23 { get; set; }
        [NotMapped]
        [ProtoMember(25)]
        public Field PlaceName24 { get; set; }
        [NotMapped]
        [ProtoMember(26)]
        public Field PlaceName25 { get; set; }
        [NotMapped]
        [ProtoMember(27)]
        public Field PlaceName26 { get; set; }
        [NotMapped]
        [ProtoMember(28)]
        public List<Field> PlaceName27 { get; set; }
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName28 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName29 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName30 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName31 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName32 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName33 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName34 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName35 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName36 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName37 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName38 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName39 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName40 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName41 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName42 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName43 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName44 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName45 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName46 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName47 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName48 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName49 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName50 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName51 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName52 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName53 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName54 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName55 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName56 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName57 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName58 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName59 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public string PlaceName60 { get; set; } = "";
        [NotMapped]
        [ProtoMember(29)]
        public Enumeration PlaceName61 { get; set; }
        [NotMapped]
        [ProtoMember(29)]
        public Enumeration PlaceName62 { get; set; }
        [NotMapped]
        [ProtoMember(29)]
        public Enumeration PlaceName63 { get; set; }
        [NotMapped]
        [ProtoMember(29)]
        public Enumeration PlaceName64 { get; set; }
        [NotMapped]
        [ProtoMember(29)]
        public Enumeration PlaceName65 { get; set; }
        [NotMapped]
        [ProtoMember(29)]
        public Enumeration PlaceName66 { get; set; }
        [NotMapped]
        [ProtoMember(29)]
        public List<Enumeration> PlaceName67 { get; set; }
    }
}