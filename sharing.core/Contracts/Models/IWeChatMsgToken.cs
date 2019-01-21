

namespace Sharing.Core
{
    public interface IWeChatMsgToken
    {
        string Signature { get; }
        string TimeStamp { get; }
        string BizMsgToken { get; }
        string EncodingAESKey { get; }
        string Nonce { get; }
        string ReqMsg { get; }        
        
    }
}
