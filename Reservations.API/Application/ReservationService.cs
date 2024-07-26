using Microsoft.EntityFrameworkCore;
using Reservations.API.Core;
using Reservations.API.Core.Interfaces.UnitOfWork;
using Reservations.API.Core.Pagination;
using Reservations.API.Core.Protos;
using Reservations.API.Endpoints.QueryParameters;

namespace Users.API.Application
{
    public class ReservationService
    {
        private IUnitOfWork _unitOfWork;

        public ReservationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //REST METHOD
        public async Task<PaginationList<Reservation>> GetAll(ReservationQueryParameters queryParameters)
        {
            var items =  await _unitOfWork.ReservationRepository.GetAll(queryParameters).ToListAsync();

            return new PaginationList<Reservation>(items, items.Count, queryParameters.PageNumber, queryParameters.PageSize);
        }

        //GRPC METHOD
        public async Task<PaginationList<Reservation>> GetAll(QueryParameters queryParameters)
        {
            var items = await _unitOfWork.ReservationRepository.GetAll(queryParameters).ToListAsync();

            return new PaginationList<Reservation>(items, items.Count, queryParameters.PageNumber, queryParameters.PageSize);
        }

        public async Task Add(Reservation reservation)
        {
            await _unitOfWork.ReservationRepository.Save(reservation);
            await _unitOfWork.SaveChanges();

        }
    }
}
