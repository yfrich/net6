using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore1.Migrations
{
    public partial class AddBird : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BirdsHahaha",
                columns: table => new
                {
                    Number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirdsHahaha", x => x.Number);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BirdsHahaha");
        }
    }
}
