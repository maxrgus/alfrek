using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace alfrek.api.Migrations
{
    public partial class AffiliationApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AffiliationId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AffiliationId",
                table: "AspNetUsers",
                column: "AffiliationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Affiliations_AffiliationId",
                table: "AspNetUsers",
                column: "AffiliationId",
                principalTable: "Affiliations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Affiliations_AffiliationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AffiliationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AffiliationId",
                table: "AspNetUsers");
        }
    }
}
