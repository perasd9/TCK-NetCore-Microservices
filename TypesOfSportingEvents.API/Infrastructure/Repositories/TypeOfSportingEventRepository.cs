using TypesOfSportingEvents.API.Core;
using TypesOfSportingEvents.API.Core.Interfaces;

namespace TypesOfSportingEvents.API.Infrastructure.Repositories
{
    public class TypeOfSportingEventRepository : ITypeOfSportingEventRepository
    {
        private TypesOfSportingEventsContext _context;

        public TypeOfSportingEventRepository(TypesOfSportingEventsContext context)
        {
            _context = context;
        }

        public IQueryable<TypeOfSportingEvent> GetAll()
        {
            return _context.TypesOfSportingEvents;
        }
    }
}
