using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Page_Backend.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Detalji",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojIndeksa = table.Column<string>(nullable: true),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Godina = table.Column<int>(nullable: false),
                    NazivStatusa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalji", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    Role = table.Column<string>(nullable: true, defaultValue: "Korisnik")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.KorisnikId);
                });

            migrationBuilder.CreateTable(
                name: "Kursevi",
                columns: table => new
                {
                    KursId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kursevi", x => x.KursId);
                });

            migrationBuilder.CreateTable(
                name: "Status_Studenta",
                columns: table => new
                {
                    StatusStudentaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivStatusa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status_Studenta", x => x.StatusStudentaId);
                });

            migrationBuilder.CreateTable(
                name: "Studenti",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojIndeksa = table.Column<string>(nullable: true),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Godina = table.Column<int>(nullable: false),
                    StatusStudentaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studenti", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Studenti_Status_Studenta_StatusStudentaId",
                        column: x => x.StatusStudentaId,
                        principalTable: "Status_Studenta",
                        principalColumn: "StatusStudentaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KursStudenti",
                columns: table => new
                {
                    KursId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KursStudenti", x => new { x.KursId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_KursStudenti_Kursevi_KursId",
                        column: x => x.KursId,
                        principalTable: "Kursevi",
                        principalColumn: "KursId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KursStudenti_Studenti_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Studenti",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KursStudenti_StudentId",
                table: "KursStudenti",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Studenti_StatusStudentaId",
                table: "Studenti",
                column: "StatusStudentaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalji");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "KursStudenti");

            migrationBuilder.DropTable(
                name: "Kursevi");

            migrationBuilder.DropTable(
                name: "Studenti");

            migrationBuilder.DropTable(
                name: "Status_Studenta");
        }
    }
}
