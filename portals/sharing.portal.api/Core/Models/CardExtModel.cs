using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharing.Portal.Api.Models
{
    public class CardExtModel
    {

        [JsonProperty("timestamp")]
        public string TimeStamp { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("nonce_str")]
        public string NonceStr { get; set; }

        [JsonProperty("outer_str")]
        public string OuterStr { get; set; }
    }
}
