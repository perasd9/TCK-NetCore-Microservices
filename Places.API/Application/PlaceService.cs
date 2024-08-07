using Microsoft.EntityFrameworkCore;
using Places.API.Core;
using Places.API.Core.Abstractions;
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
        public async Task<Result<PaginationList<Place>>> GetAll(PlaceQueryParameters queryParameters)
        {
            var items = await _unitOfWork.PlaceRepository.GetAll(queryParameters);

            return Result.Success(new PaginationList<Place>(items, items.Count, queryParameters.PageNumber, queryParameters.PageSize));
        }

        //GRPC METHOD
        public async Task<Result<PaginationList<Place>>> GetAll(QueryParameters queryParameters)
        {
            var items = await _unitOfWork.PlaceRepository.GetAll(queryParameters);

            return Result.Success(new PaginationList<Place>(items, items.Count, queryParameters.PageNumber, queryParameters.PageSize));
        }

        //REST METHOD
        public async Task<Result<Place>> GetById(Guid id)
        {
            var place = await _unitOfWork.PlaceRepository.GetById(id);

            return place == null ? Result.Failure<Place>(PlaceErrors.DoesntExist) : Result.Success(place);
        }

        //GRPC METHOD
        public async Task<Result<Place>> GetById(UUID id)
        {
            var place = await _unitOfWork.PlaceRepository.GetById(new Guid(id.Id));

            return place == null ? Result.Failure<Place>(PlaceErrors.DoesntExist) : Result.Success(place);
        }
    }
}
