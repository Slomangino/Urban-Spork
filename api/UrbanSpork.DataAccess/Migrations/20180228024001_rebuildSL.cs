using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace UrbanSpork.DataAccess.Migrations
{
    public partial class rebuildSL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    EventData = table.Column<string>(nullable: true),
                    EventType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => new { x.Id, x.Version });
                });

            migrationBuilder.CreateTable(
                name: "pendingrequestsprojection",
                columns: table => new
                {
                    PermissionId = table.Column<Guid>(nullable: false),
                    ForId = table.Column<Guid>(nullable: false),
                    RequestType = table.Column<string>(nullable: false),
                    ById = table.Column<Guid>(nullable: false),
                    DateOfRequest = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pendingrequestsprojection", x => new { x.PermissionId, x.ForId, x.RequestType });
                });

            migrationBuilder.CreateTable(
                name: "permissiondetailprojection",
                columns: table => new
                {
                    PermissionId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissiondetailprojection", x => x.PermissionId);
                });

            migrationBuilder.CreateTable(
                name: "userdetailprojection",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Department = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    PermissionList = table.Column<string>(type: "json", nullable: true),
                    Position = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userdetailprojection", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "pendingrequestsprojection");

            migrationBuilder.DropTable(
                name: "permissiondetailprojection");

            migrationBuilder.DropTable(
                name: "userdetailprojection");
        }
    }
}
