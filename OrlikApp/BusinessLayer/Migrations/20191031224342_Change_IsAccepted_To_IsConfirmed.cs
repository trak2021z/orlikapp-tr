using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLayer.Migrations
{
    public partial class Change_IsAccepted_To_IsConfirmed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "IsAccepted",
                table: "Matches",
                newName: "IsConfirmed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsConfirmed",
                table: "Matches",
                newName: "IsAccepted");

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "Matches",
                nullable: true);
        }
    }
}
