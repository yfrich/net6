﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity框架.Migrations
{
    public partial class AddJWTVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "JWTVersion",
                table: "AspNetUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JWTVersion",
                table: "AspNetUsers");
        }
    }
}
