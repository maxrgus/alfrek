using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace alfrek.api.Migrations
{
    public partial class AddSlugToSolution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Solutions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Solutions");
        }
    }
}
