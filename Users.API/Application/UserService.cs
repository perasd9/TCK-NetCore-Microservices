using Identity.API.Core;
using Identity.API.Core.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Application
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<User>> GetAll()
        {
            return await _unitOfWork.UserRepository.GetAll().ToListAsync();
        }

        public async Task IncreaseLoyaltyPoints(Guid id, double loyaltyPoints)
        {
            await _unitOfWork.UserRepository.IncreaseLoyaltyPoints(id, loyaltyPoints);

            await _unitOfWork.SaveChanges();
        }

        public async Task DecreaseLoyaltyPoints(Guid id, double loyaltyPoints)
        {
            await _unitOfWork.UserRepository.DecreaseLoyaltyPoints(id, loyaltyPoints);

            await _unitOfWork.SaveChanges();
        }
    }
}
