namespace SportingEvents.API.Core.Interfaces
{
    public interface ISportingEventRepository
    {
        public IQueryable<SportingEvent> GetAll();
    }
}
