using System.ComponentModel.DataAnnotations.Schema;

namespace Users.API.Core
{
    public class Place
    {
        public Guid PlaceId { get; set; }
        public string PlaceName { get; set; } = "";
    }
}
