

namespace Sharing.Core.Models
{
    using Newtonsoft.Json;
    public class OrderContext : IMchId, IWxUserKey
    {
        [JsonProperty("delivery")] public DeliveryTypes Delivery { get; set; }

        [JsonProperty("appid")] public string AppId { get; set; }

		[JsonProperty("mchid")]
        public long MerchantId { get; set; }

        [JsonProperty("totalMoney")]
        public int Money { get; set; }

       
        /// <summary>
        /// 派送费
        /// </summary>
        [JsonProperty("fare")]
        public string Fare { get; set; }

        [JsonProperty("openid")]
        public string OpenId { get; set; }

        [JsonProperty("addrdetail")]
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
