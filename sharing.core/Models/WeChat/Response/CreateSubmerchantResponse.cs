
namespace Sharing.WeChat.Models
{
    using Newtonsoft.Json;
    public class CreateSubmerchantResponse : WeChatResponse
    {
        [JsonProperty("merchant_id")]
        public int MerchantId { get; set; }

        [JsonProperty("app_id")]
        public string AppId { get; set; }

        [JsonProperty("create_time")]
        public long CreateTime { get; set; }

        [JsonProperty("update_time")]
        public long UpdateTime { get; set; }

        [JsonProperty("brand_name")]
        public string BrandName { get; set; }

        [JsonProperty("logo_url")]
        public string LogoUrl { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("begin_time")]
        public long BeginTime { get; set; }

        [JsonProperty("end_time")]
        public long EndTime { get; set; }

        [JsonProperty("primary_category_id")]
        public int PrimaryCategoryId { get; set; }

        [JsonProperty("secondary_category_id")]
        public int SecondaryCategoryId { get; set; }
        //       "info": {
        //"merchant_id": 12,
        //"app_id":"xxxxxxxxxxxxx",
        //"create_time": 1438790559,
        //"update_time": 1438790559,
        //"brand_name": "aaaaaa",
        //"logo_url": "http://mmbiz.xxxx",
        //"status": "CHECKING",
        //"begin_time": 1438790559,
        //"end_time": 1438990559,
        //"primary_category_id": 1,
        //"secondary_category_id": 101

    }
}