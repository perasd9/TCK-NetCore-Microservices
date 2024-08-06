using Microsoft.EntityFrameworkCore;
using TypesOfSportingEvents.API.Core;
using TypesOfSportingEvents.API.Core.Interfaces.UnitOfWork;
using TypesOfSportingEvents.API.Core.Pagination;
using TypesOfSportingEvents.API.Core.Protos;
using TypesOfSportingEvents.API.Endpoints.QueryParameters;

namespace TypesOfSportingEvents.API.Application
{
    public class TypeOfSportingEventService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TypeOfSportingEventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //REST METHOD
        public async Task<PaginationList<TypeOfSportingEvent>> GetAll(TypeOfSportingEventQueryParameters queryParameters)
        {
            var items = await _unitOfWork.TypeOfSportingEventRepository.GetAll(queryParameters);


            return new PaginationList<TypeOfSportingEvent>(items, items.Count, queryParameters.PageNumber, queryParameters.PageSize);
        }

        //GRPC METHOD
        public async Task<PaginationList<TypeOfSportingEvent>> GetAll(QueryParameters queryParameters)
        {
            var items = await _unitOfWork.TypeOfSportingEventRepository.GetAll(queryParameters);


            return new PaginationList<TypeOfSportingEvent>(items, items.Count, queryParameters.PageNumber, queryParameters.PageSize);
        }

        //REST METHOD
        public async Task<TypeOfSportingEvent?> GetById(Guid id)
        {
            return await _unitOfWork.TypeOfSportingEventRepository.GetById(id);
        }

        //GRPC METHOD
        public async Task<TypeOfSportingEvent?> GetById(UUID id)
        {
            return await _unitOfWork.TypeOfSportingEventRepository.GetById(new Guid(id.Id));
        }
    }
}
