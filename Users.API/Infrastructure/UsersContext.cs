using Microsoft.EntityFrameworkCore;
using Users.API.Core;
using Users.API.Infrastructure.Configuration;

namespace Users.API.Infrastructure
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.Ignore<Place>();
            modelBuilder.Ignore<Role>();
        }
    }
}
