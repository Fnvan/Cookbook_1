using Cookbook_1.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientController : ControllerBase
    {
        public IIngredientService IngredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            IngredientService = ingredientService;
        }
        [HttpGet("ShowAllIngredients")]
        public IActionResult ShowAllIngredients()
        {
            return Ok(IngredientService.ShowAllIngredientsForTest());
        }
        [HttpGet("AllIngredientsInRecipe")]
        public IActionResult ShowAllIngredientsInRecipes()
        {
            return Ok(IngredientService.ShowAllIngredientsInRecipeForTest());
        }
    }
}
