
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
        public TradeStates[] States { get; set; }
        public TradeStates[] ExcludeStates { get; set; }
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
            if (this.States != null && this.States.Length > 0)
            {
                var includeStates = new List<string>();
                foreach(var state in this.States)
                {
                    includeStates.Add($" TradeState & { (int)state}={(int)state}");                    
                }
                subcase.Add($"({string.Join(" OR ", includeStates) })");
            }
            if (this.ExcludeStates != null && this.ExcludeStates.Length > 0)
            {
                var excludeStates = new List<string>();
                foreach (var state in this.ExcludeStates)
                {
                    excludeStates.Add($" TradeState & { (int)state}=0");
                }
                subcase.Add($"({string.Join(" OR ", excludeStates) })");
            }
            return string.Join(" AND ", subcase);
        }
    }
}
