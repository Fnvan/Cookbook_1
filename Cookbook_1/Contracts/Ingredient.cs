using Cookbook_1.ENums;

namespace Cookbook_1.Contracts
{
    
    public record IngredientInRecipeDto(int IngredientId, double Amount, Units Units);  
    public record AddIngredientToRecipeDto(int RecipeId, int IngredientId, double Amount, Units Units);
    public record SearchIngredientInRecipeDto(string IngredientName, double Amount, Units Units);  
    public record IngredientInRecipeVm(string IngredientName, double Amount, string Units);
    public record UpdateIngredientInRecipeDto(int RecipeId, int IngredientId, string? NewName,  double? Amount, Units? Units);
    public record AddNewIngredientToRecipeDto(int RecipeId, int IngredientId, string? IngredientName, double Amount, Units Units);
}
