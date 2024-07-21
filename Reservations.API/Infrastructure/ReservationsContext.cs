using Microsoft.EntityFrameworkCore;
using Reservations.API.Core;
using Reservations.API.Infrastructure.Configuration;

namespace Reservations.API.Infrastructure
{
    public class ReservationsContext : DbContext
    {
        public ReservationsContext(DbContextOptions<ReservationsContext> options) : base(options)
        {
            
        }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ReservationsConfiguration());

            modelBuilder.Ignore<SportingEvent>();
            modelBuilder.Ignore<User>();
        }
    }
}
