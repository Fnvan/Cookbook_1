using Cookbook_1.ENums;

namespace Cookbook_1.Contracts
{
    public record CreateRecipeDto(string Name, string CookingDescription, List<IngredientInRecipeDto> RequiredIngredients);

    public record RecipeVm(string Name, string CookingDescription, List<IngredientInRecipeVm> RequiredIngredients, double Rating);

    public record UpdateRecipeDto(int Id, string? NewName, string NewDescription);
}
