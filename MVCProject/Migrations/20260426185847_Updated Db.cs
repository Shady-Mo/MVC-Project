using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVCProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accomodations",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Accomodations",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Accomodations",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Accomodations",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Accomodations",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Accomodations",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Accomodations",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Accomodations",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Accomodations",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Accomodations",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Accomodations",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Accomodations",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Accomodations",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Accomodations",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Accomodations",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Accomodations",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Accomodations",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Accomodations",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Accomodations",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Accomodations",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 119);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accomodations",
                columns: new[] { "Id", "AvailableRooms", "Image", "Location", "Name", "PricePerNight" },
                values: new object[,]
                {
                    { 100, 10, "2.jpg", "New York", "The Ritz-Carlton", 150m },
                    { 101, 12, "2.jpg", "London", "Hilton Garden Inn", 175m },
                    { 102, 14, "2.jpg", "Paris", "Marriott Marquis", 200m },
                    { 103, 16, "2.jpg", "Tokyo", "Four Seasons Resort", 225m },
                    { 104, 18, "2.jpg", "Dubai", "InterContinental", 250m },
                    { 105, 20, "2.jpg", "Sydney", "Sheraton Grand", 275m },
                    { 106, 22, "2.jpg", "Rome", "Holiday Inn Express", 300m },
                    { 107, 24, "2.jpg", "Los Angeles", "Hyatt Regency", 325m },
                    { 108, 26, "2.jpg", "Barcelona", "Radisson Blu", 350m },
                    { 109, 28, "2.jpg", "Berlin", "Wyndham Grand", 375m },
                    { 110, 30, "2.jpg", "Singapore", "Fairmont", 400m },
                    { 111, 32, "2.jpg", "Hong Kong", "Mandalay Bay", 425m },
                    { 112, 34, "2.jpg", "Istanbul", "Bellagio", 450m },
                    { 113, 36, "2.jpg", "Bangkok", "The Venetian", 475m },
                    { 114, 38, "2.jpg", "Seoul", "Caesars Palace", 500m },
                    { 115, 40, "2.jpg", "Las Vegas", "MGM Grand", 525m },
                    { 116, 42, "2.jpg", "Miami", "Wynn Las Vegas", 550m },
                    { 117, 44, "2.jpg", "San Francisco", "Aria Resort", 575m },
                    { 118, 46, "2.jpg", "Chicago", "Cosmopolitan", 600m },
                    { 119, 48, "2.jpg", "Toronto", "The Mirage", 625m }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "Airline", "ArrivalDateTime", "AvailableSeats", "DepartureAirport", "DepartureDateTime", "DestinationAirport", "Price" },
                values: new object[,]
                {
                    { 100, "Emirates", new DateTime(2025, 1, 2, 16, 30, 0, 0, DateTimeKind.Unspecified), 100, "JFK", new DateTime(2025, 1, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), "SYD", 300m },
                    { 101, "Delta Air Lines", new DateTime(2025, 1, 3, 16, 30, 0, 0, DateTimeKind.Unspecified), 105, "LHR", new DateTime(2025, 1, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), "FCO", 315m },
                    { 102, "Qatar Airways", new DateTime(2025, 1, 4, 16, 30, 0, 0, DateTimeKind.Unspecified), 110, "CDG", new DateTime(2025, 1, 4, 12, 0, 0, 0, DateTimeKind.Unspecified), "LAX", 330m },
                    { 103, "Singapore Airlines", new DateTime(2025, 1, 5, 16, 30, 0, 0, DateTimeKind.Unspecified), 115, "HND", new DateTime(2025, 1, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), "BCN", 345m },
                    { 104, "United Airlines", new DateTime(2025, 1, 6, 16, 30, 0, 0, DateTimeKind.Unspecified), 120, "DXB", new DateTime(2025, 1, 6, 12, 0, 0, 0, DateTimeKind.Unspecified), "BER", 360m },
                    { 105, "Lufthansa", new DateTime(2025, 1, 7, 16, 30, 0, 0, DateTimeKind.Unspecified), 125, "SYD", new DateTime(2025, 1, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), "SIN", 375m },
                    { 106, "British Airways", new DateTime(2025, 1, 8, 16, 30, 0, 0, DateTimeKind.Unspecified), 130, "FCO", new DateTime(2025, 1, 8, 12, 0, 0, 0, DateTimeKind.Unspecified), "HKG", 390m },
                    { 107, "Air France", new DateTime(2025, 1, 9, 16, 30, 0, 0, DateTimeKind.Unspecified), 135, "LAX", new DateTime(2025, 1, 9, 12, 0, 0, 0, DateTimeKind.Unspecified), "IST", 405m },
                    { 108, "Cathay Pacific", new DateTime(2025, 1, 10, 16, 30, 0, 0, DateTimeKind.Unspecified), 140, "BCN", new DateTime(2025, 1, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), "BKK", 420m },
                    { 109, "Qantas", new DateTime(2025, 1, 11, 16, 30, 0, 0, DateTimeKind.Unspecified), 145, "BER", new DateTime(2025, 1, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), "ICN", 435m },
                    { 110, "American Airlines", new DateTime(2025, 1, 12, 16, 30, 0, 0, DateTimeKind.Unspecified), 150, "SIN", new DateTime(2025, 1, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), "LAS", 450m },
                    { 111, "ANA", new DateTime(2025, 1, 13, 16, 30, 0, 0, DateTimeKind.Unspecified), 155, "HKG", new DateTime(2025, 1, 13, 12, 0, 0, 0, DateTimeKind.Unspecified), "MIA", 465m },
                    { 112, "Etihad Airways", new DateTime(2025, 1, 14, 16, 30, 0, 0, DateTimeKind.Unspecified), 160, "IST", new DateTime(2025, 1, 14, 12, 0, 0, 0, DateTimeKind.Unspecified), "SFO", 480m },
                    { 113, "Turkish Airlines", new DateTime(2025, 1, 15, 16, 30, 0, 0, DateTimeKind.Unspecified), 165, "BKK", new DateTime(2025, 1, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), "ORD", 495m },
                    { 114, "Air Canada", new DateTime(2025, 1, 16, 16, 30, 0, 0, DateTimeKind.Unspecified), 170, "ICN", new DateTime(2025, 1, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), "YYZ", 510m },
                    { 115, "KLM", new DateTime(2025, 1, 17, 16, 30, 0, 0, DateTimeKind.Unspecified), 175, "LAS", new DateTime(2025, 1, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "JFK", 525m },
                    { 116, "Japan Airlines", new DateTime(2025, 1, 18, 16, 30, 0, 0, DateTimeKind.Unspecified), 180, "MIA", new DateTime(2025, 1, 18, 12, 0, 0, 0, DateTimeKind.Unspecified), "LHR", 540m },
                    { 117, "Swiss Airlines", new DateTime(2025, 1, 19, 16, 30, 0, 0, DateTimeKind.Unspecified), 185, "SFO", new DateTime(2025, 1, 19, 12, 0, 0, 0, DateTimeKind.Unspecified), "CDG", 555m },
                    { 118, "Virgin Atlantic", new DateTime(2025, 1, 20, 16, 30, 0, 0, DateTimeKind.Unspecified), 190, "ORD", new DateTime(2025, 1, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), "HND", 570m },
                    { 119, "Air New Zealand", new DateTime(2025, 1, 21, 16, 30, 0, 0, DateTimeKind.Unspecified), 195, "YYZ", new DateTime(2025, 1, 21, 12, 0, 0, 0, DateTimeKind.Unspecified), "DXB", 585m }
                });
        }
    }
}
