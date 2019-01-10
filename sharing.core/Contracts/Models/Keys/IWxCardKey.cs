namespace Sharing.Core
{
    using Newtonsoft.Json;
    public interface IWxMCardId
    {
        [JsonProperty("card_id")]
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