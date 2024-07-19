using System.Linq.Expressions;
using TypesOfSportingEvents.API.Endpoints.QueryParameters;

namespace TypesOfSportingEvents.API.Core.Interfaces
{
    public interface ITypeOfSportingEventRepository
    {
        public IQueryable<TypeOfSportingEvent> GetAll(TypeOfSportingEventQueryParameters queryParameters);
        public Task<TypeOfSportingEvent?> GetById(Guid id);
    }
}
