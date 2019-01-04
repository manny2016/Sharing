

namespace Sharing.WeChat.Models
{
    public class TicketWxResponse: WeChatResponse
    {
        public TicketWxResponse() { }
        [Newtonsoft.Json.JsonProperty("ticket")]
        public string Ticket { get; set; }
        [Newtonsoft.Json.JsonProperty("expires_in")]
        public int Expiresin { get; set; }
    }
}