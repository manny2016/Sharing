

namespace Sharing.Core
{
    using Microsoft.Extensions.DependencyInjection;
    using Sharing.Core.Configuration;
    using System;
    public static class SharingConfigurations
    {
        static SharingConfigurations()
        {

        }
        private static IServiceProvider provider;
        public const string DefaultDatabase = "sharing-uat";
        public static IDatabase GenerateDatabase(string database, bool isWrite)
        {
            return GenerateDatabase(isWrite, database);
        }
        public static IDatabase GenerateDatabase(bool isWrite, string database = DefaultDatabase)
        {
            var section = DberverConfigurationSection.GetInstance();
            if (isWrite)
            {
                return section.MasterDatabaseServer.GenerateDatabase(database);
            }
            else
            {
                var idx = 0;
                if (section.SlaveDatabaseServers.Count > 0)
                {
                    idx = DateTime.Now.Second % section.SlaveDatabaseServers.Count;
                }
                return section.SlaveDatabaseServers[idx].GenerateDatabase(database);
            }
        }
        public static IServiceCollection CreateServiceCollection(Action<IServiceCollection> registry = null)
        {
            var collection = new ServiceCollection();
            collection.AddMemoryCache().AddWeChatApiService();
            if (registry != null)
            {
                registry(collection);
            }
            return collection;
        }


        public static class WeChatConstant
        {
            public const string WxBizMsgToken = "81FF58BA46784314860B59E0834562B5";
            public const string EncodingAESKey = "mkxeUAVtj2aaopbOsfLm8wJQ7SLnHMV6U41lmCQs95c";
            public const int SMSAppId = 1400108197;
            public const string SMSAppKey = "1add466d18076fcbb47e0e3b28e55490";
        }
    }
}
