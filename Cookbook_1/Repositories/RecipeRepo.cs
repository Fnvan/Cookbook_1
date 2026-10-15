using AutoMapper;
using Cookbook_1.Abstractions;
using Cookbook_1.Contracts;
using Cookbook_1.ENums;
using Cookbook_1.Exceptions;
using Cookbook_1.Models;

namespace Cookbook_1.Repositories
{
    public class RecipeRepo : IRecipeRepo
    {
        public List<Recipe> recipeList = [];
        private readonly IIngredientRepo _ingredientRepo;
        private readonly IMapper _mapper;

        public RecipeRepo(IIngredientRepo ingredientRepo, IMapper mapper)
        {
            _ingredientRepo = ingredientRepo;
            _mapper = mapper;
        }
        public List<RecipeVm> ReturnRecipeListVm()
        {
            var recipeListVm = _mapper.Map<List<RecipeVm>>(recipeList);
            return recipeListVm;
        }
        

        public Recipe CreateRecipe(CreateRecipeDto dto)
        {
            var newRecipe = _mapper.Map<Recipe>(dto);
            var recipeExists = recipeList.Where(recipe => recipe.Name == newRecipe.Name).FirstOrDefault();
            if (recipeExists != null)
            {
                throw new RecipeAlreadyExistsException(newRecipe.Name);
            }

            newRecipe.Id = recipeList.Count;
            newRecipe.Name = newRecipe.Name.Trim();

            //Этим, возможно, стоит заниматься в IngredientService
            foreach (var ingredient in newRecipe.RequiredIngredients) //Могу конечно из dto взять, но зачем тогда маппинг
            {
                var ingredientExists = _ingredientRepo.GetIngredientById(ingredient.IngredientId);

                if(ingredientExists == null)
                {
                    _ingredientRepo.AddNewIngridient(ingredient.IngredientName);
                    _ingredientRepo.AddIngredientToRecipe(newRecipe.Id, ingredient.IngredientId, ingredient.Amount, ingredient.Units);
                    
                }
                else
                {
                    _ingredientRepo.AddIngredientToRecipe(newRecipe.Id, ingredient.IngredientId, ingredient.Amount, ingredient.Units); //по логике у меня ещё не должно его существовать в рецепте
                    
                }
                ingredient.IngredientName = ingredientExists.Name;
            }
            recipeList.Add(newRecipe);
            return newRecipe;
        }

        public RecipeVm GetRecipe(int id)
        {
            var recipe = recipeList.Where(recipe => recipe.Id == id).FirstOrDefault() ?? throw new RecipeNotFoundException(id);
            var recipeVm = _mapper.Map<RecipeVm>(recipe);
            return recipeVm;
        }

        public RecipeVm UpdateRecipe(UpdateRecipeDto dto)
        {
            var recipe = recipeList.Where(recipe => recipe.Id == dto.Id).FirstOrDefault() ?? throw new RecipeNotFoundException(dto.Id);
            recipe.Name = dto.NewName ?? recipe.Name;
            recipe.CookingDescription = dto.NewDescription ?? recipe.CookingDescription;
            var recipeVm = _mapper.Map<RecipeVm>(recipe);
            return recipeVm;
        }

        public void DeleteRecipe(int id)
        {
            var recipe = recipeList.Where(recipe => recipe.Id == id).FirstOrDefault() ?? throw new RecipeNotFoundException(id);
            recipeList.Remove(recipe);
        }

        public void RateTheRecipe(int id, RecipeRating rating)
        {
            var recipe = recipeList.Where(recipe => recipe.Id == id).FirstOrDefault() ?? throw new RecipeNotFoundException(id);
            double ratingValue = (double)rating;
            recipe.ListOfRatings.Add(ratingValue);
            recipe.Rating = recipe.ListOfRatings.Sum() / recipe.ListOfRatings.Count();
        }
    }
}
