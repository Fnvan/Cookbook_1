namespace Cookbook_1.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(int id)
            : base($"User with id {id} not found")
        { }
    }
}
