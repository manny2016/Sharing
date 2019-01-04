
using System.Linq;


namespace Sharing.WeChat.Models
{
    using Newtonsoft.Json;
    /// <summary>
    /// 微信开放类目
    /// </summary>
    public class ApplyProtocolWxResponse : WeChatResponse
    {
        [JsonProperty("category")]
        public PrimaryCategory[] Categories { get; set; }
        public PrimaryCategory this[int primaryid]
        {
            get
            {
                return this.Categories.FirstOrDefault(o => o.PrimaryCategoryId == primaryid)
                    ?? new PrimaryCategory() { SecondaryCategories = new SecondaryCategory[] { } };
            }
        }
        public SecondaryCategory this[int primary, int secondary]
        {
            get
            {
                var p = this[primary];
                return p.SecondaryCategories.FirstOrDefault(o => o.SecondaryCategoryId == secondary)
                    ?? new SecondaryCategory();
            }
        }
        //        {
        //   "category": [
        //       {
        //           "primary_category_id": 1,
        //           "category_name": "美食",
        //           "secondary_category": [
        //               {
        //                   "secondary_category_id": 101,
        //                   "category_name": "粤菜",
        //                   "need_qualification_stuffs": [
        //                       "food_service_license_id",
        //                       "food_service_license_bizmedia_id"
        //                   ],
        //                   "can_choose_prepaid_card": 1,
        //                   "can_choose_payment_card": 1
        //               },
        //                       }
        //   ],
        //   "errcode": 0,
        //   "errmsg": "ok"
        //}
    }
    public class PrimaryCategory
    {
        [JsonProperty("primary_category_id")]
        public int PrimaryCategoryId { get; set; }
        [JsonProperty("category_name")]
        public string CategoryName { get; set; }

        [JsonProperty("secondary_category")]
        public SecondaryCategory[] SecondaryCategories { get; set; }
    }
    public class SecondaryCategory
    {
        [JsonProperty("secondary_category_id")]
        public int SecondaryCategoryId { get; set; }

        [JsonProperty("category_name")]
        public string CategoryName { get; set; }

        [JsonProperty("need_qualification_stuffs")]
        public string[] NeedQualificationStuffs { get; set; }

        [JsonProperty("can_choose_prepaid_card")]
        public int CanChoosePrepaidCard { get; set; }

        [JsonProperty("can_choose_payment_card")]
        public int CanChoosePaymentCard { get; set; }
    }
}