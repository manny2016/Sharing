
namespace Sharing.WeChat.Models
{
    using Newtonsoft.Json;
    public class QueryWxUserListResponse
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
        
        [JsonProperty("data")]
        public WxUserListData Data { get; set; }
    }
    public class WxUserListData
    {
        [JsonProperty("openid")]
        public string[] OpenIds { get; set; }
    }
}
