
namespace Sharing.Core {
	using System;
	using System.Collections.Generic;
	using System.Text;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Configuration.Json;
	using Microsoft.Extensions.DependencyInjection;
	using System.Linq;
	using System.Reflection;
	public class DefaultAppHostBuilder : IAppHostBuilder {
		private Type startup;
		private readonly string[] argements;

		private readonly IServiceCollection collection;
		private readonly AppHostBuilderContext context;
		public DefaultAppHostBuilder() : this(null) {

		}
		public DefaultAppHostBuilder(string[] args) : this(args, null) {

		}
		public DefaultAppHostBuilder(string[] args, Type startup) {
			this.startup = startup;
			this.argements = args;
			this.collection = new ServiceCollection();
			this.context = new AppHostBuilderContext();
		}
		public IAppHost Build() {
			return new DefaultAppHost(this.startup) {
				Services = this.collection.BuildServiceProvider()
			};
		}
		public IAppHostBuilder ConfigureAppConfiguration(Action<AppHostBuilderContext, IConfigurationBuilder> configureDelegate) {
			var configurationBuilder = new ConfigurationBuilder();
			configurationBuilder.AddConfiguration(new ConfigurationRoot(new List<IConfigurationProvider>(){
				new JsonConfigurationProvider(new JsonConfigurationSource(){
					Optional = true,
					FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(Environment.CurrentDirectory),
					Path = (this.argements??new string[]{ }).Any(x=>x.Equals("Development",StringComparison.OrdinalIgnoreCase))
					? "appsettings.Development.json"
					: "appsettings.json",
					ReloadOnChange = true
				})
			}));
			this.context.Configuration = configurationBuilder.Build();
			configureDelegate?.Invoke(this.context, configurationBuilder);
			this.collection.Add(new ServiceDescriptor(typeof(IConfiguration), this.context.Configuration));
			return this;
		}
		public IAppHostBuilder ConfigureServices(Action<IServiceCollection> configureServices) {
			configureServices?.Invoke(this.collection);
			return this;
		}
		public IAppHostBuilder ConfigureServices(Action<AppHostBuilderContext, IServiceCollection> configureServices) {
			configureServices?.Invoke(this.context, this.collection);
			return this;
		}

		public string GetSetting(string key) {
			return string.Empty;
		}

		public IAppHostBuilder UseSetting(string key, string value) {
			return this;
		}

		public IAppHostBuilder UseStartUp<TStartUp>() where TStartUp : class {
			this.startup = this.startup == null ? typeof(TStartUp) : this.startup;
			this.collection.AddScoped<TStartUp>();
			return this;
		}
	}
}
