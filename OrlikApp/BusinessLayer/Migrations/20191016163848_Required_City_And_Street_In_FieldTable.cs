using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLayer.Migrations
{
    public partial class Required_City_And_Street_In_FieldTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Fields",
                type: "nvarchar(120)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Fields",
                type: "nvarchar(120)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Fields",
                type: "nvarchar(120)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Fields",
                type: "nvarchar(120)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)");
        }
    }
}
