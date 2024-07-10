using Reservations.API.Core;
using Reservations.API.Core.Interfaces;

namespace Reservations.API.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private ReservationsContext _context;

        public ReservationRepository(ReservationsContext context)
        {
            _context = context;
        }

        public IQueryable<Reservation> GetAll()
        {
            return _context.Reservations;
        }
    }
}
