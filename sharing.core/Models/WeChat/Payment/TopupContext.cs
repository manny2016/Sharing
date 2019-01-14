﻿


namespace Sharing.WeChat.Models
{
    using Newtonsoft.Json;
    using Sharing.Core;
    public class TopupContext : IWxUserKey
    {
        [JsonProperty("appId")]
        public string AppId { get; set; }

        [JsonProperty("money")]
        public int Money { get; set; }

        [JsonProperty("openid")]
        public string OpenId { get; set; }

        //[JsonProperty("unionid")]
        //public string UnionId { get; set; }

        [JsonProperty("cardid")]
        public string CardId { get; set; }
        /// <summary>
        /// 会员卡用户code
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }
        /// <summary>
        /// 商户代码
        /// </summary>
        [JsonProperty("mcode")]
        public string MCode { get; set; }

        public long MchId { get; set; }

        public long Id { get; set; }
    }
}
