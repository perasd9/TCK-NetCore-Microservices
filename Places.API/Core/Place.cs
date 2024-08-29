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

        [ProtoMember(3)]
        public Country Country { get; set; }

        [ProtoMember(4)]
        public State State { get; set; }
            
        [ProtoMember(5)]
        public string Mayor { get; set; } = "";

        [ProtoMember(6)]
        public int Population { get; set; } 

        [ProtoMember(7)]
        public double Area { get; set; } 

        [ProtoMember(8)]
        public int Elevation { get; set; } 

        [ProtoMember(9)]
        public double Latitude { get; set; }

        [ProtoMember(10)]
        public double Longitude { get; set; }

        [ProtoMember(11)]
        public string TimeZone { get; set; } = "";

        [ProtoMember(12)]
        public string OfficialLanguage { get; set; } = ""; 

        [ProtoMember(13)]
        public List<string> SpokenLanguages { get; set; } = new List<string>(); 

        [ProtoMember(14)]
        public string Currency { get; set; } = ""; 

        [ProtoMember(15)]
        public string PostalCode { get; set; } = ""; 

        [ProtoMember(16)]
        public int NumberOfDistricts { get; set; }

        [ProtoMember(17)]
        public int YearEstablished { get; set; } 

        [ProtoMember(18)]
        public List<string> HistoricalEvents { get; set; } = new List<string>(); 

        [ProtoMember(19)]
        public string FamousLandmarks { get; set; } = ""; 

        [ProtoMember(20)]
        public string MainEconomicActivities { get; set; } = ""; 

        [ProtoMember(21)]
        public double GDP { get; set; } 

        [ProtoMember(22)]
        public ClimateType ClimateType { get; set; }

        [ProtoMember(23)]
        public double AverageAnnualRainfall { get; set; }

        [ProtoMember(24)]
        public double AverageTemperature { get; set; } 

        [ProtoMember(25)]
        public bool IsCoastal { get; set; }

        [ProtoMember(26)]
        public int NumberOfUniversities { get; set; } 

        [ProtoMember(27)]
        public int NumberOfHospitals { get; set; } 

        [ProtoMember(28)]
        public int PublicTransportRoutes { get; set; }

        [ProtoMember(29)]
        public string MajorIndustries { get; set; } = ""; 

        [ProtoMember(30)]
        public string PopularFestivals { get; set; } = ""; 

        [ProtoMember(31)]
        public int NumberOfParks { get; set; }

        [ProtoMember(32)]
        public bool HasAirport { get; set; }

        [ProtoMember(33)]
        public bool HasSeaport { get; set; } 

        [ProtoMember(34)]
        public string MajorSportTeams { get; set; } = ""; 

        [ProtoMember(35)]
        public int AnnualTourists { get; set; } 

        [ProtoMember(36)]
        public List<string> SisterCities { get; set; } = new List<string>(); 

        [ProtoMember(37)]
        public string TransportationHub { get; set; } = ""; 

        [ProtoMember(38)]
        public bool IsUNESCOHeritage { get; set; } 

        [ProtoMember(39)]
        public double UnemploymentRate { get; set; } 

        [ProtoMember(40)]
        public string LocalCuisine { get; set; } = "";
    }
}
