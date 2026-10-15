namespace Cookbook_1.Exceptions
{
    public class RecipeNotFoundException : Exception
    {
        public RecipeNotFoundException(int id)
            : base($"Recipe with Id {id} not found")
        {

        }
    }
}
