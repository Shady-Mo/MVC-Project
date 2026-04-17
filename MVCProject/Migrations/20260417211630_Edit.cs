using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.Migrations
{
    /// <inheritdoc />
    public partial class Edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accomodations_Bookings_BookingId",
                table: "Accomodations");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Bookings_BookingId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Bookings_BookingId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_BookingId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Activities_BookingId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Accomodations_BookingId",
                table: "Accomodations");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Accomodations");

            migrationBuilder.DropColumn(
                name: "CheckInDate",
                table: "Accomodations");

            migrationBuilder.DropColumn(
                name: "CheckOutDate",
                table: "Accomodations");

            migrationBuilder.CreateTable(
                name: "BookingAccomodations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    AccomodationId = table.Column<int>(type: "int", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingAccomodations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingAccomodations_Accomodations_AccomodationId",
                        column: x => x.AccomodationId,
                        principalTable: "Accomodations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingAccomodations_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingActivities_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingActivities_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingFlights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingFlights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingFlights_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingFlights_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingAccomodations_AccomodationId",
                table: "BookingAccomodations",
                column: "AccomodationId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingAccomodations_BookingId_AccomodationId",
                table: "BookingAccomodations",
                columns: new[] { "BookingId", "AccomodationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingActivities_ActivityId",
                table: "BookingActivities",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingActivities_BookingId_ActivityId",
                table: "BookingActivities",
                columns: new[] { "BookingId", "ActivityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingFlights_BookingId_FlightId",
                table: "BookingFlights",
                columns: new[] { "BookingId", "FlightId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingFlights_FlightId",
                table: "BookingFlights",
                column: "FlightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingAccomodations");

            migrationBuilder.DropTable(
                name: "BookingActivities");

            migrationBuilder.DropTable(
                name: "BookingFlights");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Activities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Accomodations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInDate",
                table: "Accomodations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutDate",
                table: "Accomodations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Flights_BookingId",
                table: "Flights",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_BookingId",
                table: "Activities",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Accomodations_BookingId",
                table: "Accomodations",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accomodations_Bookings_BookingId",
                table: "Accomodations",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Bookings_BookingId",
                table: "Activities",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Bookings_BookingId",
                table: "Flights",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
