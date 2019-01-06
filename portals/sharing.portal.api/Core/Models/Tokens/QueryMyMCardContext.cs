

namespace Sharing.Portal.Api.Models
{
    using Newtonsoft.Json;
    public class QueryMyMCardContext
    {
        [JsonProperty("appid")]
        public string AppID { get; set; }


        [JsonProperty("openid")]
        public string OpenId { get; set; }

        [JsonProperty("cardId")]
        public string CardId { get; set; }
    }
}
