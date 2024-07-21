using Places.API.Core.Protos;
using Places.API.Endpoints.QueryParameters;

namespace Places.API.Core.Interfaces
{
    //REASON TO MAKE SEPARATE METHODS FOR REST AND GRPC IS TO REDUCE GAP FOR MAPPING GRPC REQUESTS TO HTTP AND GET REAL PERFORMANCE JUST IN CASE USE REST OF GRPC
    public interface IPlaceRepository
    {
        //REST METHOD
        public IQueryable<Place> GetAll(PlaceQueryParameters queryParameters);
        //GRPC METHOD
        public IQueryable<Place> GetAll(QueryParameters queryParameters);

        public Task<Place?> GetById(Guid id);
    }
}
