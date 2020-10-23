using Microsoft.EntityFrameworkCore.Migrations;

namespace Group_Project.Migrations
{
    public partial class AmmountDue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AmmountDue",
                table: "User",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmmountDue",
                table: "User");
        }
    }
}
