

namespace Sharing.Portal.Api.Models
{
    using Newtonsoft.Json;
    using Sharing.Core;
    public class WeChatUserModel:IWxUserKey
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

        [JsonProperty("rewardMoney")]
        public int RewardMoney { get; set; }


        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("appid")]
        public string AppId { get; set; }

        [JsonProperty("mchid")]
        public long MerchantId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }
}
