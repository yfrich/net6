using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity框架.Migrations.WordItemDb
{
    public partial class AddWordItem1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_wordItems",
                table: "wordItems");

            migrationBuilder.RenameTable(
                name: "wordItems",
                newName: "T_WrodItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_WrodItems",
                table: "T_WrodItems",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_T_WrodItems",
                table: "T_WrodItems");

            migrationBuilder.RenameTable(
                name: "T_WrodItems",
                newName: "wordItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_wordItems",
                table: "wordItems",
                column: "Id");
        }
    }
}
