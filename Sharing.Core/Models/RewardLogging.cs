using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.Models {
	public class RewardLogging : IEntityWithTimestamp {
		public long Id { get; set; }
		public long WxUserId { get; set; }
		public string NickName { get; set; }
		public string RelevantTradeId { get; set; }
		public string OpenId { get; set; }
		public string AppId { get; set; }
		public int RewardMoney { get; set; }
		public int RealMoney { get; set; }
		public RewardStates State { get; set; }
		public string Description { get; set; }
		public string ErrorMessage { get; set; }
		public long CreatedDateTime { get; set; }

		public long Timestamp {
			get; set;
		}
	}
}
