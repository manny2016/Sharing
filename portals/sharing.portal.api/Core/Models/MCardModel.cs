

namespace Sharing.Portal.Api.Models
{
    using Newtonsoft.Json;
    public class MCardModel
    {
        [JsonProperty("title")]
        public virtual string Title { get; set; }

        [JsonProperty("logoUrl")]
        public virtual string LogoUrl{ get; set; }

        [JsonProperty("cardId")]
        public virtual string CardId { get; set; }

        [JsonProperty("brandName")]
        public virtual string BrandName { get; set; }

        [JsonProperty("privilege")]
        public virtual string Prerogative { get; set; }     
    }
}
