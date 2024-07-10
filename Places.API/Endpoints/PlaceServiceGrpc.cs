using Grpc.Core;
using Places.API.Core.Protos;

namespace Places.API.Endpoints
{
    public class PlaceServiceGrpc : PlaceService.PlaceServiceBase
    {
        private Places.API.Application.PlaceService _placeService;

        public PlaceServiceGrpc(Places.API.Application.PlaceService placeService)
        {
            _placeService = placeService;
        }
        public async override Task<PlaceList> GetAll(Empty request, ServerCallContext context)
        {
            var places = await _placeService.GetAll();
            List<PlaceGrpc> placesGrpc = places.Select(place => new PlaceGrpc()
            {
                PlaceId = place.PlaceId.ToString(),
                PlaceName = place.PlaceName,
            }).ToList();

            PlaceList pl = new PlaceList()
            {
                Places = { placesGrpc }
            };

            return pl;
        }
    }
}
