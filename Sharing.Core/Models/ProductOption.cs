

namespace Sharing.Core.Models
{
	using System.Collections.Generic;
	using Newtonsoft.Json;
    public class Specification
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("options")]
        public SpecificationSettings[] Options { get; set; }

        [JsonProperty("current")]
        public int Selected { get; set; }
    }
    public class ProductSettings
    {
        [JsonProperty("banners")]
        public string[] Banners { get; set; }

        /// <summary>
        /// 产品规格选项
        /// </summary>
        [JsonProperty("specifications")]
        public List<Specification> Specifications { get; set; }
    }

    public class SpecificationSettings
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

		[JsonProperty("isDefault")]
		public bool IsDefault {
			get;set;
		}
        
        
    }
}
