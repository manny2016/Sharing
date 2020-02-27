
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Sharing.Core {
	public interface IAppHost {
		//
		// Summary:
		//     The System.IServiceProvider for the host.
		IServiceProvider Services { get; }
		IConfiguration Configuration { get;}
		//
		// Summary:
		//     Starts listening on the configured addresses.
		void Start();
		//
		// Summary:
		//     Starts listening on the configured addresses.
		Task StartAsync(CancellationToken cancellationToken = default(CancellationToken));
		//
		// Summary:
		//     Attempt to gracefully stop the host.
		//
		// Parameters:
		//   cancellationToken:
		Task StopAsync(CancellationToken cancellationToken = default(CancellationToken));
	}
}
