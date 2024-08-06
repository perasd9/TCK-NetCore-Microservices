using Microsoft.EntityFrameworkCore;
using TypesOfSportingEvents.API.Core.Interfaces;
using TypesOfSportingEvents.API.Core.Interfaces.UnitOfWork;

namespace TypesOfSportingEvents.API.Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly TypesOfSportingEventsContext _context;
        private readonly IServiceProvider _serviceProvider;
        public UnitOfWork(TypesOfSportingEventsContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public ITypeOfSportingEventRepository TypeOfSportingEventRepository
        {
            get
            {
                return _serviceProvider.GetRequiredService<ITypeOfSportingEventRepository>();
            }
        }

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
