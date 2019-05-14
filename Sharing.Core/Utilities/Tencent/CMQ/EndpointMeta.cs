using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.CMQ
{
    public class EndpointMeta
    {
        public EndpointMeta(string topic, string queue)
        {
            this.Topic = topic;
            this.Queue = queue;
        }
        public string Topic { get; private set; }
        public string Queue { get; private set; }
    }
}
