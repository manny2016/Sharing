using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Sharing.Core {
	public interface IAppHostBuilder {
		//
		// Summary:
		//     Builds an Microsoft.AspNetCore.Hosting.IWebHost which hosts a web application.
		IAppHostBuilder Build();
		//
		// Summary:
		//     Adds a delegate for configuring the Microsoft.Extensions.Configuration.IConfigurationBuilder
		//     that will construct an Microsoft.Extensions.Configuration.IConfiguration.
		//
		// Parameters:
		//   configureDelegate:
		//     The delegate for configuring the Microsoft.Extensions.Configuration.IConfigurationBuilder
		//     that will be used to construct an Microsoft.Extensions.Configuration.IConfiguration.
		//
		// Returns:
		//     The Microsoft.AspNetCore.Hosting.IWebHostBuilder.
		//
		// Remarks:
		//     The Microsoft.Extensions.Configuration.IConfiguration and Microsoft.Extensions.Logging.ILoggerFactory
		//     on the Microsoft.AspNetCore.Hosting.WebHostBuilderContext are uninitialized at
		//     this stage. The Microsoft.Extensions.Configuration.IConfigurationBuilder is pre-populated
		//     with the settings of the Microsoft.AspNetCore.Hosting.IWebHostBuilder.
		//IAppHostBuilder ConfigureAppConfiguration(Action<WebHostBuilderContext, IConfigurationBuilder> configureDelegate);
		//
		// Summary:
		//     Adds a delegate for configuring additional services for the host or web application.
		//     This may be called multiple times.
		//
		// Parameters:
		//   configureServices:
		//     A delegate for configuring the Microsoft.Extensions.DependencyInjection.IServiceCollection.
		//
		// Returns:
		//     The Microsoft.AspNetCore.Hosting.IWebHostBuilder.
		IAppHostBuilder ConfigureServices(Action<IServiceCollection> configureServices);
		//
		// Summary:
		//     Adds a delegate for configuring additional services for the host or web application.
		//     This may be called multiple times.
		//
		// Parameters:
		//   configureServices:
		//     A delegate for configuring the Microsoft.Extensions.DependencyInjection.IServiceCollection.
		//
		// Returns:
		//     The Microsoft.AspNetCore.Hosting.IWebHostBuilder.
		//IAppHostBuilder ConfigureServices(Action<WebHostBuilderContext, IServiceCollection> configureServices);
		//
		// Summary:
		//     Get the setting value from the configuration.
		//
		// Parameters:
		//   key:
		//     The key of the setting to look up.
		//
		// Returns:
		//     The value the setting currently contains.
		string GetSetting(string key);
		//
		// Summary:
		//     Add or replace a setting in the configuration.
		//
		// Parameters:
		//   key:
		//     The key of the setting to add or replace.
		//
		//   value:
		//     The value of the setting to add or replace.
		//
		// Returns:
		//     The Microsoft.AspNetCore.Hosting.IWebHostBuilder.
		IAppHostBuilder UseSetting(string key, string value);
	}
}
