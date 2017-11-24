using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace alfrek.api.Migrations
{
    public partial class ExtendedMemberUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AffiliationId",
                table: "Authors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Organization",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_AffiliationId",
                table: "Authors",
                column: "AffiliationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Affiliations_AffiliationId",
                table: "Authors",
                column: "AffiliationId",
                principalTable: "Affiliations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Affiliations_AffiliationId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_AffiliationId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "AffiliationId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Organization",
                table: "AspNetUsers");
        }
    }
}
