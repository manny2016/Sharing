using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Sharing.Core;
namespace Sharing.Portal.Api.Models
{
    public class MapQueryFilter
    {
        [Newtonsoft.Json.JsonProperty("region")]
        public string Region { get; set; }

        [Newtonsoft.Json.JsonProperty("tags")]
        public string[] Tags { get; set; }

        [Newtonsoft.Json.JsonProperty("search")]
        public string Search { get; set; }


        [Newtonsoft.Json.JsonProperty("pageNum")]
        public int PageNum { get; set; }

        [Newtonsoft.Json.JsonProperty("pageSize")]
        public int PageSize { get; set; }

        


        public string GenernateQueryParameter()
        {
            var parameters = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(this.Region) == false)
                parameters.Add("region", HttpUtility.UrlEncode(this.Region));

            if (this.Tags != null && this.Tags.Length > 0)
                parameters.Add("tags", HttpUtility.UrlEncode(string.Join(",", this.Tags)));

            if (string.IsNullOrEmpty(this.Search) == false)
                parameters.Add("query", this.Search);

            parameters.Add("output", "json");
            parameters.Add("ak", BaiduConstant.AK);
            parameters.Add("page_size", 20.ToString());
            parameters.Add("page_num", this.PageNum.ToString());
            return string.Concat("?", string.Join("&", parameters.Select((ctx) =>
            {
                return $"{ctx.Key}={ctx.Value}";
            })));
        }
    }
}
