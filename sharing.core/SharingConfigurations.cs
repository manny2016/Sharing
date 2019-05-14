

namespace Sharing.Core
{
    using Microsoft.Extensions.DependencyInjection;
    using Sharing.Core.Configuration;
    using Sharing.Core.Models;
    using System;
    using System.Collections.Generic;

    public static class SharingConfigurations
    {
        static SharingConfigurations()
        {

        }
        
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
                    idx = DateTime.UtcNow.Second % section.SlaveDatabaseServers.Count;
                }
                return section.SlaveDatabaseServers[idx].GenerateDatabase(database);
            }
        }     
    }


    public static class WeChatConstant
    {
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
}
