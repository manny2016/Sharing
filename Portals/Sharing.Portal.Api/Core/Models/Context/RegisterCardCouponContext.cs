

namespace Sharing.Portal.Api.Models
{
    using Sharing.Core;
    using Newtonsoft.Json;
    public class RegisterCardCouponContext : IMCode, IWxUserOpenId, IWxAppId, IWxUnionId
    {
        public string MCode { get; set; }

        public string OpenId { get; set; }


        [JsonProperty("cardList")]
        public WxCard[] CardList { get; set; }

        public string AppId { get; set; }
        public string UnionId { get; set; }
    }
}
