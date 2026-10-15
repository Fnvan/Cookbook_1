using System.Security.Cryptography;
using System.Text;

namespace Cookbook_1.Utils
{
    public class PasswordHasher
    {
        private const int HashingIterations = 100000;
        private const int HashSize = 32;
        private const int SaltSize = 16;

        //Алгоритм, который используем
        private static readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA256;

        public static string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, HashingIterations, _hashAlgorithm, HashSize);
            return $"{Convert.ToHexString(salt)}-{Convert.ToHexString(hash)}";
        }

        public static bool VerifyPassword(string hashedPassword, string password)
        {
            string[] parts = hashedPassword.Split('-');
            if (parts.Length != 2)
                throw new FormatException("Invalid hashed password format");

            byte[] salt = Convert.FromHexString(parts[0]);
            byte[] hash = Convert.FromHexString(parts[1]);
            byte[] computedHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, HashingIterations, _hashAlgorithm, HashSize);
            return CryptographicOperations.FixedTimeEquals(computedHash, hash);
        }
    }
}
