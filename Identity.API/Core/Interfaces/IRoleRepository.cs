using System.Linq.Expressions;

namespace Identity.API.Core.Interfaces
{
    public interface IRoleRepository
    {
        public IQueryable<Role> GetByCondition(Expression<Func<Role, bool>> condition, string includes = "");
    }
}
