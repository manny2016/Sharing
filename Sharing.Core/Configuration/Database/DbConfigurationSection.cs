using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Sharing.Core.Configuration {
	public class DbConfiguration {
		public const string SectionName = "dbConfig";


		public DbConfiguration() { }
		public string Database {
			get; set;
		}
		public DatabaseServer Master { get; set; }
		public DatabaseServer[] Slaves { get; set; }


	}
}
