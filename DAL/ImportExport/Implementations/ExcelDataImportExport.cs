using Microsoft.Office.Interop.Excel;
using MrDAL.ImportExport.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Data;
using DataTable = System.Data.DataTable;

namespace MrDAL.ImportExport.Implementations;

public class ExcelDataImportExport : DataExport
{
    public override NonQueryResult ExportToFile(string filename)
    {
        throw new NotImplementedException();
    }

    public override void Load(string filename)
    {
        throw new NotImplementedException();
    }

    public DataSet Import(string filename, bool headers = true)
    {
        var xl = new Application();
        var wb = xl.Workbooks.Open(filename);
        var sheets = wb.Sheets;
        DataSet dataSet = null;
        if (sheets != null && sheets.Count != 0)
        {
            dataSet = new DataSet();
            foreach (var item in sheets)
            {
                var sheet = (Worksheet)item;
                DataTable dt = null;
                if (sheet != null)
                {
                    dt = new DataTable();
                    var columnCount = ((Range)sheet.UsedRange.Rows[1, Type.Missing]).Columns.Count;
                    var rowCount = ((Range)sheet.UsedRange.Columns[1, Type.Missing]).Rows.Count;

                    for (var j = 0; j < columnCount; j++)
                    {
                        var cell = (Range)sheet.Cells[1, j + 1];
                        var column = new DataColumn(headers ? cell.Value : string.Empty);
                        dt.Columns.Add(column);
                    }

                    for (var i = 0; i < rowCount; i++)
                    {
                        var r = dt.NewRow();
                        for (var j = 0; j < columnCount; j++)
                        {
                            var cell = (Range)sheet.Cells[i + 1 + (headers ? 1 : 0), j + 1];
                            r[j] = cell.Value;
                        }

                        dt.Rows.Add(r);
                    }
                }

                dataSet.Tables.Add(dt ?? new DataTable());
            }
        }

        xl.Quit();
        return dataSet;
    }

    public string Export(DataTable dt, string fileName, string file)
    {
        var xl = new Application();
        var wb = xl.Workbooks.Add();
        var sheet = (Worksheet)wb.ActiveSheet;
        //process columns
        for (var i = 0; i < dt.Columns.Count; i++)
        {
            var col = dt.Columns[i];
            //added columns to the top of sheet
            var currentCell = (Range)sheet.Cells[1, i + 1];
            currentCell.Value = col.ToString();
            currentCell.Font.Bold = true;
            //process rows
            for (var j = 0; j < dt.Rows.Count; j++)
            {
                var row = dt.Rows[j];
                //added rows to sheet
                var cell = (Range)sheet.Cells[j + 1 + 1, i + 1];
                cell.Value = row[i];
            }

            currentCell.EntireColumn.AutoFit();
        }

        var location = $"{fileName}/{file}.xlsx";
        wb.SaveCopyAs(location);
        xl.Quit();
        return fileName;
    }
}