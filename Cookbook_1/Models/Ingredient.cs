using Cookbook_1.ENums;

namespace Cookbook_1.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<IngredientInRecipe> RecipesList { get; set; } 
    }
}
