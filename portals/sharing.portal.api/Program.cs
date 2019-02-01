
namespace Sharing.Portal.Api
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Sharing.Core;
    using Sharing.Core.Services;
    using Microsoft.Extensions.Caching;
    using Microsoft.Extensions.DependencyInjection;
    using System.Xml;
    using System.Reflection;
    using Sharing.WeChat.Models;

    public class Program
    {
        public static IWebHostBuilder builder;
        public static void Main(string[] args)
        {
            var xml = @"
<xml>
	<appid><![CDATA[wx6a15c5888e292f99]]></appid>
	<attach><![CDATA[7080c4b96ec7eca229c471753d037ca5d3840699]]></attach>
	<bank_type><![CDATA[CCB_CREDIT]]></bank_type>
	<cash_fee><![CDATA[2000]]></cash_fee>
	<fee_type><![CDATA[CNY]]></fee_type>
	<is_subscribe><![CDATA[N]]></is_subscribe>
	<mch_id><![CDATA[1520961881]]></mch_id>
	<nonce_str><![CDATA[1008705247]]></nonce_str>
	<openid><![CDATA[o_SjX5Yt_H5En9323Syhw1Aic3Jk]]></openid>
	<out_trade_no><![CDATA[T201901240000000036]]></out_trade_no>
	<result_code><![CDATA[SUCCESS]]></result_code>
	<return_code><![CDATA[SUCCESS]]></return_code>
	<sign><![CDATA[B12E08FA08200D13B819F14E82C3D60A0E2465FC37368FDCFC90B1914000E024]]></sign>
	<time_end><![CDATA[20190124205631]]></time_end>
	<total_fee>2000</total_fee>
	<trade_type><![CDATA[JSAPI]]></trade_type>
	<transaction_id><![CDATA[4200000265201901242415603552]]></transaction_id>
</xml>
";
            


            builder = CreateWebHostBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var evn = hostingContext.HostingEnvironment;
                    config.AddEnvironmentVariables();
                    config
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile("appsettings.Development.json",
                      optional: true, reloadOnChange: true);

                })
                .ConfigureLogging((hostingContext, logging) =>
                {

                })
                .ConfigureServices((collection) =>
                {
                    collection.AddWeChatApiService();
                    collection.AddRandomGenerator();
                    collection.AddSharingHostService();
                    collection.AddWeChatPayService();
                    collection.AddWeChatUserService();
                    collection.AddMcardService();
                    collection.AddMemoryCache();
                    collection.AddWeChatMsgHandler();
                    
                    var provider = collection.BuildServiceProvider();
                    collection.Add(new ServiceDescriptor(typeof(ModelClient), new ModelClient(
                        provider.GetService<IWeChatApi>(),
                        provider.GetService<IWeChatUserService>(),
                        provider.GetService<IRandomGenerator>(),
                        provider.GetService<IWeChatPayService>(),
                        provider.GetService<IMCardService>(),
                        provider.GetService<ISharingHostService>(),
                        provider.GetService<IWeChatMsgHandler>()
                        )));
                });
            builder.Build().Run();



        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

    }
}
