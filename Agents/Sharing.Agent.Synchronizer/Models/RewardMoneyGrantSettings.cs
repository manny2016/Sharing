


namespace Sharing.Agent.Synchronizer.Models {
	using Sharing.Agent.Synchronizer.Services;
	using Sharing.Core;
	using Sharing.Core.Models;

	public class RewardMoneyGrantSetting : IProcessSetting<RewardLogging> {
		
		public IWxApp WxApp { get;set;}
		public Payment Payment { get; set; }		
		public string BrandName { get; set; }

		public IProcessService<RewardLogging> GenerateProcessService() {
			return new RewardMoneyGrantService(this);
		}
	}
}
