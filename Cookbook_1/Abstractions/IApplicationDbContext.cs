using Cookbook_1.Models;
using Microsoft.EntityFrameworkCore;

namespace Cookbook_1.Abstractions
{
    public interface IApplicationDbContext
    {
        public DbSet<Recipe> Recipes { get;}
        public DbSet<Ingredient> Ingredients { get;}
        public DbSet<IngredientInRecipe> IngredientsInRecipes { get; }

        public DbSet<User> Users { get; }

        public DbSet<Rating> Ratings { get; }

        public int SaveChanges();
    }
}
