using Microsoft.EntityFrameworkCore;
using Reservations.API.Core;
using Reservations.API.Core.Interfaces.UnitOfWork;

namespace Users.API.Application
{
    public class ReservationService
    {
        private IUnitOfWork _unitOfWork;

        public ReservationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Reservation>> GetAll()
        {
            return await _unitOfWork.ReservationRepository .GetAll().ToListAsync();
        }
    }
}
