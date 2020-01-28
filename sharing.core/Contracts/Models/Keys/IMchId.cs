using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    using Newtonsoft.Json;
    public interface IMchId
    {
        /// <summary>
        /// 商户Id
        /// </summary>
        [JsonProperty("mchid")]
        long MerchantId { get; }
    }
    public interface ISharingUserId
    {
        [JsonProperty("userid")]
        long Id { get; }
    }
}
