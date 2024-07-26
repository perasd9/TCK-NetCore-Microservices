using System.Linq.Expressions;

namespace Identity.API.Core.Interfaces
{
    public interface IUserRepository
    {
        public IQueryable<User> GetAll(string includes = "");
        public IQueryable<User> GetByCondition(Expression<Func<User, bool>> condition, string includes = "");
        public Task Add(User user);

        //saga operation commit
        public Task IncreaseLoyaltyPoints(Guid id, double loyaltyPoints);
        //saga operation rollback
        public Task DecreaseLoyaltyPoints(Guid id, double loyaltyPoints);
    }
}
