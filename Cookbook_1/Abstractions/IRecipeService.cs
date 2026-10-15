using Cookbook_1.Contracts;
using Cookbook_1.ENums;
using Cookbook_1.Models;

namespace Cookbook_1.Abstractions
{
    public interface IRecipeService
    {
        public Recipe CreateRecipe(CreateRecipeDto dto);

        public RecipeVm GetRecipe(int id);

        public void UpdateRecipe(UpdateRecipeDto dto);

        public void DeleteRecipe(int id);

        public List<RecipeVm> GetAllRecipes();

        public void RateTheRecipe(int id, RecipeRating rating);


    }
}
