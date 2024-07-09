using Microsoft.EntityFrameworkCore;
using TypesOfSportingEvents.API.Core;
using TypesOfSportingEvents.API.Core.Interfaces.UnitOfWork;

namespace TypesOfSportingEvents.API.Application
{
    public class TypeOfSportingEventService
    {
        private IUnitOfWork _unitOfWork;

        public TypeOfSportingEventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TypeOfSportingEvent>> GetAll()
        {
            return await _unitOfWork.TypeOfSportingEventRepository.GetAll().ToListAsync();
        }
    }
}
