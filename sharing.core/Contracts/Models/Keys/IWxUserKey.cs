

namespace Sharing.Core
{
    using Newtonsoft.Json;
    public interface IWxUserKey
    {
        [JsonProperty("appid")]
        string AppId { get; }

        [JsonProperty("openid")]
        string OpenId { get; }
        
    }
}
