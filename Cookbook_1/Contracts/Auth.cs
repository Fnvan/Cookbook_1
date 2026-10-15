namespace Cookbook_1.Contracts
{
    public record LogInResponse (int UserId, string Token, string RefreshToken);
}
