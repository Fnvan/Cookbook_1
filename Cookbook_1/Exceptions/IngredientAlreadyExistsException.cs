namespace Cookbook_1.Exceptions
{
    public class IngredientAlreadyExistsException : Exception 
    {
        public IngredientAlreadyExistsException(string name)
            : base($"Ingredient {name} already exists")
        {

        }
    }
}
