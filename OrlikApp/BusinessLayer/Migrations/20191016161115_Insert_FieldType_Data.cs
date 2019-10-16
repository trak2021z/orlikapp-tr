using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLayer.Migrations
{
    public partial class Insert_FieldType_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FieldTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Sztuczna murawa" },
                    { 2L, "Trawa" },
                    { 3L, "Tartan" },
                    { 4L, "Asfalt" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FieldTypes",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "FieldTypes",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "FieldTypes",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "FieldTypes",
                keyColumn: "Id",
                keyValue: 4L);
        }
    }
}
