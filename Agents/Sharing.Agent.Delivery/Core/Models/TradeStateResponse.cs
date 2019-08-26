
namespace Sharing.Agent.Delivery.Models
{
    using Sharing.Core;
    public class TradeStateResponse
    {
        [Newtonsoft.Json.JsonProperty("state")]
        public TradeStates State { get; set; }
    }
}
