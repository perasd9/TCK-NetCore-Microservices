using Grpc.Net.Client;
using Microsoft.EntityFrameworkCore;
using SportingEvents.API.Core;
using SportingEvents.API.Core.Abstractions;
using SportingEvents.API.Core.Interfaces.UnitOfWork;
using SportingEvents.API.Core.Pagination;
using SportingEvents.API.Core.Protos;
using SportingEvents.API.Endpoints.QueryParameters;
using System.Net;
using System.Text.Json;

namespace SportingEvents.API.Application
{
    public class SportingEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;

        public SportingEventService(IUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory)
        {
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
        }

        //REST METHOD
        public async Task<Result<PaginationList<SportingEvent>>> GetAll(SportingEventQueryParameters queryParameters, CancellationToken cancellationToken = default)
        {
            HttpClient http = _httpClientFactory.CreateClient();

            http.DefaultRequestVersion = HttpVersion.Version11;

            var items = await _unitOfWork.SportingEventRepository.GetAll(queryParameters).ToListAsync(cancellationToken: cancellationToken);

            HttpResponseMessage response = await http.GetAsync("https://localhost:9201/api/v1/types", cancellationToken);

            var types = JsonSerializer.Deserialize<List<TypeOfSportingEvent>>(await response.Content.ReadAsStringAsync(cancellationToken),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            foreach (var item in items)
                item.TypeOfSportingEvent = types?.FirstOrDefault(t => t.TypeOfSportingEventId == item.TypeOfSportingEventId);

            return Result.Success(new PaginationList<SportingEvent>(items, items.Count, queryParameters.PageNumber, queryParameters.PageSize));
        }

        //GRPC METHOD
        public async Task<Result<PaginationList<SportingEvent>>> GetAll(QueryParameters queryParameters, CancellationToken cancellationToken = default)
        {
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            var channel = GrpcChannel.ForAddress("https://localhost:9201", new GrpcChannelOptions
            {
                HttpHandler = httpHandler,
            });

            var client = new gRPCTypeOfSportingEventService.gRPCTypeOfSportingEventServiceClient(channel);

            var items = await _unitOfWork.SportingEventRepository.GetAll(queryParameters).ToListAsync(cancellationToken: cancellationToken);

            var reply = await client.GetAllAsync(new(), cancellationToken: cancellationToken);

            foreach (var @event in items)
            {
                var typeGrpc = reply.Types_?.FirstOrDefault(t => t.TypeOfSportingEventId == @event.TypeOfSportingEventId.ToString());
                @event.TypeOfSportingEvent = new TypeOfSportingEvent()
                {
                    TypeOfSportingEventId = new Guid(typeGrpc!.TypeOfSportingEventId),
                    TypeOfSportingEventName = typeGrpc.TypeOfSportingEventName
                };
            }

            return Result.Success(new PaginationList<SportingEvent>(items, items.Count, queryParameters.PageNumber, queryParameters.PageSize));
        }

        public async Task<Result> IncreaseAvailableTickets(Guid id, int availableTickets)
        {
            var @event = await _unitOfWork.SportingEventRepository.GetById(id);

            if (@event == null) return Result.Failure(SportingEventErrors.NotFound);

            await _unitOfWork.SportingEventRepository.IncreaseAvailableTickets(id, availableTickets);

            await _unitOfWork.SaveChanges();

            return Result.Success();
        }
        public async Task<Result> DecreaseAvailableTickets(Guid id, int availableTickets)
        {
            var @event = await _unitOfWork.SportingEventRepository.GetById(id);

            if (@event == null) return Result.Failure(SportingEventErrors.NotFound);
            if (@event.AvailableTickets < availableTickets) return Result.Failure(SportingEventErrors.DecreaseLessThanZero);

            await _unitOfWork.SportingEventRepository.DecreaseAvailableTickets(id, availableTickets);

            await _unitOfWork.SaveChanges();

            return Result.Success();
        }
    }
}
