using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.Models
{
    public class MCardKey : IWxCardKey
    {
        public string CardId { get; set; }

        public string UserCode { get; set; }

        public long? InvitedBy { get; set; }
    }
}
