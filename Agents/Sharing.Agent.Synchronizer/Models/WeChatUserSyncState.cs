

namespace Sharing.Agent.Synchronizer.Models {
	using Sharing.Agent.Synchronizer.Services;
	using Sharing.Core;
	using Sharing.WeChat.Models;
	public class WeChatUserSyncState : ProcessState<WeChatUserInfo> {
		
		public WeChatUserSyncState(IProcessSetting<WeChatUserInfo> setting) : base(setting) {
			
		}
		public override string Name {
			get {
				return "微信用户同步";
			}
		}
		public override IProcessingResultService<WeChatUserInfo> GenerateProcessingResultService() {
			return new WeChatUserResultService(this.Setting as WeChatUserSyncSettings);
		}
	}
}
