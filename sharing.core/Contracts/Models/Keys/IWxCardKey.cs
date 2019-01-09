namespace Sharing.Core
{
    public interface IWxCardKey
    {
        long WxUserId { get; }
        string CardId { get; }
        string UserCode { get; }
    }
}