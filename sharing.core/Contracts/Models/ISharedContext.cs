﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public interface ISharedContext : IMchId
    {
        long Id { get; }

       
        long? InvitedBy { get; }
    }
}
