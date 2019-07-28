
namespace Sharing.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    public abstract class QueryFilter<T>
    {

        [JsonProperty("mchid")]
        public long MchId { get; set; }
        [JsonProperty("keys")]
        public T[] Keys { get; set; }
        [JsonProperty("start")]
        public DateTime? Start { get; set; }
        [JsonProperty("end")]
        public DateTime? End { get; set; }
        public abstract string GenernateWhereCase();
        public QueryFilter()
        {

        }
    }
    public class OnineOrderQueryFilter : QueryFilter<string>
    {
        [JsonProperty("type")]
        public TradeTypes? Type { get; set; }
        public TradeStates? State { get; set; }
        public override string GenernateWhereCase()
        {
            var subcase = new List<string>();
            subcase.Add($" WHERE MchId= {this.MchId}");
            if (this.Keys != null && this.Keys.Length > 0)
            {
                var keyscase = string.Join(",", this.Keys.Select((key) =>
                {
                    return $"'{key.Replace("'", string.Empty)}'";
                }));
                subcase.Add($"TradeId IN ({keyscase})");
            }
            if (this.Start != null)
            {
                subcase.Add($"CreatedTime>={this.Start.Value.ToUnixStampDateTime()}");
            }
            if (this.End != null)
            {
                subcase.Add($"CreatedTime<{this.End.Value.ToUnixStampDateTime()}");
            }
            if (this.Type != null)
            {
                subcase.Add($"TradeType='{this.Type.ToString()}'");
            }
            if (this.State != null)
            {
                subcase.Add($"TradeState & { (int)this.State }={(int)this.State}");
            }
            return string.Join(" AND ", subcase);
        }
    }
}
