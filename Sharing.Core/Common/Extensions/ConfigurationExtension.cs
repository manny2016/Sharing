

namespace Sharing.Core {
	using System;
	using Microsoft.Extensions.Configuration;
	using Sharing.Core.Configuration;

	public static class ConfigurationExtension {
		public static WeChatConstant GetWeChatConstant(this IConfiguration configuration) {
			return configuration.GetSection(WeChatConstant.SectionName)
				.Get<WeChatConstant>();
		}
		public static DbConfiguration GetDbConfiguration(this IConfiguration configuration) {
			return configuration.GetSection(DbConfiguration.SectionName)
				.Get<DbConfiguration>();
		}
		public static RewardSettings GetRewardSettings(this IConfiguration configuration) {
			return configuration.GetSection(RewardSettings.SectionName)
				.Get<RewardSettings>();
		}
	}
}
