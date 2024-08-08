using Identity.API.Core;
using Identity.API.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Identity.API.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersContext _context;

        public UserRepository(UsersContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetAll(string includes = "")
        {
            IQueryable<User> query = _context.Users.AsQueryable().AsNoTracking();

            foreach(var include in includes.Split(",", StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(include).AsNoTracking();

            return query.AsSplitQuery();
        }

        public IQueryable<User> GetByCondition(Expression<Func<User, bool>> condition, string includes = "")
        {
            IQueryable<User> query = _context.Users.AsQueryable().AsNoTracking();

            foreach (var include in includes.Split(",", StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(include).AsNoTracking();

            query = query.Where(condition);

            return query.AsSplitQuery();
        }

        public async Task<User?> GetById(Guid id) => await _context.Users.FindAsync(id);

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task IncreaseLoyaltyPoints(Guid id, double loyaltyPoints)
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null) user.LoyaltyPoints += loyaltyPoints;
            else throw new Exception();
        }

        public async Task DecreaseLoyaltyPoints(Guid id, double loyaltyPoints)
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null) user.LoyaltyPoints -= loyaltyPoints;
            else throw new Exception();
        }
    }
}
