


namespace Sharing.WeChat.Models
{
    using Sharing.Core.Models;
    using Newtonsoft.Json;

    public class QueryWxUserCardResponse : WeChatResponse
    {

        [JsonProperty("card_list")]
        public MCardKey[] UserMCardKeys { get; set; }

    }
}
