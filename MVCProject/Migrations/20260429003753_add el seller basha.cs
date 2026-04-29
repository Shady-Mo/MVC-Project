using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.Migrations
{
    /// <inheritdoc />
    public partial class addelsellerbasha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                table: "Flights",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "AspNetUsers",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                table: "Activities",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                table: "Accomodations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flights_SellerId",
                table: "Flights",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_SellerId",
                table: "Activities",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Accomodations_SellerId",
                table: "Accomodations",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accomodations_AspNetUsers_SellerId",
                table: "Accomodations",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_AspNetUsers_SellerId",
                table: "Activities",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_AspNetUsers_SellerId",
                table: "Flights",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accomodations_AspNetUsers_SellerId",
                table: "Accomodations");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_AspNetUsers_SellerId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_AspNetUsers_SellerId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_SellerId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Activities_SellerId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Accomodations_SellerId",
                table: "Accomodations");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Accomodations");
        }
    }
}
