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

        public byte[] HashPassword(string password, string salt)
        {
            string saltAndPwd = String.Concat(password, salt);
            HashAlgorithm algorithm = new SHA256Managed();

            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPwd));
        }

        public bool ValidatePassword(byte[] array1, byte[] array2)
        {
            return array1.Length == array2.Length && !array1.Where((t, i) => t != array2[i]).Any();
        }
    }
}
