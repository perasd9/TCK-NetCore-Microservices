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
            request.PageNumber = 1;
            request.PageSize = int.MaxValue;
            var places = (await _placeService.GetAll(request)).Value;

            var pagination = new PaginationList
            {
                HasNext = places.HasNext,
                HasPrevious = places.HasPrevious,
                PageIndex = places.PageIndex,
                PageSize = places.PageSize,
                TotalPages = places.TotalPages,
                //Places = ByteString.CopyFrom(SerializeListToBytes(places.Items))
                Places = { places.Items.Select(p => new PlaceGrpc() { PlaceId = p.PlaceId.ToString(), PlaceName = p.PlaceName })! }
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

        public async override Task<PaginationListWithLargeObject> GetAllLargeObjects(QueryParameters request, ServerCallContext context)
        {
            request.PageNumber = 1;
            request.PageSize = int.MaxValue;
            var places = (await _placeService.GetAll(request)).Value;

            var pagination = new PaginationListWithLargeObject
            {
                HasNext = places.HasNext,
                HasPrevious = places.HasPrevious,
                PageIndex = places.PageIndex,
                PageSize = places.PageSize,
                TotalPages = places.TotalPages,
                Places = ByteString.CopyFrom(SerializeListToBytes(places.Items))
                //Places = { places.Items.Select(p => new PlaceGrpcLargeObject() { PlaceId = p.PlaceId.ToString(), PlaceName = p.PlaceName,
                //PlaceName17 = new Core.Protos.Field(){PlaceName4 = "", PlaceName5  = "", PlaceName6 = ""},
                //PlaceName18 = new Core.Protos.Field(){PlaceName4 = "", PlaceName5  = "", PlaceName6 = ""},
                //PlaceName19 = new Core.Protos.Field(){PlaceName4 = "", PlaceName5  = "", PlaceName6 = ""},
                //PlaceName20 = { new Core.Protos.Field(){PlaceName4 = "", PlaceName5  = "", PlaceName6 = ""}, },
                //PlaceName65 = { new Core.Protos.Enumeration(), },
                //    })
                //}
            };

            return pagination;
        }

        public async override Task<PlaceGrpc> GetById(UUID request, ServerCallContext context)
        {
            var place = (await _placeService.GetById(request)).Value;

            return new PlaceGrpc { PlaceId = place?.PlaceId.ToString(), PlaceName = place?.PlaceName };
        }
    }
}
