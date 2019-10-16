using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLayer.Migrations
{
    public partial class Add_FieldType_And_Modify_Field_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Fields");

            migrationBuilder.AlterColumn<long>(
                name: "KeeperId",
                table: "Fields",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "TypeId",
                table: "Fields",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "FieldTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(64)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fields_TypeId",
                table: "Fields",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_FieldTypes_TypeId",
                table: "Fields",
                column: "TypeId",
                principalTable: "FieldTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_FieldTypes_TypeId",
                table: "Fields");

            migrationBuilder.DropTable(
                name: "FieldTypes");

            migrationBuilder.DropIndex(
                name: "IX_Fields_TypeId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Fields");

            migrationBuilder.AlterColumn<long>(
                name: "KeeperId",
                table: "Fields",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Fields",
                type: "nvarchar(60)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Fields",
                nullable: false,
                defaultValue: 0);
        }
    }
}
