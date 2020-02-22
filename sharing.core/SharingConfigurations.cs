

namespace Sharing.Core {
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.SqlServer.Server;
	using Sharing.Core.Configuration;
	using System;
	using System.Linq;

	//public static class SharingConfigurations {
	//	static SharingConfigurations() {

	//	}

	//	public const string DefaultDatabase = "sharing-dev";
	//	public static IConfiguration Configuration { get; private set; }
		
	//	public static void Configure(IConfiguration configuration) {
	//		Configuration = configuration;
	//	}
	//}

	public static class BaiduConstant {
		public const string AK = "xW5IbKeE3pXy8unP7hwmFdbAnFgPgNtc";
		public const string SearchAPI = "http://api.map.baidu.com/place/v2/search";
	}


	public class WeChatConstant {
		public const string SectionName = "wx";
		public string WxBizMsgToken { get; set; }
		public string EncodingAESKey { get; set; }
		public int SMSAppId { get; set; }
		public string SMSAppKey { get; set; }

		public string CloudAPI_SecrectId { get; set; }
		public string CloudAPI_SecretKey { get; set; }
		public string CloudAPI_APPID { get; set; }
		public string CloudAPI_CMQ__TOPIC_ENDPOINT { get; set; }
		public string CloudAPI_CMQ_QUEUE_ENDPOINT { get; set; }
		public int AllowSharedPyramidLevel { get; set; }
	}
	public static class DAOConstants {
		public static readonly SqlMetaData[] RegisterWeChatUserStructure = new SqlMetaData[] {
			new SqlMetaData("UnionId", System.Data.SqlDbType.NVarChar,32),
			new SqlMetaData("AppId", System.Data.SqlDbType.NVarChar,32),
			new SqlMetaData("OpenId", System.Data.SqlDbType.NVarChar,32),
			new SqlMetaData("RegistrySource", System.Data.SqlDbType.Int),
			new SqlMetaData("NickName", System.Data.SqlDbType.NVarChar,50),
			new SqlMetaData("Country", System.Data.SqlDbType.NVarChar,50),
			new SqlMetaData("Province", System.Data.SqlDbType.NVarChar,50),
			new SqlMetaData("City", System.Data.SqlDbType.NVarChar,50),
			new SqlMetaData("AvatarUrl", System.Data.SqlDbType.NVarChar,200),
			new SqlMetaData("LastUpdatedBy", System.Data.SqlDbType.NVarChar,50),
			new SqlMetaData("SharedBy", System.Data.SqlDbType.NVarChar,32),
			new SqlMetaData("ScenarioId", System.Data.SqlDbType.UniqueIdentifier)
		};
	}
}
