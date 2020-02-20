

namespace Sharing.Core.Models.Excel {
	using System;
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class ExcelSheetAttribute : Attribute {
		public string SheetName { get; set; }
		public int DataMarkRow { get; set; } = 1;
		public int DataMarkColumn { get; set; } = 999;
		public int DataStartRow { get; set; } = 2;
	}
}
