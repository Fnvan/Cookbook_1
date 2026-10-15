namespace Cookbook_1.Models
{
    public class Recipe 
    {
        public int Id { get; set; }

        public int UserId { get; set; } 

        public User User { get; set; }

        public required string Name { get; set; }

        public string CookingDescription { get; set; }

        public List<IngredientInRecipe> RequiredIngredients { get; set; }

        public double Rating { get; set; }

        public ICollection<Rating> ListOfRatings { get; set; } = []; 
    }
}
