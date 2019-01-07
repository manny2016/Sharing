

namespace Sharing.Core
{
    using System.Text;
    using System;
    public static class StringExtension
    {
        public static byte[] ToBytes(this string text, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = ASCIIEncoding.Default;
            var bytes = encoding.GetBytes(text);
            return bytes;
        }
    }
}
