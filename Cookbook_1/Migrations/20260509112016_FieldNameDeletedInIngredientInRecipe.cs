using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cookbook_1.Migrations
{
    /// <inheritdoc />
    public partial class FieldNameDeletedInIngredientInRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IngredientName",
                table: "IngredientsInRecipes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IngredientName",
                table: "IngredientsInRecipes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
