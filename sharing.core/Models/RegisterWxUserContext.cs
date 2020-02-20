

namespace Sharing.Core.Models {
	using System;
	using Sharing.WeChat.Models;
	public class RegisterWxUserContext {
		//public string InvitedBy { get; set; }
		public IWxApp WxApp { get; set; }
		public AppTypes AppType { get; set; }
		public WeChatUserInfo Info { get; set; }

		public Guid? ScenarioId { get; set; }
		public long MerchantId { get; set; }
		public string LastUpdateBy { get; set; }
		public string SharedBy { get; set; }
	}
}
