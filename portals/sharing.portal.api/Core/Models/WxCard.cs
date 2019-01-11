
namespace Sharing.Portal.Api.Models
{
    using Newtonsoft.Json;
    public class WxCard
    {
        [JsonProperty("code")]
        public string EncryptedCode { get; set; }

        [JsonProperty("cardId")]
        public string CardId { get; set; }


        [JsonProperty("cardExt")]
        public string CardExt { get; set; }


        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }
    }
}
