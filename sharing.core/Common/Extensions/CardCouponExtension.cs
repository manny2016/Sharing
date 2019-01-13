

namespace Sharing.Core
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Sharing.Core.Entities;
    using System;

    public static class CardCouponExtension
    {
        /// <summary>
        /// 解析会员卡
        /// </summary>
        /// <param name="jObject"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public static MCard ParseMCard(this JObject jObject, long merchantId)
        {
            return ParseMCard(jObject, merchantId, null);
        }
        public static MCard ParseMCard(this JObject jObject, long merchantid, string cardid = null)
        {
            var mcard = new MCard() {
                MerchantId = merchantid,
                BrandName = jObject.ToObjectBasedOnCardType<string>("$.card.{0}.base_info.brand_name"),
                CardId = cardid ?? jObject.ToObjectBasedOnCardType<string>("$.card.{0}.base_info.id"),
                LogoUrl = jObject.ToObjectBasedOnCardType<string>("$.card.{0}.base_info.logo_url"),//  jObject.SelectToken("").ToObject<string>(),
                Title = jObject.ToObjectBasedOnCardType<string>("$.card.{0}.base_info.title"),
                Quantity = jObject.ToObjectBasedOnCardType<int>("$.card.{0}.base_info.sku.quantity"),
                Prerogative = jObject.ToObjectBasedOnCardType<string>("$.card.{0}.prerogative"),
                Discount = jObject.ToObjectBasedOnCardType<decimal>("$.card.{0}.discount"),
                RawData = jObject.ToString()
            };
            if (jObject.TryToObjectBasedOnCardType<int>("$.card.{0}.base_info.sku.total_quantity",out int result))
            {
                mcard.TotalQuantity = mcard.Quantity;
            }
            return mcard;            
        }
        public static T ToObjectBasedOnCardType<T>(this JObject jObject, string template)
        {
            var type = jObject.ParseCardCouponType();
            return jObject.SelectToken(string.Format(template, type.WapperName())).ToObject<T>();
        }
        public static bool TryToObjectBasedOnCardType<T>(this JObject jObject, string template, out T result)
        {
            result = default(T);
            try
            {
                result = jObject.ToObjectBasedOnCardType<T>(template);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string WapperName(this CardCouponTypes type)
        {
            var name = string.Empty;
            switch (type)
            {
                case CardCouponTypes.CASH:
                    name = "cash";
                    break;
                case CardCouponTypes.DISCOUNT:
                    name = "discount";
                    break;
                case CardCouponTypes.GENERAL_COUPON:
                    name = "general";
                    break;
                case CardCouponTypes.GIFT:
                    name = "gift";
                    break;
                case CardCouponTypes.GROUPON:
                    name = "groupon";
                    break;
                case CardCouponTypes.MEMBER_CARD:
                    name = "member_card";
                    break;
                default:
                    throw new NotSupportedException(type.ToString());
            }
            return name;
        }
        /// <summary>
        /// 解析卡券类型
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        public static CardCouponTypes ParseCardCouponType(this JObject jObject)
        {
            Guard.ArgumentNotNull(jObject.SelectToken("$.card.card_type").ToObject<string>(), "card.card_type");
            return jObject.SelectToken("$.card.card_type").ToObject<CardCouponTypes>();
        }
        public static string ParseCardId(this JObject jObject)
        {
            if(jObject.TryToObjectBasedOnCardType<string>("$.card.{0}.base_info.id", out string cardid))
            {
                return cardid;
            }
            if(jObject.TryToObjectBasedOnCardType<string>("$.card_id",out cardid))
            {
                return cardid;
            }
            return cardid;
        }

    }
}
