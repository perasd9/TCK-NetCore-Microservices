﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TypesOfSportingEvents.API.Infrastructure;

#nullable disable

namespace TypesOfSportingEvents.API.Migrations
{
    [DbContext(typeof(TypesOfSportingEventsContext))]
    [Migration("20240709110738_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TypesOfSportingEvents.API.Core.TypeOfSportingEvent", b =>
                {
                    b.Property<Guid>("TypeOfSportingEventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TypeOfSportingEventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TypeOfSportingEventId");

                    b.ToTable("TypeOfSportingEvent", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}