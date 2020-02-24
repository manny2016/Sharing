using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public interface IProcessingResultService<ProcessingResult> : IDisposable
    {
        void Save(IEnumerable<ProcessingResult> results);
    }
}
