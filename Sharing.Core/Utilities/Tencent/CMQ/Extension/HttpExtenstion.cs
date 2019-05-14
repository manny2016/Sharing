using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Sharing.Core.CMQ
{
    public static class HttpExtenstion
    {
        public static HttpWebRequest Wrap(this HttpWebRequest http, string body, int timeout = 1000 * 60)
        {
            http.Accept = "*/*";
            http.KeepAlive = true;
            http.Timeout = timeout;
            http.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1;SV1)";
            ServicePointManager.Expect100Continue = false;
            http.Method = "POST";
            http.ContentType = "application/x-www-form-urlencoded";
            
            var bytes = Encoding.GetEncoding("utf-8").GetBytes(body);
            http.ContentLength = bytes.Length;
            using (var stream = http.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
            return http;
        }
        public static HttpWebRequest Wrap(this HttpWebRequest http, int timeout = 1000 * 60)
        {
            http.Accept = "*/*";
            http.KeepAlive = true;
            http.Timeout = timeout;
            http.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1;SV1)";
            ServicePointManager.Expect100Continue = false;
            http.Method = "GET";
            return http;
        }
    }
}
