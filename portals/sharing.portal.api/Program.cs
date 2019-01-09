
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

    public class Program
    {
   
        public static void Main(string[] args)
        {
          

            


            CreateWebHostBuilder(args)                
                //.UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var evn = hostingContext.HostingEnvironment;
                    config.AddEnvironmentVariables();

                    config
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile("appsettings.Development.json",
                      optional: true, reloadOnChange: true);
                  
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
                    var provider = collection.BuildServiceProvider();
                    collection.Add(new ServiceDescriptor(typeof(ModelClient), new ModelClient(
                        provider.GetService<IWeChatApi>(),
                        provider.GetService<IWxUserService>(),
                        provider.GetService<IRandomGenerator>(),
                        provider.GetService<IWeChatPayService>(),
                        provider.GetService<ISharingHostService>())));
                })               
                .Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)                  
                .UseStartup<Startup>();

    }
}
