using Cookbook_1.Contracts;
using Cookbook_1.ENums;
using Cookbook_1.Models;

namespace Cookbook_1.Abstractions
{
    public interface IRecipeRepo
    {
        public List<RecipeVm> ReturnRecipeListVm();

        public RecipeVm CreateRecipe(CreateRecipeDto dto);
        public RecipeVm GetRecipe(int id);

        public RecipeVm UpdateRecipe(UpdateRecipeDto dto);

        public void DeleteRecipe(int id);

        public void RateTheRecipe(int id, RecipeRating rating);
    }
}
