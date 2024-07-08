using Microsoft.EntityFrameworkCore;
using Places.API.Core;
using Places.API.Infrastructure.Configuration;

namespace Places.API.Infrastructure
{
    public class PlacesContext : DbContext
    {
        public PlacesContext(DbContextOptions<PlacesContext> options) : base(options)
        {

        }

        public DbSet<Place> Places { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PlacesConfiguration());
        }
    }
}
