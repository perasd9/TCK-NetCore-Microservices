using Microsoft.EntityFrameworkCore;
using TypesOfSportingEvents.API.Core.Interfaces;
using TypesOfSportingEvents.API.Core.Interfaces.UnitOfWork;

namespace TypesOfSportingEvents.API.Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private TypesOfSportingEventsContext _context;
        private readonly TypeOfSportingEventRepository _typeOfSportingEventRepository;
        public UnitOfWork(TypesOfSportingEventsContext context)
        {
            _context = context;
            _typeOfSportingEventRepository = new TypeOfSportingEventRepository(_context);
        }

        public ITypeOfSportingEventRepository TypeOfSportingEventRepository => _typeOfSportingEventRepository;

        public async Task SaveChanges() => await _context.SaveChangesAsync();
    }
}
