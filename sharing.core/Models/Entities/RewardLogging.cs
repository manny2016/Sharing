using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.Entities
{
    public class RewardLogging
    {
        public virtual long Id { get; set; }
        public virtual long WxUserId { get; set; }
        public virtual int RewardMoney { get; set; }
        public virtual long RelevantTradeId { get; set; }
        public virtual RewardStates State { get; set; }
        public virtual long CreatedTime { get; set; }

        public virtual long LastUpdatedTime { get; set; }
    }
}
