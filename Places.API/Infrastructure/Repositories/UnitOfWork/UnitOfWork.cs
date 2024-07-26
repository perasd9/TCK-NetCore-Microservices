using Places.API.Core.Interfaces;
using Places.API.Core.Interfaces.UnitOfWork;

namespace Places.API.Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly PlaceRepository _placeRepository;
        private readonly PlacesContext _context;
        public UnitOfWork(PlacesContext context)
        {
            _context = context;
            _placeRepository = new PlaceRepository(_context);
        }
        public IPlaceRepository PlaceRepository => _placeRepository;

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
