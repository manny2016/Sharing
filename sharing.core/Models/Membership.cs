using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.Models
{
    public class Membership
    {
        public virtual long? Id { get; set; }
        public virtual string AppId { get; set; }
        public virtual string OpenId { get; set; }
        public virtual string Mobile { get; set; }
        public virtual long? MchId { get; set; }
        public virtual int? RewardMoney { get; set; }
    }
}
