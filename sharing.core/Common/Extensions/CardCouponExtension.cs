

namespace Sharing.Core
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Sharing.Core.Entities;

    public static class CardCouponExtension
    {
        public static MCard CreateMCard(this JObject jObject, long merchantId)
        {
            return new MCard()
            {
                MerchantId = merchantId,
                BrandName = jObject.SelectToken("$.card.member_card.base_info.brand_name").ToObject<string>(),
                CardId = jObject.SelectToken("$.card.member_card.base_info.id").ToObject<string>(),
                LogoUrl = jObject.SelectToken("$.card.member_card.base_info.logo_url").ToObject<string>(),
                Title = jObject.SelectToken("$.card.member_card.base_info.title").ToObject<string>(),
                Quantity = jObject.SelectToken("$.card.member_card.base_info.sku.quantity").ToObject<int>(),
                TotalQuantity = jObject.SelectToken("$.card.member_card.base_info.sku.total_quantity").ToObject<int>(),
                Prerogative = jObject.SelectToken("$.card.member_card.prerogative").ToObject<string>(),
                Discount = jObject.SelectToken("$.card.member_card.discount").ToObject<decimal>(),
                RawData = jObject.ToString()
            };
        }
    }
}
