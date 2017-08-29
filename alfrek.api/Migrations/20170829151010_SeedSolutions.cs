using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace alfrek.api.Migrations
{
    public partial class SeedSolutions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Solutions (Title,ByLine, Views, ProblemBody, SolutionBody) " +
                                 "VALUES ('First solution title', 'First dummy byLine', 0, 'Lorem ipsum'," +
                                 " 'Ipsum Lorem')");
            migrationBuilder.Sql("INSERT INTO Solutions (Title,ByLine, Views, ProblemBody, SolutionBody) " +
                                 "VALUES ('Second solution title', 'Second dummy byLine', 0, 'Lorem ipsum'," +
                                 " 'Ipsum Lorem')");
            migrationBuilder.Sql("INSERT INTO Solutions (Title,ByLine, Views, ProblemBody, SolutionBody) " +
                                 "VALUES ('Third solution title', 'Third dummy byLine', 0, 'Lorem ipsum'," +
                                 " 'Ipsum Lorem')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Solutions");
        }
    }
}
