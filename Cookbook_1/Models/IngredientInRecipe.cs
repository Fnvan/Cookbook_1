using Cookbook_1.ENums;

namespace Cookbook_1.Models
{
    public class IngredientInRecipe
    {
        public int RecipeId { get; set; }

        //public Recipe Recipe { get; set; } //это нам понадобится, если что, уточнить у Миши

        public required Ingredient Ingredient { get; set; } // отсюда доставать name где надо

        public int IngredientId {  get; set; }

        public string IngredientName {  get; set; }

        public double Amount { get; set; }
        public Units Units { get; set; }
    }
}
