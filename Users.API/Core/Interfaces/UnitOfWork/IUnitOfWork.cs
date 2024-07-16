namespace Identity.API.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IRoleRepository RoleRepository { get; }

        public Task SaveChanges();
    }
}
