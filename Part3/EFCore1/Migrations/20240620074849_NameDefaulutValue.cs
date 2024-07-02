using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore1.Migrations
{
    public partial class NameDefaulutValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BirdsHahaha",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "hello",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BirdsHahaha",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "hello");
        }
    }
}
