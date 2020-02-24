using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Sharing.Core
{
    public interface IProcessService<T> : IDisposable
    {
        void Process(
            Action<T> pass,
            CancellationToken token);
    }
}
