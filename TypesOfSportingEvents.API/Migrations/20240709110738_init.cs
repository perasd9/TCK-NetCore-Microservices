using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypesOfSportingEvents.API.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypeOfSportingEvent",
                columns: table => new
                {
                    TypeOfSportingEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeOfSportingEventName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfSportingEvent", x => x.TypeOfSportingEventId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TypeOfSportingEvent");
        }
    }
}
