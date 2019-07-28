

namespace Sharing.WeChat.Models
{
    using Newtonsoft.Json;
    public class CustomMessageContext
    {
        [JsonProperty("touser")]
        public string ToUser { get; set; }

        [JsonProperty("msgtype")]
        public string MsgType { get; set; }

        [JsonProperty("text")]
        public CustomMessageContent Text { get; set; }
    }

    public class CustomMessageContent
    {
        [JsonProperty("content")]
        public string Content { get; set; }
    }

}
