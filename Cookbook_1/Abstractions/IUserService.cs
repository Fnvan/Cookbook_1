using Cookbook_1.Contracts;
using Cookbook_1.Models;

namespace Cookbook_1.Abstractions
{
    public interface IUserService
    {
        Task<UserVm> CreateNewUserAsync(CreateUserDto user);

        Task<UserVm> GetUserProfileAsync(int userId);
        
        Task<UserVm> UpdateUserAsync(UpdateUserDto user);

        Task UpdateUserPasswordAsync(UpdateUserPasswordDto userPassword);

        Task <List<Recipe>> ShowUserRecipesAsync(int userId);
    }
}
