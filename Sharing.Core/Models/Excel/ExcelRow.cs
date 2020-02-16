
namespace Sharing.Core.Models.Excel {
	using System;
	using System.Collections.Generic;
	using System.Text;

	public abstract class ExcelBulkEditRow {

		public int RowId { get; set; }
		
		public string SheetName { get; set; }

		public bool Changed { get; set; }
	}
}
