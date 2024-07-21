using Microsoft.EntityFrameworkCore;
using Reservations.API.Core;
using Reservations.API.Core.Interfaces;
using Reservations.API.Core.Protos;
using Reservations.API.Endpoints.QueryParameters;
using Reservations.API.Infrastructure.Repositories.Base;

namespace Reservations.API.Infrastructure.Repositories
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(ReservationsContext context) : base(context)
        {
        }


        public IQueryable<Reservation> GetAll(ReservationQueryParameters queryParameters) => GetAll().Include(res => res.ReservationComponents)
            .Where(reservation => reservation.DateOfReservation.ToString().Contains(queryParameters.Search!.Trim().ToLower()))
                .OrderBy(reservation => reservation.DateOfReservation)
                    .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize).Take(queryParameters.PageSize);

        public IQueryable<Reservation> GetAll(QueryParameters queryParameters) => GetAll().Include(res => res.ReservationComponents)
            .Where(reservation => reservation.DateOfReservation.ToString().Contains(queryParameters.Search!.Trim().ToLower()))
                .OrderBy(reservation => reservation.DateOfReservation)
                    .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize).Take(queryParameters.PageSize);
    }
}
