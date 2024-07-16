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
    }
}
