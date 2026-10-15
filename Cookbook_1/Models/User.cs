namespace Cookbook_1.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public required string Login {  get; set; }
        public required string Password { get; set; } //как тут по приватности правильно сделать?

        public List<Recipe> RecipeList { get; set; } 
    }
}
