

namespace Sharing.Core {
	using System;
	using System.Linq;
	using System.Reflection;
	using Sharing.Core.Models.Excel;
	using System.Collections.Generic;
	public static class ExcelBulkEditHelperExtenstion {
		public static ExcelSheetAttribute GetWorksheetAttrbiute(this Type type) {
			var attribute = type.GetCustomAttributes(typeof(ExcelSheetAttribute), false)
				   .SingleOrDefault() as ExcelSheetAttribute;

			if ( attribute == null ) {
				throw new InvalidOperationException("The type didn't has attribute ExcelSheetAttribute");
			}
			return attribute;
		}
		public static PropertyInfo[] GetPropertyInfosWithExcelColumnOption(this Type type) {
			var sorted = new SortedDictionary<int, PropertyInfo>();
			foreach ( var property in type.GetProperties() ) {
				var attribute = property.GetCustomAttribute<ExcelColumnAttribute>();
				if ( attribute == null ) {
					continue;
				}
				var sortId = sorted.ContainsKey(attribute.SortId) ? sorted.Max(o => o.Key) + 1 : attribute.SortId;
				sorted.Add(sortId, property);
			}
			return sorted.Select(o => o.Value).ToArray();
		}
		public static void ParseValue(this PropertyInfo property, object obj, object value) {
			if ( property.MemberType != MemberTypes.Property ) {
				throw new NotSupportedException();
			}
			if ( value == null ) {
				return;
			}
			switch ( property.PropertyType.Name ) {
				case "Int64":

					property.SetValue(obj, long.Parse(value.ToString()));
					break;
				case "Int32":
					property.SetValue(obj, int.Parse(value.ToString()));
					break;
				case "Boolean":
					property.SetValue(obj, bool.Parse(value.ToString()));
					break;
				
				case "String":
					property.SetValue(obj, value.ToString());
					break;
				default:
					throw new NotSupportedException();
			}
		}
	}
}
