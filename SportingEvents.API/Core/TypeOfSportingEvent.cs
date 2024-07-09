using System.ComponentModel.DataAnnotations.Schema;

namespace SportingEvents.API.Core
{
    public class TypeOfSportingEvent
    {
        public Guid PlaceId { get; set; }
        public string PlaceName { get; set; } = "";
    }
}
