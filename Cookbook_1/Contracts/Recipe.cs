namespace Cookbook_1.Contracts
{
    public record CreateRecipeDto(string Name, string CookingDescription, List<IngredientInRecipeDto> RequiredIngredients);

    public record RecipeVm(int UserId, string Name, string CookingDescription, List<IngredientInRecipeVm> RequiredIngredients, double? Rating); 

    public record UpdateRecipeDto(int Id, string? NewName, string? NewDescription, List<AddNewIngredientToRecipeDto>? NewIngredients, List<UpdateIngredientInRecipeDto>? EditIngredients);
}
