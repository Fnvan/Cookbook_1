using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cookbook_1.Migrations
{
    /// <inheritdoc />
    public partial class AlterRecipeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Recipes_RatedRecipeId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Users_RatedUserId",
                table: "Rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rating",
                table: "Rating");

            migrationBuilder.RenameTable(
                name: "Rating",
                newName: "Ratings");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_RatedRecipeId",
                table: "Ratings",
                newName: "IX_Ratings_RatedRecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                columns: new[] { "RatedUserId", "RatedRecipeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Recipes_RatedRecipeId",
                table: "Ratings",
                column: "RatedRecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Users_RatedUserId",
                table: "Ratings",
                column: "RatedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Recipes_RatedRecipeId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Users_RatedUserId",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.RenameTable(
                name: "Ratings",
                newName: "Rating");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_RatedRecipeId",
                table: "Rating",
                newName: "IX_Rating_RatedRecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rating",
                table: "Rating",
                columns: new[] { "RatedUserId", "RatedRecipeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Recipes_RatedRecipeId",
                table: "Rating",
                column: "RatedRecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Users_RatedUserId",
                table: "Rating",
                column: "RatedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
