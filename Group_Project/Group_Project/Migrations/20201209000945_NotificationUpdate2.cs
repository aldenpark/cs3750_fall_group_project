﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group_Project.Migrations
{
    public partial class NotificationUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Seen",
                table: "Notification");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Notification",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Seen",
                table: "Notification",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
