using Cookbook_1.Contracts;

namespace Cookbook_1.Abstractions
{
    public interface IAuthService
    {
        Task <LogInResponse> SignUpAsync(CreateUserDto dto);
        Task <LogInResponse?> LogInAsync(string login, string password);
        Task<bool> LogOutAsync(int userId);
        bool VerifyToken(int userId, string token);

        Task<LogInResponse?> RefreshTokenAsync(string refreshToken);
    }
}
