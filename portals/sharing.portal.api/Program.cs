
namespace Sharing.Portal.Api {
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;
	using Sharing.Core;
	using Sharing.Core.Services;
	using Microsoft.Extensions.Caching;

	using System.Xml;
	using System.Reflection;
	using Sharing.WeChat.Models;
	using Sharing.Portal.Api.Filters;
	using Sharing.Core.CMQ;

	public class Program {
		public static IWebHostBuilder builder;
		public static void Main(string[] args) {
			builder = CreateWebHostBuilder(args)
				.ConfigureServices((collection) => {

					collection.AddWeChatApiService();
					collection.AddRandomGenerator();
					collection.AddSharingHostService();
					collection.AddWeChatPayService();
					collection.AddWeChatUserService();
					collection.AddMcardService();
					collection.AddMemoryCache();
					collection.AddWeChatMsgHandler();
					collection.AddMvc((setup) => {
						setup.Filters.Add(new ExceptionFilter());
					});
					collection.AddLogging((cfg) => {
						cfg.AddConsole();
						cfg.AddLog4Net();
					});
					collection.AddTencentCMQ();
					collection.AddDatabaseFactory();					
					collection.AddScoped<ModelClient>();
					
				})
				.ConfigureAppConfiguration((hostingContext, configurationBuilder) => {
					var evn = hostingContext.HostingEnvironment;
					configurationBuilder.AddEnvironmentVariables();
				});

			builder.Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();

	}
}
