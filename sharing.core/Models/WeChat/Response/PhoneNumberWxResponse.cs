

namespace Sharing.WeChat.Models
{
    using Newtonsoft.Json;
    public class PhoneNumberWxResponse
    {
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty("purePhoneNumber")]
        public string PurePhoneNumber { get; set; }
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
        //[JsonProperty("watermark")]
        //public Watermark Watermark { get; set; }
    }

    //    {
    //    "phoneNumber": "13580006666",  
    //    "purePhoneNumber": "13580006666", 
    //    "countryCode": "86",
    //    "watermark":
    //    {
    //        "appid":"APPID",
    //        "timestamp":TIMESTAMP
    //    }
    //}

    //---------------------

    //本文来自 多来哈米 的CSDN 博客 ，全文地址请点击：https://blog.csdn.net/hgg923/article/details/79374257?utm_source=copy 
}