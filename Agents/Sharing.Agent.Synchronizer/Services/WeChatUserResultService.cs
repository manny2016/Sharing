
namespace Sharing.Agent.Synchronizer.Services {
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using Microsoft.SqlServer.Server;
	using Sharing.Agent.Synchronizer.Models;
	using Sharing.Core;
	using Sharing.WeChat.Models;
	using System.Linq;
	using Sharing.Core.Models;
	using System.Threading;
	public class WeChatUserResultService : IProcessingResultService<WeChatUserInfo> {
		public void Dispose() {

		}
		private readonly WeChatUserSyncSettings settings;
		private static AutoResetEvent signal = new AutoResetEvent(true);
		public WeChatUserResultService(WeChatUserSyncSettings settings) {
			this.settings = settings;
		}
		public void Save(IEnumerable<WeChatUserInfo> results) {
			if ( results == null || results.Count() == 0 ) {
				return;
			}
			signal.WaitOne();
			using ( var database = new DatabaseFactory(AppHost.Host.Configuration).GenerateDatabase() ) {
				var queryString = @"[dbo].[spRegisterWeChatUser]";
				var parameters = new IDbDataParameter[] {
					new SqlParameter() {
						SqlDbType = SqlDbType.Structured,
						TypeName = "[dbo].[RegisterWeChatUserStructure]",
						Value = results.Select(x=>Unity.Convert(new Core.Models.RegisterWxUserContext(){
							 AppType = this.settings.AppType,
							 Info = x,
							 LastUpdateBy = "Synchronizer",
							 MerchantId = this.settings.MerchantId,
						 	 WxApp = new WxApp(){ AppId = this.settings.AppId, Secret = this.settings.Secret,AppType =this.settings.AppType }
						})).ToArray() ,
						ParameterName = "@wxuser"
					}
				};
				database.Execute(queryString, parameters, System.Data.CommandType.StoredProcedure);
			}
			signal.Set();
		}
	}
}
