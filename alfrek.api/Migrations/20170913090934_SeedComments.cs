using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace alfrek.api.Migrations
{
    public partial class SeedComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Comments (UserId, SolutionId, CommentBody) " +
                                 "VALUES ('123456', '987654', 'Hej detta är en kommentar!')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Comments");
        }
    }
}
