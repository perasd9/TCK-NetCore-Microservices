using Microsoft.EntityFrameworkCore;
using TypesOfSportingEvents.API.Core;
using TypesOfSportingEvents.API.Core.Interfaces;
using TypesOfSportingEvents.API.Endpoints.QueryParameters;

namespace TypesOfSportingEvents.API.Infrastructure.Repositories
{
    public class TypeOfSportingEventRepository : ITypeOfSportingEventRepository
    {
        private readonly TypesOfSportingEventsContext _context;

        public TypeOfSportingEventRepository(TypesOfSportingEventsContext context)
        {
            _context = context;
        }

        public IQueryable<TypeOfSportingEvent> GetAll(TypeOfSportingEventQueryParameters queryParameters) => _context.TypesOfSportingEvents.AsNoTracking()
            .Where(type => type.TypeOfSportingEventName.Contains(queryParameters.Search!.Trim().ToLower()))
                .OrderBy(type => type.TypeOfSportingEventName)
                    .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize).Take(queryParameters.PageSize);

        public async Task<TypeOfSportingEvent?> GetById(Guid id) => await _context.TypesOfSportingEvents.FindAsync(id);
    }
}
