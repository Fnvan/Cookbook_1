namespace Cookbook_1.Models
{
    public class Recipe // потом добавить пользователя
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string CookingDescription { get; set; }
        public List<IngredientInRecipe> RequiredIngredients { get; set; }
        public double Rating { get; set; }
        public List<double> ListOfRatings { get; set; } = []; //наверно, при создании бд это пропадёт
    }
}
