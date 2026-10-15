using Cookbook_1.ENums;

namespace Cookbook_1.Models
{
    public class Rating
    {
        public RecipeRating Value { get; set; }
        public int RatedUserId { get; set; }
        public User User { get; set; } = null!;
        public int RatedRecipeId { get; set; }

        public Recipe Recipe { get; set; } = null!;
    }
}
