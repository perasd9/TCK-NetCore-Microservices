namespace SportingEvents.API.Core.Abstractions
{
    public static class SportingEventErrors
    {
        public static readonly Error DecreaseLessThanZero =
            new("SportingEvent.DecreaseLessThanZero", "You cannot decrease available tickets cause amount is greater");
    }
}
