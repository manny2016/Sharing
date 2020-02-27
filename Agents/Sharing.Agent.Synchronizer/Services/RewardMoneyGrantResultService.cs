

namespace Sharing.Agent.Synchronizer.Services {
	using System;
	using System.Collections.Generic;
	using System.Text;
	using Sharing.Agent.Synchronizer.Models;
	using Sharing.Core;
	using Sharing.Core.Models;
	using System.Linq;
	using Microsoft.Extensions.Configuration;

	public class RewardMoneyGrantResultService : IProcessingResultService<RewardLogging> {
		private readonly RewardMoneyGrantSetting Settings;
		private readonly IDatabaseFactory databaseFactory;
		private readonly IConfiguration configuration;
		public RewardMoneyGrantResultService(RewardMoneyGrantSetting settings) {
			this.Settings = settings;
			this.databaseFactory = AppHost.Host.Services.GetService(typeof(IDatabaseFactory)) as IDatabaseFactory;

		}
		public void Dispose() {

		}
		public void Save(IEnumerable<RewardLogging> results) {
			if ( results == null || results.Count() == 0 ) {
				return;
			}
			var queryString = @"
MERGE INTO [dbo].[RewardLogging] [target]
USING (
	VALUES
	{0}
)[source]([Id],[State],[LastUpdatedBy],[ErrorMessage])
ON [target].[Id] = [source].[Id]
WHEN MATCHED THEN UPDATE SET
	[target].[State] = [source].[State],
	[target].[LastUpdatedBy] =[source].[LastUpdatedBy],
	[target].[ErrorMessage] = [source].[ErrorMessage],
	[target].[LastUpdatedDateTime] = DATEDIFF(S,'1970-01-01',SYSUTCDATETIME());
";
			using ( var database = databaseFactory.GenerateDatabase() ) {
				var values = string.Join(",", results.Select((ctx) => {
					return $"({ctx.Id},{(int)ctx.State},'Sharing.Agent.Synchronizer','{ctx.ErrorMessage}')";
				}));
				database.Execute(string.Format(queryString, values), null, System.Data.CommandType.Text);
			}
		}
	}
}
