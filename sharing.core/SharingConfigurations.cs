

namespace Sharing.Core {
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.SqlServer.Server;
	using Sharing.Core.Configuration;
	using System;
	using System.Linq;

	public static class SharingConfigurations {
		static SharingConfigurations() {

		}

		public const string DefaultDatabase = "sharing-dev";

		public static IDatabase GenerateDatabase(string database = DefaultDatabase, bool isWriteOnly = true,
			Microsoft.Extensions.Configuration.IConfiguration configuration = null) {
			if ( configuration == null ) {
				configuration = IoC.GetService<Microsoft.Extensions.Configuration.IConfiguration>();
			}

			var section = configuration.GetSection(DbConfiguration.SectionName).Get<DbConfiguration>();
			if ( isWriteOnly ) {
				return section.Master.GenerateDatabase(section.Database);
				
			} else {
				var idx = 0;
				if ( section.Slaves.Length > 0 ) {
					idx = DateTime.UtcNow.Second % section.Slaves.Length;
				}
				return section.Slaves[idx].GenerateDatabase(section.Database);
			}
		}
	}

	public static class BaiduConstant {
		public const string AK = "xW5IbKeE3pXy8unP7hwmFdbAnFgPgNtc";
		public const string SearchAPI = "http://api.map.baidu.com/place/v2/search";
	}


	public static class WeChatConstant {
		public const string WxBizMsgToken = "81FF58BA46784314860B59E0834562B5";
		public const string EncodingAESKey = "mkxeUAVtj2aaopbOsfLm8wJQ7SLnHMV6U41lmCQs95c";
		public const int SMSAppId = 1400108197;
		public const string SMSAppKey = "1add466d18076fcbb47e0e3b28e55490";

		public const string CloudAPI_SecrectId = "AKIDRju17pcex48CFD9YoX2UkpiNn7lHwVnw";
		public const string CloudAPI_SecretKey = "A9DVbFi05yQnJdVv8IMBpZDsMft1xy75";
		public const string CloudAPI_APPID = "1256489221";
		public const string CloudAPI_CMQ__TOPIC_ENDPOINT = "https://cmq-topic-cd.api.qcloud.com";
		public const string CloudAPI_CMQ_QUEUE_ENDPOINT = "https://cmq-queue-cd.api.qcloud.com";
		public const int AllowSharedPyramidLevel = 2;
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
			new SqlMetaData("ScenarioId", System.Data.SqlDbType.UniqueIdentifier)
		};
	}
}
