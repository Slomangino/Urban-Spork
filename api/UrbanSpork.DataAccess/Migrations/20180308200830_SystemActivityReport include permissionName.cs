using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace UrbanSpork.DataAccess.Migrations
{
    public partial class SystemActivityReportincludepermissionName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PermissionName",
                table: "systemactivityprojection",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermissionName",
                table: "systemactivityprojection");
        }
    }
}
