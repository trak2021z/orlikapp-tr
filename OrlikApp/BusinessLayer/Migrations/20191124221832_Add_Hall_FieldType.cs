using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLayer.Migrations
{
    public partial class Add_Hall_FieldType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FieldTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5L, "Hala" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FieldTypes",
                keyColumn: "Id",
                keyValue: 5L);
        }
    }
}
