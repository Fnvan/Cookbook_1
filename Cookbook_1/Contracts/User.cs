using Cookbook_1.Models;

namespace Cookbook_1.Contracts
{
    public record CreateUserDto(string Login, string Password);

    public record LogInUserDto(string Login, string Password);

    public record UpdateUserDto(string Login);

    public record UpdateUserPasswordDto(string Password);

    public record UserVm(string Login, List<RecipeVm> UserRecipes);

}
    