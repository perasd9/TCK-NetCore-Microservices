using Identity.API.Core;
using Identity.API.Core.Abstractions;
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

        public async Task<Result<List<User>>> GetAll()
        {
            var users = await _unitOfWork.UserRepository.GetAll().ToListAsync();

            return Result.Success(users);
        }

        public async Task<Result> IncreaseLoyaltyPoints(Guid id, double loyaltyPoints)
        {
            await _unitOfWork.UserRepository.IncreaseLoyaltyPoints(id, loyaltyPoints);

            await _unitOfWork.SaveChanges();

            return Result.Success();
        }

        public async Task<Result> DecreaseLoyaltyPoints(Guid id, double loyaltyPoints)
        {
            await _unitOfWork.UserRepository.DecreaseLoyaltyPoints(id, loyaltyPoints);

            await _unitOfWork.SaveChanges();

            return Result.Success();
        }
    }
}
