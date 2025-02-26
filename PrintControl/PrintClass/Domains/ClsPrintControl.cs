using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Models.Common;

namespace PrintControl.PrintClass.Domains;

public class ClsPrintControl
{
    private readonly ArrayList _arrColumnLefts = new(); //Used to save left coordinates of columns
    private readonly ArrayList _arrColumnWidths = new(); //Used to save column widths

    private bool _bFirstPage; //Used to check whether we are printing first page
    private bool _bNewPage; // Used to check whether we are printing a new page
    private int _iCellHeight;
    private int _iHeaderHeight;

    private int _iRow;
    private int _iTotalWidth; //

    private readonly StringFormat _strFormat = new();

    // OBJECT FOR THIS FORM
    public DataGridView RGrid { get; set; }

    public string ReportName { get; set; }
    public string AccPeriodDate { get; set; }
    public string CompanyAddress { get; set; }
    public string CompanyPanNo { get; set; }

    public string ReportDate { get; set; }

    // CUSTOM PRINT FUNCTION
    public void PrintFunctionAdditional()
    {
        for (var index = 0; index < RGrid.Columns.Count; index++)
        {
            var col = RGrid.Columns[index];
            if (!col.Visible)
            {
                RGrid.Columns.RemoveAt(index);
                continue;
            }

            col.DefaultCellStyle.Font = new Font("Bookman Old Style", 9, FontStyle.Bold);
        }

        for (var index = 0; index < RGrid.Columns.Count; index++)
        {
            var col = RGrid.Columns[index];
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        var function = new PrinterFunction
        {
            Title = ObjGlobal.LogInCompany,
            DocName = ReportName,
            PrintHeader = true,
            Footer = "",
            TitlePrint = PrinterFunction.PrintLocation.FirstOnly,
            EnableLogging = false,
            SubTitle = "",
            SubTitleAlignment = StringAlignment.Near,
            ColumnWidth = PrinterFunction.ColumnWidthSetting.Porportional,
            RowHeight = PrinterFunction.RowHeightSetting.DataHeight,
            PageNumberAlignment = StringAlignment.Center,
            PageSettings =
            {
                Margins = new Margins(20, 20, 100, 20)
            }
        };
        function.printDocument.DefaultPageSettings.Landscape = RGrid.Width > 1024;
        function.PageNumberPrint = PrinterFunction.PrintLocation.All;
        function.FooterAlignment = StringAlignment.Far;
        function.PrintPreviewNoDisplay(RGrid);
        //function.PrintDataGridView(RGrid);
    }

    //PRINT REPORT FUNCTION

    #region --------------- BEGIN PRINT EVENT HANDLER ---------------

    public void PrintDocument1_BeginPrint(object sender, CancelEventArgs e)
    {
        try
        {
            _arrColumnLefts.Clear();
            _arrColumnWidths.Clear();
            _iCellHeight = 0;
            _iRow = 0;
            _bFirstPage = true;
            _bNewPage = true;
            // Calculating Total Widths
            for (var index = 0; index < RGrid.Columns.Count; index++)
            {
                var col = RGrid.Columns[index];
                if (!col.Visible)
                {
                    RGrid.Columns.RemoveAt(index);
                    continue;
                }

                RGrid.Columns[index].DefaultCellStyle.Font = new Font("Bookman Old Style", 7, FontStyle.Bold);
                RGrid.Columns[index].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            _iTotalWidth = 0;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            ex.DialogResult();
        }
    }

    public void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
    {
        try
        {
            //Set the left margin
            var iLeftMargin = e.MarginBounds.Left;
            //Set the top margin
            var iTopMargin = e.MarginBounds.Top;
            //Whether more pages have to print or not
            var bMorePagesToPrint = false;
            for (var index = 0; index < RGrid.Columns.Count; index++)
            {
                RGrid.Columns[index].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                if (!RGrid.Columns[index].Visible) continue;
                var col = RGrid.Columns[index];
                _iTotalWidth += col.Width;
            }

            //For the first page to print set the cell width and header height
            if (_bFirstPage)
                foreach (DataGridViewColumn gridCol in RGrid.Columns)
                {
                    if (!gridCol.Visible) continue;
                    var iTmpWidth = (int)Math.Floor(gridCol.Width / (double)_iTotalWidth * _iTotalWidth *
                                                    (e.MarginBounds.Width / (double)_iTotalWidth));
                    if (gridCol.InheritedStyle != null)
                        _iHeaderHeight = (int)e.Graphics.MeasureString(gridCol.HeaderText,
                            new Font("Bookman Old Style", 7, FontStyle.Bold), iTmpWidth).Height + 11;
                    // Save width and height of headers
                    _arrColumnLefts.Add(iLeftMargin);
                    _arrColumnWidths.Add(iTmpWidth);
                    iLeftMargin += iTmpWidth;
                }

            //Loop till all the grid rows not get printed
            while (_iRow <= RGrid.Rows.Count - 1)
            {
                var gridRow = RGrid.Rows[_iRow];
                //Set the cell height
                _iCellHeight = gridRow.Height; //+ 5;
                var iCount = 0;
                //Check whether the current page settings allow more rows to print
                if (iTopMargin + _iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                {
                    _bNewPage = true;
                    _bFirstPage = false;
                    bMorePagesToPrint = true;
                    break;
                }

                if (_bNewPage)
                {
                    //Draw Header
                    var headerPosition = e.MarginBounds.Top - e.Graphics.MeasureString(ObjGlobal.LogInCompany,
                        new Font(RGrid.Font, FontStyle.Bold), e.MarginBounds.Width).Height - 13;
                    e.Graphics.DrawString(ObjGlobal.LogInCompany, new Font("Bookman Old Style", 14, FontStyle.Bold),
                        Brushes.Black, e.MarginBounds.Left, headerPosition + 10);
                    headerPosition += 25;
                    if (ObjGlobal.CompanyAddress.IsValueExits())
                    {
                        e.Graphics.DrawString(ObjGlobal.CompanyAddress,
                            new Font("Bookman Old Style", 8, FontStyle.Regular), Brushes.Black, e.MarginBounds.Left,
                            headerPosition + 10);
                        headerPosition += 20;
                    }

                    if (ObjGlobal.CompanyPanNo.IsValueExits())
                    {
                        e.Graphics.DrawString(ObjGlobal.CompanyPanNo,
                            new Font("Bookman Old Style", 8, FontStyle.Regular), Brushes.Black, e.MarginBounds.Left,
                            headerPosition + 10);
                        headerPosition += 20;
                    }

                    e.Graphics.DrawString(ReportName, new Font("Bookman Old Style", 11, FontStyle.Regular), Brushes.Red,
                        e.MarginBounds.Left, headerPosition + 10);
                    headerPosition += 20;
                    e.Graphics.DrawString(ReportDate, new Font("Bookman Old Style", 8, FontStyle.Regular),
                        Brushes.Black, e.MarginBounds.Left, headerPosition + 10);
                    headerPosition += 20;

                    //Draw Date
                    var xAxis = e.MarginBounds.Left + (e.MarginBounds.Width - e.Graphics
                        .MeasureString("Accounting Period", new Font("Bookman Old Style", 8, FontStyle.Bold),
                            e.MarginBounds.Width).Width);
                    var yAxis = e.MarginBounds.Top - e.Graphics.MeasureString("[ACCOUNTING PERIOD]",
                        new Font(new Font("Bookman Old Style", 8, FontStyle.Bold), FontStyle.Bold),
                        e.MarginBounds.Width).Height - 25;

                    e.Graphics.DrawString("[ACCOUNTING PERIOD]", new Font("Bookman Old Style", 10, FontStyle.Bold),
                        Brushes.Black, xAxis - 35, yAxis + 26);
                    xAxis += 20;
                    yAxis += 20;

                    e.Graphics.DrawString("[" + AccPeriodDate + "]",
                        new Font("Bookman Old Style", 8, FontStyle.Regular), Brushes.Black, xAxis - 70, yAxis + 25);

                    //Draw Columns
                    iTopMargin = e.MarginBounds.Top + headerPosition.GetInt() + 100;
                    _strFormat.Alignment = StringAlignment.Near;
                    foreach (DataGridViewColumn gridCol in RGrid.Columns)
                    {
                        if (!gridCol.Visible) continue;
                        _strFormat.Alignment = StringAlignment.Near;

                        e.Graphics.FillRectangle(new SolidBrush(gridCol.DefaultCellStyle.BackColor),
                            new Rectangle((int)_arrColumnLefts[iCount], iTopMargin, (int)_arrColumnWidths[iCount],
                                _iHeaderHeight + 10));
                        e.Graphics.DrawRectangle(Pens.LightGray,
                            new Rectangle((int)_arrColumnLefts[iCount], iTopMargin, (int)_arrColumnWidths[iCount],
                                _iHeaderHeight + 10));
                        if (gridCol.InheritedStyle != null)
                            e.Graphics.DrawString(gridCol.HeaderText,
                                new Font("Bookman Old Style", 7, FontStyle.Bold),
                                new SolidBrush(gridCol.InheritedStyle.ForeColor),
                                new RectangleF((int)_arrColumnLefts[iCount], iTopMargin, (int)_arrColumnWidths[iCount],
                                    _iHeaderHeight + 10), _strFormat);
                        iCount++;
                    }

                    _bNewPage = false;
                    iTopMargin += _iHeaderHeight + 10;
                }

                iCount = 0;
                //Draw Columns Contents
                foreach (DataGridViewCell cel in gridRow.Cells)
                {
                    if (!cel.Visible) continue;
                    if (cel.Value != null)
                    {
                        _strFormat.Alignment = cel.OwningColumn.DefaultCellStyle.Alignment switch
                        {
                            DataGridViewContentAlignment.MiddleRight => StringAlignment.Far,
                            DataGridViewContentAlignment.MiddleCenter => StringAlignment.Center,
                            _ => StringAlignment.Near
                        };
                        var style = cel.OwningRow.DefaultCellStyle.Font.Style;
                        e.Graphics.DrawString(cel.Value.ToString(),
                            new Font("Bookman Old Style", 7, style),
                            new SolidBrush(cel.InheritedStyle.ForeColor),
                            new RectangleF((int)_arrColumnLefts[iCount], iTopMargin + 10, (int)_arrColumnWidths[iCount],
                                _iCellHeight), _strFormat);
                    }

                    //Drawing Cells Borders
                    e.Graphics.DrawRectangle(Pens.LightGray,
                        new Rectangle((int)_arrColumnLefts[iCount], iTopMargin, (int)_arrColumnWidths[iCount],
                            _iCellHeight));
                    iCount++;
                }

                _iRow++;
                iTopMargin += _iCellHeight;
            }

            //If more lines exist, print another page.
            e.HasMorePages = bMorePagesToPrint;
        }
        catch (Exception exc)
        {
            exc.ToNonQueryErrorResult(exc.StackTrace);
            exc.DialogResult();
        }
    }

    #endregion --------------- BEGIN PRINT EVENT HANDLER ---------------
}