using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Sharing.Core;
using Sharing.Core.Models;

namespace Sharing.Agent.Synchronizer {
	class Program {
		private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(Program));
		static void Main(string[] args) {

			var workitems = "https://www.yourc.club/api/sharing/QueryMerchantDetails"
				.GetUriJsonContent<JObject>().SelectToken("$.data").ToString()
				.DeserializeToObject<MerchantDetails[]>()
				.GenernateWorkItems();

			var scheduler = new WebJobScheduler((cancellation) => {
				Parallel.ForEach(workitems, new ParallelOptions() {
					MaxDegreeOfParallelism = 5
				}, (workitem) => {
					while ( cancellation.IsCancellationRequested == false ) {

						try {
							var offset = 60D * 10;//1 hour     
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
				foreach ( var workitem in workitems ) {
					workitem.Abort();
				}
			};
			scheduler.Start();
		}
	}
}
