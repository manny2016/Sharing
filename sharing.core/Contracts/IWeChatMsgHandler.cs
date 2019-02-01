



namespace Sharing.Core
{
    using Sharing.Core.Models;
    using Sharing.WeChat.Models;
    using System.Linq;

    public interface IWeChatMsgHandler
    {
        void Proccess(IWeChatMsgToken token, string appid);
    }
}
