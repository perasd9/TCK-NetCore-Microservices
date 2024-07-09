using Microsoft.EntityFrameworkCore;
using SportingEvents.API.Core;
using SportingEvents.API.Infrastructure.Configuration;

namespace SportingEvents.API.Infrastructure
{
    public class SportingEventsContext : DbContext
    {
        public SportingEventsContext(DbContextOptions<SportingEventsContext> options) : base(options)
        {
            
        }

        public DbSet<SportingEvent> SportingEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new SportingEventsConfiguration());
            modelBuilder.Ignore<TypeOfSportingEvent>();
            modelBuilder.Ignore<User>();
        }
    }
}
