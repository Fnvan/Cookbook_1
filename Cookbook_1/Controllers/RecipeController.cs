using Cookbook_1.Abstractions;
using Cookbook_1.Contracts;
using Cookbook_1.ENums;
using Cookbook_1.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cookbook_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {  
        private readonly IRecipeService _recipeService;
        private readonly IIngredientService _ingredientService;
        public RecipeController(IRecipeService recipeService, IIngredientService ingredientService)
        {
            _recipeService = recipeService;
            _ingredientService = ingredientService;
        }

        [Authorize]
        [HttpPost("/newrecipe")]
        public async Task<IActionResult> CreateNewRecipe(CreateRecipeDto dto, CancellationToken cancellationToken)
        {
            var userId = HttpContext.ExtractUserIdFromClaims();

            if (userId is null)
                return Unauthorized();

            var newRecipe = await _recipeService.CreateRecipeAsync(userId.Value, dto, cancellationToken);
            return Ok(newRecipe);
            
        }

        [AllowAnonymous]
        [HttpGet("/recipe/{id}")]
        public async Task<IActionResult> GetRecipe(int id, CancellationToken cancellationToken)
        {
            var recipe = await _recipeService.GetRecipeAsync(id, cancellationToken);
            return Ok(recipe);
        }

        [Authorize(Policy = "RecipeOwner")]
        [HttpPut("/{id}/update")]
        public async Task<IActionResult> UpdateRecipe(UpdateRecipeDto dto, CancellationToken cancellationToken)
        {
            await _recipeService.UpdateRecipeAsync(dto, cancellationToken); 
            return Ok();
        }

        [Authorize (Policy = "RecipeOwner")]
        [HttpDelete("/{id}/delete")]
        public async Task<IActionResult> DeleteRecipe(int id, CancellationToken cancellationToken)
        {
            await _recipeService.DeleteRecipeAsync(id, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpPut("/{id}/rate")]
        public async Task<IActionResult> RateRecipe(int id, RecipeRating rating, CancellationToken cancellationToken)
        {
            var userId = HttpContext.ExtractUserIdFromClaims();

            if (userId is null)
                return Unauthorized();

            await _recipeService.RateTheRecipeAsync(id, userId.Value, rating, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpPost("/newingredient")]
        public async Task<IActionResult> AddIngredient(string name)
        {
            await _ingredientService.AddIngredientAsync(name);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("/user{userId}/allrecipes")]
        public async Task<IActionResult> GetUsersRecipes(int userId, CancellationToken cancellationToken)
        {
            var usersRecipes = await _recipeService.GetFilteredRecipesListByUserAsync(userId, cancellationToken);
            return Ok(usersRecipes);
        }

        [AllowAnonymous]
        [HttpGet("/filtered")]
        public async Task<IActionResult> GetFilteredRecipeList(double? minRating, int userId, string? recipeName, RecipeFilterBy filterType, bool? ascending, CancellationToken cancellationToken)
        {
            var filteredRecipeList = await _recipeService.GetFilteredRecipeListAsync(minRating, userId, recipeName, filterType, ascending, cancellationToken);
            return Ok(filteredRecipeList);
        }
    }
}
