using Identity.API.Core;
using Identity.API.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Infrastructure
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UsersConfiguration());

            modelBuilder.Entity<Role>().ToTable(nameof(Role));
            modelBuilder.Entity<Role>().HasData(
                    new Role { RoleId = new Guid(), RoleName = "User"},
                    new Role { RoleId = new Guid(), RoleName = "Admin"}
                );

            modelBuilder.Ignore<Place>();
        }
    }
}
