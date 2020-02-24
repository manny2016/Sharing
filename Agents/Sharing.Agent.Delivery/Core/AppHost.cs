
namespace Sharing.Agent.Delivery {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Configuration.Json;

	public static class AppHost {
		private static IAppHostBuilder builder;
		private static IConfiguration configuration;
		//public IAppHostBuilder CreateDefaultBuilder(string[] args) {

		//}
		public static void Exit() {
			Application.Exit();
		}
		public static IConfiguration GetConfiguration() {
			if ( configuration == null ) {
				configuration = new ConfigurationRoot(new List<IConfigurationProvider>() {
					new  JsonConfigurationProvider(new JsonConfigurationSource(){
						 Optional = true,
						 FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(System.Environment.CurrentDirectory),
						 Path ="appsettings.json",
						 ReloadOnChange  =true
					})
				});
			}
			return configuration;
		}
	}
}
