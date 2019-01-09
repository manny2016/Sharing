
namespace Sharing.Core
{

    using Newtonsoft.Json;

    /// <summary>
    /// 商户唯一标识
    /// </summary>
    public interface IMCode
    {
        [JsonProperty("mcode")]
        string MCode { get; }
    }
}
