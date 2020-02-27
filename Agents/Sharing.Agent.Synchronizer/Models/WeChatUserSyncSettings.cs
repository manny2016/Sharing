


namespace Sharing.Agent.Synchronizer.Models {
	using System.Collections.Generic;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Configuration.Json;
	using Sharing.Agent.Synchronizer.Services;
	using Sharing.Core;
	using Sharing.WeChat.Models;

	public class WeChatUserSyncSettings : IProcessSetting<WeChatUserInfo> {

		public string AppId { get; set; }
		public string Secret { get; set; }
		public AppTypes AppType { get; set; }
		public long MerchantId { get; set; }
		public string BrandName { get; set; }
		public IProcessService<WeChatUserInfo> GenerateProcessService() {
			return new WeChatUserSynchronizationService(this);
		}
	}
}
