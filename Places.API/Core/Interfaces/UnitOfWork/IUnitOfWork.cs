namespace Places.API.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IPlaceRepository PlaceRepository { get; }

        public Task SaveChanges();
    }
}
