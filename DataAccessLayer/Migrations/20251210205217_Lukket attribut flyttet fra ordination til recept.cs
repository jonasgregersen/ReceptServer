using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Lukketattributflyttetfraordinationtilrecept : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lukket",
                table: "Ordinationer");

            migrationBuilder.AddColumn<bool>(
                name: "Lukket",
                table: "Recepter",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lukket",
                table: "Recepter");

            migrationBuilder.AddColumn<bool>(
                name: "Lukket",
                table: "Ordinationer",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
