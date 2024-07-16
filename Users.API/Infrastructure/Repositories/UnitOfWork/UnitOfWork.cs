using Identity.API.Core.Interfaces;
using Identity.API.Core.Interfaces.UnitOfWork;

namespace Identity.API.Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly UsersContext _context;
        private readonly UserRepository _userRepository;
        private readonly RoleRepository _roleRepository;
        public UnitOfWork(UsersContext context)
        {
            _context = context;
            _userRepository = new UserRepository(_context);
            _roleRepository = new RoleRepository(_context);
        }

        public IUserRepository UserRepository => _userRepository;
        public IRoleRepository RoleRepository => _roleRepository;

        public async Task SaveChanges() => await _context.SaveChangesAsync();


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
