using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore1.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Titme",
                table: "T_Books");

            migrationBuilder.AddColumn<double>(
                name: "Salary",
                table: "T_Persons",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "T_Books",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "T_Books",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Dogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dogs");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "T_Persons");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "T_Books");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "T_Books");

            migrationBuilder.AddColumn<string>(
                name: "Titme",
                table: "T_Books",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
