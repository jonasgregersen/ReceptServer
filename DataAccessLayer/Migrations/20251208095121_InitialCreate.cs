using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apoteker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Navn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apoteker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lægehus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Navn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YderNummer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lægehus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recepter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YderNummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CprNummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OprettelsesDato = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LægehusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recepter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recepter_Lægehus_LægehusId",
                        column: x => x.LægehusId,
                        principalTable: "Lægehus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ordinationer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lægemiddel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Udleveringer = table.Column<int>(type: "int", nullable: false),
                    FortagneUdleveringer = table.Column<int>(type: "int", nullable: false),
                    Lukket = table.Column<bool>(type: "bit", nullable: false),
                    ReceptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordinationer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ordinationer_Recepter_ReceptId",
                        column: x => x.ReceptId,
                        principalTable: "Recepter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceptUdleveringer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UdleveringsDato = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Antal = table.Column<int>(type: "int", nullable: false),
                    ApotekId = table.Column<int>(type: "int", nullable: false),
                    ReceptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptUdleveringer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceptUdleveringer_Apoteker_ApotekId",
                        column: x => x.ApotekId,
                        principalTable: "Apoteker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceptUdleveringer_Recepter_ReceptId",
                        column: x => x.ReceptId,
                        principalTable: "Recepter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ordinationer_ReceptId",
                table: "Ordinationer",
                column: "ReceptId");

            migrationBuilder.CreateIndex(
                name: "IX_Recepter_LægehusId",
                table: "Recepter",
                column: "LægehusId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptUdleveringer_ApotekId",
                table: "ReceptUdleveringer",
                column: "ApotekId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptUdleveringer_ReceptId",
                table: "ReceptUdleveringer",
                column: "ReceptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ordinationer");

            migrationBuilder.DropTable(
                name: "ReceptUdleveringer");

            migrationBuilder.DropTable(
                name: "Apoteker");

            migrationBuilder.DropTable(
                name: "Recepter");

            migrationBuilder.DropTable(
                name: "Lægehus");
        }
    }
}
