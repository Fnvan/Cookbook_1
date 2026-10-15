using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cookbook_1.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListOfRatings",
                table: "Recipes");

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    RatedUserId = table.Column<int>(type: "integer", nullable: false),
                    RatedRecipeId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => new { x.RatedUserId, x.RatedRecipeId });
                    table.ForeignKey(
                        name: "FK_Rating_Recipes_RatedRecipeId",
                        column: x => x.RatedRecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rating_Users_RatedUserId",
                        column: x => x.RatedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rating_RatedRecipeId",
                table: "Rating",
                column: "RatedRecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.AddColumn<List<double>>(
                name: "ListOfRatings",
                table: "Recipes",
                type: "double precision[]",
                nullable: false);
        }
    }
}
