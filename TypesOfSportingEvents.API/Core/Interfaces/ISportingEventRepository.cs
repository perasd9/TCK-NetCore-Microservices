namespace TypesOfSportingEvents.API.Core.Interfaces
{
    public interface ITypeOfSportingEventRepository
    {
        public IQueryable<TypeOfSportingEvent> GetAll();
    }
}
