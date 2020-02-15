using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.Models {
	public class Membership {
		public long Id { get; set; }
		public string AppId { get; set; }
		public string OpenId { get; set; }
		public string UnionId { get; set; }
		public string Mobile { get; set; }
		public long MerchantId { get; set; }
		public int RewardMoney { get; set; }
	}
}
