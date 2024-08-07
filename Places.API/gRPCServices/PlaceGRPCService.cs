using Google.Protobuf;
using Grpc.Core;
using Places.API.Application;
using Places.API.Core;
using Places.API.Core.Protos;

namespace Places.API.gRPCServices
{
    public class PlaceGRPCService : gRPCPlaceService.gRPCPlaceServiceBase
    {
        private readonly PlaceService _placeService;

        public PlaceGRPCService(PlaceService placeService)
        {
            _placeService = placeService;
        }

        public async override Task<PaginationList> GetAll(QueryParameters request, ServerCallContext context)
        {
            var places = (await _placeService.GetAll(request)).Value;
           
            var pagination = new PaginationList
            {
                HasNext = places.HasNext,
                HasPrevious = places.HasPrevious,
                PageIndex = places.PageIndex,
                PageSize = places.PageSize,
                TotalPages = places.TotalPages,
                Places = ByteString.CopyFrom(SerializeListToBytes(places.Items))
            };

            return pagination;
        }

        //helper method for serializing list
        private static byte[] SerializeListToBytes(IEnumerable<Place> places)
        {
            using var memoryStream = new MemoryStream();

            ProtoBuf.Serializer.Serialize(memoryStream, places);
            return memoryStream.ToArray();
        }

        public async override Task<PlaceGrpc> GetById(UUID request, ServerCallContext context)
        {
            var place = (await _placeService.GetById(request)).Value;

            return new PlaceGrpc { PlaceId = place?.PlaceId.ToString(), PlaceName = place?.PlaceName };
        }
    }
}
