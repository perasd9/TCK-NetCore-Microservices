using Microsoft.EntityFrameworkCore;
using Reservations.API.Core.Interfaces;
using Reservations.API.Core.Interfaces.UnitOfWork;

namespace Reservations.API.Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
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
    }
}
