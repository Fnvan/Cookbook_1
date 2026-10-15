using Cookbook_1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cookbook_1.Database.Configurations
{
    public class IngredientEntityConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasKey(ingredient => ingredient.Id);
            builder.Property(ingredient => ingredient.Name).IsRequired().HasMaxLength(64);
            builder.HasIndex(ingredient => ingredient.Name).IsUnique();
        }
    }
}
