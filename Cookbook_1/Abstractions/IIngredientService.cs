using Cookbook_1.Contracts;
using Cookbook_1.Models;

namespace Cookbook_1.Abstractions
{
    public interface IIngredientService
    {
        public Ingredient GetIngredientById(int ingredientId);

        public void AddIngredient(string name);

        public void AddIngredientToRecipe(AddIngredientToRecipeDto dto);
    }
}