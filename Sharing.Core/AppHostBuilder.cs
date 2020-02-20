using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sharing.Core.Models;

namespace Sharing.Core {
	public class AppHostBuilder : IAppHostBuilder {
		public IAppHostBuilder Build() => throw new NotImplementedException();
		public IAppHostBuilder ConfigureAppConfiguration(Action<AppHostBuilderContext, IConfigurationBuilder> configureDelegate) => throw new NotImplementedException();

		public IAppHostBuilder ConfigureServices(Action<IServiceCollection> configureServices) {
			return this;
		}
		public string GetSetting(string key) {
			return string.Empty;
		}
		public IAppHostBuilder UseSetting(string key, string value) {
			return this;
		}
	}
}
