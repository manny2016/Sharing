



namespace Sharing.Core.CMQ
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public static class SignExtension
    {
        public static string CreateRequestUrl(this SortedDictionary<string, string> sorted, string url, string httpMethod = "GET")
        {
            if (httpMethod.ToUpper() == "GET")
            {
                return string.Concat(url,
              string.Join("&", sorted.Select(ctx =>
              {
                  return $"{ctx.Key}={HttpUtility.UrlEncode(ctx.Value)}";
              })));
            }

            else
            {
                return url;
            }
        }
        public static string CreateRequestBody(this SortedDictionary<string, string> sorted)
        {
            return string.Join("&", sorted.Select(ctx =>
            {
                return $"{ctx.Key}={HttpUtility.UrlEncode(ctx.Value)}";
            }));
        }


    }
}
