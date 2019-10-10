using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLayer.Migrations
{
    public partial class Add_Match_Founder_Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FounderId",
                table: "Matches",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_FounderId",
                table: "Matches",
                column: "FounderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_FounderId",
                table: "Matches",
                column: "FounderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_FounderId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_FounderId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "FounderId",
                table: "Matches");
        }
    }
}
