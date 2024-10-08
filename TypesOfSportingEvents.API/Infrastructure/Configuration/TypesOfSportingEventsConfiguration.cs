﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypesOfSportingEvents.API.Core;

namespace TypesOfSportingEvents.API.Infrastructure.Configuration
{
    public class TypesOfSportingEventsConfiguration : IEntityTypeConfiguration<TypeOfSportingEvent>
    {
        public void Configure(EntityTypeBuilder<TypeOfSportingEvent> builder)
        {
            builder.ToTable(nameof(TypeOfSportingEvent));
            builder.HasData(
                    new TypeOfSportingEvent { TypeOfSportingEventId = Guid.NewGuid(), TypeOfSportingEventName = "Basketball" },
                    new TypeOfSportingEvent { TypeOfSportingEventId = Guid.NewGuid(), TypeOfSportingEventName = "Soccer" }
                );
        }
    }
}
