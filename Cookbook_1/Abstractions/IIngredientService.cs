using Cookbook_1.Contracts;
using Cookbook_1.Models;

namespace Cookbook_1.Abstractions
{
    public interface IIngredientService
    {
        Task <Ingredient> GetIngredientByIdAsync(int ingredientId);

        Task AddIngredientAsync(string name);

        Task AddIngredientToRecipeAsync(AddIngredientToRecipeDto dto);

        Task <List<Ingredient>> ShowAllIngredientsForTest();
        Task<List<IngredientInRecipe>> ShowAllIngredientsInRecipeForTest();
    }
}