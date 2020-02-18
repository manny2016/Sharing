



namespace Sharing.Core.Services {
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Configuration;
	using System.Collections.Generic;
	using Microsoft.Extensions.Configuration.Json;

	public static class SharingCoreServiceServiceCollectionExtensions {
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
			var configurationRoot = new ConfigurationRoot(new List<IConfigurationProvider>() {
				new  JsonConfigurationProvider(new JsonConfigurationSource(){
					 Optional = true,
					 FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(System.Environment.CurrentDirectory),
					 Path ="appsettings.json",
					 ReloadOnChange  =true
				})
			});
			collection.Add(new ServiceDescriptor(typeof(IConfiguration), configurationRoot));
			builder.Build();
			return collection;
		}
	}
}
