
using Microsoft.Extensions.Configuration;

namespace Sharing.Core.Models {
	public class AppHostBuilderContext {
		//
		// Summary:
		//     Gets or sets the name of the environment. The host automatically sets this property
		//     to the value of the "ASPNETCORE_ENVIRONMENT" environment variable, or "environment"
		//     as specified in any other configuration source.
		public string EnvironmentName { get; set; }
		//
		// Summary:
		//     Gets or sets the name of the application. This property is automatically set
		//     by the host to the assembly containing the application entry point.
		public string ApplicationName { get; set; }
		
		//
		// Summary:
		//     The Microsoft.Extensions.Configuration.IConfiguration containing the merged configuration
		//     of the application and the Microsoft.AspNetCore.Hosting.IWebHost.
		public IConfiguration Configuration { get; set; }
	}
}
