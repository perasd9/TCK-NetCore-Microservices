using Microsoft.EntityFrameworkCore;
using SportingEvents.API.Core;
using SportingEvents.API.Core.Interfaces.UnitOfWork;

namespace SportingEvents.API.Application
{
    public class SportingEventService
    {
        private IUnitOfWork _unitOfWork;

        public SportingEventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SportingEvent>> GetAll()
        {
            return await _unitOfWork.SportingEventRepository.GetAll().ToListAsync();
        }
    }
}
