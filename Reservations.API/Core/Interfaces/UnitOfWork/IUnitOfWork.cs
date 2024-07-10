namespace Reservations.API.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IReservationRepository ReservationRepository { get; }

        public Task SaveChanges();
    }
}
