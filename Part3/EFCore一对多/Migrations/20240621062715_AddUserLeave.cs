﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore一对多.Migrations
{
    public partial class AddUserLeave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Leaves",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequesterId = table.Column<long>(type: "bigint", nullable: false),
                    ApproverId = table.Column<long>(type: "bigint", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Leaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_Leaves_T_Users_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "T_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_Leaves_T_Users_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "T_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_Leaves_ApproverId",
                table: "T_Leaves",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Leaves_RequesterId",
                table: "T_Leaves",
                column: "RequesterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Leaves");

            migrationBuilder.DropTable(
                name: "T_Users");
        }
    }
}
