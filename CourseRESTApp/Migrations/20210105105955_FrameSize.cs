using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseRESTApp.Migrations
{
    public partial class FrameSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FrameSize",
                table: "Bike",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FrameSize",
                table: "Bike");
        }
    }
}
