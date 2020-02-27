

namespace Sharing.Agent.Synchronizer.Models {
	using Sharing.Agent.Synchronizer.Services;
	using Sharing.Core;
	using Sharing.WeChat.Models;
	public class WeChatUserSyncState : ProcessState<WeChatUserInfo> {
		
		public WeChatUserSyncState(WeChatUserSyncSettings setting) : base(setting) {
			
		}
		public override string Name {
			get {
				return "WeChat users synchronization.";
			}
		}
		public override IProcessingResultService<WeChatUserInfo> GenerateProcessingResultService() {
			return new WeChatUserResultService(this.Setting as WeChatUserSyncSettings);
		}
	}
}
