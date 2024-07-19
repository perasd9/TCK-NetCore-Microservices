using TypesOfSportingEvents.API.Endpoints.QueryParameters.Base;

namespace TypesOfSportingEvents.API.Endpoints.QueryParameters
{
    public class TypeOfSportingEventQueryParameters : Params
    {
        public string? Search { get; set; } = string.Empty;
        public string? OrderBy { get; set; } = "TypeOfSportingEventName";
    }
}
