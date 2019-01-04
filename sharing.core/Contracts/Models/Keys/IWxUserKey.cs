using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public interface IWxUserKey
    {
        string AppId { get; }
        string OpenId { get; }
    }
}
