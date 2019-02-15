
namespace Sharing.Core.Models
{
    using Newtonsoft.Json;
    public class ProductModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("mchid")]
        public long MchId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("fixedPrice")]
        public string DisplayPrice
        {
            get
            {
                return ((float)(this.Price / 100)).ToString("0.00");
            }
        }
        [JsonProperty("salesVol")]
        public int SalesVol { get; set; }

        [JsonProperty("sortNo")]
        public int SortNo { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
    }
    public class ProductTreeNodeModel
    {
        [JsonProperty("categoryId")]
        public long CategoryId { get; set; }
        [JsonProperty("mchid")]
        public long MchId { get; set; }
        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }

        [JsonProperty("products")]
        public ProductModel[] Products { get; set; }
    }
}

