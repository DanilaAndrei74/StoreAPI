using System.Security.Cryptography;
using System.Text;

namespace StoreAPI.Services.Authentication
{
    public class AuthenticationService
    {
        public string CreateSalt()
        {
            return Guid.NewGuid().ToString();
        }

        public string HashPassword(string password, string salt)
        {
            string saltAndPwd = String.Concat(password, salt);
            HashAlgorithm algorithm = new SHA256Managed();

            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPwd)).ToString();
        }
    }
}
