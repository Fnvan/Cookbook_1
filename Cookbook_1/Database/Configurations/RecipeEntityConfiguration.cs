using Cookbook_1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cookbook_1.Database.Configurations
{
    public class RecipeEntityConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasKey(recipe => recipe.Id);
            builder.Property(recipe => recipe.Name).IsRequired().HasMaxLength(128);
            builder.Property(recipe => recipe.CookingDescription).HasMaxLength(256);

            builder.HasMany(r => r.ListOfRatings)
                .WithOne(rating => rating.Recipe)
                .HasForeignKey(rating => rating.RatedRecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.RequiredIngredients)
            .WithOne(ir => ir.Recipe)
            .HasForeignKey(ir => ir.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.User)
           .WithMany(u => u.UserRecipes)
           .HasForeignKey(r => r.UserId);

            // Переименовываем колонку CreatorId → UserId
            builder.Property(r => r.UserId).HasColumnName("UserId");
        }
    }
}
