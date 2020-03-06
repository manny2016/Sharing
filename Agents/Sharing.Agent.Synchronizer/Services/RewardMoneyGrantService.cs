
namespace Sharing.Agent.Synchronizer.Services {
	using System;
	using System.Threading;
	using Sharing.Agent.Synchronizer.Models;
	using Sharing.Core;
	using Sharing.Core.Models;
	using Newtonsoft.Json.Linq;
	using System.Text;
	using Sharing.WeChat.Models;

	public class RewardMoneyGrantService : IProcessService<RewardLogging> {
		private RewardMoneyGrantSetting Settings { get; set; }
		private readonly IWeChatApi api;
		private readonly IRandomGenerator generator;
		private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(RewardMoneyGrantService));
		public void Dispose() {

		}
		public RewardMoneyGrantService(RewardMoneyGrantSetting settings) {
			this.Settings = settings;
			this.api = AppHost.Host.Services.GetService(typeof(IWeChatApi)) as IWeChatApi;
			this.generator = AppHost.Host.Services.GetService(typeof(IRandomGenerator)) as IRandomGenerator;
		}
		public void Process(Action<RewardLogging> pass, CancellationToken token) {
			var rewards = "https://www.yourc.club/api/sharing/GetRewardloggings"
				.GetUriJsonContent<JObject>((http) => {
					http.Method = "POST";
					http.ContentType = "application/json; encoding=utf-8";
					using ( var stream = http.GetRequestStream() ) {

						var data = new {
							State = (int)RewardStates.Waitting,
							AppId = this.Settings.WxApp.AppId
						};
						var body = data.SerializeToJson();
						var buffers = UTF8Encoding.UTF8.GetBytes(body);
						stream.Write(buffers, 0, buffers.Length);
						stream.Flush();
					}
					return http;
				})
				.TryGetValues<RewardLogging>("$.data");
			////TODO : need to impove. make sure it can be configured
			foreach ( var reward in rewards ) {
				var redpack = new Redpack(nonce_str: generator.Genernate(),
					mch_billno: string.Concat(reward.RelevantTradeId, reward.Id.ToString()),
					mch_id: "1520961881",
					wxappid: reward.AppId,
					send_name: "柠檬工坊东坡里店",
					re_openid: reward.OpenId,
					total_amount: reward.RewardMoney,
					total_num: 1,
					wishing: "感谢你的推荐,请收下这笔推广佣金!",
					client_ip: "112.44.210.5",
					act_name: "鼓励金",
					remark: "来自柠檬工坊东坡里店的推荐佣金");
				var result = this.api.SendRedpack(this.Settings.WxApp, "EA62B75D5D3941C3A632B8F18C7B3575", redpack);
				reward.ErrorMessage = string.Concat(result.ErrorCodeDescription.Value, "sendlistid", result.SendListId.Value);
				reward.State = result.ResultCode.Value.Equals("SUCCESS",StringComparison.OrdinalIgnoreCase) ? RewardStates.GrantSuccessfully : RewardStates.GrantFailed;
				if ( reward.State == RewardStates.GrantSuccessfully ) {
					Logger.Info($"Grant successfully.{reward.NickName},{reward.RewardMoney / 100}");
				} else {
					Logger.Warn($"Grant failed.{reward.NickName},{reward.RewardMoney / 100}");
				}
				pass(reward);
			}
		}
	}
}

