

namespace Sharing.Portal.Api.Models
{
    using Sharing.Core;
    using Newtonsoft.Json;
    public class MCardModel:IWxMCardId
    {
        [JsonProperty("title")]
        public virtual string Title { get; set; }

        [JsonProperty("logoUrl")]
        public virtual string LogoUrl{ get; set; }

        
        public virtual string CardId { get; set; }

        [JsonProperty("brandName")]
        public virtual string BrandName { get; set; }

        [JsonProperty("privilege")]
        public virtual string Prerogative { get; set; }     
    }
}
