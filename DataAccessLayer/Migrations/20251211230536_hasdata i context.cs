using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class hasdataicontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Apoteker",
                columns: new[] { "Id", "Adresse", "Navn" },
                values: new object[,]
                {
                    { 1, "Sct. Clemens Torv 4, 8000 Aarhus C", "Sct. Clement Apotek" },
                    { 2, "Østergade 15, 7800 Skive", "Skive Apotek" },
                    { 3, "Nørregade 20, 7500 Holstebro", "Holstebro Løve Apotek" }
                });

            migrationBuilder.InsertData(
                table: "Lægehus",
                columns: new[] { "Id", "Navn", "YderNummer" },
                values: new object[,]
                {
                    { 1, "Skive Lægehus", "123456" },
                    { 2, "Aarhus Sundhedscenter", "654321" },
                    { 3, "Viborg Lægeklinik", "112233" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Apoteker",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Apoteker",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Apoteker",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Lægehus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lægehus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Lægehus",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
