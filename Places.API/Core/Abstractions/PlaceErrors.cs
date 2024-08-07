namespace Places.API.Core.Abstractions
{
    public static class PlaceErrors
    {
        public static readonly Error DoesntExist = new("Place.DoesntExist", "Place doesn't exist");
    }
}
