using Identity.API.Core;
using Identity.API.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Identity.API.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly UsersContext _context;

        public RoleRepository(UsersContext context)
        {
            _context = context;
        }

        public IQueryable<Role> GetByCondition(Expression<Func<Role, bool>> condition, string includes = "")
        {
            IQueryable<Role> query = _context.Roles.AsQueryable().AsNoTracking();

            foreach (var include in includes.Split(",", StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(include).AsNoTracking();

            query = query.Where(condition);

            return query.AsSplitQuery();
        }
    }
}
