

namespace Sharing.Portal.Api.Models
{
    using Sharing.Core;
    using Newtonsoft.Json;
    public class MerchantKey : IMerchantKey
    {
        [JsonProperty("mcode")]
        public string MCode { get; set; }
    }
}
