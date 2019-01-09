

namespace Sharing.Portal.Api.Models
{
    using Sharing.Core;
    using Newtonsoft.Json;
    public class MerchantKey : IMCode
    {
        [JsonProperty("mcode")]
        public string MCode { get; set; }
    }
}
