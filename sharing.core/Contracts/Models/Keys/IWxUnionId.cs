

namespace Sharing.Core
{
    using Newtonsoft.Json;
    public interface IWxUnionId
    {
        [JsonProperty("unionid")]
        string UnionId { get; }
    }
}
