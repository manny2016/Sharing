using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public interface IWxAppId
    {
        [JsonProperty("appid")]
        string AppId { get; }
    }
    public interface IWxAppSecret
    {
        [JsonProperty("secret")]
        string Secret { get; }
    }

    public interface IWxApp : IWxAppId, IWxAppSecret
    {
        AppTypes AppType { get; }
    }

}
