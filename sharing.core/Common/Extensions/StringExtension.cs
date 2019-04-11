

namespace Sharing.Core
{
    using System.Text;
    using System;
    using System.Security.Cryptography;
    using System.Linq;
    using System.Collections;
    using Sharing.WeChat.Models;

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
        public static string Sign(this string[] parameters)
        {
            ArrayList AL = new ArrayList();
            foreach (var parameter in parameters)
            {
                if (string.IsNullOrEmpty(parameter))
                    continue;
                AL.Add(parameter);
            }
            AL.Sort(new DictionarySort());
            var perpare = string.Join(string.Empty, AL.ToArray());
            return perpare.GetSHA1Crypto();
        }
        public static void Sign(this WxPayAttach attach, int money)
        {
            var array = new string[] { attach.NonceStr, attach.TimeStamp.ToString(), attach.UserCode??"", attach.CardId??"", money.ToString() };
            attach.Paysign = array.Sign();
        }
    }
}
