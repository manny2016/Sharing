using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core {
	public interface IDatabaseFactory {
		IDatabase GenerateDatabase(string database = "sharing-dev", bool isWriteOnly = true,
			Microsoft.Extensions.Configuration.IConfiguration configuration = null) ;
	}
}
