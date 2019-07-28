using Sharing.Core.Configuration;
using System;
using System.Configuration;
using Sharing.Core;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Sharing.WeChat.Models;
using System.Threading;
using Sharing.Core.Models;
using Sharing.Core.Services;
using Sharing.Core.CMQ;
namespace ConsoleApp
{
    class Program
    {
        static ServiceCollection collection = new ServiceCollection();
        static void Main(string[] args)
        {
            var provider = collection.AddWeChatApiService()
              .AddRandomGenerator()
              .AddWeChatPayService()
              .AddWeChatUserService()
              .AddMcardService()
              .AddMemoryCache()
              .AddWeChatMsgHandler()
              .BuildServiceProvider();

            var api = provider.GetService<IWeChatApi>();
            var app = new WxApp()
            {
                AppId = "wx20da9548445a2ca7",
                Secret = "6be5c3202dfd0d074851615588596e6c",
                OriginalId = "gh_085392ac0d21",
                AppType = AppTypes.Official
            };

            //foreach (var user in api.QueryAllWxUsers(app))
            //{
            //    if (user.UnionId.Equals("oSL3y1Eas8fynefiHWCW7cFMS2PU"))
            //    {

            //    }
            //}
            var client = TencentCMQClientFactory.Create<OnlineOrder>("lemon");
            client.Initialize();
            client.Monitor((entity) =>
            {
                //发送消息给跑腿公司
                var result = api.SendWeChatMessage(app, "o18KQ54oC3bWFss8Zzk__G8CW9TU", entity.GenernateforTakeout());
                client.DeleteMessage(entity);
            });
        }

    }
}
