namespace Reservations.API.Core.Interfaces
{
    public interface IReservationRepository
    {
        public IQueryable<Reservation> GetAll();
    }
}
