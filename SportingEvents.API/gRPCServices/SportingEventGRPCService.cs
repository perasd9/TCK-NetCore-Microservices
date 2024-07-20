using Google.Protobuf;
using Grpc.Core;
using SportingEvents.API.Application;
using SportingEvents.API.Core;
using SportingEvents.API.Core.Protos;

namespace SportingEvents.API.gRPCServices
{
    public class SportingEventGRPCService : gRPCSportingEventService.gRPCSportingEventServiceBase
    {
        private SportingEventService _sportingEventService;

        public SportingEventGRPCService(SportingEventService sportingEventService)
        {
            _sportingEventService = sportingEventService;
        }

        public async override Task<PaginationList> GetAll(QueryParameters request, ServerCallContext context)
        {
            var events = await _sportingEventService.GetAll(request);

            var pagination = new PaginationList
            {
                HasNext = events.HasNext,
                HasPrevious = events.HasPrevious,
                PageIndex = events.PageIndex,
                PageSize = events.PageSize,
                TotalPages = events.TotalPages,
                SportingEvents = ByteString.CopyFrom(SerializeListToBytes(events.Items))
            };

            return pagination;
        }

        //helper method for serializing list
        private static byte[] SerializeListToBytes(IEnumerable<SportingEvent> places)
        {
            using var memoryStream = new MemoryStream();

            ProtoBuf.Serializer.Serialize(memoryStream, places);
            return memoryStream.ToArray();
        }
    }
}
