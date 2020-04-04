﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCookbook.Data.Migrations
{
    public partial class AddMaxLengthRecipeAdvices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Advices",
                table: "Recipes",
                maxLength: 800,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Advices",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 800,
                oldNullable: true);
        }
    }
}