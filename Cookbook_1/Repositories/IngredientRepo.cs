using Cookbook_1.Abstractions;
using Cookbook_1.ENums;
using Cookbook_1.Exceptions;
using Cookbook_1.Models;

namespace Cookbook_1.Repositories
{
    public class IngredientRepo : IIngredientRepo
    {
        public readonly List<Ingredient> Ingredients = [];
        public readonly List<IngredientInRecipe> IngredientsInRecipes = [];

        public void AddNewIngridient(string name)
        {
            var checkIfIngredientExists = Ingredients.Where(ingredient => ingredient.Name.ToLower() == name.ToLower().Trim()).FirstOrDefault(); //Как будто правильнее так, а не по id
            if (checkIfIngredientExists != null)
            {
                throw new IngredientAlreadyExistsException(name); // Нет констистентности, другая ошибка возвращает int id
            }
            Ingredient ingredient = new()
            {
                Id = Ingredients.Count(),
                Name = name,
            };
            Ingredients.Add(ingredient);
        }

        public List<Ingredient> GetAllIngredients()
        {
            return Ingredients;
        }

        public List<Ingredient> GetRecipeIngredients(int recipeId)
        {
            throw new NotImplementedException();
        }
        public Ingredient GetIngredientById(int id)
        {
            var ingredient = Ingredients.FirstOrDefault(i => i.Id == id);
            if (ingredient == null)
            {
                throw new IngredientNotFoundException(id);
            }
            return ingredient;
            
        }

        public List<Ingredient> GetIngredientsForRecipeById(List<int> ingredientsId)
        {
            var IngredientsForRecipe = Ingredients.Where(i => ingredientsId.Contains(i.Id)).ToList(); //ошибочку бы тут какую обработать
            //А что если будт неправльно переданы ID и достанется не всё? нужно это как то обрабатывать?
            return IngredientsForRecipe;
        }

        public void AddIngredientToRecipe(int recipeId,  int ingredientId, double amount, Units units)
        {
            var ingredient = GetIngredientById(ingredientId);
            var ingredientInRecipe = new IngredientInRecipe
            {
                RecipeId = recipeId,
                Ingredient = ingredient,
                IngredientName = ingredient.Name,
                Amount = amount,
                Units = units
            };
            IngredientsInRecipes.Add(ingredientInRecipe);
        } 
    }
}
