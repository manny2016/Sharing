

namespace Sharing.WeChat.Models
{
    using Newtonsoft.Json;
    public class CreateCouponWxResponse : WeChatResponse
    {
        [JsonProperty("card_id")]
        public string CardId { get; set; }
    }
}