



namespace Sharing.WeChat.Models
{
    using Newtonsoft.Json;
    public class JSCodeApiToken
    {
        [JsonProperty("appid")]
        public string AppId { get; set; }

        [JsonProperty("secret")]
        public string Secret { get; set; }

        [JsonProperty("js_code")]
        public string Code { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; }
    }
}
