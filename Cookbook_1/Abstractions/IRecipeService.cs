using Cookbook_1.Contracts;
using Cookbook_1.ENums;
using System.Linq.Expressions;

namespace Cookbook_1.Abstractions
{
    public interface IRecipeService
    {
        Task<RecipeVm> CreateRecipeAsync(int userId, CreateRecipeDto dto, CancellationToken cancellationToken);

        Task<RecipeVm> GetRecipeAsync(int id, CancellationToken cancellationToken);

        Task<RecipeVm> UpdateRecipeAsync(UpdateRecipeDto dto, CancellationToken cancellationToken);

        Task DeleteRecipeAsync(int id, CancellationToken cancellationToken);

        Task RateTheRecipeAsync(int id, int userId, RecipeRating rating, CancellationToken cancellationToken);

        Task<List<RecipeVm>> GetFilteredRecipesListByUserAsync(int userId, CancellationToken cancellationToken);

        Task<List<RecipeVm>> GetFilteredRecipeListAsync(double? minRating, int? userId, string? name, RecipeFilterBy? filterType, bool? ascending, CancellationToken cancellationTokens);
    }
}
