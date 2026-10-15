using Cookbook_1.Abstractions;
using Cookbook_1.Contracts;
using Cookbook_1.ENums;
using Cookbook_1.Models;

namespace Cookbook_1.Services
{
    public class IngredientService : IIngredientService
    {
        private IIngredientRepo _ingredientRepo; 
        public readonly List<IngredientInRecipe> ingredientsInRecipes = [];
        public IngredientService(IIngredientRepo ingredientRepo) 
        {
            _ingredientRepo = ingredientRepo;
        }

        public Ingredient GetIngredientById(int ingredientId)
        {
            var ingredient = _ingredientRepo.GetIngredientById(ingredientId);
            return ingredient;
        }

        public void AddIngredient(string name)
        {
            _ingredientRepo.AddNewIngridient(name);
        }

        public void AddIngredientToRecipe(AddIngredientToRecipeDto dto)
        {
            _ingredientRepo.AddIngredientToRecipe(dto.RecipeId, dto.IngredientId, dto.Amount, dto.Units);  
        }
    }
}
