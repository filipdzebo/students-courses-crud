using Microsoft.EntityFrameworkCore.Migrations;

namespace Page_Backend.Migrations
{
    public partial class spGetStudentById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"CREATE PROCEDURE spGetStudentById
                                @Id int
                                AS
                                BEGIN
	                            SELECT StudentId ,BrojIndeksa ,Ime,Prezime,Godina ,
	                            NazivStatusa FROM Studenti,Status_Studenta 
	                            WHERE Studenti.StudentId=@Id AND 
                                Studenti.StatusStudentaId=Status_Studenta.StatusStudentaId;
                                END";
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Drop PROCEDURE spGetStudentById";
            migrationBuilder.Sql(procedure);
        }
    }
}
