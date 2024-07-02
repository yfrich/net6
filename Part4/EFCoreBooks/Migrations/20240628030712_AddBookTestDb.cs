using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreBooks.Migrations
{
    public partial class AddBookTestDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestMoreDB",
                table: "T_Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestMoreDB",
                table: "T_Books");
        }
    }
}
