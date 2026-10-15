namespace Cookbook_1.Exceptions
{
    public class IngredientNotFoundException : Exception
    {
        public IngredientNotFoundException(int id)
            :base($"Ingredient with id {id} does not exists")
        {

        }
    }
}
