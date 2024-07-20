using Places.API.Core.Protos;
using Places.API.Endpoints.QueryParameters;

namespace Places.API.Core.Interfaces
{
    public interface IPlaceRepository
    {
        //REST METHOD
        public IQueryable<Place> GetAll(PlaceQueryParameters queryParameters);
        //GRPC METHOD
        public IQueryable<Place> GetAll(QueryParameters queryParameters);

        public Task<Place?> GetById(Guid id);
    }
}
