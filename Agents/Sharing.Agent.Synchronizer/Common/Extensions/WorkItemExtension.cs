

namespace Sharing.Agent.Synchronizer {
	using Sharing.Agent.Synchronizer.Models;
	using Sharing.Core;
	using Sharing.Core.Models;
	using Sharing.WeChat.Models;
	using System.Linq;
	public static class WorkItemExtension {
		public static IASfPWorkItem[] GenernateWorkItems(this MerchantDetails[] details) {
			return details.SelectMany((x) => {
				return x.Apps.Select((app) => {
					var settings = new WeChatUserSyncSettings() {
						AppId = app.AppId,
						AppType = app.AppType,
						MerchantId = x.Id,
						Secret =  app.Secret
					};
					var state = new WeChatUserSyncState(settings);
					return new WorkItemWithDataflow<WeChatUserSyncState, WeChatUserInfo>(state);
				});
			}).ToArray();
			
		}
	}
}
