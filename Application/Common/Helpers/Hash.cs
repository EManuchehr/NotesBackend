using System.Security.Cryptography;
using System.Text;

namespace Application.Common.Helpers;

public static class Hash
{
    public static string Sha256(string value)
    {
        StringBuilder sb = new StringBuilder();

        using (var hash = SHA256.Create())
        {
            Encoding enc = Encoding.UTF8;
            Byte[] result = hash.ComputeHash(enc.GetBytes(value));

            foreach (Byte b in result)
                sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
    }
}