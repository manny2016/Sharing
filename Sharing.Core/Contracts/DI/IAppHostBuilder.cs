
namespace Sharing.Core {
	using System;
	using System.Collections.Generic;
	using System.Text;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	public interface IAppHostBuilder {

		IAppHost Build();

		IAppHostBuilder ConfigureAppConfiguration(Action<AppHostBuilderContext, IConfigurationBuilder> configureDelegate);

		IAppHostBuilder ConfigureServices(Action<IServiceCollection> configureServices);

		IAppHostBuilder ConfigureServices(Action<AppHostBuilderContext, IServiceCollection> configureServices);

		string GetSetting(string key);

		IAppHostBuilder UseSetting(string key, string value);
		IAppHostBuilder UseStartUp<TStartUp>() where TStartUp : class;
	}
}
