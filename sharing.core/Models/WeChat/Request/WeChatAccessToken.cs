using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.WeChat.Models
{
    public class WeChatAccessToken
    {
        [Newtonsoft.Json.JsonProperty("access_token")]
        public string Token { get; set; }
        [Newtonsoft.Json.JsonProperty("expires_in")]
        public int Expiresin { get; set; }
    }
}
