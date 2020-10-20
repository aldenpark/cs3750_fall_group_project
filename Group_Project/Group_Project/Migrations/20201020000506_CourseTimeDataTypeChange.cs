using Microsoft.EntityFrameworkCore.Migrations;

namespace Group_Project.Migrations
{
    public partial class CourseTimeDataTypeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructorID = table.Column<int>(nullable: false),
                    CourseName = table.Column<string>(nullable: true),
                    CourseNumber = table.Column<int>(nullable: false),
                    Department = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreditHours = table.Column<int>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Monday = table.Column<int>(nullable: false),
                    Tuesday = table.Column<int>(nullable: false),
                    Wednesday = table.Column<int>(nullable: false),
                    Thursday = table.Column<int>(nullable: false),
                    Friday = table.Column<int>(nullable: false),
                    Saturday = table.Column<int>(nullable: false),
                    Sunday = table.Column<int>(nullable: false),
                    CourseTime = table.Column<string>(nullable: true),
                    AMPM = table.Column<string>(nullable: true),
                    MaxStudents = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Course");
        }
    }
}
