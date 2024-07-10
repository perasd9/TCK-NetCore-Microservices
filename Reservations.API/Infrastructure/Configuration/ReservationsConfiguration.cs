using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.API.Core;

namespace Reservations.API.Infrastructure.Configuration
{
    public class ReservationsConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable(nameof(Reservation));
            builder.OwnsMany(r => r.ReservationComponents, rc =>
            {
                rc.WithOwner(rc => rc.Reservation)
                  .HasForeignKey(rc => rc.ReservationId);

                rc.HasKey(rc => new { rc.ReservationId, rc.SerialNumber });
            });
        }
    }
}
