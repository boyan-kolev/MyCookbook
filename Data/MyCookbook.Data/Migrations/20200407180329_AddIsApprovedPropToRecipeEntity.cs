using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCookbook.Data.Migrations
{
    public partial class AddIsApprovedPropToRecipeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Recipes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Recipes");
        }
    }
}
