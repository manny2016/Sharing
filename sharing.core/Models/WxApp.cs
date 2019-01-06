using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.Models
{
    public class WxApp : IWxApp
    {
        public string AppId { get; set; }
        public string Secret { get; set; }
    }
}
