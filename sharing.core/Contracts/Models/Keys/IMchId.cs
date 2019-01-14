using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public interface IMchId
    {
        long MchId { get; }
    }
    public interface ISharingUserId
    {
        long Id { get; }
    }
}
