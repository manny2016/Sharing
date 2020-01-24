using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharing.Portal.Api.Models
{
    public class ShopOnMapEntity
    {
        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("telephone")]
        public string Phone { get; set; }

        [Newtonsoft.Json.JsonProperty("address")]
        public string Address { get; set; }
    }

    public class BaiduAPIQueryResponse
    {
        [Newtonsoft.Json.JsonProperty("results")]
        public ShopOnMapEntity[] Shops { get; set; }

        [Newtonsoft.Json.JsonProperty("status")]
        public int Status { get; set; }

        [Newtonsoft.Json.JsonProperty("message")]
        public string Message { get; set; }

        [Newtonsoft.Json.JsonProperty("total")]
        public int Total { get; set; }
    }
}
