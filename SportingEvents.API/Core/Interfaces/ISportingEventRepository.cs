using SportingEvents.API.Core.Protos;
using SportingEvents.API.Endpoints.QueryParameters;

namespace SportingEvents.API.Core.Interfaces
{
    public interface ISportingEventRepository
    {
        public IQueryable<SportingEvent> GetAll(SportingEventQueryParameters queryParameters);
        public IQueryable<SportingEvent> GetAll(QueryParameters queryParameters);
    }
}
