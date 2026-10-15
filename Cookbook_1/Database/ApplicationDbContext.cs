using Cookbook_1.Abstractions;
using Cookbook_1.Models;
using Microsoft.EntityFrameworkCore;

namespace Cookbook_1.Database
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientInRecipe> IngredientsInRecipes { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<JwtToken> JwtTokens { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Rating> Ratings { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IWebHostEnvironment environment) : base(options) {}

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
        }
    }
}
