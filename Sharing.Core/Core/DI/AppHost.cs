using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core {
	public static class AppHost {

		internal static void UsedAppHost(IAppHost host) {
			Host = host;
		}
		public static IAppHost Host { get; private set; }
		public static IAppHostBuilder CreateDefaultBuilder() {
			return CreateDefaultBuilder(null);
		}

		public static IAppHostBuilder CreateDefaultBuilder(string[] args) {
			return new DefaultAppHostBuilder(args);
		}

		public static IAppHostBuilder CreateDefaultBuilder<TStartup>(string[] args) where TStartup : class {
			return new DefaultAppHostBuilder(args, typeof(TStartup));
		}
	}
}
