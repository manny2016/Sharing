using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public interface IWxApp
    {
        string AppId { get; }
        string Secret { get; }
    }
}
