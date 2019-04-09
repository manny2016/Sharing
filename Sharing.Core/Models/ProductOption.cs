

namespace Sharing.Core.Models
{
    using Newtonsoft.Json;
    public class Specification
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("options")]
        public SpecificationSettings[] Options { get; set; }

        [JsonProperty("current")]
        public int Default { get; set; }
    }
    public class ProductSettings
    {
        [JsonProperty("banners")]
        public string[] Banners { get; set; }

        /// <summary>
        /// 产品规格选项
        /// </summary>
        [JsonProperty("specifications")]
        public Specification[] Specifications { get; set; }
    }

    public class SpecificationSettings
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }
        
        
    }
}
