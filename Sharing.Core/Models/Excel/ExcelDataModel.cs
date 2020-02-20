

namespace Sharing.Core.Models.Excel {
	using System;
	using System.Collections.Generic;
	using System.Text;

	public abstract class ExcelDataModel {
		public Dictionary<string, Dictionary<string, string>> DropDownValueOptions { get; set; }
		public ExcelDataMark DataMark { get; set; }
		public List<string> Messages { get; set; } = new List<string>();
	}
	public class ExcelDataModel<T> : ExcelDataModel
		where T : ExcelBulkEditRow {
		public IEnumerable<T> Data { get; set; }
	}
	public class ExcelDataModel<T1, T2> : ExcelDataModel
		where T1 : ExcelBulkEditRow
		where T2 : ExcelBulkEditRow {
		public IEnumerable<T1> Data1 { get; set; }
		public IEnumerable<T2> Data2 { get; set; }

	}

	public class ExcelDataModel<T1, T2, T3> : ExcelDataModel
		where T1 : ExcelBulkEditRow
		where T2 : ExcelBulkEditRow
		where T3 : ExcelBulkEditRow {
		public IEnumerable<T1> Data1 { get; set; }
		public IEnumerable<T2> Data2 { get; set; }
		public IEnumerable<T3> Data3 { get; set; }
	}

}
