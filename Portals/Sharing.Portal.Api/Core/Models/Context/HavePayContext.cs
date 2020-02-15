


namespace Sharing.Portal.Api.Models {
	using Sharing.Core;	
	using Newtonsoft.Json;
	public class HavePayContext {
		[JsonProperty("tradeId")]
		public string TradeId { get; set; }

		[JsonProperty("state")]
		public TradeStates State { get;set;}
	}
}
