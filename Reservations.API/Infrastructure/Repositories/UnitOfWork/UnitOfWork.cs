using Microsoft.EntityFrameworkCore;
using Reservations.API.Core.Interfaces;
using Reservations.API.Core.Interfaces.UnitOfWork;

namespace Reservations.API.Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ReservationsContext _context;
        private readonly ReservationRepository _reservationRepository;
        public UnitOfWork(ReservationsContext context)
        {
            _context = context;
            _reservationRepository = new ReservationRepository(_context);
        }

        public IReservationRepository ReservationRepository => _reservationRepository;

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
