using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Sharing.Core {
	public class AppHostBuilderContext {		
		
		public IConfiguration Configuration { get; set; }
	}
}
