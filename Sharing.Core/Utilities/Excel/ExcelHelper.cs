namespace Sharing.Core 
{
    using System.Collections.Generic;
    using OfficeOpenXml;
    using System;
    public class ExcelHelper
    {
        public IEnumerable<T> ReadExcel<T>(string excel,
            Func<int, object[], T> convert,
            int workSeetIndex)
        {
            using (var package = new ExcelPackage(new System.IO.FileInfo(excel)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var columns = worksheet.Dimension.Columns;
                for (var row = 1; row <= worksheet.Dimension.Rows; row++)
                {
                    var rangs = new List<object>();
                    for (int cel = 1; cel <= columns; cel++)
                    {
                        rangs.Add(worksheet.Cells[row, cel].Value);
                    }
                    yield return convert(row, rangs.ToArray());
                }
            }
        }
    }
}
