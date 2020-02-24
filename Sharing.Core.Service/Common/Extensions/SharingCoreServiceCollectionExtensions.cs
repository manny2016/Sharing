



namespace Sharing.Core.Services {
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Configuration;
	using System.Collections.Generic;
	using Microsoft.Extensions.Configuration.Json;
	using Sharing.Core.CMQ;

	public static class SharingCoreServiceCollectionExtensions {
		public static IServiceCollection AddWeChatUserService(this IServiceCollection collection) {
			collection.Add(new ServiceDescriptor(typeof(IWeChatUserService), typeof(WeChatUserService), ServiceLifetime.Transient));
			return collection;
		}
		public static IServiceCollection AddMcardService(this IServiceCollection collection) {
			collection.Add(new ServiceDescriptor(typeof(IMCardService), typeof(MCardService), ServiceLifetime.Transient));
			return collection;
		}
		public static IServiceCollection AddWeChatPayService(this IServiceCollection collection) {
			collection.Add(new ServiceDescriptor(typeof(IWeChatPayService), typeof(WeChatPayService), ServiceLifetime.Transient));
			return collection;
		}
		public static IServiceCollection AddSharingHostService(this IServiceCollection collection) {
			collection.Add(new ServiceDescriptor(typeof(ISharingHostService), typeof(SharingHostService), ServiceLifetime.Singleton));
			return collection;
		}
		public static IServiceCollection AddWeChatMsgHandler(this IServiceCollection collection) {
			collection.Add(new ServiceDescriptor(typeof(IWeChatMsgHandler), typeof(WeChatMsgHandler), ServiceLifetime.Transient));
			return collection;
		}
		public static IServiceCollection AddExcelBulExittHelper(this IServiceCollection collection) {
			collection.Add(new ServiceDescriptor(typeof(IExcelBulkEditHelper), typeof(DefaultExcelBulkEditHelper), ServiceLifetime.Transient));
			return collection;
		}

		public static IServiceCollection ConfigureAppConfiguration(this IServiceCollection collection) {

			var builder = new ConfigurationBuilder();
#if DEBUG
			var configurationRoot = new ConfigurationRoot(new List<IConfigurationProvider>() {
				new  JsonConfigurationProvider(new JsonConfigurationSource(){
					 Optional = true,
					 FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(System.Environment.CurrentDirectory),
					 Path ="appsettings.Development.json",
					 ReloadOnChange  =true
				})
			});
#else
			var configurationRoot = new ConfigurationRoot(new List<IConfigurationProvider>() {
				new  JsonConfigurationProvider(new JsonConfigurationSource(){
					 Optional = true,
					 FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(System.Environment.CurrentDirectory),
					 Path ="appsettings.json",
					 ReloadOnChange  =true
				})
			});
#endif

			collection.Add(new ServiceDescriptor(typeof(IConfiguration), configurationRoot));
			builder.Build();
			return collection;
		}

		public static IServiceCollection AddTencentCMQ(this IServiceCollection collection) {
			collection.Add(new ServiceDescriptor(typeof(TencentCMQClientFactory), typeof(TencentCMQClientFactory), ServiceLifetime.Singleton));
			return collection;
		}
		public static IServiceCollection AddDatabaseFactory(this IServiceCollection collection) {
			collection.Add(new ServiceDescriptor(typeof(IDatabaseFactory), typeof(DatabaseFactory), ServiceLifetime.Singleton));
			return collection;
		}
	}
}
