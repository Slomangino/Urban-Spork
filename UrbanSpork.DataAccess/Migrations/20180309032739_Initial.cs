using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace UrbanSpork.DataAccess.Migrations
{
    public partial class Initial : Migration
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
                    ByFirstName = table.Column<string>(nullable: true),
                    ByFullName = table.Column<string>(nullable: true),
                    ById = table.Column<Guid>(nullable: false),
                    ByLastName = table.Column<string>(nullable: true),
                    DateOfRequest = table.Column<DateTime>(type: "timestamp", nullable: false),
                    ForFirstName = table.Column<string>(nullable: true),
                    ForFullName = table.Column<string>(nullable: true),
                    ForLastName = table.Column<string>(nullable: true)
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
                name: "systemdropdownprojection",
                columns: table => new
                {
                    PermissionID = table.Column<Guid>(nullable: false),
                    PermissionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_systemdropdownprojection", x => x.PermissionID);
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

            migrationBuilder.CreateTable(
                name: "usermanagementprojection",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Department = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usermanagementprojection", x => x.UserId);
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
                name: "systemdropdownprojection");

            migrationBuilder.DropTable(
                name: "userdetailprojection");

            migrationBuilder.DropTable(
                name: "usermanagementprojection");
        }
    }
}
