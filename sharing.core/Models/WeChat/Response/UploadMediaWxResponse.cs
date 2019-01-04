

namespace Sharing.WeChat.Models
{
    public class UploadMediaWxResponse : WeChatResponse
    {
        [Newtonsoft.Json.JsonProperty("media_id")]
        public string MediaId { get; set; }

        [Newtonsoft.Json.JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [Newtonsoft.Json.JsonProperty("value")]
        public string Value { get; set; }

        [Newtonsoft.Json.JsonProperty("url",NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Url { get; set; }
    }
}