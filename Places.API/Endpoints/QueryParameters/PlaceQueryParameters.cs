using Places.API.Endpoints.QueryParameters.Base;

namespace Places.API.Endpoints.QueryParameters
{
    public class PlaceQueryParameters : Params
    {
        public string? Search { get; set; } = string.Empty;
        public string? OrderBy { get; set; } = "PlaceName";
    }
}
