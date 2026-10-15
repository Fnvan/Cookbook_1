using AutoMapper;
using Cookbook_1.Abstractions;
using Cookbook_1.Contracts;
using Cookbook_1.ENums;
using Cookbook_1.Exceptions;
using Cookbook_1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Linq;



namespace Cookbook_1.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _applicationDbContext;

        public RecipeService(IMapper mapper, IApplicationDbContext applicationDbContext)
        {
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }


        public async Task<RecipeVm> CreateRecipeAsync(int userId, CreateRecipeDto dto, CancellationToken cancellationToken)
        {
            var newRecipe = _mapper.Map<Recipe>(dto);
            newRecipe.UserId = userId;

            var recipeExists = await _applicationDbContext.Recipes.AsNoTracking().FirstOrDefaultAsync(recipe => recipe.Name == newRecipe.Name);
            if (recipeExists != null)
            {
                throw new RecipeAlreadyExistsException(newRecipe.Name);
            }

            var existingIngredients = await GetExistingIngredients(newRecipe.RequiredIngredients);
            newRecipe.Name = newRecipe.Name.Trim();

            foreach (var ingredientInRecipe in newRecipe.RequiredIngredients)
            {
                ingredientInRecipe.Recipe = newRecipe;
                ingredientInRecipe.Ingredient = existingIngredients[ingredientInRecipe.IngredientId];
            }

            _applicationDbContext.Recipes.Add(newRecipe);
            _applicationDbContext.SaveChanges();
            var newRecipeVm = _mapper.Map<RecipeVm>(newRecipe);
            return newRecipeVm;
        }

        public async Task<RecipeVm> GetRecipeAsync(int id, CancellationToken cancellationToken)
        {
            var recipe = await GetRecipeWithIngredientsOrThrowExceptionAsync(id);

            var recipeVm = _mapper.Map<RecipeVm>(recipe); 

            return recipeVm;
        }

        public async Task<RecipeVm> UpdateRecipeAsync(UpdateRecipeDto dto, CancellationToken cancellationToken)
        {
            var recipeToUpdate = await GetRecipeWithIngredientsOrThrowExceptionAsync(dto.Id);
            recipeToUpdate.Name = dto.NewName ?? recipeToUpdate.Name;
            recipeToUpdate.CookingDescription = dto.NewDescription ?? recipeToUpdate.CookingDescription;

            if (dto.NewIngredients != null)
            {
                foreach (var ingredient in dto.NewIngredients)
                {
                    var IngredientAlreadyInRecipe = recipeToUpdate.RequiredIngredients.FirstOrDefault(i => i.IngredientId == ingredient.IngredientId);
                    if (IngredientAlreadyInRecipe != null)
                    {
                        throw new IngredientAlreadyExistsException(IngredientAlreadyInRecipe.Ingredient.Name); //Тут он же продолжит работу и пойдёт дальше по списку, да?
                    }
                    var newIngredientMap = _mapper.Map<IngredientInRecipe>(ingredient);
                    recipeToUpdate.RequiredIngredients.Add(newIngredientMap);
                }
            }

            if (dto.EditIngredients != null)
            {
                foreach (var ingredient in dto.EditIngredients)
                {
                    //Было бы круто иметь  dto без recipeId, и присваивать этот ID беря его из рецепта
                    var ingredientToUpdate = recipeToUpdate.RequiredIngredients.FirstOrDefault(i => i.IngredientId == ingredient.IngredientId)
                        ?? throw new IngredientNotFoundException(ingredient.IngredientId);
                    ingredientToUpdate.Amount = ingredient.Amount ?? ingredientToUpdate.Amount;
                    ingredientToUpdate.Units = ingredient.Units ?? ingredientToUpdate.Units;
                }
            }
            _applicationDbContext.SaveChanges();
            var recipeVm = _mapper.Map<RecipeVm>(recipeToUpdate);
            return recipeVm;
        }

        public async Task DeleteRecipeAsync(int id, CancellationToken cancellationToken)
        {
            var recipe = await _applicationDbContext.Recipes.FirstOrDefaultAsync(recipe => recipe.Id == id) ?? throw new RecipeNotFoundException(id);
            _applicationDbContext.Recipes.Remove(recipe); 
            _applicationDbContext.SaveChanges();
        }

        public async Task RateTheRecipeAsync(int id, int userId, RecipeRating rating, CancellationToken cancellationToken)
        {
            var recipe = await GetRecipeWithIngredientsOrThrowExceptionAsync(id);
            var existingRating = await _applicationDbContext.Ratings.FirstOrDefaultAsync(r => r.RatedRecipeId == id && r.RatedUserId == userId);
            if (existingRating is not null)
                throw new InvalidOperationException("Пользователь уже оценил этот рецепт");

            Rating ratingValue = new Rating
            {
                Value = rating,
                RatedUserId = userId,
                RatedRecipeId = id

            };

            recipe.ListOfRatings.Add(ratingValue);
            _applicationDbContext.Ratings.Add(ratingValue);
            
           _applicationDbContext.SaveChanges();

            var ratings = _applicationDbContext.Ratings.Where(rating => rating.RatedRecipeId == id);
            recipe.Rating = ratings.Average(r => (int)r.Value);
            _applicationDbContext.SaveChanges();

        }

        private async Task<Dictionary<int, Ingredient>> GetExistingIngredients(List<IngredientInRecipe> ingredients)
        {
            List<int> IngredientsIds = [.. ingredients.Select(i => i.IngredientId)]; //что за две точки?
            var existingIngredients = await _applicationDbContext.Ingredients.Where(i => IngredientsIds.Contains(i.Id)).ToDictionaryAsync(i => i.Id);
            return existingIngredients;
        }

        private async Task<Recipe> GetRecipeWithIngredientsOrThrowExceptionAsync(int recipeId)
        {
            var recipe = await _applicationDbContext.Recipes.Where(r => r.Id == recipeId)
                .Include(i => i.RequiredIngredients)
                .ThenInclude(i => i.Ingredient).FirstOrDefaultAsync() ?? throw new RecipeNotFoundException(recipeId);
            return recipe;
        }

        public async Task<List<RecipeVm>> GetFilteredRecipesListByUserAsync(int userId, CancellationToken cancellationToken)
        {
            var filteredRecipes = await _applicationDbContext.Recipes.Where(u => u.UserId == userId)
                .Include(i => i.RequiredIngredients)
                .ThenInclude(i => i.Ingredient).ToListAsync();

            if (filteredRecipes.Count() == 0)
            {
                throw new UserNotFoundException(userId);
            }

            var recipeListVm = _mapper.Map<List<RecipeVm>>(filteredRecipes);
            return recipeListVm;
        }

        public async Task<List<RecipeVm>> GetFilteredRecipeListAsync(double? minRating, int? userId, string? name, RecipeFilterBy? filterType, bool? ascending, CancellationToken cancellationToken)
        {
             
            List<RecipeVm> recipeListVm;

            if (minRating is not null)
            {
                recipeListVm = await FilterAsync(r => r.Rating >= minRating);
                if (ascending is not null)
                    recipeListVm = SortList(recipeListVm, r => r.Rating, ascending.Value);
            }
            else if (!string.IsNullOrEmpty(name))
            {
                recipeListVm = await FilterAsync(r => r.Name == name);
                if (ascending is not null)
                    recipeListVm = SortList(recipeListVm, r => r.Name, ascending.Value);
            }
            else if (userId is not null)
            {
                recipeListVm = await FilterAsync(r => r.UserId == userId);
            }
            else
                throw new FilterException("Неверные настройки фильтра");

            if(!recipeListVm.Any())
                throw new FilterException("Рецептов по заданному фильтру не найдено");

            return recipeListVm;
        }

        private async Task<List<RecipeVm>> FilterAsync(Expression<Func<Recipe, bool>> expression)
        {
            var filteredRecipes = await _applicationDbContext.Recipes
                .Where(expression)
                .Include(i => i.RequiredIngredients)
                .ThenInclude(i => i.Ingredient)
                .ToListAsync();

            var recipeListVm = _mapper.Map<List<RecipeVm>>(filteredRecipes);
            return recipeListVm;
        }

        private List<RecipeVm> SortList<Tkey>(List<RecipeVm> list, Func<RecipeVm, Tkey> keySelector, bool ascending)
        {
            return ascending
                ? list.OrderBy(keySelector).ToList()
                : list.OrderByDescending(keySelector).ToList();
        }
    }
}