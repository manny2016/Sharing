using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.Models
{
    public class MCardKey : IWxCardKey
    {
        public long WxUserId { get; set; }

        public string CardId { get; set; }

        public string UserCode { get; set; }
    }
}
