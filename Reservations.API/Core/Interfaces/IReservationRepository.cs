using Reservations.API.Core.Protos;
using Reservations.API.Endpoints.QueryParameters;

namespace Reservations.API.Core.Interfaces
{
    //REASON TO MAKE SEPARATE METHODS FOR REST AND GRPC IS TO REDUCE GAP FOR MAPPING GRPC REQUESTS TO HTTP AND GET REAL PERFORMANCE JUST IN CASE USE REST OF GRPC
    public interface IReservationRepository
    {
        //REST METHOD
        public IQueryable<Reservation> GetAll(ReservationQueryParameters queryParameters);
        //GRPC METHOD
        public IQueryable<Reservation> GetAll(QueryParameters queryParameters);
    }
}
