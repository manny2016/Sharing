

namespace Sharing.Agent.Synchronizer {
	using Sharing.Agent.Synchronizer.Models;
	using Sharing.Core;
	using Sharing.Core.Models;
	using Sharing.WeChat.Models;
	using System.Linq;
	using System.Collections.Generic;
	public static class WorkItemExtension {
		public static IASfPWorkItem[] GenernateWorkItems(this IEnumerable<MerchantDetails> details, AppTypes[] filter) {
			return details.SelectMany((x) => {
				return x.Apps.Where(app=>filter.Contains(app.AppType)).Select((app) => {					
					var settings = new WeChatUserSyncSettings() {
						AppId = app.AppId,
						AppType = app.AppType,
						MerchantId = x.Id,
						Secret = app.Secret
					};
					var state = new WeChatUserSyncState(settings);
					return new WorkItemWithDataflow<WeChatUserSyncState, WeChatUserInfo>(state);
				});
			})
			.Where(x => x != null)
			.ToArray();

		}
	}
}
