using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportingEvents.API.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SportingEvent",
                columns: table => new
                {
                    SportingEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SportingEventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SportingEventDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SportingEventTicketPrice = table.Column<double>(type: "float", nullable: false),
                    DateOfSportingEvent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeOfSportingEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportingEvent", x => x.SportingEventId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SportingEvent");
        }
    }
}
