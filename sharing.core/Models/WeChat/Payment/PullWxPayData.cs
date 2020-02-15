

namespace Sharing.WeChat.Models
{
    using Newtonsoft.Json;
    public class PullWxPayData
    {
        [JsonProperty("timeStamp")]
        public string timeStamp { get; set; }

        [JsonProperty("nonceStr")]
        public string nonceStr { get; set; }

        [JsonProperty("package")]
        public string package { get; set; }

        [JsonProperty("signType")]
        public string signType { get; set; }

        [JsonProperty("paySign")]
        public string paySign { get; set; }

		[JsonProperty("tradeId")]
		public string TradeId { get;set;}
		[JsonProperty("wxOrderId")]
		public string WxOrderId { get;set;}
	}
}
