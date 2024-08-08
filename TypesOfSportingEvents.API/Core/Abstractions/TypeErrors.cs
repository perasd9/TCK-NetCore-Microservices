namespace TypesOfSportingEvents.API.Core.Abstractions
{
    public static class TypeErrors
    {
        public static readonly Error DoesntExist = Error.NotFound("TypeOfSportingEvent.DoesntExist", "Type doesn't exist");
    }
}
