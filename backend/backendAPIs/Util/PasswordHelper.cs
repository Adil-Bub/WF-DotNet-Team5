using System.Security.Cryptography;

namespace backend.Services
{
    public class PasswordHelper
    {

        private const int Iterations = 10000;
        private const int SaltSize = 16; // 256 bits

        public static (string Hash, string Salt) HashPassword(string password)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var saltBytes = new byte[SaltSize];
                rng.GetBytes(saltBytes);

                var salt = Convert.ToBase64String(saltBytes);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations))
                {
                    var hashBytes = pbkdf2.GetBytes(32); // 256 bits
                    var hash = Convert.ToBase64String(hashBytes);

                    return (hash, salt);
                }
            }
        }

        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);

           
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations))
            {
                var hashBytes = pbkdf2.GetBytes(32); // 256 bits
                var inputHash = Convert.ToBase64String(hashBytes);

                return inputHash == storedHash;
            }
        }
    }
}
