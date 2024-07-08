namespace Places.API.Core.Interfaces
{
    public interface IPlaceRepository
    {
        public IQueryable<Place> GetAll();
    }
}
