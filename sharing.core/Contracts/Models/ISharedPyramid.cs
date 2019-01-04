using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public interface ISharedPyramid
    {
        long Id { get; }
        long? InvitedBy { get; }
    }
}
