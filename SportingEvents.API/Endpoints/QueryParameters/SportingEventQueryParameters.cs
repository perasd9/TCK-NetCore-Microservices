using SportingEvents.API.Endpoints.QueryParameters.Base;

namespace SportingEvents.API.Endpoints.QueryParameters
{
    public class SportingEventQueryParameters : Params
    {
        public string? Search { get; set; } = string.Empty;
        public string? OrderBy { get; set; } = "SportingEventName";
    }
}
