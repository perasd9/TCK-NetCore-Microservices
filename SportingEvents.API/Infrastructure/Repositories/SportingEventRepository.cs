using Microsoft.EntityFrameworkCore;
using SportingEvents.API.Core;
using SportingEvents.API.Core.Interfaces;
using SportingEvents.API.Core.Protos;
using SportingEvents.API.Endpoints.QueryParameters;

namespace SportingEvents.API.Infrastructure.Repositories
{
    public class SportingEventRepository : ISportingEventRepository
    {
        private readonly SportingEventsContext _context;

        public SportingEventRepository(SportingEventsContext context)
        {
            _context = context;
        }

        //REST METHOD
        public IQueryable<SportingEvent> GetAll(SportingEventQueryParameters queryParameters) => _context.SportingEvents.AsNoTracking()
            .Where(@event => @event.SportingEventName.Contains(queryParameters.Search!.Trim().ToLower()))
                .OrderBy(@event => @event.SportingEventName)
                    .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize).Take(queryParameters.PageSize);

        //GRPC METHOD
        public IQueryable<SportingEvent> GetAll(QueryParameters queryParameters) => _context.SportingEvents.AsNoTracking()
            .Where(@event => @event.SportingEventName.Contains(queryParameters.Search!.Trim().ToLower()))
                .OrderBy(@event => @event.SportingEventName)
                    .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize).Take(queryParameters.PageSize);


        public async Task DecreaseAvailableTickets(Guid id, int availableTickets)
        {
            var @event = await _context.SportingEvents.FindAsync(id);

            if (@event != null) @event.AvailableTickets -= availableTickets;
            else throw new Exception();
        }

        public async Task IncreaseAvailableTickets(Guid id, int availableTickets)
        {
            var @event = await _context.SportingEvents.FindAsync(id);

            if (@event != null) @event.AvailableTickets += availableTickets;
            else throw new Exception();
        }

        public async Task<SportingEvent?> GetById(Guid id) => await _context.SportingEvents.FindAsync(id);
    }
}
