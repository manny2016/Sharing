

namespace Sharing.Core.Models.Excel {
	using System;
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class ExcelSheetAttribute : Attribute {
		public string SheetName { get; set; }
		public int Row { get; set; } = 1;
		public int Column { get; set; } = 999;
	}
}
