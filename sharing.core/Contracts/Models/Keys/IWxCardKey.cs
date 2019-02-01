namespace Sharing.Core
{
    using Newtonsoft.Json;
    public interface IWxMCardId
    {
        [JsonProperty("cardid")]
        string CardId { get; }
    }
    public interface IWxCardCode
    {
        [JsonProperty("code")]
        string UserCode { get; }
    }

    public interface IWxCardKey : IWxMCardId, IWxCardCode
    {
        
    }
}