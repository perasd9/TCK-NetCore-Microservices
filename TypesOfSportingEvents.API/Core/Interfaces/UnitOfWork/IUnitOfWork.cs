namespace TypesOfSportingEvents.API.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        public ITypeOfSportingEventRepository TypeOfSportingEventRepository { get; }

        public Task SaveChanges();
    }
}
