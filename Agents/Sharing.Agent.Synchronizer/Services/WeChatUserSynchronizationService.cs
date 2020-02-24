
namespace Sharing.Agent.Synchronizer.Services {
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Threading;
	using Newtonsoft.Json.Linq;
	using Sharing.Agent.Synchronizer.Models;
	using Sharing.Core;
	using Sharing.Core.Services;
	using Sharing.WeChat.Models;

	public class WeChatUserSynchronizationService : IProcessService<WeChatUserInfo> {
		private readonly WeChatUserSyncSettings settings;
		private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(WeChatUserSynchronizationService));
		public WeChatUserSynchronizationService(WeChatUserSyncSettings settings) {
			this.settings = settings;
		}
		public void Dispose() {

		}
		public void Process(Action<WeChatUserInfo> pass, CancellationToken token) {
			var loop = true;
			do {
				var result = "https://www.yourc.club/api/sharing/QueryAllWxUsers".GetUriJsonContent<JObject>((http) => {
					http.Method = "POST";
					http.ContentType = "application/json; encoding=utf-8";
					using ( var stream = http.GetRequestStream() ) {
						var data = new {
							WxApp = new {appid = this.settings.AppId, secret = this.settings.Secret }
						};
						var body = data.SerializeToJson();
						var buffers = UTF8Encoding.UTF8.GetBytes(body);
						stream.Write(buffers, 0, buffers.Length);
						stream.Flush();
					}
					return http;
				});
				var response = result.SelectToken("$.data").ToString();
				var xx = response.DeserializeToObject<QueryWxUserDetailsResponse>();
				Logger.Info($"Queried {xx.WeChatUserInfos.Length} users form WX api.");
				if ( result == null ) {
					break;
				}
				foreach(var model in xx.WeChatUserInfos ) {
					pass(model);
				}
				loop = !string.IsNullOrEmpty(xx.NextOpenId);
			}
			while ( loop );

		}
	}
}
