using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cookbook_1.Migrations
{
    /// <inheritdoc />
    public partial class AddConstrains : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientsInRecipes_Recipes_RecipeId",
                table: "IngredientsInRecipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientsInRecipes",
                table: "IngredientsInRecipes");

            migrationBuilder.DropIndex(
                name: "IX_IngredientsInRecipes_RecipeId",
                table: "IngredientsInRecipes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Recipes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CookingDescription",
                table: "Recipes",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "IngredientsInRecipes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "RecipeId1",
                table: "IngredientsInRecipes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ingredients",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientsInRecipes",
                table: "IngredientsInRecipes",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsInRecipes_IngredientId",
                table: "IngredientsInRecipes",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsInRecipes_RecipeId1",
                table: "IngredientsInRecipes",
                column: "RecipeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientsInRecipes_Recipes_RecipeId1",
                table: "IngredientsInRecipes",
                column: "RecipeId1",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientsInRecipes_Recipes_RecipeId1",
                table: "IngredientsInRecipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientsInRecipes",
                table: "IngredientsInRecipes");

            migrationBuilder.DropIndex(
                name: "IX_IngredientsInRecipes_IngredientId",
                table: "IngredientsInRecipes");

            migrationBuilder.DropIndex(
                name: "IX_IngredientsInRecipes_RecipeId1",
                table: "IngredientsInRecipes");

            migrationBuilder.DropColumn(
                name: "RecipeId1",
                table: "IngredientsInRecipes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Recipes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "CookingDescription",
                table: "Recipes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "IngredientsInRecipes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ingredients",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientsInRecipes",
                table: "IngredientsInRecipes",
                columns: new[] { "IngredientId", "RecipeId" });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsInRecipes_RecipeId",
                table: "IngredientsInRecipes",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientsInRecipes_Recipes_RecipeId",
                table: "IngredientsInRecipes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
