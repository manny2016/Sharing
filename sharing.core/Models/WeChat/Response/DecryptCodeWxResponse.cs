using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.WeChat.Models
{
    using Newtonsoft.Json;
    public class DecryptCodeWxResponse : WeChatResponse
    {

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
