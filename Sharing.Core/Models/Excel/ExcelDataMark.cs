

namespace Sharing.Core.Models.Excel {
	using System;
	using System.Collections.Generic;
	using System.Text;
	public class ExcelDataMark {
		public long DateTime { get; set; }
		public Version Version { get; set; }
		public Dictionary<string, string> Additional { get; set; } = new Dictionary<string, string>();
	}
}
