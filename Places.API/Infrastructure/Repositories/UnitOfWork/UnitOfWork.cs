using Places.API.Core.Interfaces;
using Places.API.Core.Interfaces.UnitOfWork;

namespace Places.API.Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly PlacesContext _context;
        public UnitOfWork(PlacesContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }
        public IPlaceRepository PlaceRepository
        {
            get
            {
                return _serviceProvider.GetRequiredService<IPlaceRepository>();
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
