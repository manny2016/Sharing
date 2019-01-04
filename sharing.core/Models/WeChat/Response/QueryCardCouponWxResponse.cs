using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sharing.WeChat.Models
{
    using Newtonsoft.Json;
    public class QueryCardCouponWxResponse : WeChatResponse
    {
        [JsonProperty("card_id_list")]
        public string[] CardIdList { get; set; }

        [JsonProperty("total_num")]
        public int TotalNum { get; set; }
    }
}