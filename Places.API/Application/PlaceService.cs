using Microsoft.EntityFrameworkCore;
using Places.API.Core;
using Places.API.Core.Interfaces.UnitOfWork;
using Places.API.Core.Pagination;
using Places.API.Core.Protos;
using Places.API.Endpoints.QueryParameters;

namespace Places.API.Application
{
    public class PlaceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlaceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //REST METHOD
        public async Task<PaginationList<Place>> GetAll(PlaceQueryParameters queryParameters)
        {
            var items = await _unitOfWork.PlaceRepository.GetAll(queryParameters);

            return new PaginationList<Place>(items, items.Count, queryParameters.PageNumber, queryParameters.PageSize);
        }

        //GRPC METHOD
        public async Task<PaginationList<Place>> GetAll(QueryParameters queryParameters)
        {
            var items = await _unitOfWork.PlaceRepository.GetAll(queryParameters);

            return new PaginationList<Place>(items, items.Count, queryParameters.PageNumber, queryParameters.PageSize);
        }

        //REST METHOD
        public async Task<Place?> GetById(Guid id)
        {
            return await _unitOfWork.PlaceRepository.GetById(id);
        }

        //GRPC METHOD
        public async Task<Place?> GetById(UUID id)
        {
            return await _unitOfWork.PlaceRepository.GetById(new Guid(id.Id));
        }
    }
}
