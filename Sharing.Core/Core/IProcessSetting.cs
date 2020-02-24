using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public interface IProcessSetting<ProcessingResult>
    {
        IProcessService<ProcessingResult> GenerateProcessService();
    }
}
