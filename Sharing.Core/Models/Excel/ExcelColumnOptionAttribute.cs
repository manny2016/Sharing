


namespace Sharing.Core.Models.Excel {
	using System;
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class ExcelColumnAttribute : Attribute {
		public string HeaderText { get; set; }
		public bool ReadOnly { get; set; } = false;
		public bool Hidden { get; set; } = false;
		public bool Required { get; set; } = false;
		public string Formula { get; set; }
		public int SortId { get; set; } = 1;
		public string DropDownProvider { get; set; }
		public ExcelEditInputTypes ExcelEditInputType { get; set; } = ExcelEditInputTypes.TextBox;
	}
}
