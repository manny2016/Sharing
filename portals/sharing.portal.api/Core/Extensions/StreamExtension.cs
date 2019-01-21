

namespace Sharing.Portal.Api
{
    using Sharing.Core;
    using System.IO;
    using System.Text;
    using System.Linq;
    public static class StreamExtension
    {
        private static readonly Encoding defaultEncoding = Encoding.UTF8;
        public static string ReadAsStringAsync(this Stream stream, Encoding encoding = null)
        {
            Guard.ArgumentNotNull(stream, "stream");
            if (encoding == null)
                encoding = defaultEncoding;
            using (stream)
            {
                var bytes = new byte[1024 * 1024 * 1024];
                var read = stream.Read(bytes, 0, bytes.Length);
                return encoding.GetString(bytes, 0, read);
            }
        }
    }
}
