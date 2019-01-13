

namespace Sharing.Portal.Api.Models
{
    using Sharing.Core;
    using Newtonsoft.Json;
    public class RegisterMCardContext : IWxApp, IWxUserOpenId
    {
        public string AppId { get; set; }

        public string OpenId { get; set; }


        [JsonProperty("cardList")]
        public WxCard[] CardList { get; set; }


        [JsonProperty("encryptedData")]
        public string EncryptedData { get; set; }

        public string Secret { get; set; }

        public AppTypes AppType { get; set; }
    }
}
