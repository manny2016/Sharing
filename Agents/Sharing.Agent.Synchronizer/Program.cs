


namespace Sharing.Agent.Synchronizer {

	using Sharing.Core;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;
	using Sharing.Core.Services;
	using Sharing.WeChat.Models;

	class Program {

		static void Main(string[] args) {

			var xx = Sharing.Agent.Synchronizer.Strings.RewardSuccessfully.DeserializeFromXml<RedpackResponse>();
			var bb = Strings.RewardFailed.DeserializeFromXml<RedpackResponse>();
			AppHost.CreateDefaultBuilder(args)
				.ConfigureServices((collection) => {
					collection.AddRandomGenerator();
					collection.AddWeChatApiService();
					collection.AddRewardMoneyGrantService();
					collection.AddDatabaseFactory();
					collection.AddWxChatOfficialSyncService();
					collection.AddMemoryCache();
					collection.AddLogging((cfg) => {
						cfg.AddConsole();
						cfg.AddLog4Net();
					});
				})
				.ConfigureAppConfiguration((context, builder) => { })
				.UseStartUp<StartUp>()
				.Build()
				.Start();

		}
	}
}
