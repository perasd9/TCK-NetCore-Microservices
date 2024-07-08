namespace Users.API.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }

        public Task SaveChanges();
    }
}
