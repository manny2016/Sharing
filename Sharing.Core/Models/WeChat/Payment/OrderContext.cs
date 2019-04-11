

namespace Sharing.Core.Models
{
    using Newtonsoft.Json;
    public class OrderContext : IMchId, IWxUserKey
    {
        [JsonProperty("appid")]
        public string AppId { get; set; }

        public long MchId { get; set; }

        [JsonProperty("totalMoney")]
        public string Money { get; set; }

        [JsonIgnore]
        public int? Totalfee
        {
            get
            {
                if (int.TryParse(this.Money, out int result))
                {
                    return result * 100;
                }
                return null;
            }
        }
        [JsonProperty("openid")]
        public string OpenId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("tel")]
        public string Tel { get; set; }

        [JsonProperty("details")]
        public OrderDetail[] Details
        {
            get; set;
        }

        [JsonProperty("id")]

        public long Id { get; set; }
        [JsonProperty("remarks")]
        public string Remarks { get; set; }
    }
    public class OrderDetail
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("option")]
        public string Option { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }
    }
}
