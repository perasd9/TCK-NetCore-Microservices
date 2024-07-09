using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportingEvents.API.Core;

namespace SportingEvents.API.Infrastructure.Configuration
{
    public class SportingEventsConfiguration : IEntityTypeConfiguration<SportingEvent>
    {
        public void Configure(EntityTypeBuilder<SportingEvent> builder)
        {
            builder.ToTable(nameof(SportingEvent));
        }
    }
}
