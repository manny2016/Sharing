using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public interface IWeChatMsg
    {
        WeChatMsgTypes MsgType { get; }

        WeChatEventTypes Event { get; }
    }
}
