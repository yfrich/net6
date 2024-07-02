using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore1.Migrations
{
    public partial class Add_Cat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Cats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(22)", maxLength: 22, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Cats", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Cats");
        }
    }
}
