using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.Migrations
{
    /// <inheritdoc />
    public partial class addimagetoaccomodation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Accomodations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Accomodations");
        }
    }
}
