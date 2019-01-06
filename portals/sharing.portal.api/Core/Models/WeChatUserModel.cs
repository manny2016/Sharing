

namespace Sharing.Portal.Api.Models
{
    using Newtonsoft.Json;
    public class WeChatUserModel
    {

        [JsonProperty("openid")]
        public string OpenId { get; set; }

        [JsonProperty("unionid")]
        public string UnionId { get; set; }

        [JsonProperty("hasMobile")]
        public bool HasMobile
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.Mobile);
            }
        }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }
    }
}
