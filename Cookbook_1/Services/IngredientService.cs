using AutoMapper;
using Cookbook_1.Abstractions;
using Cookbook_1.Contracts;
using Cookbook_1.Exceptions;
using Cookbook_1.Models;
using Microsoft.EntityFrameworkCore;

namespace Cookbook_1.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IApplicationDbContext _applicationDbContext; 
        private readonly IMapper _mapper;
        public IngredientService(IApplicationDbContext applicationDbContext, IMapper mapper) 
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<Ingredient> GetIngredientByIdAsync(int ingredientId)
        {
            var ingredient = await _applicationDbContext.Ingredients.AsNoTracking().FirstOrDefaultAsync(ingredient => ingredient.Id == ingredientId)
                ?? throw new IngredientNotFoundException(ingredientId);
            return ingredient;
        }

        public async Task AddIngredientAsync(string name)
        {
            var checkIfIngredientExists = await _applicationDbContext.Ingredients.AsNoTracking().FirstOrDefaultAsync(ingredient => ingredient.Name.ToLower() ==  name.ToLower().Trim());
            if (checkIfIngredientExists != null)
            {
                throw new IngredientAlreadyExistsException(name); // Нет констистентности, другая ошибка возвращает int id
            }
            _applicationDbContext.Ingredients.Add(new Ingredient { Name = name});
            _applicationDbContext.SaveChanges();
        }

        public async Task AddIngredientToRecipeAsync(AddIngredientToRecipeDto dto)
        {
            var ingredient = await _applicationDbContext.Ingredients.AsNoTracking().FirstOrDefaultAsync(ingredient => ingredient.Id == dto.IngredientId)
                ?? throw new IngredientNotFoundException(dto.IngredientId);

            var newIngredientInRecipe = _mapper.Map<IngredientInRecipe>(dto);
            newIngredientInRecipe.Ingredient = ingredient;
            _applicationDbContext.IngredientsInRecipes.Add(newIngredientInRecipe);
            _applicationDbContext.SaveChanges();
        }
        
        public async Task<List<Ingredient>> ShowAllIngredientsForTest()
        {
            var AllIngredients = await _applicationDbContext.Ingredients.ToListAsync();
            return AllIngredients;
        }

        public async Task<List<IngredientInRecipe>> ShowAllIngredientsInRecipeForTest()
        {
            var AllIngredientsInRecipes = await _applicationDbContext.IngredientsInRecipes.ToListAsync();
            return AllIngredientsInRecipes;
        }
    }
}
