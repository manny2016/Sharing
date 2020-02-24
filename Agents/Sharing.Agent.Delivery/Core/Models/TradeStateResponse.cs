
namespace Sharing.Agent.Delivery.Models
{
    using Sharing.Core;
    public class TradeStateResponse
    {
		public bool Success { get; set; }
		public TradeStates Data { get; set; }
		public string Message { get; set; }
	}
}
