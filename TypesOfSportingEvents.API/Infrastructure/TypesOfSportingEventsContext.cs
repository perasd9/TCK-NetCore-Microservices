using Microsoft.EntityFrameworkCore;
using TypesOfSportingEvents.API.Core;
using TypesOfSportingEvents.API.Infrastructure.Configuration;

namespace TypesOfSportingEvents.API.Infrastructure
{
    public class TypesOfSportingEventsContext : DbContext
    {
        public TypesOfSportingEventsContext(DbContextOptions<TypesOfSportingEventsContext> options) : base(options)
        {
            
        }

        public DbSet<TypeOfSportingEvent> TypesOfSportingEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TypesOfSportingEventsConfiguration());
            modelBuilder.Ignore<SportingEvent>();
        }
    }
}
