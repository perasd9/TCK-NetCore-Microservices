using SportingEvents.API.Core.Interfaces;

namespace SportingEvents.API.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        public ISportingEventRepository SportingEventRepository { get; }

        public Task SaveChanges();
    }
}
