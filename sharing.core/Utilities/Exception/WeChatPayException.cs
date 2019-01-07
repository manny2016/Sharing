using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public class WeChatPayException : Exception
    {
        public WeChatPayException(string message) : base(message)
        {
        }

    }
}
