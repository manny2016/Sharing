using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    using Newtonsoft.Json;
  
   
    public interface IWxUserOpenId
    {
        [JsonProperty("openid")]
        string OpenId { get; }
    }
   
}
