using AutoMapper;
using Cookbook_1.Abstractions;
using Cookbook_1.Contracts;
using Cookbook_1.ENums;
using Cookbook_1.Exceptions;
using Cookbook_1.Models;



namespace Cookbook_1.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepo _recipeRepo;
        private readonly IIngredientRepo _ingredientRepo;
        private readonly IMapper _mapper;
        public RecipeService(IIngredientRepo ingredientRepo, IRecipeRepo recipeRepo, IMapper mapper) 
        {
            _ingredientRepo = ingredientRepo;
            _recipeRepo = recipeRepo;
            _mapper = mapper;
        }
        public Recipe CreateRecipe(CreateRecipeDto dto)
        {
            var recipeVm = _recipeRepo.CreateRecipe(dto);
            return recipeVm;
        }

        public RecipeVm GetRecipe(int id) 
        {
            var recipe = _recipeRepo.GetRecipe(id);
            if (recipe == null)
            {
                throw new RecipeNotFoundException(id);
            }
            
            //Я так полагаю, этим надо заниматься в репе
            //var recipeVm = new RecipeVm(recipe.Name, recipe.CookingDescription, [], recipe.Rating);
            //foreach (var ingredient in recipe.RequieredIngredients)
            //{
            //    var ingredientVm = new IngredientInRecipeVm(
            //    ingredient.Ingredient.Name,
            //    ingredient.Amount,
            //    ingredient.Units
            //    );
            //    recipeVm.RequieredIngredients.Add(ingredientVm);
            //}
            
            return recipe;
            
        }

        public void UpdateRecipe(UpdateRecipeDto dto)// потом добавить возможность добавлять ингредиенты
        {
            _recipeRepo.UpdateRecipe(dto); 

        }
        public void DeleteRecipe(int id)
        {
            _recipeRepo.DeleteRecipe(id);   
        }

        public List<RecipeVm> GetAllRecipes()
        {
            return _recipeRepo.ReturnRecipeListVm();
        }

        public void RateTheRecipe(int id, RecipeRating rating)
        {
            _recipeRepo.RateTheRecipe(id, rating);
        }

        
    }
}
