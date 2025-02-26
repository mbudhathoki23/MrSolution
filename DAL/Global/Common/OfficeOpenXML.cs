using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace MrDAL.Global.Common;

public sealed class OfficeOpenXml
{
    private static readonly Lazy<OfficeOpenXml> Instance = new(() => new OfficeOpenXml());

    private OfficeOpenXml()
    {
    }

    public static OfficeOpenXml GetInstance()
    {
        return Instance.Value;
    }

    public MemoryStream GetExcelStream(DataSet ds, bool firstRowAsHeader = false)
    {
        if (ds == null || ds.Tables.Count == 0) return null;

        var stream = new MemoryStream();
        using (var excel = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
        {
            //create doc and workbook
            var workbookPart = excel.AddWorkbookPart();
            var workbook = new Workbook();
            var sheets = new Sheets();
            //loop all tables in the dataset
            for (var iTable = 0; iTable < ds.Tables.Count; iTable++)
            {
                var table = ds.Tables[iTable];
                //create sheet part
                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                var worksheet = new Worksheet();
                var data = new SheetData();
                var allRows = new List<Row>();

                //setting header of the sheet
                var headerRow = new Row { RowIndex = 1 };
                for (var iColumn = 0; iColumn < table.Columns.Count; iColumn++)
                {
                    var col = table.Columns[iColumn];
                    //if first row of table is not the header then set columns of table as header of sheet
                    if (!firstRowAsHeader)
                        headerRow.Append(new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(col.ColumnName)
                        });
                    else
                        headerRow.Append(new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(Convert.ToString(table.Rows[0][col]))
                        });
                }

                allRows.Add(headerRow);

                //setting other data rows
                if (table.Rows != null && table.Rows.Count != 0)
                    for (var iRow = firstRowAsHeader ? 1 : 0; iRow < table.Rows.Count; iRow++)
                    {
                        var row = table.Rows[iRow];
                        var valueRow = new Row { RowIndex = (uint)(iRow + (firstRowAsHeader ? 1 : 2)) };

                        for (var iColumn = 0; iColumn < table.Columns.Count; iColumn++)
                        {
                            var col = table.Columns[iColumn];
                            valueRow.Append(new Cell
                            {
                                DataType = Format(col.DataType),
                                CellValue = new CellValue(Convert.ToString(row[col]))
                            });
                        }

                        allRows.Add(valueRow);
                    }

                //add rows to the data
                data.Append(allRows);
                worksheet.Append(data);
                worksheetPart.Worksheet = worksheet;
                worksheetPart.Worksheet.Save();

                //add worksheet to main sheets
                sheets.Append(new Sheet
                {
                    Name = string.IsNullOrWhiteSpace(table.TableName) ? "Sheet" + (iTable + 1) : table.TableName,
                    Id = workbookPart.GetIdOfPart(worksheetPart),
                    SheetId = (uint)iTable + 1
                });
            } //single table processing ends here

            //add created sheets to workbook
            workbook.Append(sheets);
            if (excel.WorkbookPart != null)
            {
                excel.WorkbookPart.Workbook = workbook;
                excel.WorkbookPart.Workbook.Save();
            }
            excel.Dispose();
        }

        stream.Seek(0, SeekOrigin.Begin);
        stream.Capacity = (int)stream.Length;
        return stream;
    }

    public MemoryStream GetExcelStream(DataTable dt, bool firstRowAsHeader = false)
    {
        var ds = new DataSet();
        var dtClone = dt.Clone();
        ds.Tables.Add(dtClone);
        return GetExcelStream(ds, firstRowAsHeader);
    }

    #region Excel Helpers

    private CellValues Format(Type t)
    {
        return t.ToString() switch
        {
            "System.String" => CellValues.String,
            "System.DateTime" => CellValues.Date,
            "System.Boolean" => CellValues.Boolean,
            "System.Int16" => CellValues.Number,
            "System.Int32" => CellValues.Number,
            "System.Int64" => CellValues.Number,
            "System.UInt16" => CellValues.Number,
            "System.UInt32" => CellValues.Number,
            "System.UInt64" => CellValues.Number,
            "System.Decimal" => CellValues.Number,
            "System.Double" => CellValues.Number,
            "System.Single" => CellValues.Number,
            _ => CellValues.String
        };
    }

    #endregion Excel Helpers
}