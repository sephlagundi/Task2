using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestApi.Migrations
{
    public partial class storedprocedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE getallemployees

            AS

            BEGIN
                SELECT * FROM Employees;
            END";

            migrationBuilder.Sql(sp);
          

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE getallemployees";


            migrationBuilder.Sql(sp);

        }
    }
}
