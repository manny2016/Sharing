

namespace Sharing.Portal.Api
{
    using Sharing.Core;
    using System.IO;
    using System.Text;
    public static class StreamExtension
    {
        public static string ReadAsStringAsync(this Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            using (stream)
            {
                var bytes = new byte[1024 * 1024];
                var read = stream.Read(bytes, 0, bytes.Length);
                return string.Empty;
            }
        }
    }
}
