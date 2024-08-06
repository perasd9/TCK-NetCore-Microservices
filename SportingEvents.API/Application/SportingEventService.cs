using Microsoft.EntityFrameworkCore;
using SportingEvents.API.Core;
using SportingEvents.API.Core.Interfaces.UnitOfWork;
using SportingEvents.API.Core.Pagination;
using SportingEvents.API.Core.Protos;
using SportingEvents.API.Endpoints.QueryParameters;

namespace SportingEvents.API.Application
{
    public class SportingEventService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SportingEventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //REST METHOD
        public async Task<PaginationList<SportingEvent>> GetAll(SportingEventQueryParameters queryParameters)
        {
            var items = await _unitOfWork.SportingEventRepository.GetAll(queryParameters).ToListAsync();

            return new PaginationList<SportingEvent>(items, items.Count, queryParameters.PageNumber, queryParameters.PageSize);
        }

        //GRPC METHOD
        public async Task<PaginationList<SportingEvent>> GetAll(QueryParameters queryParameters)
        {
            var items = await _unitOfWork.SportingEventRepository.GetAll(queryParameters).ToListAsync();

            return new PaginationList<SportingEvent>(items, items.Count, queryParameters.PageNumber, queryParameters.PageSize);
        }

        public async Task IncreaseAvailableTickets(Guid id, int availableTickets)
        {
            await _unitOfWork.SportingEventRepository.IncreaseAvailableTickets(id, availableTickets);

            await _unitOfWork.SaveChanges();
        }
        public async Task DecreaseAvailableTickets(Guid id, int availableTickets)
        {
            await _unitOfWork.SportingEventRepository.DecreaseAvailableTickets(id, availableTickets);

            await _unitOfWork.SaveChanges();
        }
    }
}
