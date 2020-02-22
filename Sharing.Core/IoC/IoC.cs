
//namespace Sharing.Core {
//	using Microsoft.Extensions.DependencyInjection;
//	using Microsoft.Extensions.Logging;
//	using System;
//	using System.Collections.Generic;
//	using Microsoft.Extensions.Configuration;

//	public static class IoC {
//		private static IServiceCollection services;
//		private static IServiceProvider provider;

//		public static void ConfigureService(IServiceCollection collection = null
//			, Action<IServiceCollection> configure = null) {
//			if ( provider == null ) {
//				collection = collection ?? new ServiceCollection();

//				services = collection;
//				//collection.Configure
//				configure?.Invoke(collection);
//				collection.AddLogging((cfg) => {
//					cfg.AddConsole();
//					cfg.AddLog4Net();
//				});
//			}
//			provider = collection.BuildServiceProvider();
//		}

//		public static T GetService<T>() {
//			provider = provider?? services.BuildServiceProvider();
//			if ( provider == null ) throw new NullReferenceException("Need run ConfigureServiceProvider first");
//			return provider.GetService<T>();
//		}
//		public static IEnumerable<T> GetServices<T>() {
//			if ( provider == null ) throw new NullReferenceException("Need run ConfigureServiceProvider first");
//			return provider.GetServices<T>();
//		}

		
//	}
//}
