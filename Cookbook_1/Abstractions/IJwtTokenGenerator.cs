using Cookbook_1.Models;

namespace Cookbook_1.Abstractions
{
    public interface IJwtTokenGenerator
    {
        JwtToken Generate(User User);

        RefreshToken GetRefreshToken(int UserId);
    }
}
