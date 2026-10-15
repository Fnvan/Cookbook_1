using System.ComponentModel.DataAnnotations;

namespace Cookbook_1
{
    public class JwtOptions
    {
        //Приложение, которое будет подписывать токен
        [Required]
        public required string Issuer { get; set; }
        //Приложение, для которого подписывают токен
        [Required]
        public required string Subject { get; set; }
        //Секретная фраза для шифрования
        [Required]
        public required string Secrets { get; set; }
    }
}
