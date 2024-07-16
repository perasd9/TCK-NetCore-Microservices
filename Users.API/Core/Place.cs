using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.API.Core
{
    public class Place
    {
        public Guid PlaceId { get; set; }
        public string PlaceName { get; set; } = "";
    }
}
