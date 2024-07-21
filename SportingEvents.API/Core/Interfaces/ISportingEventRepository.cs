using SportingEvents.API.Core.Protos;
using SportingEvents.API.Endpoints.QueryParameters;

namespace SportingEvents.API.Core.Interfaces
{
    //REASON TO MAKE SEPARATE METHODS FOR REST AND GRPC IS TO REDUCE GAP FOR MAPPING GRPC REQUESTS TO HTTP AND GET REAL PERFORMANCE JUST IN CASE USE REST OF GRPC
    public interface ISportingEventRepository
    {
        public IQueryable<SportingEvent> GetAll(SportingEventQueryParameters queryParameters);
        public IQueryable<SportingEvent> GetAll(QueryParameters queryParameters);
    }
}
