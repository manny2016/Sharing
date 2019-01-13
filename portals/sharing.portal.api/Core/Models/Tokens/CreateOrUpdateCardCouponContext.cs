
namespace Sharing.Portal.Api.Models
{
    using Sharing.Core;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    public class CreateOrUpdateCardCouponContext : IMCode
    {
        public string MCode { get; set; }

        [JsonProperty("body")]
        public JObject Body { get; set; }
    }
}
