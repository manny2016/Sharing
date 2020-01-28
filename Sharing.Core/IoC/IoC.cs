
namespace Sharing.Core {
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;
	using System;
	using System.Collections.Generic;
	using Microsoft.Extensions.Configuration;

	public static class IoC {
		private static IServiceCollection services;
		private static ServiceProvider provider;

		public static void ConfigureServiceProvider(IServiceCollection collection = null
			, Action<IServiceCollection> configure = null) {
			if ( provider == null ) {
				collection = collection ?? new ServiceCollection();
				services = collection;
				//collection.Configure
				if ( configure != null ) {
					configure(collection);
				}
				collection.AddLogging((cfg) => {
					cfg.AddConsole();
					cfg.AddLog4Net();
				});
				provider = collection.BuildServiceProvider();
				
			}
		}

		public static T GetService<T>() {
			if ( provider == null ) throw new NullReferenceException("Need run ConfigureServiceProvider first");
			return provider.GetService<T>();
		}
		public static IEnumerable<T> GetServices<T>() {
			if ( provider == null ) throw new NullReferenceException("Need run ConfigureServiceProvider first");
			return provider.GetServices<T>();
		}
	}
}
