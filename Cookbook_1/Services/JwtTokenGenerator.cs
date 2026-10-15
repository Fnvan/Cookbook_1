using Cookbook_1.Abstractions;
using Cookbook_1.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Cookbook_1.Services
{
    public class JwtTokenGenerator(IOptions<JwtOptions> options) : IJwtTokenGenerator
    {
        private readonly JwtOptions _options = options.Value;
        public JwtToken Generate(User user)
        {
            SigningCredentials credentials = new(
                //Расшифровка секрета из настроек
                new SymmetricSecurityKey(Convert.FromBase64String(_options.Secrets)),
                SecurityAlgorithms.HmacSha256
            );

            // Клеймы (метаданные пользователя, которому выдаём токен)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.Login),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.Login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var now = DateTime.UtcNow;
            //Время жизни токена
            var expiration = now.AddMinutes(5);

            JwtSecurityToken securityToken = new(
                //Сервис, который подписал токен
                issuer: _options.Issuer,
                //Сервис, для которого подписан токен
                audience: _options.Subject,
                expires: expiration,
                claims: claims,
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return new JwtToken
            {
                UserId = user.Id,
                Token = token,
                CreatedAt = now,
                ExpiresAt = expiration,
            };


        }
        public RefreshToken GetRefreshToken(int userId) => new()
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)),
            UserId = userId,
            ExpiresAt = DateTime.UtcNow.AddDays(2),
        };

        
    }
}
