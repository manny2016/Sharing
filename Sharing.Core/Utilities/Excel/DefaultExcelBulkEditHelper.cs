

namespace Sharing.Core {
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Text;
	using OfficeOpenXml;
	using Sharing.Core.Models.Excel;
	using System.Linq;
	using System.Reflection;

	public class DefaultExcelBulkEditHelper : IExcelBulkEditHelper {
		public void Dispose() {

		}




		public ExcelDataModel<T> Read<T>(Stream stream,
			Dictionary<string, Dictionary<string, string>> provider = null,
			bool onlyReturnChanged = false) where T : ExcelBulkEditRow {
			throw new NotImplementedException();
		}
		public ExcelDataModel<T1, T2> Read<T1, T2>(Stream stream,
			Dictionary<string, Dictionary<string, string>> provider = null,
			bool onlyReturnChanged = false)
			where T1 : ExcelBulkEditRow
			where T2 : ExcelBulkEditRow {
			throw new NotImplementedException();
		}

		public ExcelDataModel<T1, T2, T3> Read<T1, T2, T3>(
			Stream stream,
			Dictionary<string, Dictionary<string, string>> provider = null,
			bool onlyReturnChanged = false)
			where T1 : ExcelBulkEditRow
			where T2 : ExcelBulkEditRow
			where T3 : ExcelBulkEditRow {
			throw new NotImplementedException();
		}
		public void Write<T>(Stream stream, ExcelDataModel<T> model)
			where T : ExcelBulkEditRow {
			Guard.ArgumentNotNull(model, "model");
			Guard.ArgumentNotNull(model.DataMark, "model.DataMark");
			using ( var package = new ExcelPackage(stream) ) {

				Write(package, typeof(T), model.DataMark, model.Data.Select(o => o as object).ToArray());
				package.SaveAs(stream);
			}
		}
		private void Write(ExcelPackage package, Type type, ExcelDataMark dataMark, object[] dataArray) {

			var excelSheetAttribute = type.GetCustomAttributes(typeof(ExcelSheetAttribute), false)
				.SingleOrDefault() as ExcelSheetAttribute;
			var sheetName = excelSheetAttribute == null ? type.Name : excelSheetAttribute.SheetName;
			var worksheet = package.Workbook.Worksheets.Add(sheetName);
			this.WriteDataMark(worksheet, dataMark, excelSheetAttribute.Row, excelSheetAttribute.Column);

			var properties = type.GetProperties()
				.Where(x => x.GetCustomAttributes(typeof(ExcelColumnAttribute), true).SingleOrDefault() != null)
				.ToArray();

			WriteWorkSheet(worksheet, dataArray, properties);
		}
		public void Write<T1, T2>(Stream stream, ExcelDataModel<T1, T2> model)
			where T1 : ExcelBulkEditRow
			where T2 : ExcelBulkEditRow {
			using ( var package = new ExcelPackage(stream) ) {
				Write(package, typeof(T1), model.DataMark, model.Data1.Select(o => o as object).ToArray());
				Write(package, typeof(T2), model.DataMark, model.Data1.Select(o => o as object).ToArray());
				package.SaveAs(stream);
			}
		}
		public void Write<T1, T2, T3>(Stream stream, ExcelDataModel<T1, T2, T3> model)
			where T1 : ExcelBulkEditRow
			where T2 : ExcelBulkEditRow
			where T3 : ExcelBulkEditRow {
			using ( var package = new ExcelPackage(stream) ) {
				Write(package, typeof(T1), model.DataMark, model.Data1?.Select(o => o as object).ToArray());
				Write(package, typeof(T2), model.DataMark, model.Data2?.Select(o => o as object).ToArray());
				Write(package, typeof(T3), model.DataMark, model.Data3?.Select(o => o as object).ToArray());
				package.SaveAs(stream);
			}
		}

		private ExcelDataMark ReadDataMark(ExcelWorksheet worksheet, int row, int column) {
			return worksheet.Cells[row, column].Value.ToString()
				.DeserializeToObject<ExcelDataMark>();
		}
		private void WriteDataMark(ExcelWorksheet worksheet, ExcelDataMark mark, int row, int column) {
			worksheet.Column(column).Hidden = true;
			worksheet.Cells[row, column].Value = mark.SerializeToJson();
		}
		private void WriteHeader(ExcelWorksheet worksheet, PropertyInfo[] properties, int row = 1) {
			for ( int column = 1; column <= properties.Length; column++ ) {
				var option = properties[column - 1].GetCustomAttributes(typeof(ExcelColumnAttribute)).SingleOrDefault() as ExcelColumnAttribute;
				var headerText = option.HeaderText;
				worksheet.Cells[row, column].Value = headerText;
				worksheet.Column(column).Hidden = option.Hidden;
			}
		}

		private void WriteWorkSheet(ExcelWorksheet worksheet, object[] dataArray, PropertyInfo[] properties, int startRowIndex = 1) {
			//Write Header
			for ( int column = 1; column <= properties.Length; column++ ) {
				var option = properties[column - 1].GetCustomAttributes(typeof(ExcelColumnAttribute))
					.SingleOrDefault() as ExcelColumnAttribute;

				var headerText = option.HeaderText;
				worksheet.Cells[startRowIndex, column].Value = headerText;
				worksheet.Column(column).Hidden = option.Hidden;
			}
			//Write Data
			if ( dataArray == null || dataArray.Length == 0 ) {
				return;
			}
			for ( var row = startRowIndex + 1; row <= dataArray.Length; row++ ) {
				this.WriteExcelRow(worksheet, dataArray[row - startRowIndex], properties, row);
			}
		}
		private void WriteExcelRow(ExcelWorksheet worksheet, object data, PropertyInfo[] properties, int row) {
			for ( var column = 1; column <= properties.Length; column++ ) {
				if ( properties[column - 1].GetType().Name == "String[]" ) {
					var values = properties[column - 1].GetValue(data) as string[];
					worksheet.Cells[row, column].Value = string.Join(",",values);

				} else {
					worksheet.Cells[row, column].Value = properties[column - 1].GetValue(data)?.ToString();
				}

			}
			worksheet.Cells[row, properties.Length + 1].Value = data.SerializeToJson();
		}
	}
}
