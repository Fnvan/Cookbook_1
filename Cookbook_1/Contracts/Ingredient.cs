using Cookbook_1.ENums;

namespace Cookbook_1.Contracts
{
    //public record CreateRecipeDto(int Id, string Name); 
    
    public record IngredientInRecipeDto(int RecipeId, int IngredientId, double Amount, Units Units);
    public record AddIngredientToRecipeDto(int RecipeId, int IngredientId, double Amount, Units Units);

    public record IngredientInRecipeVm(string IngredientName, double Amount, Units Units);
}
