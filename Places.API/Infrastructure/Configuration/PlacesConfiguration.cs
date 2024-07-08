using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Places.API.Core;

namespace Places.API.Infrastructure.Configuration
{
    public class PlacesConfiguration : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.ToTable(nameof(Place));
        }
    }
}
