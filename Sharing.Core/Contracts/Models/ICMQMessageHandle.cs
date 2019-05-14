using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public interface ICMQMessageHandle
    {
        
        string ReceiptHandle { get; set; }
        
    }
}
