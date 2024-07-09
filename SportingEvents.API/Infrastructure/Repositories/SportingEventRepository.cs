using SportingEvents.API.Core;
using SportingEvents.API.Core.Interfaces;

namespace SportingEvents.API.Infrastructure.Repositories
{
    public class SportingEventRepository : ISportingEventRepository
    {
        private SportingEventsContext _context;

        public SportingEventRepository(SportingEventsContext context)
        {
            _context = context;
        }

        public IQueryable<SportingEvent> GetAll()
        {
            return _context.SportingEvents;
        }
    }
}
