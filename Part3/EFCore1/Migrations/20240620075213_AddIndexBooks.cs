using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore1.Migrations
{
    public partial class AddIndexBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_T_Books_nametwo_AuthorName",
                table: "T_Books",
                columns: new[] { "nametwo", "AuthorName" });

            migrationBuilder.CreateIndex(
                name: "IX_T_Books_Title",
                table: "T_Books",
                column: "Title",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_T_Books_nametwo_AuthorName",
                table: "T_Books");

            migrationBuilder.DropIndex(
                name: "IX_T_Books_Title",
                table: "T_Books");
        }
    }
}
