using Users.API.Core;
using Users.API.Core.Interfaces;

namespace Users.API.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private UsersContext _context;

        public UserRepository(UsersContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users;
        }
    }
}
