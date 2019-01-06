

namespace Sharing.WeChat.Models
{
    using Newtonsoft.Json;
    public class SessionWxResponse : WeChatResponse
    {

        [Newtonsoft.Json.JsonProperty("session_key")]
        public string SessionKey { get; set; }


        [Newtonsoft.Json.JsonProperty("openid")]
        public string OpenId { get; set; }


        [Newtonsoft.Json.JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
