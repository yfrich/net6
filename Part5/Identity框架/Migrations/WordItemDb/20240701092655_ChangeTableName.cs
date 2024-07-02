using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity框架.Migrations.WordItemDb
{
    public partial class ChangeTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_T_WrodItems",
                table: "T_WrodItems");

            migrationBuilder.RenameTable(
                name: "T_WrodItems",
                newName: "T_WordItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_WordItems",
                table: "T_WordItems",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_T_WordItems",
                table: "T_WordItems");

            migrationBuilder.RenameTable(
                name: "T_WordItems",
                newName: "T_WrodItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_WrodItems",
                table: "T_WrodItems",
                column: "Id");
        }
    }
}
