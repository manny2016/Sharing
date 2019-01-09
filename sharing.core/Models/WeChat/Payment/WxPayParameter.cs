

namespace Sharing.WeChat.Models
{
    using Sharing.Core;
    using Newtonsoft.Json;
    using System;
    using Microsoft.Extensions.DependencyInjection;
    public class WxPayParameter
    {
        //private IServiceProvider provider = SharingConfigurations.CreateServiceCollection().BuildServiceProvider();
        private readonly IRandomGenerator generator;
        public WxPayParameter(IRandomGenerator generator)
        {
            this.generator=generator;
        }
        public WxPayParameter(WeChatUnifiedorderResponse response)
        {
            this.ReturnCode = response.ReturnCode.Value;
            this.ReturnMsg = response.ReturnMsg.Value;
            //this.MchId = response.MchId.Value;
            this.NonceStr = response.NonceStr.Value;
            this.Package = string.Format("prepay_id={0}", response.PrepayId.Value);
            this.AppId = response.AppId.Value;
            this.NonceStr = this.generator.Genernate();
            this.SignType = WxPayData.SIGN_TYPE_HMAC_SHA256;
            this.TimeStamp = DateTime.Now.ToUnixStampDateTime();

        }


        //[JsonProperty("return_code")]
        public string ReturnCode
        {
            get; set;
        }


        //[JsonProperty("return_msg")]
        public string ReturnMsg
        {
            get; set;
        }


        [JsonProperty("appId")]
        public string AppId
        {
            get; set;
        }



        [JsonProperty("nonceStr")]
        public string NonceStr
        {
            get; set;
        }



        [JsonProperty("paySign")]
        public string PaySign
        {
            get; set;
        }


        [JsonProperty("package")]
        public string Package
        {
            get; set;
        }

        [JsonProperty("signType")]
        public string SignType
        {
            get; set;
        }


        [JsonProperty("timeStamp")]
        public long TimeStamp { get; set; }
        //        <xml>
        //   <return_code><![CDATA[SUCCESS]]></return_code>
        //   <return_msg><![CDATA[OK]]></return_msg>
        //   <appid><![CDATA[wx2421b1c4370ec43b]]></appid>
        //   <mch_id><![CDATA[10000100]]></mch_id>
        //   <nonce_str><![CDATA[IITRi8Iabbblz1Jc]]></nonce_str>
        //   <sign><![CDATA[7921E432F65EB8ED0CE9755F0E86D72F]]></sign>
        //   <result_code><![CDATA[SUCCESS]]></result_code>
        //   <prepay_id><![CDATA[wx201411101639507cbf6ffd8b0779950874]]></prepay_id>
        //   <trade_type><![CDATA[JSAPI]]></trade_type>
        //</xml>
    }
}
