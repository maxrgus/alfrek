using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace alfrek.api.Migrations
{
    public partial class AddManytoManySolutionPurposedRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurposedRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurposedRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolutionRole",
                columns: table => new
                {
                    SolutionId = table.Column<int>(type: "int", nullable: false),
                    PurposedRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolutionRole", x => new { x.SolutionId, x.PurposedRoleId });
                    table.ForeignKey(
                        name: "FK_SolutionRole_PurposedRoles_PurposedRoleId",
                        column: x => x.PurposedRoleId,
                        principalTable: "PurposedRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolutionRole_Solutions_SolutionId",
                        column: x => x.SolutionId,
                        principalTable: "Solutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolutionRole_PurposedRoleId",
                table: "SolutionRole",
                column: "PurposedRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolutionRole");

            migrationBuilder.DropTable(
                name: "PurposedRoles");
        }
    }
}
