using Microsoft.EntityFrameworkCore;
using Places.API.Core;
using Places.API.Core.Interfaces.UnitOfWork;

namespace Places.API.Application
{
    public class PlaceService
    {
        private IUnitOfWork _unitOfWork;

        public PlaceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Place>> GetAll()
        {
            return await _unitOfWork.PlaceRepository.GetAll().ToListAsync();
        }
    }
}
