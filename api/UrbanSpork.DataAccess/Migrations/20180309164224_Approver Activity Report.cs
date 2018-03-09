using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace UrbanSpork.DataAccess.Migrations
{
    public partial class ApproverActivityReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "approveractivityprojection",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApproverId = table.Column<Guid>(nullable: false),
                    ForFullName = table.Column<string>(nullable: true),
                    ForId = table.Column<Guid>(nullable: false),
                    PermissionName = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    TruncatedEventType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_approveractivityprojection", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "approveractivityprojection");
        }
    }
}
