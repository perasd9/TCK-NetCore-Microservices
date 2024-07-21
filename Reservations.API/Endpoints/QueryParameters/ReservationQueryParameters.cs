using Reservations.API.Endpoints.QueryParameters.Base;

namespace Reservations.API.Endpoints.QueryParameters
{
    public class ReservationQueryParameters : Params
    {
        public string? Search { get; set; } = string.Empty;
        public string? OrderBy { get; set; } = "DateOfReservation";
    }
}
