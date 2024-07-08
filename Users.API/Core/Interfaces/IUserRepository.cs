namespace Users.API.Core.Interfaces
{
    public interface IUserRepository
    {
        public IQueryable<User> GetAll();
    }
}
