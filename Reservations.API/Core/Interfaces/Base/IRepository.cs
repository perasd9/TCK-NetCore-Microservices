using System.Linq.Expressions;

namespace Reservations.API.Core.Interfaces.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public IQueryable<TEntity> GetAll();
        public IQueryable<TEntity> GetBySearch(Expression<Func<TEntity, bool>> expression);
        public void Save(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
    }
}
