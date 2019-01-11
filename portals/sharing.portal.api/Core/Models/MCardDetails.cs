
namespace Sharing.Portal.Api.Models
{
    using Newtonsoft.Json;
    using Sharing.Core;
    public class MCardDetails : IWxMCardId, IWxCardCode
    {
        [JsonProperty("brandName")]
        public virtual string BrandName { get; set; }

        [JsonProperty("title")]
        public virtual string Title { get; set; }

        [JsonProperty("ready")]
        public virtual bool Ready { get; set; }

        
        public virtual string CardId { get; set; }

        
        public virtual string UserCode { get; set; }

        [JsonProperty("money")]
        public virtual string Money { get; set; }

        [JsonProperty("prerogative")]
        public virtual string Prerogative { get; set; }

        [JsonProperty("address")]
        public virtual string Address { get; set; }

        [JsonProperty("rewardMoney")]
        public virtual string RewardMoney { get; set; }
    }
}
