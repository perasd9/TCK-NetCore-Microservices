using Microsoft.EntityFrameworkCore;
using Reservations.API.Core.Interfaces.Base;
using System.Linq.Expressions;

namespace Reservations.API.Infrastructure.Repositories.Base
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected ReservationsContext _context;

        protected Repository(ReservationsContext context)
        {
            _context = context;
        }


        public IQueryable<TEntity> GetAll() => _context.Set<TEntity>().AsNoTracking();

        public IQueryable<TEntity> GetBySearch(Expression<Func<TEntity, bool>> expression) => _context.Set<TEntity>()
            .AsNoTracking().Where(expression);

        public void Save(TEntity entity) => _context.Add(entity);
        public void Update(TEntity entity) => _context.Update(entity);
        public void Delete(TEntity entity) => _context.Remove(entity);
    }
}
