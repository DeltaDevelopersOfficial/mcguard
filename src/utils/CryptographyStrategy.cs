using System.Security.Cryptography;
using System.Text;

namespace McGuard.src.utils
{
    internal class CryptographyStrategy
    {
        /// <summary>
        /// Converts text to SHA-1
        /// </summary>
        /// <param name="input">String your want convert to SHA-1</param>
        /// <returns>SHA1 hash</returns>
        public static string GetSha1FromText(string input)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder hashString = new StringBuilder();
                
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2"));
                }

                return hashString.ToString();
            }
        }
    }
}
