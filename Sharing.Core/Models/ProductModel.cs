
namespace Sharing.Core.Models {
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;
	using System.Linq;
	public class ProductModel {
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("mchid")]
		public long MerchantId { get; set; }

		[JsonIgnore]
		public long CategoryId { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("price")]
		public int Price { get; set; }

		[JsonProperty("fixedPrice")]
		public string DisplayPrice {
			get {
				return ((float)(this.Price / 100)).ToString("0.00");
			}
		}
		[JsonProperty("salesVol")]
		public int SalesVol { get; set; }

		[JsonProperty("sortNo")]
		public int SortNo { get; set; }

		[JsonProperty("imageUrl")]
		public string ImageUrl { get; set; }

		private ProductSettings settings { get; set; }

		[JsonProperty("settings")]
		public ProductSettings ProductSettings {
			get;
			set;
		}

		private string options = "{}";
		[JsonProperty("options")]
		public string Options {
			get {
				return this.options;
			}
			set {
				this.options = value;
				this.ProductSettings = (value ?? "{}").DeserializeToObject<ProductSettings>();
				this.ProductSettings.Specifications?.ForEach((ctx) => {
					for ( var index = 0; index < ctx.Options.Length; index++ ) {
						if ( ctx.Options[index].IsDefault ) {
							ctx.Selected = index;
							break;
						}
					}
				});
			}
		}

		[JsonProperty("enabled")]
		public bool Enabled { get; set; }
	}
	public class ProductTreeNodeModel {
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

