using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace UrbanSpork.DataAccess.Migrations
{
    public partial class ES_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EventType = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    UserDTO = table.Column<string>(type: "json", nullable: true),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Access = table.Column<string>(type: "json", nullable: true),
                    Department = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Equipment = table.Column<string>(type: "json", nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    InitialApprovedDate = table.Column<DateTime>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserEvents");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
