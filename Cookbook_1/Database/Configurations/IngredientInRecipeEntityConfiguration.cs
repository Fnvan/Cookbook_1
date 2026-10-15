using Cookbook_1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cookbook_1.Database.Configurations
{
    public class IngredientInRecipeEntityConfiguration : IEntityTypeConfiguration<IngredientInRecipe>
    {
        public void Configure(EntityTypeBuilder<IngredientInRecipe> builder)
        {
            builder.HasKey(ingredient => new { ingredient.IngredientId, ingredient.RecipeId });

            builder.HasOne(i => i.Recipe).WithMany(r => r.RequiredIngredients).HasForeignKey(r => r.RecipeId);

            builder.HasOne(i => i.Ingredient).WithMany(i => i.RecipesList).HasForeignKey(i => i.IngredientId);
        }
    }
}
