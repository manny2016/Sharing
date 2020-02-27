

namespace Sharing.Agent.Synchronizer {
	using System;
	using System.Collections.Generic;
	using System.Text;
	using Microsoft.Extensions.DependencyInjection;
	using Newtonsoft.Json.Linq;
	using Sharing.Agent.Synchronizer.Models;
	using Sharing.Agent.Synchronizer.Services;
	using Sharing.Core;
	using Sharing.Core.Models;
	using Sharing.WeChat.Models;
	using System.Linq;
	public static class ServiceExtension {

		public static IServiceCollection AddWxChatOfficialSyncService(this IServiceCollection collection) {
			var appfilters = new AppTypes[] { AppTypes.Official };
			var apps = "https://www.yourc.club/api/sharing/QueryMerchantDetails"
				.GetUriJsonContent<JObject>().TryGetValues<MerchantDetails>("$.data")
				.SelectMany((merchant) => {
					return merchant.Apps.Where(app => appfilters.Any(x => x == app.AppType))
					.Select(x => new {
						App = x,
						MerchantId = merchant.Id,
						MerchantName = merchant.BrandName
					});
				});
			foreach ( var app in apps ) {
				var state = new WeChatUserSyncState(new WeChatUserSyncSettings() {
					AppId = app.App.AppId,
					AppType = app.App.AppType,
					Secret = app.App.Secret,
					MerchantId = app.MerchantId,
					BrandName = app.MerchantName
				});
				collection.Add(new ServiceDescriptor(typeof(IASfPWorkItem),
					new WorkItemWithDataflow<WeChatUserSyncState, WeChatUserInfo>(state)));
			}
			return collection;
		}
		public static IServiceCollection AddRewardMoneyGrantService(this IServiceCollection collection) {
			var appfilters = new AppTypes[] { AppTypes.Official };
			var apps = "https://www.yourc.club/api/sharing/QueryMerchantDetails"
				.GetUriJsonContent<JObject>().TryGetValues<MerchantDetails>("$.data")
				.SelectMany((merchant) => {
					return merchant.Apps.Where(app => appfilters.Any(x => x == app.AppType))
					.Select(x => new {
						App = x,
						MerchantId = merchant.Id,
						MerchantName = merchant.BrandName
					});
				});
			foreach ( var app in apps ) {
				var state = new RewardMoneyGrantState(new RewardMoneyGrantSetting() {
					WxApp = app.App,
					BrandName = app.MerchantName
				});

				collection.Add(new ServiceDescriptor(typeof(IASfPWorkItem),
					new WorkItemWithDataflow<RewardMoneyGrantState, RewardLogging>(state)));
			}
			return collection;
		}
	}
}
