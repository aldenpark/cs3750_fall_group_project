using Microsoft.EntityFrameworkCore.Migrations;

namespace Group_Project.Migrations
{
    public partial class MergeFromPeterToAric : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link1",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link2",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link3",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePic",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Zip",
                table: "User",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "User");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "User");

            migrationBuilder.DropColumn(
                name: "City",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Link1",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Link2",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Link3",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ProfilePic",
                table: "User");

            migrationBuilder.DropColumn(
                name: "State",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "User");
        }
    }
}
