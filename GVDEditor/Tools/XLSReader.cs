using System;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;

namespace GVDEditor.Tools
{
    /// <summary>
    ///     Nacitava udaje zo .XLS a .XLSX suborov
    /// </summary>
    public class XLSReader : TableFileReader
    {
        private readonly Application excelApp;
        private readonly Workbook workbook;
        private readonly Worksheet worksheet;

        /// <summary>
        ///     Konstruktor
        /// </summary>
        /// <param name="fileName">cesta k suboru</param>
        /// <param name="worksheetID">ID sheetu</param>
        public XLSReader(string fileName, int worksheetID = 1)
        {
            excelApp = new Application();
            workbook = excelApp.Workbooks.Open(fileName, 0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            worksheet = workbook.Worksheets[worksheetID];

            ReadWorksheet();
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            workbook.Close();
            excelApp.Quit();
            Marshal.ReleaseComObject(worksheet);
            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(excelApp);
        }

        private void ReadWorksheet()
        {
            var range = worksheet.UsedRange;
            RowCount = range.Rows.Count;
            ColumnCount = range.Columns.Count;

            Data = new string[RowCount, ColumnCount];

            for (var r = 1; r <= RowCount; r++)
            for (var c = 1; c <= ColumnCount; c++)
                Data[r - 1, c - 1] = (range.Cells[r, c] as Range)?.Value2.ToString();
        }
    }
}