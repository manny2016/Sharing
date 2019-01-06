


namespace Sharing.Portal.Api.Models
{
    using Newtonsoft.Json;
    using Sharing.WeChat.Models;

    public class RegisterWeChatUserContext
    {
        [JsonProperty("appid")]
        public string AppId { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("iv")]
        public string IV { get; set; }

        [JsonProperty("sessionKey")]
        public string SessionKey { get; set; }

        [JsonProperty("invitedBy")]
        public string InvitedBy { get; set; }

        [JsonProperty("wx")]
        public WeChatUserInfo WxChatUser { get; set; }
    }
}
