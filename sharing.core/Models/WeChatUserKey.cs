using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.Models
{
    public class WxUserKey : IWxUserKey
    {
        public long Id { get; set; }

        public string AppId { get; set; }

        public string OpenId { get; set; }

        public long MerchantId { get; set; }

        
    }
}
