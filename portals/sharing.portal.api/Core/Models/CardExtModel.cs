using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharing.Portal.Api.Models
{
    using Sharing.Core;
    public class CardExtModel 
    {
        //    [JsonProperty("code")]
        //    public string Code { get; set; }

        //[JsonProperty("openid")]
        //public string OpenId { get; set; }

        [JsonProperty("timestamp")]
        public string TimeStamp { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("nonceStr", NullValueHandling = NullValueHandling.Ignore)]
        public string NonceStr { get; set; }

        //[JsonProperty("outer_str")]
        //public string OuterStr { get; set; }

        //[JsonProperty("cardId")]
        //public string CardId { get; set; }


    }
}
