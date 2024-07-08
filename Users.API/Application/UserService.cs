using Microsoft.EntityFrameworkCore;
using Users.API.Core;
using Users.API.Core.Interfaces.UnitOfWork;

namespace Users.API.Application
{
    public class UserService
    {
        private IUnitOfWork _unitOfWork;

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
