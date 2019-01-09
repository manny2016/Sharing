using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.Models
{
    public class WxUserKey : IWxUserKey
    {
        
        public string AppId { get; set; }

        public string OpenId { get; set; }
    }
}
