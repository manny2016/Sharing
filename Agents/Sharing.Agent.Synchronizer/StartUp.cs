

namespace Sharing.Agent.Synchronizer {

	using Newtonsoft.Json.Linq;
	using Sharing.Core.Models;
	using Sharing.Core;
	using System.Threading.Tasks;
	using System.Threading;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Microsoft.Extensions.Configuration;
	using System.Security.Cryptography.X509Certificates;

	public class StartUp {
		private readonly IEnumerable<IASfPWorkItem> workItems;
		private readonly IConfiguration configuration;
		private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(Program));
		public StartUp(IEnumerable<IASfPWorkItem> workItems, IConfiguration configuration) {
			this.workItems = workItems;
			this.configuration = configuration;
		}


		public void Run() {

			var scheduler = new WebJobScheduler((cancellation) => {
				Parallel.ForEach(this.workItems, new ParallelOptions() {
					MaxDegreeOfParallelism = 5
				}, (workitem) => {
					while ( cancellation.IsCancellationRequested == false ) {

						try {
							var offset = 60D * 10;//10 minutes
							workitem.Execute();
							for ( var i = 0; ((cancellation.IsCancellationRequested == false) && (i < offset)); i++ ) {
								Thread.Sleep(1000);
							}
						} catch ( Exception ex ) {
							Logger.Error(ex.Message, ex);
						}
					}
				});
			});
			scheduler.Shutdown += (sender, xx) => {
				foreach ( var workitem in this.workItems ) {
					workitem.Abort();
				}
			};
			scheduler.Start();
		}
	}
}
