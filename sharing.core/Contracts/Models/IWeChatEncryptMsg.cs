using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public interface IWeChatEncryptMsg
    {
        string ToUserName { get; }
        string Encrypt { get; }
    }
}
