

namespace Sharing.Core
{
    using System.Text;
    using System;
    using System.Security.Cryptography;
    using System.Linq;
    public static class StringExtension
    {
        public static byte[] ToBytes(this string text, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = ASCIIEncoding.Default;
            var bytes = encoding.GetBytes(text);
            return bytes;
        }
        public static string GetSHA1Crypto(this string text)
        {
            var bytes = SHA1.Create().ComputeHash(UTF8Encoding.Default.GetBytes(text));
            return bytes.ToHexString();
        }
        public static string ToHexString(this byte[] bytes)
        {
            return string.Join(string.Empty, bytes.Select(o => o.ToString("X2").ToLower()));
        }
    }
}
