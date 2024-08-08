namespace Reservations.API.Core.Abstractions
{
    public static class ReservationErrors
    {
        public static Error IncreaseLoyaltyPoints(string message) { return new Error("Reservations.InreaseLoyaltyPoints", ErrorType.Validation, message); }
        public static Error IncreaseAvailableTickets(string message) { return new Error("Reservations.IncreaseAvailableTickets", ErrorType.Validation, message); }
    }
}
