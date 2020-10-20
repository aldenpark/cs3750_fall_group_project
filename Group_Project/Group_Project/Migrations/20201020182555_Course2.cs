using Microsoft.EntityFrameworkCore.Migrations;

namespace Group_Project.Migrations
{
    public partial class Course2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseTime",
                table: "Course");

            migrationBuilder.AddColumn<string>(
                name: "EndTime",
                table: "Course",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartTime",
                table: "Course",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Course");

            migrationBuilder.AddColumn<string>(
                name: "CourseTime",
                table: "Course",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
