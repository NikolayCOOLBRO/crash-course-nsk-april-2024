using System.Security.Cryptography;
using System.Text;

namespace Market.Misc
{
    public class PasswordHasher
    {
        public static string GeneratePaswordHash(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

            return Convert.ToBase64String(hash);
        }
    }
}
