
namespace Sharing.Portal.Api.Models
{
    using Sharing.Core.Models;
    using Newtonsoft.Json;
    public class SharingContext
    {
        [JsonProperty("sharedBy")]
        public WxUserKey SharedBy { get; set; }

        [JsonProperty("current")]
        public WxUserKey Current { get; set; }
    }
}
