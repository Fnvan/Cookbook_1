namespace Cookbook_1.Exceptions
{
    public class RecipeAlreadyExistsException : Exception
    {
        public RecipeAlreadyExistsException(string name)
            : base($" Recipe with ID {name} already exists") 
        {

        }
    }
}
