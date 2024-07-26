using Microsoft.EntityFrameworkCore;
using SportingEvents.API.Core.Interfaces;
using SportingEvents.API.Core.Interfaces.UnitOfWork;

namespace SportingEvents.API.Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private SportingEventsContext _context;
        private readonly SportingEventRepository _sportingEventRepository;
        public UnitOfWork(SportingEventsContext context)
        {
            _context = context;
            _sportingEventRepository = new SportingEventRepository(_context);
        }

        public ISportingEventRepository SportingEventRepository => _sportingEventRepository;

        public async Task SaveChanges() => await _context.SaveChangesAsync();


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
