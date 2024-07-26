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

        //saga operation commit
        public async Task Save(TEntity entity) => await _context.AddAsync(entity);
        public void Update(TEntity entity) => _context.Update(entity);
        //saga operation rollback
        public void Delete(TEntity entity) => _context.Remove(entity);
    }
}
