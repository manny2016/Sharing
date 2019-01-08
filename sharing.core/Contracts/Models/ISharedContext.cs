using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public interface ISharedContext
    {
        long Id { get; }
        string OpenId { get; }
        long? InvitedBy { get; }
    }
}
