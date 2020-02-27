

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Sharing.Core {
	public class DefaultAppHost : IAppHost {
		public DefaultAppHost(Type startup) {
			this.StartUp = startup;
		}
		public IServiceProvider Services {
			get;  set;
		}

		public IConfiguration Configuration {
			get; private set;
		}

		private Type StartUp { get; set; }
		public void Start() {
			var runner = this.Services.GetService(this.StartUp);
			AppHost.UsedAppHost(this);
			Configuration = Services.GetService(typeof(IConfiguration)) as IConfiguration;
			this.StartUp.GetMethod("Run").Invoke(runner, null);
		}
		public Task StartAsync(CancellationToken cancellationToken = default(CancellationToken)) => throw new NotImplementedException();
		public Task StopAsync(CancellationToken cancellationToken = default(CancellationToken)) => throw new NotImplementedException();
	}
}
