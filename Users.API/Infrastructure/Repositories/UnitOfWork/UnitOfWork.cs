using Microsoft.EntityFrameworkCore;
using Users.API.Core.Interfaces;
using Users.API.Core.Interfaces.UnitOfWork;

namespace Users.API.Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private UsersContext _context;
        private readonly UserRepository _userRepository;
        public UnitOfWork(UsersContext context)
        {
            _context = context;
            _userRepository = new UserRepository(_context);
        }

        public IUserRepository UserRepository => _userRepository;

        public async Task SaveChanges() => await _context.SaveChangesAsync();
    }
}
