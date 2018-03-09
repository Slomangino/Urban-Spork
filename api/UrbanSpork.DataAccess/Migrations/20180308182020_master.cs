using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace UrbanSpork.DataAccess.Migrations
{
    public partial class master : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ByFirstName",
                table: "pendingrequestsprojection",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ByFullName",
                table: "pendingrequestsprojection",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ByLastName",
                table: "pendingrequestsprojection",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForFirstName",
                table: "pendingrequestsprojection",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForFullName",
                table: "pendingrequestsprojection",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForLastName",
                table: "pendingrequestsprojection",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ByFirstName",
                table: "pendingrequestsprojection");

            migrationBuilder.DropColumn(
                name: "ByFullName",
                table: "pendingrequestsprojection");

            migrationBuilder.DropColumn(
                name: "ByLastName",
                table: "pendingrequestsprojection");

            migrationBuilder.DropColumn(
                name: "ForFirstName",
                table: "pendingrequestsprojection");

            migrationBuilder.DropColumn(
                name: "ForFullName",
                table: "pendingrequestsprojection");

            migrationBuilder.DropColumn(
                name: "ForLastName",
                table: "pendingrequestsprojection");
        }
    }
}
