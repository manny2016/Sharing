using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public interface IASfPWorkItem
    {
        object WorkItemState { get; }

        void Execute();

        void Abort();
    }
}
