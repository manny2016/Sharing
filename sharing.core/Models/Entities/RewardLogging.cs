using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.Entities
{
    /// <summary>
    /// 鼓励金获取历史记录
    /// </summary>
    public class RewardLogging
    {
        public  long Id { get; set; }

        public  long WxUserId { get; set; }

        public  int RewardMoney { get; set; }

        public  long RelevantTradeId { get; set; }

        public  int RewardIntegral { get; set; }

        public  RewardStates State { get; set; }

        public  long CreatedDateTime { get; set; }

        public  long LastUpdatedDateTime { get; set; }
    }
}
