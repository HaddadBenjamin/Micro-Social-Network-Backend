using System;
using System.Security.Cryptography;
using System.Text;

namespace DiabloII.Application.Helpers
{
    public static class SecurityAlgorithmHelpers
    {
        public static string GenerateSha1Hash(string text)
        {
            var textBytes = Encoding.ASCII.GetBytes(text);
            var hashBytes = new SHA1Managed().ComputeHash(textBytes);
            var hashStringBuilder = new StringBuilder();

            foreach (var hashByte in hashBytes)
                hashStringBuilder.Append(hashByte.ToString("x2"));
            
            return hashStringBuilder.ToString();
        }
    }
}
