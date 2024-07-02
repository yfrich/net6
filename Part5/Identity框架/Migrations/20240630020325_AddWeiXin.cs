using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity框架.Migrations
{
    public partial class AddWeiXin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WeiXinAccount",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeiXinAccount",
                table: "AspNetUsers");
        }
    }
}
