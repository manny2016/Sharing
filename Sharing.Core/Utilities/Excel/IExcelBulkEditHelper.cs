


namespace Sharing.Core {
	using System;
	using System.Collections.Generic;
	using System.IO;
	using Sharing.Core.Models.Excel;

	public interface IExcelBulkEditHelper : IDisposable {
		ExcelDataModel<T> Read<T>(Stream stream,
			Dictionary<string, Dictionary<string, string>> provider = null,
			bool onlyReturnChanged = false)
			where T : ExcelBulkEditRow;

		ExcelDataModel<T1, T2> Read<T1, T2>(Stream stream,
			 Dictionary<string, Dictionary<string, string>> provider = null,
			bool onlyReturnChanged = false)
			where T1 : ExcelBulkEditRow
			where T2 : ExcelBulkEditRow;

		ExcelDataModel<T1, T2, T3> Read<T1, T2, T3>(Stream stream,
			Dictionary<string, Dictionary<string, string>> provider = null,
			bool onlyReturnChanged = false)
			where T1 : ExcelBulkEditRow
			where T2 : ExcelBulkEditRow
			where T3 : ExcelBulkEditRow;

		void Write<T>(Stream stream, ExcelDataModel<T> model)
			where T : ExcelBulkEditRow;

		void Write<T1, T2>(Stream stream, ExcelDataModel<T1, T2> model)
			where T1 : ExcelBulkEditRow
			where T2 : ExcelBulkEditRow;

		void Write<T1, T2, T3>(Stream stream, ExcelDataModel<T1, T2, T3> model)
			where T1 : ExcelBulkEditRow
			where T2 : ExcelBulkEditRow
			where T3 : ExcelBulkEditRow;

	}
}
