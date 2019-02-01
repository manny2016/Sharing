
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

        [JsonIgnore]
        public virtual float Money { get; set; }
        [JsonProperty("money")]
        public virtual string MoneyString
        {
            get
            {
                return this.Money.ToString("0.00");
            }
        }
        [JsonProperty("prerogative")]
        public virtual string Prerogative { get; set; }

        [JsonProperty("address")]
        public virtual string Address { get; set; }

        
        [JsonIgnore]
        public virtual float RewardMoney { get; set; }

        [JsonProperty("rewardMoney")]
        public virtual string RewardMoneyString
        {
            get
            {
                return this.RewardMoney.ToString("0.00");
            }
        }

    }
}
