using ReportService.Application.Abstractions.Services;
using System.ComponentModel;
using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ReportService.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public string SaveDataExcelFormat<T>(List<T> data, string folderPath, string fileName)
        {

            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);


            List<string> columns = new();
            if (data.Count > 0)
                columns.AddRange(data.First()!.GetType().GetProperties().Select(p => p.Name));

            DataTable dt = ListToDataTable(data);

            var filecontent = ConvertToExcel(dt, columns, fileName, true);

            string filePath = string.Empty;
            SaveToExcel(filecontent, folderPath, fileName, out filePath);

            return filePath;
        }

        private static DataTable ListToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dataTable = new DataTable();

            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            object?[] values = new object?[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                    values[i] = properties[i].GetValue(item);
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        private static byte[] ConvertToExcel(DataTable dataTable, List<string> columns, string heading = "", bool showSrNo = false)
        {
            byte[] result = new byte[0];

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add($"{heading} Data");
                int startRowFrom = String.IsNullOrEmpty(heading) ? 1 : 3;

                for (int i = 0; i < dataTable.Columns.Count; i++)
                    dataTable.Columns[i].ColumnName = columns[i];

                if (showSrNo)
                {
                    DataColumn dataColumn = dataTable.Columns.Add("#", typeof(int));
                    dataColumn.SetOrdinal(0);
                    int index = 1;
                    foreach (DataRow item in dataTable.Rows)
                    {
                        item[0] = index;
                        index++;
                    }
                }

                // add the content into the Excel file  
                workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);

                // autofit width of cells with small content  
                int columnIndex = 1;
                foreach (DataColumn column in dataTable.Columns)
                {
                    ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];
                    int maxLength = columnCells.Count();
                    if (maxLength < 150)
                        workSheet.Column(columnIndex).AutoFit();

                    columnIndex++;
                }

                // format header - bold, yellow on black  
                using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
                {
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                }

                // format cells - add borders  
                using (ExcelRange r = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])
                {
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                }

                if (!String.IsNullOrEmpty(heading))
                {
                    workSheet.Cells["A1"].Value = heading;
                    workSheet.Cells["A1"].Style.Font.Size = 20;

                    workSheet.InsertColumn(1, 1);
                    workSheet.InsertRow(1, 1);
                    workSheet.Column(1).Width = 5;
                }
                result = package.GetAsByteArray();
                package.Dispose();
            }

            return result;
        }
        private static void SaveToExcel(byte[] bytes, string path, string fileName, out string savedFileName)
        {
            using MemoryStream ms = new(bytes);
            using ExcelPackage package = new(ms);
            savedFileName = fileName = $"{fileName}.xlsx";
            package.SaveAs(new FileInfo($"{path}\\{fileName}"));
        }
    }
}
