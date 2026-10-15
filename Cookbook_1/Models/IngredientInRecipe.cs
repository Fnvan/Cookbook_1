using Cookbook_1.ENums;

namespace Cookbook_1.Models
{
    public class IngredientInRecipe
    {
        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; } 

        public required Ingredient Ingredient { get; set; }

        public int IngredientId {  get; set; }

        public double Amount { get; set; }
        public Units Units { get; set; }
    }
}
