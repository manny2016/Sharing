


namespace Sharing.WeChat.Models
{
    using Newtonsoft.Json;
    using Sharing.Core;
    public class TopupContext : IWxUserOpenId, IWxAppId, IWxCardCode, IWxMCardId, IMCode,IMchId
    {


        [JsonProperty("money")]
        public int Money { get; set; }

        public string OpenId { get; set; }

        public string AppId { get; set; }

        public string UserCode { get; set; }

        public string CardId { get; set; }        

        public string MCode { get; set; }
        public long MchId { get; set; }
    }
}
