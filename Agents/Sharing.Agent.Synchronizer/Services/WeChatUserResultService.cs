
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

	public class WeChatUserResultService : IProcessingResultService<WeChatUserInfo> {
		public void Dispose() {

		}
		private readonly WeChatUserSyncSettings settings;
		public WeChatUserResultService(WeChatUserSyncSettings settings) {
			this.settings = settings;
		}
		public void Save(IEnumerable<WeChatUserInfo> results) {
			using ( var database = new DatabaseFactory(this.settings.GetConfiguration()).GenerateDatabase() ) {
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
		}
	}
}
