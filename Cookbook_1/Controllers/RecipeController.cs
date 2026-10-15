using Cookbook_1.Abstractions;
using Cookbook_1.Contracts;
using Cookbook_1.ENums;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {  
        private IRecipeService _recipeService;
        private readonly IIngredientService _ingredientService;
        public RecipeController(IRecipeService recipeService, IIngredientService ingredientService)
        {
            _recipeService = recipeService;
            _ingredientService = ingredientService;
        }
        [HttpGet("recipelist")]
        public IActionResult GetRecipeList()
        {
            var recipes = _recipeService.GetAllRecipes();
            return Ok(recipes);
        }

        [HttpPost("newrecipe")]
        public IActionResult CreateNewRecipe(CreateRecipeDto dto)
        {
            var newRecipe = _recipeService.CreateRecipe(dto);
            return Ok(newRecipe);
        }

        [HttpGet("{id}")]
        public IActionResult GetRecipe(int id)
        {
            var recipe = _recipeService.GetRecipe(id);
            return Ok(recipe);
        }

        [HttpPut("{id}/update")]
        public IActionResult UpdateRecipe(UpdateRecipeDto dto)
        {
            _recipeService.UpdateRecipe(dto); //Возвращать новый рецепт
            return Ok();
        }

        [HttpDelete("{id}/delete")]
        public IActionResult DeleteRecipe(int id)
        {
            _recipeService.DeleteRecipe(id);
            return Ok();
        }

        [HttpPut("{id}/rate")]
        public IActionResult RateRecipe(int id, RecipeRating rating)
        {
            _recipeService.RateTheRecipe(id, rating);
            return Ok();
        }

        [HttpPost("/newingredient")]
        public IActionResult AddIngredient(string name)
        {
            _ingredientService.AddIngredient(name);
            return Ok();
        } 
    }
}
