using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLayer.Migrations
{
    public partial class Delete_Number_And_IsRightFooted_From_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRightFooted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRightFooted",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Users",
                nullable: true);
        }
    }
}
