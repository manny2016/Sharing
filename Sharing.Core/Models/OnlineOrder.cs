

namespace Sharing.Core.Models
{
    using Newtonsoft.Json;
    using System;

    public class OnlineOrder : ICMQMessageHandle
    {
        /// <summary>
        /// 消息句柄，用于删除消息发送时不填
        /// </summary>

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tradeId")]
        public string TradeId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("code")]
        public string TradeCode { get; set; }

        [JsonProperty("items")]
        public OnlineOrderItem[] Items { get; set; }

        [JsonProperty("total")]
        public decimal? Total { get; set; }

        [JsonProperty("receiptHandle", NullValueHandling = NullValueHandling.Ignore)]
        public string ReceiptHandle { get; set; }

        /// <summary>
        /// 派送费
        /// </summary>
        [JsonProperty("fare")]
        public decimal? Fare { get; set; }

        [JsonProperty("state")]
        public TradeStates State
        {
            get; set;
        }
        [JsonProperty("delivery")]
        public DeliveryTypes Delivery { get; set; }

        /// <summary>
        /// 订单创建时间
        /// </summary>
        [JsonProperty("createdTime")]
        public DateTime? CreatedTime { get; set; }
    }
    public class OnlineOrderItem
    {
        [JsonProperty("product")]
        public string Product { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("option")]
        public string Option { get; set; }

        [JsonProperty("money")]
        public decimal Money { get; set; }
    }

}
