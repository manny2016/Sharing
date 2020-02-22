

namespace Sharing.Core {
	using System;
	using Microsoft.Extensions.Configuration;
	public class DatabaseFactory : IDatabaseFactory {
		private readonly IConfiguration CONFIGURATION;
		public DatabaseFactory(IConfiguration configuration) {
			this.CONFIGURATION = configuration;
		}
		

		public IDatabase GenerateDatabase(string database = "sharing-dev", 
			bool isWriteOnly = true, 
			IConfiguration configuration = null) {

			configuration = configuration ?? CONFIGURATION;
			var dbconfig = configuration.GetDbConfiguration();
			if ( isWriteOnly ) {
				return dbconfig.Master.GenerateDatabase(dbconfig.Database);

			} else {
				var idx = 0;
				if ( dbconfig.Slaves.Length > 0 ) {
					idx = DateTime.UtcNow.Second % dbconfig.Slaves.Length;
				}
				return dbconfig.Slaves[idx].GenerateDatabase(dbconfig.Database);
			}
		}
		
	}
}
