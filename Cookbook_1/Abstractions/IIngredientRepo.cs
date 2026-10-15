using Cookbook_1.ENums;
using Cookbook_1.Models;

namespace Cookbook_1.Abstractions
{
    public interface IIngredientRepo
    {
        public void AddNewIngridient(string name);

        public List<Ingredient> GetAllIngredients();

        List<Ingredient> GetRecipeIngredients(int recipeId);

        public Ingredient GetIngredientById(int id);

        public List<Ingredient> GetIngredientsForRecipeById(List<int> ingredientsId);

        //public List<IngredientInRecipe> SetIngredientsForRecipe(int recipeId, List<Ingredient> listOfingrediens);

        public void AddIngredientToRecipe(int recipeId, int ingredientId, double amount, Units units);
    }
}
