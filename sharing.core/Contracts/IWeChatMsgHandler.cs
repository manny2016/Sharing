



namespace Sharing.Core
{
    using Sharing.WeChat.Models;
    using System.Linq;

    public interface IWeChatMsgHandler
    {
        void Proccess(IWeChatMsgToken token, string appid);
    }
}
