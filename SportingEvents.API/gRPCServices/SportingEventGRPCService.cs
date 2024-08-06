using Google.Protobuf;
using Grpc.Core;
using SportingEvents.API.Application;
using SportingEvents.API.Core;
using SportingEvents.API.Core.Protos;

namespace SportingEvents.API.gRPCServices
{
    public class SportingEventGRPCService : gRPCSportingEventService.gRPCSportingEventServiceBase
    {
        private readonly SportingEventService _sportingEventService;

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
        private static byte[] SerializeListToBytes(IEnumerable<SportingEvent> events)
        {
            using var memoryStream = new MemoryStream();

            ProtoBuf.Serializer.Serialize(memoryStream, events);
            return memoryStream.ToArray();
        }

        public override Task<SportingEventGrpc> GetById(UUID request, ServerCallContext context)
        {
            return base.GetById(request, context);
        }

        public async override Task<Empty> DecreaseAvailableTickets(DecreaseAvailableTicketsRequestGRPC request, ServerCallContext context)
        {
            try
            {
                await _sportingEventService.DecreaseAvailableTickets(new Guid(request.SportingEventId.Id), request.Amount);
                return new();

            }
            catch (Exception)
            {
                context.Status = new Status(StatusCode.InvalidArgument, "Bad request!");
                return new();

            }
        }

        public async override Task<Empty> IncreaseAvailableTickets(IncreaseAvailableTicketsRequestGRPC request, ServerCallContext context)
        {
            try
            {
                await _sportingEventService.IncreaseAvailableTickets(new Guid(request.SportingEventId.Id), request.Amount);
                return new();

            }
            catch (Exception)
            {
                context.Status = new Status(StatusCode.InvalidArgument, "Bad request!");
                return new();

            }
        }
    }
}
