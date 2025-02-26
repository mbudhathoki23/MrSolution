using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

//[module:CLSCompliant(true)]
namespace MrDAL.Models.Common;
//AllocationRequest

#region Supporting Classes

#endregion Supporting Classes

/// <summary>
///     Data Grid View Printer. Print functions for a datagridview, since MS
///     didn't see fit to do it.
/// </summary>
public class PrinterFunction
{
    public enum Alignment
    {
        NotSet,
        Left,
        Right,
        Center
    }

    public enum Location
    {
        Header,
        Footer,
        Absolute
    }

    public enum PrintLocation
    {
        All,
        FirstOnly,
        LastOnly,
        None
    }

    public enum SizeType
    {
        CellSize,
        StringSize,
        Porportional
    }

    //---------------------------------------------------------------------
    // Constructor
    //---------------------------------------------------------------------
    /// <summary>
    ///     Constructor for DGVPrinter
    /// </summary>
    public PrinterFunction()
    {
        // create print document
        printDocument = new PrintDocument();
        //printDoc.PrintPage += new PrintPageEventHandler(PrintPageEventHandler);
        //printDoc.BeginPrint += new PrintEventHandler(BeginPrintEventHandler);
        PrintMargins = new Margins(5, 5, 15, 5);

        // awlays print the datagridview in a4 paper size
        var paperSizes = new PrinterSettings().PaperSizes.Cast<PaperSize>();
        var sizeA4 = paperSizes.First(size => size.Kind == PaperKind.A4); // setting paper size to A4 size
        printDocument.DefaultPageSettings.PaperSize = sizeA4;

        // set default fonts
        PageNumberFont = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point);
        PageNumberColor = Color.Black;
        TitleFont = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);
        TitleColor = Color.Black;
        SubTitleFont = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point);
        SubTitleColor = Color.Black;
        FooterFont = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point);
        FooterColor = Color.Black;

        // default spacing
        TitleSpacing = 0;
        SubTitleSpacing = 0;
        FooterSpacing = 0;

        // Create string formatting objects
        buildstringformat(ref titleformat, null, StringAlignment.Center, StringAlignment.Center,
            StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip, StringTrimming.Word);
        buildstringformat(ref subtitleformat, null, StringAlignment.Center, StringAlignment.Center,
            StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip, StringTrimming.Word);
        buildstringformat(ref footerformat, null, StringAlignment.Center, StringAlignment.Center,
            StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip, StringTrimming.Word);
        buildstringformat(ref pagenumberformat, null, StringAlignment.Far, StringAlignment.Center,
            StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip, StringTrimming.Word);

        // Set these formatting objects to null to flag whether or not they were set by the caller
        columnheadercellformat = null;
        rowheadercellformat = null;
        cellformat = null;

        // Print Preview properties
        Owner = null;
        PrintPreviewZoom = 1.0;

        // Deprecated properties - retain for backwards compatibility
        HeaderCellAlignment = StringAlignment.Near;
        HeaderCellFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
        CellAlignment = StringAlignment.Near;
        CellFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
    }

    //---------------------------------------------------------------------
    //---------------------------------------------------------------------
    // Primary Interface - Presents a dialog and then prints or previews the
    // indicated data grid view
    //---------------------------------------------------------------------
    //---------------------------------------------------------------------

    /// <summary>
    ///     Start the printing process, print to a printer.
    /// </summary>
    /// <param name="dgv">The DataGridView to print</param>
    /// NOTE: Any changes to this method also need to be done in PrintPreviewDataGridView
    public void PrintDataGridView(DataGridView dgv)
    {
        if (EnableLogging) Logger.LogInfoMsg("PrintDataGridView process started");
        if (null == dgv) throw new Exception("Null Parameter passed to DGVPrinter.");
        if (!typeof(DataGridView).IsInstanceOfType(dgv))
            throw new Exception("Invalid Parameter passed to DGVPrinter.");

        // save the datagridview we're printing
        this.dgv = dgv;

        // display dialog and print
        if (DialogResult.OK == DisplayPrintDialog()) PrintNoDisplay(dgv);
    }

    /// <summary>
    ///     Start the printing process, print to a print preview dialog
    /// </summary>
    /// <param name="dgv">The DataGridView to print</param>
    /// NOTE: Any changes to this method also need to be done in PrintDataGridView
    public void PrintPreviewDataGridView(DataGridView dgv)
    {
        if (EnableLogging) Logger.LogInfoMsg("PrintPreviewDataGridView process started");
        if (null == dgv) throw new Exception("Null Parameter passed to DGVPrinter.");
        if (!typeof(DataGridView).IsInstanceOfType(dgv))
            throw new Exception("Invalid Parameter passed to DGVPrinter.");

        // save the datagridview we're printing
        this.dgv = dgv;

        // display dialog and print
        if (DialogResult.OK == DisplayPrintDialog()) PrintPreviewNoDisplay(dgv);
    }

    //---------------------------------------------------------------------
    //---------------------------------------------------------------------
    // Alternative Interface. In order to set the print information correctly
    // either the DisplayPrintDialog() routine must be called, OR the
    // PrintDocument (and PrinterSettings) must be Handled through calling
    // PrintDialog separately.
    //
    // Once the PrintDocument has been setup, the PrintNoDisplay() and/or
    // PrintPreviewNoDisplay() routines can be called to print multiple
    // DataGridViews using the same print setup.
    //---------------------------------------------------------------------
    //---------------------------------------------------------------------

    /// <summary>
    ///     Display a printdialog and return the result. Either this method or
    ///     the equivalent must be done prior to calling either of the PrintNoDisplay
    ///     or PrintPreviewNoDisplay methods.
    /// </summary>
    /// <returns></returns>
    public DialogResult DisplayPrintDialog()
    {
        if (EnableLogging) Logger.LogInfoMsg("DisplayPrintDialog process started");
        // create new print dialog and set options
        var pd = new PrintDialog();
        pd.UseEXDialog = PrintDialogSettings.UseEXDialog;
        pd.AllowSelection = PrintDialogSettings.AllowSelection;
        pd.AllowSomePages = PrintDialogSettings.AllowSomePages;
        pd.AllowCurrentPage = PrintDialogSettings.AllowCurrentPage;
        pd.AllowPrintToFile = PrintDialogSettings.AllowPrintToFile;
        pd.ShowHelp = PrintDialogSettings.ShowHelp;
        pd.ShowNetwork = PrintDialogSettings.ShowNetwork;

        //// setup print dialog with internal setttings
        pd.Document = printDocument;
        if (!string.IsNullOrEmpty(PrinterName))
            printDocument.PrinterSettings.PrinterName = PrinterName;

        // show the dialog and display the result
        return pd.ShowDialog();
    }

    /// <summary>
    ///     Print the provided grid view. Either DisplayPrintDialog() or it's equivalent
    ///     setup must be completed prior to calling this routine
    /// </summary>
    /// <param name="dgv"></param>
    public void PrintNoDisplay(DataGridView dgv)
    {
        if (EnableLogging) Logger.LogInfoMsg("PrintNoDisplay process started");
        if (null == dgv) throw new Exception("Null Parameter passed to DGVPrinter.");
        if (!(dgv is DataGridView))
            throw new Exception("Invalid Parameter passed to DGVPrinter.");

        // save the grid we're printing
        this.dgv = dgv;

        printDocument.PrintPage += PrintPageEventHandler;
        printDocument.BeginPrint += BeginPrintEventHandler;

        // setup and do printing
        SetupPrint();
        printDocument.Print();
    }

    /// <summary>
    ///     Preview the provided grid view. Either DisplayPrintDialog() or it's equivalent
    ///     setup must be completed prior to calling this routine
    /// </summary>
    /// <param name="dgv"></param>
    public void PrintPreviewNoDisplay(DataGridView dgv)
    {
        if (EnableLogging) Logger.LogInfoMsg("PrintPreviewNoDisplay process started");
        if (null == dgv) throw new Exception("Null Parameter passed to DGVPrinter.");
        if (!(dgv is DataGridView))
            throw new Exception("Invalid Parameter passed to DGVPrinter.");

        // save the grid we're printing
        this.dgv = dgv;

        printDocument.PrintPage += PrintPageEventHandler;
        printDocument.BeginPrint += BeginPrintEventHandler;

        // display the preview dialog
        SetupPrint();

        // if the caller hasn't provided a print preview dialog, then create one
        if (null == PreviewDialog)
            PreviewDialog = new PrintPreviewDialog();

        // set up dialog for preview
        PreviewDialog.Document = printDocument;
        PreviewDialog.UseAntiAlias = true;
        PreviewDialog.Owner = Owner;
        PreviewDialog.PrintPreviewControl.Zoom = PrintPreviewZoom;
        PreviewDialog.Width = PreviewDisplayWidth();
        PreviewDialog.Height = PreviewDisplayHeight();

        if (null != PreviewDialogIcon)
            PreviewDialog.Icon = PreviewDialogIcon;

        // show the dialog
        PreviewDialog.ShowDialog();
    }

    //---------------------------------------------------------------------
    //---------------------------------------------------------------------
    // Print Process Interface Methods
    //---------------------------------------------------------------------
    //---------------------------------------------------------------------

    // NOTE: This is retained only for backward compatibility, and should
    // not be used for printing grid views that might be larger than the
    // input print area.
    public bool EmbeddedPrint(DataGridView dgv, Graphics g, Rectangle area)
    {
        if (EnableLogging) Logger.LogInfoMsg("EmbeddedPrint process started");
        // verify we've been set up properly
        if (null == dgv)
            throw new Exception("Null Parameter passed to DGVPrinter.");

        // set the embedded print flag
        EmbeddedPrinting = true;

        // save the grid we're printing
        this.dgv = dgv;

        //-----------------------------------------------------------------
        // Force setting for embedded printing
        //-----------------------------------------------------------------

        // set margins so we print within the provided area
        var saveMargins = PrintMargins;
        PrintMargins.Top = area.Top;
        PrintMargins.Bottom = 0;
        PrintMargins.Left = area.Left;
        PrintMargins.Right = 0;

        // set "page" height and width to our destination area
        pageHeight = area.Height + area.Top;
        printWidth = area.Width;
        pageWidth = area.Width + area.Left;

        // force 'off' header and footer
        PrintHeader = false;
        PrintFooter = false;
        PageNumbers = false;

        //-----------------------------------------------------------------
        // Determine what's going to be printed and set the columns to print
        //-----------------------------------------------------------------
        SetupPrint();

        //-----------------------------------------------------------------
        // Do a single "Print" and return false - we're just printing what
        // we can in the space provided.
        //-----------------------------------------------------------------
        PrintPage(g);
        return false;
    }

    public void EmbeddedPrintMultipageSetup(DataGridView dgv, Rectangle area)
    {
        if (EnableLogging) Logger.LogInfoMsg("EmbeddedPrintMultipageSetup process started");
        // verify we've been set up properly
        if (null == dgv)
            throw new Exception("Null Parameter passed to DGVPrinter.");

        // set the embedded print flag
        EmbeddedPrinting = true;

        // save the grid we're printing
        this.dgv = dgv;

        //-----------------------------------------------------------------
        // Force setting for embedded printing
        //-----------------------------------------------------------------

        // set margins so we print within the provided area
        var saveMargins = PrintMargins;
        PrintMargins.Top = area.Top;
        PrintMargins.Bottom = 0;
        PrintMargins.Left = area.Left;
        PrintMargins.Right = 0;

        // set "page" height and width to our destination area
        pageHeight = area.Height + area.Top;
        printWidth = area.Width;
        pageWidth = area.Width + area.Left;

        // force 'off' header and footer
        PrintHeader = false;
        PrintFooter = false;
        PageNumbers = false;

        //-----------------------------------------------------------------
        // Determine what's going to be printed and set the columns to print
        //-----------------------------------------------------------------
        SetupPrint();
    }

    /// <summary>
    ///     BeginPrint Event Handler
    ///     Set values at start of print run
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BeginPrintEventHandler(object sender, CancelEventArgs e)
    {
        if (EnableLogging) Logger.LogInfoMsg("BeginPrintEventHandler called. Printing started.");
        // reset counters since we'll go through this twice if we print from preview
        currentpageset = 0;
        lastrowprinted = -1;
        CurrentPage = 0;
    }

    /// <summary>
    ///     PrintPage event handler. This routine prints one page. It will
    ///     skip non-printable pages if the user selected the "some pages" option
    ///     on the print dialog.
    /// </summary>
    /// <param name="sender">default object from windows</param>
    /// <param name="e">Event info from Windows about the printing</param>
    public void PrintPageEventHandler(object sender, PrintPageEventArgs e)
    {
        if (EnableLogging) Logger.LogInfoMsg("PrintPageEventHandler called. Printing a page.");
        e.HasMorePages = PrintPage(e.Graphics);
    }

    //---------------------------------------------------------------------
    //---------------------------------------------------------------------
    // Internal Methods
    //---------------------------------------------------------------------
    //---------------------------------------------------------------------

    /// <summary>
    ///     Set up the print job. Save information from print dialog
    ///     and print document for easy access. Also sets up the rows
    ///     and columns that will be printed. At this point, we're
    ///     collecting all columns in colstoprint. This will be broken
    ///     up into pagesets later on
    /// </summary>
    private void SetupPrint()
    {
        if (EnableLogging)
        {
            Logger.LogInfoMsg("SetupPrint process started");
            var m = printDocument.DefaultPageSettings.Margins;
            Logger.LogInfoMsg($"Initial Printer Margins are {m.Left}, {m.Right}, {m.Top}, {m.Bottom}");
        }

        PrintColumnHeaders ??= dgv.ColumnHeadersVisible;

        PrintRowHeaders ??= dgv.RowHeadersVisible;

        // Set the default row header style where we don't have an override
        // and we do have rows
        if (null == RowHeaderCellStyle && 0 != dgv.Rows.Count)
            RowHeaderCellStyle = dgv.Rows[0].HeaderCell.InheritedStyle;

        /* Functionality to come - redo of styling
        foreach (DataGridViewColumn col in dgv.Columns)
        {
            // Set the default column styles where we've not been given an override
            if (!ColumnStyles.ContainsKey(col.Name))
                ColumnStyles[col.Name] = dgv.Columns[col.Name].InheritedStyle;

            // Set the default column header styles where we don't have an override
            if (!ColumnHeaderStyles.ContainsKey(col.Name))
                ColumnHeaderStyles[col.Name] = dgv.Columns[col.Name].HeaderCell.InheritedStyle;
        }
        */

        //-----------------------------------------------------------------
        // Set row and column headercell and normal cell print formats if they were not
        // explicitly set by the caller
        //-----------------------------------------------------------------
        if (null == columnheadercellformat)
            buildstringformat(ref columnheadercellformat, dgv.Columns[0].HeaderCell.InheritedStyle,
                HeaderCellAlignment, StringAlignment.Near, HeaderCellFormatFlags,
                StringTrimming.Word);
        if (null == rowheadercellformat)
            buildstringformat(ref rowheadercellformat, RowHeaderCellStyle,
                HeaderCellAlignment, StringAlignment.Near, HeaderCellFormatFlags,
                StringTrimming.Word);
        if (null == cellformat)
            buildstringformat(ref cellformat, dgv.DefaultCellStyle,
                CellAlignment, StringAlignment.Near, CellFormatFlags,
                StringTrimming.Word);

        //-----------------------------------------------------------------
        // get info on the limits of the printer's actual print area available. Convert
        // to int's to work with margins.
        //
        // note: do this only if we're not doing embedded printing.
        //-----------------------------------------------------------------

        if (!EmbeddedPrinting)
        {
            int printareawidth;
            var hardx = (int)Math.Round(printDocument.DefaultPageSettings.HardMarginX);
            var hardy = (int)Math.Round(printDocument.DefaultPageSettings.HardMarginY);
            if (printDocument.DefaultPageSettings.Landscape)
                printareawidth = (int)Math.Round(printDocument.DefaultPageSettings.PrintableArea.Height);
            else
                printareawidth = (int)Math.Round(printDocument.DefaultPageSettings.PrintableArea.Width);

            //-----------------------------------------------------------------
            // set the print area we're working within
            //-----------------------------------------------------------------

            pageHeight = printDocument.DefaultPageSettings.Bounds.Height;
            pageWidth = printDocument.DefaultPageSettings.Bounds.Width;

            //-----------------------------------------------------------------
            // Set the printable area: margins and pagewidth
            //-----------------------------------------------------------------

            // Set initial printer margins
            PrintMargins = printDocument.DefaultPageSettings.Margins;

            // adjust for when the margins are less than the printer's hard x/y limits
            PrintMargins.Right = hardx > PrintMargins.Right ? hardx : PrintMargins.Right;
            PrintMargins.Left = hardx > PrintMargins.Left ? hardx : PrintMargins.Left;
            PrintMargins.Top = hardy > PrintMargins.Top ? hardy : PrintMargins.Top;
            PrintMargins.Bottom = hardy > PrintMargins.Bottom ? hardy : PrintMargins.Bottom;

            // Now, we can calc default print width, again, respecting the printer's limitations
            printWidth = pageWidth - PrintMargins.Left - PrintMargins.Right;
            printWidth = printWidth > printareawidth ? printareawidth : printWidth;

            // log margin changes
            if (EnableLogging)
            {
                Logger.LogInfoMsg($"Printer 'Hard' X limit is {hardx} and 'Hard' Y limit is {hardy}");
                Logger.LogInfoMsg(
                    $"Printer height limit is {pageHeight} and width limit is {pageWidth}, print width is {printWidth}");
                Logger.LogInfoMsg(
                    $"Final overall margins are {PrintMargins.Left}, {PrintMargins.Right}, {PrintMargins.Top}, {PrintMargins.Bottom}");
                Logger.LogInfoMsg($"Table Alignment is {TableAlignment.ToString()}");
            }
        }

        //-----------------------------------------------------------------
        // Figure out which pages / rows to print
        //-----------------------------------------------------------------

        // save print range
        printRange = printDocument.PrinterSettings.PrintRange;
        if (EnableLogging) Logger.LogInfoMsg($"PrintRange is {printRange}");

        // pages to print handles "some pages" option
        if (PrintRange.SomePages == printRange)
        {
            // set limits to only print some pages
            fromPage = printDocument.PrinterSettings.FromPage;
            toPage = printDocument.PrinterSettings.ToPage;
        }
        else
        {
            // set extremes so that we'll print all pages
            fromPage = 0;
            toPage = maxPages;
        }

        //-----------------------------------------------------------------
        // Determine what's going to be printed
        //-----------------------------------------------------------------
        SetupPrintRange();

        //-----------------------------------------------------------------
        // Set up width overrides and fixed columns
        //-----------------------------------------------------------------
        SetupColumns();

        //-----------------------------------------------------------------
        // Now that we know what we're printing, measure the print area and
        // count the pages.
        //-----------------------------------------------------------------

        // Measure the print area
        measureprintarea(printDocument.PrinterSettings.CreateMeasurementGraphics());

        // Count the pages
        totalpages = Pagination();
    }

    /// <summary>
    ///     Set up width override and fixed columns lists
    /// </summary>
    private void SetupColumns()
    {
        // identify fixed columns by their column number in the print list
        foreach (var colname in FixedColumns)
            try
            {
                fixedcolumns.Add(GetColumnIndex(colname));
            }
            catch // (Exception ex)
            {
                // missing column, so add it to print list and retry
                colstoprint.Add(dgv.Columns[colname]);
                fixedcolumns.Add(GetColumnIndex(colname));
            }

        // Adjust override list to have the same number of entries as colstoprint,
        foreach (DataGridViewColumn col in colstoprint)
            if (ColumnWidths.ContainsKey(col.Name))
                colwidthsoverride.Add(ColumnWidths[col.Name]);
            else
                colwidthsoverride.Add(-1);
    }

    /// <summary>
    ///     Determine the print range based on dialog selections and user input. The rows
    ///     and columns are sorted to ensure that the rows appear in their correct index
    ///     order and the columns appear in DisplayIndex order to account for added columns
    ///     and re-ordered columns.
    /// </summary>
    private void SetupPrintRange()
    {
        //-----------------------------------------------------------------
        // set up the rows and columns to print
        //
        // Note: The "Selectedxxxx" lists in the datagridview are 'stacks' that
        //  have the selected items pushed in the *in the order they were selected*
        //  i.e. not the order you want to print them in!
        //-----------------------------------------------------------------
        SortedList temprowstoprint = null;
        SortedList tempcolstoprint = null;

        // rows to print (handles "selection" and "current page" options
        if (PrintRange.Selection == printRange)
        {
            temprowstoprint = new SortedList(dgv.SelectedCells.Count);
            tempcolstoprint = new SortedList(dgv.SelectedCells.Count);

            //if DGV has rows selected, it's easy, selected rows and all visible columns
            if (0 != dgv.SelectedRows.Count)
            {
                temprowstoprint = new SortedList(dgv.SelectedRows.Count);
                tempcolstoprint = new SortedList(dgv.Columns.Count);

                // sort the rows into index order
                temprowstoprint = new SortedList(dgv.SelectedRows.Count);
                foreach (DataGridViewRow row in dgv.SelectedRows)
                    if (row.Visible && !row.IsNewRow)
                        temprowstoprint.Add(row.Index, row);

                // sort the columns into display order
                foreach (DataGridViewColumn col in dgv.Columns)
                    if (col.Visible)
                        tempcolstoprint.Add(col.DisplayIndex, col);
            }
            // if selected columns, then all rows, and selected columns
            else if (0 != dgv.SelectedColumns.Count)
            {
                temprowstoprint = new SortedList(dgv.Rows.Count);
                tempcolstoprint = new SortedList(dgv.SelectedColumns.Count);

                foreach (DataGridViewRow row in dgv.Rows)
                    if (row.Visible && !row.IsNewRow)
                        temprowstoprint.Add(row.Index, row);

                foreach (DataGridViewColumn col in dgv.SelectedColumns)
                    if (col.Visible)
                        tempcolstoprint.Add(col.DisplayIndex, col);
            }
            // we just have a bunch of selected cells so we have to do some work
            else
            {
                // set up sorted lists. the selectedcells method does not guarantee
                // that the cells will always be in left-right top-bottom order.
                temprowstoprint = new SortedList(dgv.SelectedCells.Count);
                tempcolstoprint = new SortedList(dgv.SelectedCells.Count);

                // for each selected cell, add unique rows and columns
                int displayindex, colindex, rowindex;
                foreach (DataGridViewCell cell in dgv.SelectedCells)
                {
                    displayindex = cell.OwningColumn.DisplayIndex;
                    colindex = cell.ColumnIndex;
                    rowindex = cell.RowIndex;

                    // add unique rows
                    if (!temprowstoprint.Contains(rowindex))
                    {
                        var row = dgv.Rows[rowindex];
                        if (row.Visible && !row.IsNewRow)
                            temprowstoprint.Add(rowindex, dgv.Rows[rowindex]);
                    }

                    // add unique columns
                    if (!tempcolstoprint.Contains(displayindex))
                        tempcolstoprint.Add(displayindex, dgv.Columns[colindex]);
                }
            }
        }
        // if current page was selected, print visible columns for the
        // displayed rows
        else if (PrintRange.CurrentPage == printRange)
        {
            // create lists
            temprowstoprint = new SortedList(dgv.DisplayedRowCount(true));
            tempcolstoprint = new SortedList(dgv.Columns.Count);

            // select all visible rows on displayed page
            for (var i = dgv.FirstDisplayedScrollingRowIndex;
                 i < dgv.FirstDisplayedScrollingRowIndex + dgv.DisplayedRowCount(true);
                 i++)
            {
                var row = dgv.Rows[i];
                if (row.Visible) temprowstoprint.Add(row.Index, row);
            }

            // select all visible columns
            foreach (DataGridViewColumn col in dgv.Columns)
                if (col.Visible)
                    tempcolstoprint.Add(col.DisplayIndex, col);
        }
        // this is the default for print all - everything marked visible will be printed
        // this is also used when printing specific pages or page ranges as we won't know
        // what to print until we size all the rows
        else
        {
            temprowstoprint = new SortedList(dgv.Rows.Count);
            tempcolstoprint = new SortedList(dgv.Columns.Count);

            // select all visible rows and all visible columns - but don't include the new 'data entry row'
            foreach (DataGridViewRow row in dgv.Rows)
                if (row.Visible && !row.IsNewRow)
                    temprowstoprint.Add(row.Index, row);

            // sort the columns into display order
            foreach (DataGridViewColumn col in dgv.Columns)
                if (col.Visible)
                    tempcolstoprint.Add(col.DisplayIndex, col);
        }

        // move rows and columns into global containers
        rowstoprint = new List<rowdata>(temprowstoprint.Count);
        foreach (var item in temprowstoprint.Values) rowstoprint.Add(new rowdata { row = (DataGridViewRow)item });

        colstoprint = new List<DataGridViewColumn>(tempcolstoprint.Count);
        foreach (var item in tempcolstoprint.Values) colstoprint.Add(item);

        // remove "hidden" columns from list of columns to print
        foreach (var columnname in HideColumns) colstoprint.Remove(dgv.Columns[columnname]);

        if (EnableLogging) Logger.LogInfoMsg($"Grid Printout Range is {colstoprint.Count} columns");
        if (EnableLogging) Logger.LogInfoMsg($"Grid Printout Range is {rowstoprint.Count} rows");
    }

    /// <summary>
    ///     Centralize the string format settings. Build a string format object
    ///     using passed in settings, (allowing a user override of a single setting)
    ///     and get the alignment from the cell control style.
    /// </summary>
    /// <param name="format">String format, ref parameter with return settings</param>
    /// <param name="controlstyle">DataGridView style to apply (if available)</param>
    /// <param name="alignment">Override text Alignment</param>
    /// <param name="linealignment">Override line alignment</param>
    /// <param name="flags">String format flags</param>
    /// <param name="trim">Override string trimming flags</param>
    /// <returns></returns>
    private void buildstringformat(ref StringFormat format, DataGridViewCellStyle controlstyle,
        StringAlignment alignment, StringAlignment linealignment, StringFormatFlags flags, StringTrimming trim)
    {
        // allocate format if it doesn't already exist
        if (null == format)
            format = new StringFormat();

        // Set defaults
        format.Alignment = alignment;
        format.LineAlignment = linealignment;
        format.FormatFlags = flags;
        format.Trimming = trim;

        // Check on right-to-left flag. This is set at the grid level, but doesn't show up
        // as a cell format. Urgh.
        if (null != dgv && RightToLeft.Yes == dgv.RightToLeft)
            format.FormatFlags |= StringFormatFlags.DirectionRightToLeft;

        // use cell alignment to override defaulted alignments
        if (null != controlstyle)
        {
            // Adjust the format based on the control settings, bias towards centered
            var cellalign = controlstyle.Alignment;
            if (cellalign.ToString().Contains("Center")) format.Alignment = StringAlignment.Center;
            else if (cellalign.ToString().Contains("Left")) format.Alignment = StringAlignment.Near;
            else if (cellalign.ToString().Contains("Right")) format.Alignment = StringAlignment.Far;

            if (cellalign.ToString().Contains("Top")) format.LineAlignment = StringAlignment.Near;
            else if (cellalign.ToString().Contains("Middle")) format.LineAlignment = StringAlignment.Center;
            else if (cellalign.ToString().Contains("Bottom")) format.LineAlignment = StringAlignment.Far;
        }
    }

    /// <summary>
    ///     Calculate cell size based on data versus size settings
    /// </summary>
    /// <param name="g">Current graphics context</param>
    /// <param name="cell">Cell being measured</param>
    /// <param name="index">Column index of cell being measured</param>
    /// <param name="cellstyle">Computed Style of cell being measured</param>
    /// <param name="basewidth">Initial width for size calculation</param>
    /// <param name="format">Computed string format for cell data</param>
    /// <returns>Size of printed cell</returns>
    private SizeF CalculateCellSize(Graphics g, DataGridViewCell cell, DataGridViewCellStyle cellstyle, float basewidth,
        float overridewidth, StringFormat format)
    {
        // Start with the grid view cell size
        var size = new SizeF(cell.Size);

        // If we need to do any calculated cell sizes, we need to measure the cell contents
        if (RowHeightSetting.DataHeight == RowHeight ||
            ColumnWidthSetting.DataWidth == ColumnWidth ||
            ColumnWidthSetting.Porportional == ColumnWidth)
        {
            SizeF datasize;

            //-------------------------------------------------------------
            // Measure cell contents
            //-------------------------------------------------------------
            if ("DataGridViewImageCell" == dgv.Columns[cell.ColumnIndex].CellType.Name
                && ("Image" == cell.ValueType.Name || "Byte[]" == cell.ValueType.Name))
            {
                // image to measure
                Image img;

                // if we don't actually have a value, then just exit with a minimum size.
                if (null == cell.Value || typeof(DBNull) == cell.Value.GetType())
                    return new SizeF(1, 1);

                // Check on type of image cell value - may not be an actual "image" type
                if ("Image" == cell.ValueType.Name || "Object" == cell.ValueType.Name)
                {
                    // if it's an "image" type, then load it directly
                    img = (Image)cell.Value;
                }
                else if ("Byte[]" == cell.ValueType.Name)
                {
                    // if it's not an "image" type (i.e. loaded from a database to a bound column)
                    // convert the underlying byte array to an image
                    var ic = new ImageConverter();
                    img = (Image)ic.ConvertFrom((byte[])cell.Value);
                }
                else
                {
                    throw new Exception(
                        $"Unknown image cell underlying type: {cell.ValueType.Name} in column {cell.ColumnIndex}");
                }

                // size to print is size of image
                datasize = img.Size;
            }
            else
            {
                var width = -1 != overridewidth ? overridewidth : basewidth;

                // measure the data for each column, keep widths and biggest height
                datasize = g.MeasureString(cell.EditedFormattedValue.ToString(), cellstyle.Font,
                    new SizeF(width, maxPages), format);

                // if we have excessively large cell, limit it to one page width
                if (printWidth < datasize.Width)
                    datasize = g.MeasureString(cell.FormattedValue.ToString(), cellstyle.Font,
                        new SizeF(pageWidth - cellstyle.Padding.Left - cellstyle.Padding.Right, maxPages),
                        format);
            }

            //-------------------------------------------------------------
            // Add in padding for data based cell sizes and porportional columns
            //-------------------------------------------------------------

            // set cell height to string height if indicated
            if (RowHeightSetting.DataHeight == RowHeight)
                size.Height = datasize.Height + cellstyle.Padding.Top + cellstyle.Padding.Bottom;

            // set cell width to calculated width if indicated
            if (ColumnWidthSetting.DataWidth == ColumnWidth ||
                ColumnWidthSetting.Porportional == ColumnWidth)
                size.Width = datasize.Width + cellstyle.Padding.Left + cellstyle.Padding.Right;
        }

        return size;
    }

    /// <summary>
    ///     Recalculate row heights for cells whose width is greater than the set column width.
    ///     Called when column widths are changed in order to flow text down the page instead of
    ///     accross.
    /// </summary>
    /// <param name="g">Graphics Context for measuring image columns</param>
    /// <param name="colindex">column index in colstoprint</param>
    /// <param name="newcolwidth">new column width</param>
    private void RecalcRowHeights(Graphics g, int colindex, float newcolwidth)
    {
        DataGridViewCell cell = null;

        // search calculated cell sizes for widths larger than our new width
        foreach (var t in rowstoprint)
        {
            cell = t.row.Cells[((DataGridViewColumn)colstoprint[colindex]).Index];

            var sizeHeight = 0F;
            if (RowHeightSetting.DataHeight == RowHeight)
            {
                StringFormat format = null;
                // get column style
                var viewCellStyle = GetStyle(t.row, (DataGridViewColumn)colstoprint[colindex]);

                // build the cell style and font
                buildstringformat(ref format, viewCellStyle, cellformat.Alignment, cellformat.LineAlignment,
                    cellformat.FormatFlags, cellformat.Trimming);

                // recalculate cell size using new width. This will flow data down the page and
                // change the row height
                var size = CalculateCellSize(g, cell, viewCellStyle, newcolwidth, colwidthsoverride[colindex], format);
                sizeHeight = size.Height;
            }
            else
            {
                sizeHeight = cell.Size.Height;
            }

            // change the saved row height based on the recalculated size
            t.height = t.height < sizeHeight ? sizeHeight : t.height;
        }
    }

    /// <summary>
    ///     Scan all the rows and columns to be printed and calculate the
    ///     overall individual column width (based on largest column value),
    ///     the header sizes, and determine all the row heights.
    /// </summary>
    /// <param name="g">The graphics context for all measurements</param>
    private void measureprintarea(Graphics g)
    {
        int i, j;
        colwidths = new List<float>(colstoprint.Count);
        footerHeight = 0;

        // temp variables
        DataGridViewColumn col;

        //-----------------------------------------------------------------
        // measure the page headers and footers, including the grid column header cells
        //-----------------------------------------------------------------

        // set initial column sizes based on column titles
        for (i = 0; i < colstoprint.Count; i++)
        {
            col = (DataGridViewColumn)colstoprint[i];

            //-------------------------------------------------------------
            // Build String format and Cell style
            //-------------------------------------------------------------

            // get gridview style, and override if we have a set style for this column
            StringFormat currentformat = null;
            var headercolstyle = col.HeaderCell.InheritedStyle.Clone();
            if (ColumnHeaderStyles.ContainsKey(col.Name))
            {
                headercolstyle = ColumnHeaderStyles[col.Name];

                // build the cell style and font
                buildstringformat(ref currentformat, headercolstyle, cellformat.Alignment, cellformat.LineAlignment,
                    cellformat.FormatFlags, cellformat.Trimming);
            }
            else if (col.HasDefaultCellStyle)
            {
                // build the cell style and font
                buildstringformat(ref currentformat, headercolstyle, cellformat.Alignment, cellformat.LineAlignment,
                    cellformat.FormatFlags, cellformat.Trimming);
            }
            else
            {
                currentformat = columnheadercellformat;
            }

            //-------------------------------------------------------------
            // Calculate and accumulate column header width and height
            //-------------------------------------------------------------
            SizeF size = col.HeaderCell.Size;

            // deal with overridden col widths
            float usewidth = 0;
            if (0 <= colwidthsoverride[i])
            //usewidth = colwidthsoverride[i];
            {
                colwidths.Add(colwidthsoverride[i]); // override means set that size
            }
            else if (ColumnWidthSetting.CellWidth == ColumnWidth || ColumnWidthSetting.Porportional == ColumnWidth)
            {
                usewidth = col.HeaderCell.Size.Width;
                // calculate the size of column header cells
                size = CalculateCellSize(g, col.HeaderCell, headercolstyle, usewidth, colwidthsoverride[i],
                    columnheadercellformat);
                colwidths.Add(col.Width); // otherwise use the data width
            }
            else
            {
                usewidth = printWidth;
                // calculate the size of column header cells
                size = CalculateCellSize(g, col.HeaderCell, headercolstyle, usewidth, colwidthsoverride[i],
                    columnheadercellformat);
                colwidths.Add(size.Width);
            }

            // accumulate heights, saving largest for data sized option
            if (RowHeightSetting.DataHeight == RowHeight)
                colheaderheight = colheaderheight < size.Height ? size.Height : colheaderheight;
            else
                colheaderheight = col.HeaderCell.Size.Height;
        }

        //-----------------------------------------------------------------
        // measure the page number
        //-----------------------------------------------------------------

        if (PageNumbers)
            pagenumberHeight = g.MeasureString("Page", PageNumberFont, printWidth, pagenumberformat).Height;

        //-----------------------------------------------------------------
        // Calc height of header.
        // Header height is height of page number, title, subtitle and height of column headers
        //-----------------------------------------------------------------
        if (PrintHeader)
        {
            // calculate title and subtitle heights
            titleheight = g.MeasureString(title, TitleFont, printWidth, titleformat).Height;
            subtitleheight = g.MeasureString(SubTitle, SubTitleFont, printWidth, subtitleformat).Height;
        }

        //-----------------------------------------------------------------
        // measure the footer, if one is provided. Include the page number if we're printing
        // it on the bottom
        //-----------------------------------------------------------------
        if (PrintFooter)
        {
            if (!string.IsNullOrEmpty(Footer))
                footerHeight += g.MeasureString(Footer, FooterFont, printWidth, footerformat).Height;

            footerHeight += FooterSpacing;
        }

        //-----------------------------------------------------------------
        // Calculate column widths, adjusting for porportional columns
        // and datawidth columns. Row heights are calculated later
        //-----------------------------------------------------------------
        for (i = 0; i < rowstoprint.Count; i++)
        {
            var row = rowstoprint[i].row;

            // add row headers if they're visible
            if ((bool)PrintRowHeaders)
            {
                // provide a default 'blank' value to prevent a 0 length if we're supposed to show
                // row headers
                var text = string.IsNullOrEmpty(row.HeaderCell.FormattedValue.ToString())
                    ? RowHeaderCellDefaultText
                    : row.HeaderCell.FormattedValue.ToString();

                var measureString = g.MeasureString(text,
                    row.HeaderCell.InheritedStyle.Font);
                rowheaderwidth = rowheaderwidth < measureString.Width ? measureString.Width : rowheaderwidth;
            }

            // calculate widths for each column. We're looking for the largest width needed for
            // all the rows of data.
            for (j = 0; j < colstoprint.Count; j++)
            {
                col = (DataGridViewColumn)colstoprint[j];

                //-------------------------------------------------------------
                // Build string format and cell style
                //-------------------------------------------------------------

                // get gridview style, and override if we have a set style for this column
                StringFormat currentformat = null;
                var colstyle = GetStyle(row, col); // = row.Cells[col.Index].InheritedStyle.Clone();

                // build the cell style and font
                buildstringformat(ref currentformat, colstyle, cellformat.Alignment, cellformat.LineAlignment,
                    cellformat.FormatFlags, cellformat.Trimming);

                //-------------------------------------------------------------
                // Calculate and accumulate cell widths and heights
                //-------------------------------------------------------------
                float basewidth;

                // get the default width, depending on overrides. Only calculate data
                // sizes for DataWidth column setting.
                if (0 <= colwidthsoverride[j])
                // set overridden column width
                {
                    basewidth = colwidthsoverride[j];
                }
                else if (ColumnWidthSetting.CellWidth == ColumnWidth || ColumnWidthSetting.Porportional == ColumnWidth)
                // set default to same as title cell width
                {
                    basewidth = colwidths[j];
                }
                else
                {
                    // limit to one page
                    basewidth = printWidth;

                    // remove padding
                    basewidth -= colstyle.Padding.Left + colstyle.Padding.Right;

                    // calc cell size
                    var size = CalculateCellSize(g, row.Cells[col.Index], colstyle,
                        basewidth, colwidthsoverride[j], currentformat);

                    basewidth = size.Width;
                }

                // if width is not overridden and we're using data width then accumulate column widths
                if (!(0 <= colwidthsoverride[j]) && ColumnWidthSetting.DataWidth == ColumnWidth)
                    colwidths[j] = colwidths[j] < basewidth ? basewidth : colwidths[j];
            }
        }

        //-----------------------------------------------------------------
        // Break the columns accross page sets. This is the key to printing
        // where the total width is wider than one page.
        //-----------------------------------------------------------------

        // assume everything will fit on one page
        pagesets = new List<PageDef>();
        pagesets.Add(new PageDef(PrintMargins, colstoprint.Count, pageWidth));
        var pset = 0;

        // Account for row headers
        pagesets[pset].coltotalwidth = rowheaderwidth;

        // account for 'fixed' columns - these appear on every pageset
        for (j = 0; j < fixedcolumns.Count; j++)
        {
            var fixedcol = fixedcolumns[j];
            pagesets[pset].columnindex.Add(fixedcol);
            pagesets[pset].colstoprint.Add(colstoprint[fixedcol]);
            pagesets[pset].colwidths.Add(colwidths[fixedcol]);
            pagesets[pset].colwidthsoverride.Add(colwidthsoverride[fixedcol]);
            pagesets[pset].coltotalwidth += colwidthsoverride[fixedcol] >= 0
                ? colwidthsoverride[fixedcol]
                : colwidths[fixedcol];
        }

        // check on fixed columns
        if (printWidth < pagesets[pset].coltotalwidth)
            throw new Exception("Fixed column widths exceed the page width.");

        // split remaining columns into page sets
        float columnwidth;
        for (i = 0; i < colstoprint.Count; i++)
        {
            // skip 'fixed' columns since we've already accounted for them
            if (fixedcolumns.Contains(i))
                continue;

            // get initial column width
            columnwidth = colwidthsoverride[i] >= 0
                ? colwidthsoverride[i]
                : colwidths[i];

            // See if the column width takes us off the page - Except for the
            // first column. This will prevent printing an empty page!! Otherwise,
            // columns longer than the page width are printed on their own page
            if (printWidth < pagesets[pset].coltotalwidth + columnwidth && i != 0)
            {
                pagesets.Add(new PageDef(PrintMargins, colstoprint.Count, pageWidth));
                pset++;

                // Account for row headers
                pagesets[pset].coltotalwidth = rowheaderwidth;

                // account for 'fixed' columns - these appear on every pageset
                for (j = 0; j < fixedcolumns.Count; j++)
                {
                    var fixedcol = fixedcolumns[j];
                    pagesets[pset].columnindex.Add(fixedcol);
                    pagesets[pset].colstoprint.Add(colstoprint[fixedcol]);
                    pagesets[pset].colwidths.Add(colwidths[fixedcol]);
                    pagesets[pset].colwidthsoverride.Add(colwidthsoverride[fixedcol]);
                    pagesets[pset].coltotalwidth += colwidthsoverride[fixedcol] >= 0
                        ? colwidthsoverride[fixedcol]
                        : colwidths[fixedcol];
                }

                // check on fixed columns
                if (printWidth < pagesets[pset].coltotalwidth)
                    throw new Exception("Fixed column widths exceed the page width.");
            }

            // update page set definition
            pagesets[pset].columnindex.Add(i);
            pagesets[pset].colstoprint.Add(colstoprint[i]);
            pagesets[pset].colwidths.Add(colwidths[i]);
            pagesets[pset].colwidthsoverride.Add(colwidthsoverride[i]);
            pagesets[pset].coltotalwidth += columnwidth;
        }

        // for right to left language, reverse the column order for each page set
        if (RightToLeft.Yes == dgv.RightToLeft)
            for (pset = 0; pset < pagesets.Count; pset++)
            {
                pagesets[pset].columnindex.Reverse();
                pagesets[pset].colstoprint.Reverse();
                pagesets[pset].colwidths.Reverse();
                pagesets[pset].colwidthsoverride.Reverse();
            }

        for (i = 0; i < pagesets.Count; i++)
        {
            var pageset = pagesets[i];
            if (EnableLogging)
            {
                var columnlist = "";

                Logger.LogInfoMsg($"PageSet {i} Information ----------------------------------------------");

                // list out all the columns printed on this page since we may have fixed columns to account for
                for (var k = 0; k < pageset.colstoprint.Count; k++)
                    columnlist = $"{columnlist},{((DataGridViewColumn)pageset.colstoprint[k]).Index}";
                Logger.LogInfoMsg($"Measured columns {columnlist.Substring(1)}");
                columnlist = "";

                // list original column widths for this page
                for (var k = 0; k < pageset.colstoprint.Count; k++)
                    columnlist = $"{columnlist},{pageset.colwidths[k]}";
                Logger.LogInfoMsg($"Original Column Widths: {columnlist.Substring(1)}");
                columnlist = "";

                // list column width override values
                for (var k = 0; k < pageset.colstoprint.Count; k++)
                    columnlist = $"{columnlist},{pageset.colwidthsoverride[k]}";
                Logger.LogInfoMsg($"Overridden Column Widths: {columnlist.Substring(1)}");
                columnlist = "";
            }

            //-----------------------------------------------------------------
            // Adjust column widths and table margins for each page
            //-----------------------------------------------------------------
            AdjustPageSets(g, pageset);

            //-----------------------------------------------------------------
            // Log Pagesets
            //-----------------------------------------------------------------
            if (EnableLogging)
            {
                var columnlist = "";

                // list final column widths for this page
                for (var k = 0; k < pageset.colstoprint.Count; k++)
                    columnlist = $"{columnlist},{pageset.colwidths[k]}";
                Logger.LogInfoMsg($"Final Column Widths: {columnlist.Substring(1)}");
                columnlist = "";

                Logger.LogInfoMsg(
                    $"pageset print width is {pageset.printWidth}, total column width to be printed is {pageset.coltotalwidth}");
            }
        }
    }

    /// <summary>
    ///     Adjust column widths for fixed and porportional columns, set the
    ///     margins to enforce the selected tablealignment.
    /// </summary>
    /// <param name="g">The graphics context for all measurements</param>
    /// <param name="pageset">The pageset to adjust</param>
    private void AdjustPageSets(Graphics g, PageDef pageset)
    {
        int i;
        var fixedcolwidth = rowheaderwidth;
        float remainingcolwidth = 0;
        float ratio;

        //-----------------------------------------------------------------
        // Adjust the column widths in the page set to their final values,
        // accounting for overridden widths and porportional column stretching
        //-----------------------------------------------------------------

        // calculate the amount of space reserved for fixed width columns
        for (i = 0; i < pageset.colwidthsoverride.Count; i++)
            if (pageset.colwidthsoverride[i] >= 0)
                fixedcolwidth += pageset.colwidthsoverride[i];

        // calculate the amount space requested for non-overridden columns
        for (i = 0; i < pageset.colwidths.Count; i++)
            if (pageset.colwidthsoverride[i] < 0)
                remainingcolwidth += pageset.colwidths[i];

        // calculate the ratio for porportional columns, use 1 for
        // non-overridden columns or not porportional
        if ((porportionalcolumns || ColumnWidthSetting.Porportional == ColumnWidth) &&
            0 < remainingcolwidth)
            ratio = (printWidth - fixedcolwidth) / remainingcolwidth;
        else
            ratio = (float)1.0;

        // reset all column widths for override and/or porportionality. coltotalwidth
        // for each pageset should be <= pageWidth
        pageset.coltotalwidth = rowheaderwidth;
        for (i = 0; i < pageset.colwidths.Count; i++)
        {
            if (pageset.colwidthsoverride[i] >= 0)
                // use set width
                pageset.colwidths[i] = pageset.colwidthsoverride[i];
            else if (ColumnWidthSetting.Porportional == ColumnWidth)
                // change the width by the ratio
                pageset.colwidths[i] = pageset.colwidths[i] * ratio;
            else if (pageset.colwidths[i] > printWidth - pageset.coltotalwidth)
                pageset.colwidths[i] = printWidth - pageset.coltotalwidth;

            //recalculate any rows that need to flow down the page
            RecalcRowHeights(g, pageset.columnindex[i], pageset.colwidths[i]);

            pageset.coltotalwidth += pageset.colwidths[i];
        }

        //-----------------------------------------------------------------
        // Table Alignment - now that we have the column widths established
        // we can reset the table margins to get left, right and centered
        // for the table on the page
        //-----------------------------------------------------------------

        // Reset Print Margins based on table alignment
        if (Alignment.Left == TableAlignment)
        {
            // Bias table to the left by setting "right" value
            pageset.margins.Right = pageWidth - pageset.margins.Left - (int)pageset.coltotalwidth;
            if (0 > pageset.margins.Right) pageset.margins.Right = 0;
        }
        else if (Alignment.Right == TableAlignment)
        {
            // Bias table to the right by setting "left" value
            pageset.margins.Left = pageWidth - pageset.margins.Right - (int)pageset.coltotalwidth;
            if (0 > pageset.margins.Left) pageset.margins.Left = 0;
        }
        else if (Alignment.Center == TableAlignment)
        {
            // Bias the table to the center by setting left and right equal
            pageset.margins.Left = (pageWidth - (int)pageset.coltotalwidth) / 2;
            if (0 > pageset.margins.Left) pageset.margins.Left = 0;
            pageset.margins.Right = pageset.margins.Left;
        }
    }

    /// <summary>
    ///     Set page breaks for the rows to be printed, and count total pages
    /// </summary>
    private int Pagination()
    {
        float pos = 0;
        var newpage = paging.keepgoing;

        //// if we're printing by pages, the total pages is the last page to
        //// print
        //if (toPage < maxPages)
        //    return toPage;

        // Start counting pages at 1
        CurrentPage = 1;

        // Calculate where to stop printing the grid - count up from the bottom of the page.
        staticheight = pageHeight - FooterHeight - pagesets[currentpageset].margins.Bottom; //PrintMargins.Bottom;

        // add in the page number height - doesn't matter at this point if it's printing on top or bottom
        staticheight -= PageNumberHeight;

        // Calculate where to start printing the grid for page 1
        pos = PrintMargins.Top + HeaderHeight;

        // set starting value for 'break on value change' column
        if (!string.IsNullOrEmpty(BreakOnValueChange))
            oldvalue = rowstoprint[0].row.Cells[BreakOnValueChange].EditedFormattedValue;

        // if we're printing by rows, sum up rowheights until we're done.
        for (var currentrow = 0; currentrow < rowstoprint.Count; currentrow++)
        {
            // end of page: Count the page and reset to top of next page
            if (pos + rowstoprint[currentrow].height >= staticheight) newpage = paging.outofroom;

            // if we're breaking on value change in a column then watch that column
            if (!string.IsNullOrEmpty(BreakOnValueChange) &&
                !oldvalue.Equals(rowstoprint[currentrow].row.Cells[BreakOnValueChange].EditedFormattedValue))
            {
                newpage = paging.datachange;
                oldvalue = rowstoprint[currentrow].row.Cells[BreakOnValueChange].EditedFormattedValue;
            }

            // if we need to start a new page, count it and reset counters
            if (newpage != paging.keepgoing)
            {
                // note page break
                rowstoprint[currentrow].pagebreak = true;

                // count the page
                CurrentPage++;

                // if we're printing by pages, stop when we pass our limit
                if (CurrentPage > toPage)
                    // we're done
                    return toPage;

                // reset the counter - depending on setting
                if (KeepRowsTogether
                    || newpage == paging.datachange
                    || (newpage == paging.outofroom && staticheight - pos < KeepRowsTogetherTolerance))
                {
                    // if we are keeping rows together and too little would be showing, put whole row on next page
                    pos = rowstoprint[currentrow].height;
                }
                else
                {
                    // note page split
                    rowstoprint[currentrow].splitrow = true;

                    // if we're not keeping rows together, only put remainder on next page
                    pos = pos + rowstoprint[currentrow].height - staticheight;
                }

                // Recalculate where to stop printing the grid because available space can change w/ dynamic header/footers.
                staticheight =
                    pageHeight - FooterHeight - pagesets[currentpageset].margins.Bottom; //PrintMargins.Bottom;

                // add in the page number height - doesn't matter at this point if it's printing on top or bottom
                staticheight += PageNumberHeight;

                // account for static space at the top of the page
                pos += PrintMargins.Top + HeaderHeight + PageNumberHeight;
            }
            else
            {
                // add row space
                pos += rowstoprint[currentrow].height;
            }

            // reset flag
            newpage = paging.keepgoing;
        }

        // return counted pages
        return CurrentPage;
    }

    /// <summary>
    ///     Check for more pages. This is called at the end of printing a page set.
    ///     If there's another page set to print, we return true.
    /// </summary>
    private bool DetermineHasMorePages()
    {
        currentpageset++;
        if (currentpageset < pagesets.Count)
            //currentpageset--;   // decrement back to a valid pageset number
            return true; // tell the caller we're through.
        return false;
    }

    /// <summary>
    ///     This routine prints one page. It will skip non-printable pages if the user
    ///     selected the "some pages" option on the print dialog. This is called during
    ///     the Print event.
    /// </summary>
    /// <param name="g">Graphics object to print to</param>
    private bool PrintPage(Graphics g)
    {
        // for tracing and logging purposes
        var firstrow = 0;

        // flag for continuing or ending print process
        var HasMorePages = false;

        // flag for handling printing some pages rather than all
        var printthispage = false;

        // current printing position within one page
        float printpos = pagesets[currentpageset].margins.Top;

        // increment page number & check page range
        CurrentPage++;
        if (EnableLogging) Logger.LogInfoMsg($"Print Page processing page {CurrentPage} -----------------------");
        if (CurrentPage >= fromPage && CurrentPage <= toPage)
            printthispage = true;

        // calculate the static vertical space available - this is where we stop printing rows
        // Note: leave room for the page number if it's on the bottom
        staticheight = pageHeight - FooterHeight - pagesets[currentpageset].margins.Bottom;
        if (!PageNumberInHeader)
            staticheight -= PageNumberHeight;

        // count space used as we work our way down the page
        float used = 0;

        // current row information block
        rowdata thisrow = null;

        // next row (lookahead) information block
        rowdata nextrow = null;

        //-----------------------------------------------------------------
        // scan down heights until we're off this (non-printing) page
        //-----------------------------------------------------------------

        while (!printthispage)
        {
            if (EnableLogging)
                Logger.LogInfoMsg(
                    $"Print Page skipping page {CurrentPage} part {currentpageset + 1}");

            // calculate and increment over the page we're not printing
            printpos = pagesets[currentpageset].margins.Top + HeaderHeight + PageNumberHeight;

            // are we done with this page?
            var pagecomplete = false;
            currentrow = lastrowprinted + 1;

            // for logging
            firstrow = currentrow;

            do
            {
                thisrow = rowstoprint[currentrow];

                // this is how much space this row will use on this page
                used = thisrow.height - rowstartlocation > staticheight - printpos
                    ? staticheight - printpos
                    : thisrow.height - rowstartlocation;
                printpos += used;

                // Now, look at the next row and start checking on whether or not we're out of room & need to count a page
                lastrowprinted++;
                currentrow++;
                nextrow = currentrow < rowstoprint.Count ? rowstoprint[currentrow] : null;
                if (null != nextrow && nextrow.pagebreak) // pagebreak before the next row
                {
                    pagecomplete = true;

                    if (nextrow.splitrow)
                        // account for the partial row that would go on this page
                        rowstartlocation += nextrow.height - rowstartlocation > staticheight - printpos
                            ? staticheight - printpos
                            : nextrow.height - rowstartlocation;
                }
                else
                {
                    // completed a row, so reset startlocation and count this row.
                    rowstartlocation = 0;
                }

                // if we're out of data (no partial rows and no more rows)
                if (0 == rowstartlocation && lastrowprinted >= rowstoprint.Count - 1)
                    pagecomplete = true;
            } while (!pagecomplete);

            // log rows skipped
            if (EnableLogging) Logger.LogInfoMsg($"Print Page skipped rows {firstrow} to {currentrow}");

            // skip to the next page & see if it's in the print range
            CurrentPage++;

            if (CurrentPage >= fromPage && CurrentPage <= toPage)
                printthispage = true;

            // partial row means more to print
            if (0 != rowstartlocation)
            {
                // we're not done with this row yet
                HasMorePages = true;
            }
            // done with this page set so see if there are any more pagesets to print
            else if (lastrowprinted >= rowstoprint.Count - 1 || CurrentPage > toPage)
            {
                // reset for next pageset or tell the caller we're complete
                HasMorePages = DetermineHasMorePages();

                // reset counters since we'll go through this twice if we print from preview
                lastrowprinted = -1;
                CurrentPage = 0;

                return HasMorePages;
            }
        }

        if (EnableLogging)
        {
            Logger.LogInfoMsg($"Print Page printing page {CurrentPage} part {currentpageset + 1}");
            var m = pagesets[currentpageset].margins;
            Logger.LogInfoMsg($"Current Margins are {m.Left}, {m.Right}, {m.Top}, {m.Bottom}");
        }

        //-----------------------------------------------------------------
        // print statically located images
        //-----------------------------------------------------------------

        // print any "absolute" images so that anything else we print will be 'on top'
        ImbeddedImageList.Where(p => p.ImageLocation == Location.Absolute).DrawImbeddedImage(g,
            pagesets[currentpageset].printWidth,
            pageHeight, pagesets[currentpageset].margins);

        //-----------------------------------------------------------------
        // print headers
        //-----------------------------------------------------------------

        // reset printpos as it may have changed during the 'skip pages' routine just above.
        printpos = pagesets[currentpageset].margins.Top;

        // Skip headers if the flag is false
        if (PrintHeader)
        {
            // print any "header" images so that anything else we print will be 'on top'
            ImbeddedImageList.Where(p => p.ImageLocation == Location.Header).DrawImbeddedImage(g,
                pagesets[currentpageset].printWidth,
                pageHeight, pagesets[currentpageset].margins);

            // print page number if user selected it
            if (PageNumberInHeader) printpos = PrintPageNo(g, printpos);

            // print title if provided, & we're not skipping it
            if (0 != TitleHeight && !string.IsNullOrEmpty(title))
                GetPrintSection(g, ref printpos, title, TitleFont,
                    TitleColor, titleformat, overridetitleformat,
                    pagesets[currentpageset],
                    TitleBackground, TitleBorder);

            // account for title spacing
            printpos += TitleHeight;

            // print subtitle if provided
            if (0 != SubTitleHeight && !string.IsNullOrEmpty(SubTitle))
                GetPrintSection(g, ref printpos, SubTitle, SubTitleFont,
                    SubTitleColor, subtitleformat, overridesubtitleformat,
                    pagesets[currentpageset],
                    SubTitleBackground, SubTitleBorder);

            // account for subtitle spacing
            printpos += SubTitleHeight;
        }

        // print the column headers or not based on our processing flag
        if ((bool)PrintColumnHeaders)
            // print column headers
            printcolumnheaders(g, ref printpos, pagesets[currentpageset]);

        //-----------------------------------------------------------------
        // print rows until the page is complete
        //-----------------------------------------------------------------
        var continueprinting = true;
        currentrow = lastrowprinted + 1;

        // for logging
        firstrow = currentrow;

        if (currentrow >= rowstoprint.Count)
            // indicate that we're done printing
            continueprinting = false;

        while (continueprinting)
        {
            thisrow = rowstoprint[currentrow];

            // print the part of the row that we can, and accumulate the space used
            used = printrow(g, printpos, thisrow.row,
                pagesets[currentpageset], rowstartlocation);
            printpos += used;

            // Now, start checking on whether or not to print the next row
            // (or if we even have a next row)
            lastrowprinted++;
            currentrow++;
            nextrow = currentrow < rowstoprint.Count ? rowstoprint[currentrow] : null;
            if (null != nextrow && nextrow.pagebreak)
            {
                continueprinting = false;

                // print a partial row before breaking
                if (nextrow.splitrow)
                    // print what we can on this page, print the remainder on the next page
                    rowstartlocation += printrow(g, printpos, nextrow.row,
                        pagesets[currentpageset], rowstartlocation);
            }
            else
            {
                // completed a row, so reset startlocation.
                rowstartlocation = 0;
            }

            // if we're out of data (no partial rows and no more rows)
            if (0 == rowstartlocation && lastrowprinted >= rowstoprint.Count - 1)
                continueprinting = false;
        }

        // log rows skipped
        if (EnableLogging)
        {
            Logger.LogInfoMsg($"Print Page printed rows {firstrow} to {currentrow}");
            var pageset = pagesets[currentpageset];
            var columnlist = "";

            // list out all the columns printed on this page since we may have fixed columns to account for
            for (var i = 0; i < pageset.colstoprint.Count; i++)
                columnlist = $"{columnlist},{((DataGridViewColumn)pageset.colstoprint[i]).Index}";

            Logger.LogInfoMsg($"Print Page printed columns {columnlist.Substring(1)}");
        }

        //-----------------------------------------------------------------
        // print footer
        //-----------------------------------------------------------------
        if (PrintFooter)
        {
            // print any "footer" images so that anything else we print will be 'on top'
            ImbeddedImageList.Where(p => p.ImageLocation == Location.Footer).DrawImbeddedImage(g,
                pagesets[currentpageset].printWidth,
                pageHeight, pagesets[currentpageset].margins);

            //Note: need to force printpos to the bottom of the page
            // as we may have run out of data anywhere on the page
            printpos = pageHeight - footerHeight - pagesets[currentpageset].margins.Bottom; // - margins.Top

            // add spacing
            printpos += FooterSpacing;

            // print the page number if it's on the bottom.
            if (!PageNumberInHeader) printpos = PrintPageNo(g, printpos);

            if (0 != FooterHeight)
                GetPrintFooter(g, ref printpos, pagesets[currentpageset]);
        }

        //-----------------------------------------------------------------
        // bottom check, see if this is the last page to print
        //-----------------------------------------------------------------

        // partial row means more to print
        if (0 != rowstartlocation)
            // we're not done with this row yet
            HasMorePages = true;

        // done with this page set so see if there are any more pagesets to print
        if (CurrentPage >= toPage || lastrowprinted >= rowstoprint.Count - 1)
        {
            // reset for next pageset or tell the caller we're complete
            HasMorePages = DetermineHasMorePages();

            // reset counters since we'll go through this twice if we print from preview
            rowstartlocation = 0;
            lastrowprinted = -1;
            CurrentPage = 0;
        }
        else
        {
            // we're not done yet
            HasMorePages = true;
        }

        return HasMorePages;
    }

    /// <summary>
    ///     Print the page number
    /// </summary>
    /// <param name="g"></param>
    /// <param name="printpos"></param>
    /// <returns></returns>
    private float PrintPageNo(Graphics g, float printpos)
    {
        if (PageNumbers)
        {
            var pagenumber = PageText + CurrentPage.ToString(CultureInfo.CurrentCulture);
            if (ShowTotalPageNumber) pagenumber += PageSeparator + totalpages.ToString(CultureInfo.CurrentCulture);
            if (1 < pagesets.Count)
                pagenumber += PartText + (currentpageset + 1).ToString(CultureInfo.CurrentCulture);

            // ... then print it
            GetPrintSection(g, ref printpos,
                pagenumber, PageNumberFont, PageNumberColor, pagenumberformat,
                overridepagenumberformat, pagesets[currentpageset],
                null, null);

            // if the page number is not on a separate line, don't "use up" it's vertical space
            if (PageNumberOnSeparateLine)
                printpos += pagenumberHeight;
        }

        return printpos;
    }

    /// <summary>
    ///     Print a header or footer section. Used for page numbers and titles
    /// </summary>
    /// <param name="g">Graphic context to print in</param>
    /// <param name="pos">Track vertical space used; 'y' location</param>
    /// <param name="text">String to print</param>
    /// <param name="font">Font to use for printing</param>
    /// <param name="color">Color to print in</param>
    /// <param name="format">String format for text</param>
    /// <param name="useroverride">True if the user overrode the alignment or flags</param>
    /// <param name="margins">The table's print margins</param>
    /// <param name="background">Background fill for the section; may be null for no background</param>
    /// <param name="border">Border for the section; may be null for no border</param>
    private void GetPrintSection(Graphics g, ref float pos, string text, Font font, Color color, StringFormat format,
        bool useroverride, PageDef pageset, Brush background, Pen border)
    {
        // measure string
        var printsize = g.MeasureString(text, font, pageset.printWidth, format);

        // build area to print within
        var printarea = new RectangleF(pageset.margins.Left, pos, pageset.printWidth,
            printsize.Height);

        // draw a background, if a Brush has been provided
        if (null != background) g.FillRectangle(background, printarea);

        // draw a border, if a Pen has been provided
        if (null != border) g.DrawRectangle(border, printarea.X, printarea.Y, printarea.Width, printarea.Height);

        // do the actual print
        g.DrawString(text, font, new SolidBrush(color), printarea, format);
    }

    /// <summary>
    ///     Print the footer. This handles the footer spacing, and printing the page number
    ///     at the bottom of the page (if the page number is not in the header).
    /// </summary>
    /// <param name="g">Graphic context to print in</param>
    /// <param name="pos">Track vertical space used; 'y' location</param>
    /// <param name="margins">The table's print margins</param>
    private void GetPrintFooter(Graphics g, ref float pos, PageDef pageset)
    {
        // print the footer
        GetPrintSection(g, ref pos, Footer, FooterFont, FooterColor, footerformat,
            overridefooterformat, pageset, FooterBackground, FooterBorder);
    }

    /// <summary>
    ///     Print the column headers. Most printing format info is retrieved from the
    ///     source DataGridView.
    /// </summary>
    /// <param name="g">Graphics Context to print within</param>
    /// <param name="pos">Track vertical space used; 'y' location</param>
    /// <param name="pageset">Current pageset - defines columns and margins</param>
    private void printcolumnheaders(Graphics g, ref float pos, PageDef pageset)
    {
        // track printing location accross the page. start position is hard left,
        // adjusted for the row headers. Note rowheaderwidth is 0 if row headers are not printed
        var xcoord = pageset.margins.Left + rowheaderwidth;

        // set the pen for drawing the grid lines
        var lines = new Pen(dgv.GridColor, 1);

        //-----------------------------------------------------------------
        // Print the column headers
        //-----------------------------------------------------------------
        DataGridViewColumn col;
        for (var i = 0; i < pageset.colstoprint.Count; i++)
        {
            col = (DataGridViewColumn)pageset.colstoprint[i];

            // calc cell width, account for columns larger than the print area!
            var cellwidth = pageset.colwidths[i] > pageset.printWidth - rowheaderwidth
                ? pageset.printWidth - rowheaderwidth
                : pageset.colwidths[i];

            // get column style
            var style = col.HeaderCell.InheritedStyle.Clone();
            if (ColumnHeaderStyles.ContainsKey(col.Name)) style = ColumnHeaderStyles[col.Name];

            // set print area for this individual cell, account for cells larger
            // than the print area!
            var cellprintarea = new RectangleF(xcoord, pos, cellwidth, colheaderheight);

            DrawCell(g, cellprintarea, style, col.HeaderCell, 0, columnheadercellformat, lines);

            xcoord += pageset.colwidths[i];
        }

        // all done, consume "used" vertical space, including space for border lines
        pos += colheaderheight +
               (dgv.ColumnHeadersBorderStyle != DataGridViewHeaderBorderStyle.None ? lines.Width : 0);
    }

    /// <summary>
    ///     Print one row of the DataGridView. Most printing format info is retrieved
    ///     from the DataGridView.
    /// </summary>
    /// <param name="g">Graphics Context to print within</param>
    /// <param name="pos">Track vertical space used; 'y' location</param>
    /// <param name="row">The row that will be printed</param>
    /// <param name="pageset">Current Pageset - defines columns and margins</param>
    /// <param name="startline">Line no. in row to start printing text at</param>
    private float printrow(Graphics g, float finalpos, DataGridViewRow row, PageDef pageset, float startlocation)
    {
        // track printing location accross the page
        float xcoord = pageset.margins.Left;
        var pos = finalpos;

        // set the pen for drawing the grid lines
        var lines = new Pen(dgv.GridColor, 1);

        // calc row width, account for columns wider than the print area!
        var rowwidth = pageset.coltotalwidth > pageset.printWidth ? pageset.printWidth : pageset.coltotalwidth;

        // calc row heigth in pixels to print
        var rowheight = rowstoprint[currentrow].height - startlocation > staticheight - pos
            ? staticheight - pos
            : rowstoprint[currentrow].height - startlocation;

        //-----------------------------------------------------------------
        // Print Row background
        //-----------------------------------------------------------------

        // get current row style, and current header style
        var rowstyle = row.InheritedStyle.Clone();
        var headerstyle = row.HeaderCell.InheritedStyle.Clone();

        // define print rectangle
        var printarea = new RectangleF(xcoord, pos, rowwidth,
            rowheight);

        // fill in the row background as the default color
        g.FillRectangle(new SolidBrush(rowstyle.BackColor), printarea);

        //-----------------------------------------------------------------
        // Print the Row Headers, if they are visible
        //-----------------------------------------------------------------
        if ((bool)PrintRowHeaders)
        {
            // set print area for this individual cell
            var headercellprintarea = new RectangleF(xcoord, pos,
                rowheaderwidth, rowheight);

            DrawCell(g, headercellprintarea, headerstyle, row.HeaderCell, startlocation,
                rowheadercellformat, lines);

            // track horizontal space used
            xcoord += rowheaderwidth;
        }

        //-----------------------------------------------------------------
        // Print the row: write and draw each cell
        //-----------------------------------------------------------------
        DataGridViewColumn col;
        for (var i = 0; i < pageset.colstoprint.Count; i++)
        {
            // access the cell and column being printed
            col = (DataGridViewColumn)pageset.colstoprint[i];
            var cell = row.Cells[col.Index];

            // calc cell width, account for columns larger than the print area!
            var cellwidth = pageset.colwidths[i] > pageset.printWidth - rowheaderwidth
                ? pageset.printWidth - rowheaderwidth
                : pageset.colwidths[i];

            // SLG 01112010 - only draw columns with an actual width
            if (cellwidth > 0)
            {
                // get DGV column style and see if we have an override for this column
                StringFormat finalformat = null;
                Font cellfont = null;
                var colstyle = GetStyle(row, col); // = row.Cells[col.Index].InheritedStyle.Clone();

                // set string format
                buildstringformat(ref finalformat, colstyle, cellformat.Alignment, cellformat.LineAlignment,
                    cellformat.FormatFlags, cellformat.Trimming);
                cellfont = colstyle.Font;

                // set overall print area for this individual cell
                var cellprintarea = new RectangleF(xcoord, pos, cellwidth,
                    rowheight);

                DrawCell(g, cellprintarea, colstyle, cell, startlocation, finalformat, lines);
            }

            // track horizontal space used
            xcoord += pageset.colwidths[i];
        }

        //-----------------------------------------------------------------
        // All done with this row, consume "used" vertical space
        //-----------------------------------------------------------------
        return rowheight;
    }

    /// <summary>
    ///     Allow override of cell drawing. This is to support grids that have onPaint
    ///     overridden to do things like images in header rows and vertical printing
    /// </summary>
    /// <param name="g"></param>
    /// <param name="rowindex"></param>
    /// <param name="columnindex"></param>
    /// <param name="rectf"></param>
    /// <param name="style"></param>
    /// <returns></returns>
    private bool DrawOwnerDrawCell(Graphics g, int rowindex, int columnindex, RectangleF rectf,
        DataGridViewCellStyle style)
    {
        var args = new DGVCellDrawingEventArgs(g, rectf, style,
            rowindex, columnindex);
        OnCellOwnerDraw(args);
        return args.Handled;
    }

    /// <summary>
    ///     Draw a cell. Used for column and row headers and body cells.
    /// </summary>
    /// <param name="g"></param>
    /// <param name="cellprintarea"></param>
    /// <param name="style"></param>
    /// <param name="cell"></param>
    /// <param name="startlocation"></param>
    /// <param name="cellformat"></param>
    /// <param name="lines"></param>
    private void DrawCell(Graphics g, RectangleF cellprintarea, DataGridViewCellStyle style, DataGridViewCell cell,
        float startlocation, StringFormat cellformat, Pen lines)
    {
        // Draw the cell if it's not overridden by ownerdrawing
        if (!DrawOwnerDrawCell(g, cell.RowIndex, cell.ColumnIndex, cellprintarea, style))
        {
            // save original clipping bounds
            var clip = g.ClipBounds;

            // fill in the full cell background - using the selected style
            //g.FillRectangle(new SolidBrush(colstyle.BackColor), cellprintarea);
            g.FillRectangle(new SolidBrush(style.BackColor), cellprintarea);

            // reset print area for this individual cell, adjusting 'inward' for cell padding
            var paddedcellprintarea = new RectangleF(cellprintarea.X + style.Padding.Left,
                cellprintarea.Y + style.Padding.Top,
                cellprintarea.Width - style.Padding.Right - style.Padding.Left,
                cellprintarea.Height - style.Padding.Bottom - style.Padding.Top);

            // set clipping to current print area - i.e. our cell
            g.SetClip(cellprintarea);

            // define the *actual* print area based on the given startlocation. Offset the start by
            // minus the start location, increase the print area height by the startlocation
            var actualprint = new RectangleF(paddedcellprintarea.X, paddedcellprintarea.Y - startlocation,
                paddedcellprintarea.Width, paddedcellprintarea.Height + startlocation);

            // draw content based on cell style, but only for "body" cells
            if (0 <= cell.RowIndex && 0 <= cell.ColumnIndex)
            {
                if ("DataGridViewImageCell" == dgv.Columns[cell.ColumnIndex].CellType.Name)
                    // draw the image for image cells
                    DrawImageCell(g, (DataGridViewImageCell)cell, actualprint);
                else if ("DataGridViewCheckBoxCell" == dgv.Columns[cell.ColumnIndex].CellType.Name)
                    // draw a checkbox for checkbox cells
                    DrawCheckBoxCell(g, (DataGridViewCheckBoxCell)cell, actualprint);
                else
                    // this handles drawing for textbox, button, combobox, and link cell types.
                    // currently these are not drawn as "controls" for performance reasons.
                    // draw the text for the cell at the row / col intersection
                    g.DrawString(cell.FormattedValue.ToString(), style.Font,
                        new SolidBrush(style.ForeColor), actualprint, cellformat);
            }
            else
            {
                // draw the text for the cell at the row / col intersection
                g.DrawString(cell.FormattedValue.ToString(), style.Font,
                    new SolidBrush(style.ForeColor), actualprint, cellformat);
            }

            // reset clipping bounds to "normal"
            g.SetClip(clip);

            // draw the borders - default to the dgv's border setting, and use unpadded cell print area
            if (dgv.CellBorderStyle != DataGridViewCellBorderStyle.None)
                g.DrawRectangle(lines, cellprintarea.X, cellprintarea.Y, cellprintarea.Width, cellprintarea.Height);
        }
    }

    /// <summary>
    ///     Draw a body cell that is a checkbox
    /// </summary>
    /// <param name="g"></param>
    /// <param name="checkboxcell"></param>
    /// <param name="rectf"></param>
    private void DrawCheckBoxCell(Graphics g, DataGridViewCheckBoxCell checkboxcell, RectangleF rectf)
    {
        // create a non-printing graphics context in which to draw the checkbox control
        Image i = new Bitmap((int)rectf.Width, (int)rectf.Height);
        var tg = Graphics.FromImage(i);

        // determine checked or notchecked (or undetermined for tristate checkboxes)
        var state = CheckBoxState.UncheckedNormal;
        if (checkboxcell.ThreeState)
        {
            if ((CheckState)checkboxcell.EditedFormattedValue == CheckState.Checked)
                state = CheckBoxState.CheckedNormal;
            else if ((CheckState)checkboxcell.EditedFormattedValue == CheckState.Indeterminate)
                state = CheckBoxState.MixedNormal;
        }
        else
        {
            if ((bool)checkboxcell.EditedFormattedValue)
                state = CheckBoxState.CheckedNormal;
        }

        // get the size and location to print the checkbox - currently centered, may change later
        var size = CheckBoxRenderer.GetGlyphSize(tg, state);
        var x = ((int)rectf.Width - size.Width) / 2;
        var y = ((int)rectf.Height - size.Height) / 2;

        // draw the checkbox in our temporary graphics context
        CheckBoxRenderer.DrawCheckBox(tg, new Point(x, y), state);

        //calculate image drawing origin based on cell alignment
        switch (checkboxcell.InheritedStyle.Alignment)
        {
            case DataGridViewContentAlignment.BottomCenter:
                rectf.Y += y;
                break;

            case DataGridViewContentAlignment.BottomLeft:
                rectf.X -= x;
                rectf.Y += y;
                break;

            case DataGridViewContentAlignment.BottomRight:
                rectf.X += x;
                rectf.Y += y;
                break;

            case DataGridViewContentAlignment.MiddleCenter:
                break;

            case DataGridViewContentAlignment.MiddleLeft:
                rectf.X -= x;
                break;

            case DataGridViewContentAlignment.MiddleRight:
                rectf.X += x;
                break;

            case DataGridViewContentAlignment.TopCenter:
                rectf.Y -= y;
                break;

            case DataGridViewContentAlignment.TopLeft:
                rectf.X -= x;
                rectf.Y -= y;
                break;

            case DataGridViewContentAlignment.TopRight:
                rectf.X += x;
                rectf.Y -= y;
                break;

            case DataGridViewContentAlignment.NotSet:
                break;
        }

        // now draw the image of the checkbox to our print output
        g.DrawImage(i, rectf);

        // clean up after ourselves
        tg.Dispose();
        i.Dispose();
    }

    /// <summary>
    ///     Draw a body cell that has an imbedded image
    /// </summary>
    /// <param name="g"></param>
    /// <param name="imagecell"></param>
    /// <param name="rectf"></param>
    private void DrawImageCell(Graphics g, DataGridViewImageCell imagecell, RectangleF rectf)
    {
        // image to draw
        Image img;

        // if we don't actually have a value, then just exit.
        if (null == imagecell.Value || imagecell.Value is DBNull)
            return;

        // Check on type of image cell value - may not be an actual "image" type
        if ("Image" == imagecell.ValueType.Name)
        {
            // if it's an "image" type, then load it directly
            img = (Image)imagecell.Value;
        }
        else if ("Byte[]" == imagecell.ValueType.Name)
        {
            // if it's not an "image" type (i.e. loaded from a database to a bound column)
            // convert the underlying byte array to an image
            var ic = new ImageConverter();
            img = (Image)ic.ConvertFrom((byte[])imagecell.Value);
        }
        else
        {
            throw new Exception(
                $"Unknown image cell underlying type: {imagecell.ValueType.Name} in column {imagecell.ColumnIndex}");
        }

        // clipping bounds. This is the portion of the image to fit into the drawing rectangle
        var src = new Rectangle();

        // calculate deltas
        var dx = 0;
        var dy = 0;

        // drawn normal size, clipped to cell
        if (DataGridViewImageCellLayout.Normal == imagecell.ImageLayout ||
            DataGridViewImageCellLayout.NotSet == imagecell.ImageLayout)
        {
            // calculate origin deltas, used to move image
            dx = img.Width - (int)rectf.Width;
            dy = img.Height - (int)rectf.Height;

            // set destination width and height to clip to cell
            if (0 > dx) rectf.Width = src.Width = img.Width;
            else src.Width = (int)rectf.Width;
            if (0 > dy) rectf.Height = src.Height = img.Height;
            else src.Height = (int)rectf.Height;
        }
        else if (DataGridViewImageCellLayout.Stretch == imagecell.ImageLayout)
        {
            // stretch image to fit cell size
            src.Width = img.Width;
            src.Height = img.Height;

            // change the origin delta's to 0 so we don't move the image
            dx = 0;
            dy = 0;
        }
        else // DataGridViewImageCellLayout.Zoom
        {
            // scale image to fit in cell
            src.Width = img.Width;
            src.Height = img.Height;

            var vertscale = rectf.Height / src.Height;
            var horzscale = rectf.Width / src.Width;
            float scale;

            // use the smaller scaling factor to ensure the image will fit in the cell
            if (vertscale > horzscale)
            {
                // use horizontal scale, don't move image horizontally
                scale = horzscale;
                dx = 0;
                dy = (int)(src.Height * scale - rectf.Height);
            }
            else
            {
                // use vertical scale, don't move image vertically
                scale = vertscale;
                dy = 0;
                dx = (int)(src.Width * scale - rectf.Width);
            }

            // set target size to match scaled image
            rectf.Width = src.Width * scale;
            rectf.Height = src.Height * scale;
        }

        //calculate image drawing origin based on origin deltas
        switch (imagecell.InheritedStyle.Alignment)
        {
            case DataGridViewContentAlignment.BottomCenter:
                if (0 > dy) rectf.Y -= dy;
                else src.Y = dy;
                if (0 > dx) rectf.X -= dx / 2;
                else src.X = dx / 2;
                break;

            case DataGridViewContentAlignment.BottomLeft:
                if (0 > dy) rectf.Y -= dy;
                else src.Y = dy;
                src.X = 0;
                break;

            case DataGridViewContentAlignment.BottomRight:
                if (0 > dy) rectf.Y -= dy;
                else src.Y = dy;
                if (0 > dx) rectf.X -= dx;
                else src.X = dx;
                break;

            case DataGridViewContentAlignment.MiddleCenter:
                if (0 > dy) rectf.Y -= dy / 2;
                else src.Y = dy / 2;
                if (0 > dx) rectf.X -= dx / 2;
                else src.X = dx / 2;
                break;

            case DataGridViewContentAlignment.MiddleLeft:
                if (0 > dy) rectf.Y -= dy / 2;
                else src.Y = dy / 2;
                src.X = 0;
                break;

            case DataGridViewContentAlignment.MiddleRight:
                if (0 > dy) rectf.Y -= dy / 2;
                else src.Y = dy / 2;
                if (0 > dx) rectf.X -= dx;
                else src.X = dx;
                break;

            case DataGridViewContentAlignment.TopCenter:
                src.Y = 0;
                if (0 > dx) rectf.X -= dx / 2;
                else src.X = dx / 2;
                break;

            case DataGridViewContentAlignment.TopLeft:
                src.Y = 0;
                src.X = 0;
                break;

            case DataGridViewContentAlignment.TopRight:
                src.Y = 0;
                if (0 > dx) rectf.X -= dx;
                else src.X = dx;
                break;

            case DataGridViewContentAlignment.NotSet:
                if (0 > dy) rectf.Y -= dy / 2;
                else src.Y = dy / 2;
                if (0 > dx) rectf.X -= dx / 2;
                else src.X = dx / 2;
                break;
        }

        // Now we can draw our image
        g.DrawImage(img, rectf, src, GraphicsUnit.Pixel);
    }

    //---------------------------------------------------------------------
    // internal classes/structs
    //---------------------------------------------------------------------

    #region Internal Classes

    // Identify the reason for a new page when tracking rows
    private enum paging
    {
        keepgoing,
        outofroom,
        datachange
    }

    // Allow the user to provide images that will be printed as either logos in the
    // header and/or footer or watermarked as in printed behind the text.
    public class ImbeddedImage
    {
        public Image theImage { get; set; }
        public Alignment ImageAlignment { get; set; }
        public Location ImageLocation { get; set; }
        public int ImageX { get; set; }
        public int ImageY { get; set; }

        internal Point upperleft(int pagewidth, int pageheight, Margins margins)
        {
            var y = 0;
            var x = 0;

            // if we've been given an absolute location, just use it
            if (ImageLocation == Location.Absolute)
                return new Point(ImageX, ImageY);

            // set the y location based on header or footer
            switch (ImageLocation)
            {
                case Location.Header:
                    y = margins.Top;
                    break;

                case Location.Footer:
                    y = pageheight - theImage.Height - margins.Bottom;
                    break;

                default:
                    throw new ArgumentException($"Unkown value: {ImageLocation}");
            }

            // set the x location based on left,right,center
            switch (ImageAlignment)
            {
                case Alignment.Left:
                    x = margins.Left;
                    break;

                case Alignment.Center:
                    x = pagewidth / 2 - theImage.Width / 2 + margins.Left;
                    break;

                case Alignment.Right:
                    x = pagewidth - theImage.Width + margins.Left;
                    break;

                case Alignment.NotSet:
                    x = ImageX;
                    break;

                default:
                    throw new ArgumentException($"Unkown value: {ImageAlignment}");
            }

            return new Point(x, y);
        }
    }

    public IList<ImbeddedImage> ImbeddedImageList = new List<ImbeddedImage>();

    // handle wide-column printing - that is, lists of columns that extend
    // wider than one page width. Columns are broken up into "Page Sets" that
    // are printed one after another until all columns are printed.
    private class PageDef
    {
        public readonly List<object> colstoprint;
        public float coltotalwidth;

        public readonly List<int> columnindex;
        public readonly List<float> colwidths;
        public readonly List<float> colwidthsoverride;
        public readonly Margins margins;
        private readonly int pageWidth;

        public PageDef(Margins m, int count, int pagewidth)
        {
            columnindex = new List<int>(count);
            colstoprint = new List<object>(count);
            colwidths = new List<float>(count);
            colwidthsoverride = new List<float>(count);
            coltotalwidth = 0;
            margins = (Margins)m.Clone();
            pageWidth = pagewidth;
        }

        public int printWidth => pageWidth - margins.Left - margins.Right;
    }

    private IList<PageDef> pagesets;
    private int currentpageset;

    // class to hold settings for the PrintDialog presented to the user during
    // the print process
    public class PrintDialogSettingsClass
    {
        public bool AllowCurrentPage = true;
        public bool AllowPrintToFile = false;
        public bool AllowSelection = true;
        public bool AllowSomePages = true;
        public bool ShowHelp = true;
        public bool ShowNetwork = true;
        public bool UseEXDialog = true;
    }

    // class to identify row data for printing
    public class rowdata
    {
        public float height;
        public bool pagebreak;
        public DataGridViewRow row;
        public bool splitrow;
    }

    #endregion Internal Classes

    //---------------------------------------------------------------------
    // global variables
    //---------------------------------------------------------------------

    #region global variables

    // the data grid view we're printing
    private DataGridView dgv;

    // print document

    // logging
    private LogManager Logger;

    // print status items
    private bool EmbeddedPrinting;

    private List<rowdata> rowstoprint;
    private IList colstoprint; // divided into pagesets for printing
    private int lastrowprinted = -1;
    private int currentrow = -1;
    private int fromPage;
    private int toPage = -1;
    private const int maxPages = 2147483647;

    // page formatting options
    private int pageHeight;

    private float staticheight;
    private float rowstartlocation;
    private int pageWidth;
    private int printWidth;
    private float rowheaderwidth;
    private int CurrentPage;
    private int totalpages;
    private PrintRange printRange;

    // calculated values
    //private float headerHeight = 0;
    private float footerHeight;

    private float pagenumberHeight;
    private float colheaderheight;

    //private List<float> rowheights;
    private List<float> colwidths;

    //private List<List<SizeF>> cellsizes;

    #endregion global variables

    //---------------------------------------------------------------------
    // properties - settable by user
    //---------------------------------------------------------------------

    #region properties

    #region global properties

    /// <summary>
    ///     Enable logging of of the print process. Default is to log to a file named
    ///     'DGVPrinter_yyyymmdd.Log' in the current directory. Since logging may have
    ///     an impact on performance, it should be used for troubleshooting purposes only.
    /// </summary>
    protected bool enablelogging;

    public bool EnableLogging
    {
        get => enablelogging;
        set
        {
            enablelogging = value;
            if (enablelogging) Logger = new LogManager(".", "DGVPrinter");
        }
    }

    /// <summary>
    ///     Allow the user to change the logging directory. Setting this enables logging by default.
    /// </summary>
    public string LogDirectory
    {
        get
        {
            if (null != Logger)
                return Logger.BasePath;
            return null;
        }
        set
        {
            if (null == Logger)
                EnableLogging = true;
            Logger.BasePath = value;
        }
    }

    /// <summary>
    ///     OwnerDraw Event declaration. Callers can subscribe to this event to override the
    ///     cell drawing.
    /// </summary>
    public event CellOwnerDrawEventHandler OwnerDraw;

    /// <summary>
    ///     provide an override for the print preview dialog "owner" field
    ///     Note: Changed style for VS2005 compatibility
    /// </summary>
    //public Form Owner
    //{ get; set; }
    protected Form _Owner;

    public Form Owner
    {
        get => _Owner;
        set => _Owner = value;
    }

    /// <summary>
    ///     provide an override for the print preview zoom setting
    ///     Note: Changed style for VS2005 compatibility
    /// </summary>
    //public Double PrintPreviewZoom
    //{ get; set; }
    protected double _PrintPreviewZoom = 1.0;

    public double PrintPreviewZoom
    {
        get => _PrintPreviewZoom;
        set => _PrintPreviewZoom = value;
    }

    /// <summary>
    ///     expose printer settings to allow access to calling program
    /// </summary>
    public PrinterSettings PrintSettings => printDocument.PrinterSettings;

    /// <summary>
    ///     expose settings for the PrintDialog displayed to the user
    /// </summary>
    public PrintDialogSettingsClass PrintDialogSettings { get; } = new();

    /// <summary>
    ///     Set Printer Name
    /// </summary>
    public string PrinterName { get; set; }

    /// <summary>
    ///     Allow access to the underlying print document
    /// </summary>
    public PrintDocument printDocument { get; set; }

    /// <summary>
    ///     Allow caller to set the upper-left corner icon used
    ///     in the print preview dialog
    /// </summary>
    public Icon PreviewDialogIcon { get; set; } = null;

    /// <summary>
    ///     Allow caller to set print preview dialog
    /// </summary>
    public PrintPreviewDialog PreviewDialog { get; set; }

    /// <summary>
    ///     Flag to control whether or not we print the Page Header
    /// </summary>
    public bool PrintHeader { get; set; } = true;

    /// <summary>
    ///     Determine the height of the header
    /// </summary>
    private float HeaderHeight
    {
        get
        {
            float headerheight = 0;

            // Add in title and subtitle heights - this is sensitive to
            // wether or not titles are printed on the current page
            // TitleHeight and SubTitleHeight have their respective spacing
            // already included
            headerheight += TitleHeight + SubTitleHeight;

            // Add in column header heights
            if ((bool)PrintColumnHeaders) headerheight += colheaderheight;

            // return calculated height
            return headerheight;
        }
    }

    /// <summary>
    ///     Flag to control whether or not we print the Page Footer
    /// </summary>
    public bool PrintFooter { get; set; } = true;

    /// <summary>
    ///     Flag to control whether or not we print the Column Header line
    /// </summary>
    public bool? PrintColumnHeaders { get; set; }

    /// <summary>
    ///     Flag to control whether or not we print the Column Header line
    ///     Defaults to False to match previous functionality
    /// </summary>
    public bool? PrintRowHeaders { get; set; } = false;

    /// <summary>
    ///     Flag to control whether rows are printed whole or if partial
    ///     rows should be printed to fill the bottom of the page. Turn this
    ///     "Off" (i.e. false) to print cells/rows deeper than one page
    /// </summary>
    public bool KeepRowsTogether { get; set; } = true;

    /// <summary>
    ///     How much of a row must show on the current page before it is
    ///     split when KeepRowsTogether is set to true.
    /// </summary>
    public float KeepRowsTogetherTolerance { get; set; } = 15;

    #endregion global properties

    // Title

    #region title properties

    // override flag
    private bool overridetitleformat;

    // formatted height of title
    private float titleheight;

    /// <summary>
    ///     Title for this report. Default is empty.
    /// </summary>
    private string title;

    public string Title
    {
        get => title;
        set
        {
            title = value;
            if (docName == null) printDocument.DocumentName = value;
        }
    }

    /// <summary>
    ///     Name of the document. Default is report title (can be empty)
    /// </summary>
    private string docName;

    public string DocName
    {
        get => docName;
        set
        {
            printDocument.DocumentName = value;
            docName = value;
        }
    }

    /// <summary>
    ///     Font for the title. Default is Tahoma, 18pt.
    /// </summary>
    public Font TitleFont { get; set; }

    /// <summary>
    ///     Foreground color for the title. Default is Black
    /// </summary>
    public Color TitleColor { get; set; }

    /// <summary>
    ///     Allow override of the header cell format object
    /// </summary>
    private StringFormat titleformat;

    public StringFormat TitleFormat
    {
        get => titleformat;
        set
        {
            titleformat = value;
            overridetitleformat = true;
        }
    }

    /// <summary>
    ///     Allow the user to override the title string alignment. Default value is
    ///     Alignment - Near;
    /// </summary>
    public StringAlignment TitleAlignment
    {
        get => titleformat.Alignment;
        set
        {
            titleformat.Alignment = value;
            overridetitleformat = true;
        }
    }

    /// <summary>
    ///     Allow the user to override the title string format flags. Default values
    ///     are: FormatFlags - NoWrap, LineLimit, NoClip
    /// </summary>
    public StringFormatFlags TitleFormatFlags
    {
        get => titleformat.FormatFlags;
        set
        {
            titleformat.FormatFlags = value;
            overridetitleformat = true;
        }
    }

    /// <summary>
    ///     Control where in the document the title prints
    /// </summary>
    public PrintLocation TitlePrint { get; set; } = PrintLocation.All;

    /// <summary>
    ///     Return the title height based whether to print it or not
    /// </summary>
    private float TitleHeight
    {
        get
        {
            if (PrintLocation.All == TitlePrint)
                return titleheight + TitleSpacing;

            if (PrintLocation.FirstOnly == TitlePrint && 1 == CurrentPage)
                return titleheight + TitleSpacing;

            if (PrintLocation.LastOnly == TitlePrint && totalpages == CurrentPage)
                return titleheight + TitleSpacing;

            return 0;
        }
    }

    /// <summary>
    ///     Mandatory spacing between the grid and the footer
    /// </summary>
    public float TitleSpacing { get; set; }

    /// <summary>
    ///     Title Block Background Color
    /// </summary>
    public Brush TitleBackground { get; set; }

    /// <summary>
    ///     Title Block Border
    /// </summary>
    public Pen TitleBorder { get; set; }

    #endregion title properties

    // SubTitle

    #region subtitle properties

    // override flat
    private bool overridesubtitleformat;

    // formatted height of subtitle
    private float subtitleheight;

    /// <summary>
    ///     SubTitle for this report. Default is empty.
    /// </summary>
    public string SubTitle { get; set; }

    /// <summary>
    ///     Font for the subtitle. Default is Tahoma, 12pt.
    /// </summary>
    public Font SubTitleFont { get; set; }

    /// <summary>
    ///     Foreground color for the subtitle. Default is Black
    /// </summary>
    public Color SubTitleColor { get; set; }

    /// <summary>
    ///     Allow override of the header cell format object
    /// </summary>
    private StringFormat subtitleformat;

    public StringFormat SubTitleFormat
    {
        get => subtitleformat;
        set
        {
            subtitleformat = value;
            overridesubtitleformat = true;
        }
    }

    /// <summary>
    ///     Allow the user to override the subtitle string alignment. Default value is
    ///     Alignment - Near;
    /// </summary>
    public StringAlignment SubTitleAlignment
    {
        get => subtitleformat.Alignment;
        set
        {
            subtitleformat.Alignment = value;
            overridesubtitleformat = true;
        }
    }

    /// <summary>
    ///     Allow the user to override the subtitle string format flags. Default values
    ///     are: FormatFlags - NoWrap, LineLimit, NoClip
    /// </summary>
    public StringFormatFlags SubTitleFormatFlags
    {
        get => subtitleformat.FormatFlags;
        set
        {
            subtitleformat.FormatFlags = value;
            overridesubtitleformat = true;
        }
    }

    /// <summary>
    ///     Control where in the document the title prints
    /// </summary>
    public PrintLocation SubTitlePrint { get; set; } = PrintLocation.All;

    /// <summary>
    ///     Return the title height based whether to print it or not
    /// </summary>
    private float SubTitleHeight
    {
        get
        {
            if (PrintLocation.All == SubTitlePrint)
                return subtitleheight + SubTitleSpacing;

            if (PrintLocation.FirstOnly == SubTitlePrint && 1 == CurrentPage)
                return subtitleheight + SubTitleSpacing;

            if (PrintLocation.LastOnly == SubTitlePrint && totalpages == CurrentPage)
                return subtitleheight + SubTitleSpacing;

            return 0;
        }
    }

    /// <summary>
    ///     Mandatory spacing between the grid and the footer
    /// </summary>
    public float SubTitleSpacing { get; set; }

    /// <summary>
    ///     Title Block Background Color
    /// </summary>
    public Brush SubTitleBackground { get; set; }

    /// <summary>
    ///     Title Block Border
    /// </summary>
    public Pen SubTitleBorder { get; set; }

    #endregion subtitle properties

    // Footer

    #region footer properties

    // override flag
    private bool overridefooterformat;

    /// <summary>
    ///     footer for this report. Default is empty.
    /// </summary>
    public string Footer { get; set; }

    /// <summary>
    ///     Font for the footer. Default is Tahoma, 10pt.
    /// </summary>
    public Font FooterFont { get; set; }

    /// <summary>
    ///     Foreground color for the footer. Default is Black
    /// </summary>
    public Color FooterColor { get; set; }

    /// <summary>
    ///     Allow override of the header cell format object
    /// </summary>
    private StringFormat footerformat;

    public StringFormat FooterFormat
    {
        get => footerformat;
        set
        {
            footerformat = value;
            overridefooterformat = true;
        }
    }

    /// <summary>
    ///     Allow the user to override the footer string alignment. Default value is
    ///     Alignment - Center;
    /// </summary>
    public StringAlignment FooterAlignment
    {
        get => footerformat.Alignment;
        set
        {
            footerformat.Alignment = value;
            overridefooterformat = true;
        }
    }

    /// <summary>
    ///     Allow the user to override the footer string format flags. Default values
    ///     are: FormatFlags - NoWrap, LineLimit, NoClip
    /// </summary>
    public StringFormatFlags FooterFormatFlags
    {
        get => footerformat.FormatFlags;
        set
        {
            footerformat.FormatFlags = value;
            overridefooterformat = true;
        }
    }

    /// <summary>
    ///     Mandatory spacing between the grid and the footer
    /// </summary>
    public float FooterSpacing { get; set; }

    /// <summary>
    ///     Control where in the document the title prints
    /// </summary>
    public PrintLocation FooterPrint { get; set; } = PrintLocation.All;

    /// <summary>
    ///     Determine the height of the footer
    /// </summary>
    private float FooterHeight
    {
        get
        {
            float footerheight = 0;

            // return calculated height if we're printing the footer
            if (PrintLocation.All == FooterPrint
                || (PrintLocation.FirstOnly == FooterPrint && 1 == CurrentPage)
                || (PrintLocation.LastOnly == FooterPrint && totalpages == CurrentPage))
                // Add in footer text height
                footerheight += footerHeight + FooterSpacing;

            return footerheight;
        }
    }

    /// <summary>
    ///     Title Block Background Color
    /// </summary>
    public Brush FooterBackground { get; set; }

    /// <summary>
    ///     Title Block Border
    /// </summary>
    public Pen FooterBorder { get; set; }

    #endregion footer properties

    // Page Numbering

    #region page number properties

    // override flag
    private bool overridepagenumberformat;

    /// <summary>
    ///     Include page number in the printout. Default is true.
    /// </summary>
    public bool PageNumbers { get; set; } = true;

    /// <summary>
    ///     Font for the page number, Default is Tahoma, 8pt.
    /// </summary>
    public Font PageNumberFont { get; set; }

    /// <summary>
    ///     Text color (foreground) for the page number. Default is Black
    /// </summary>
    public Color PageNumberColor { get; set; }

    /// <summary>
    ///     Allow override of the header cell format object
    /// </summary>
    private StringFormat pagenumberformat;

    public StringFormat PageNumberFormat
    {
        get => pagenumberformat;
        set
        {
            pagenumberformat = value;
            overridepagenumberformat = true;
        }
    }

    /// <summary>
    ///     Allow the user to override the page number string alignment. Default value is
    ///     Alignment - Near;
    /// </summary>
    public StringAlignment PageNumberAlignment
    {
        get => pagenumberformat.Alignment;
        set
        {
            pagenumberformat.Alignment = value;
            overridepagenumberformat = true;
        }
    }

    /// <summary>
    ///     Allow the user to override the pagenumber string format flags. Default values
    ///     are: FormatFlags - NoWrap, LineLimit, NoClip
    /// </summary>
    public StringFormatFlags PageNumberFormatFlags
    {
        get => pagenumberformat.FormatFlags;
        set
        {
            pagenumberformat.FormatFlags = value;
            overridepagenumberformat = true;
        }
    }

    /// <summary>
    ///     Allow the user to select whether to have the page number at the top or bottom
    ///     of the page. Default is false: page numbers on the bottom of the page
    /// </summary>
    public bool PageNumberInHeader { get; set; } = false;

    /// <summary>
    ///     Should the page number be printed on a separate line, or printed on the
    ///     same line as the header / footer? Default is false;
    /// </summary>
    public bool PageNumberOnSeparateLine { get; set; } = false;

    /// <summary>
    ///     Show the total page number as n of total
    /// </summary>
    public bool ShowTotalPageNumber { get; set; } = false;

    /// <summary>
    ///     Text separating page number and total page number. Default is ' of '.
    /// </summary>
    public string PageSeparator { get; set; } = " of ";

    public string PageText { get; set; } = "Page ";

    public string PartText { get; set; } = " - Part ";

    /// <summary>
    ///     Control where in the document the title prints
    /// </summary>
    public PrintLocation PageNumberPrint { get; set; } = PrintLocation.All;

    /// <summary>
    ///     Determine the height of the footer
    /// </summary>
    private float PageNumberHeight
    {
        get
        {
            // return calculated height if we're printing the footer
            if (PrintLocation.All == PageNumberPrint
                || (PrintLocation.FirstOnly == PageNumberPrint && 1 == CurrentPage)
                || (PrintLocation.LastOnly == PageNumberPrint && totalpages == CurrentPage))
            {
                // return page number height if we're printing it on a separate line
                // if we're not printing on a separate line, but we're suppressing the
                // header or footer then we still need to reserve space for the page number
                if (PageNumberOnSeparateLine)
                    return pagenumberHeight;
                if (PageNumberInHeader && 0 == TitleHeight && 0 == SubTitleHeight)
                    return pagenumberHeight;
                if (!PageNumberInHeader && 0 == FooterHeight) return FooterSpacing + pagenumberHeight;
            }

            return 0;
        }
    }

    #endregion page number properties

    // Header Cell Printing

    #region header cell properties

    public DataGridViewCellStyle RowHeaderCellStyle { get; set; }

    /// <summary>
    ///     Allow override of the row header cell format object
    /// </summary>
    private StringFormat rowheadercellformat;

    public StringFormat GetRowHeaderCellFormat(DataGridView grid)
    {
        // get default values from provided data grid view, but only
        // if we don't already have a header cell format
        if (null != grid && null == rowheadercellformat)
            buildstringformat(ref rowheadercellformat, grid.Rows[0].HeaderCell.InheritedStyle,
                HeaderCellAlignment, StringAlignment.Near, HeaderCellFormatFlags,
                StringTrimming.Word);

        // if we still don't have a header cell format, create an empty
        if (null == rowheadercellformat)
            rowheadercellformat = new StringFormat(HeaderCellFormatFlags);

        return rowheadercellformat;
    }

    /// <summary>
    ///     Default value to show in the row header cell if no value is provided in the DataGridView.
    ///     Defaults to one tab space
    /// </summary>
    public string RowHeaderCellDefaultText { get; set; } = "\t";

    /// <summary>
    ///     Allow override of the header cell format object
    /// </summary>
    public Dictionary<string, DataGridViewCellStyle> ColumnHeaderStyles { get; } = new();

    /// <summary>
    ///     Allow override of the header cell format object
    /// </summary>
    private StringFormat columnheadercellformat;

    public StringFormat GetColumnHeaderCellFormat(DataGridView grid)
    {
        // get default values from provided data grid view, but only
        // if we don't already have a header cell format
        if (null != grid && null == columnheadercellformat)
            buildstringformat(ref columnheadercellformat, grid.Columns[0].HeaderCell.InheritedStyle,
                HeaderCellAlignment, StringAlignment.Near, HeaderCellFormatFlags,
                StringTrimming.Word);

        // if we still don't have a header cell format, create an empty
        if (null == columnheadercellformat)
            columnheadercellformat = new StringFormat(HeaderCellFormatFlags);

        return columnheadercellformat;
    }

    /// <summary>
    ///     Deprecated - use HeaderCellFormat
    ///     Allow the user to override the header cell string alignment. Default value is
    ///     Alignment - Near;
    /// </summary>
    public StringAlignment HeaderCellAlignment { get; set; }

    /// <summary>
    ///     Deprecated - use HeaderCellFormat
    ///     Allow the user to override the header cell string format flags. Default values
    ///     are: FormatFlags - NoWrap, LineLimit, NoClip
    /// </summary>
    public StringFormatFlags HeaderCellFormatFlags { get; set; }

    #endregion header cell properties

    // Individual Cell Printing

    #region cell properties

    /// <summary>
    ///     Allow override of the cell printing format
    /// </summary>
    private StringFormat cellformat;

    public StringFormat GetCellFormat(DataGridView grid)
    {
        // get default values from provided data grid view, but only
        // if we don't already have a cell format
        if (null != grid && null == cellformat)
            buildstringformat(ref cellformat, grid.Rows[0].Cells[0].InheritedStyle,
                CellAlignment, StringAlignment.Near, CellFormatFlags,
                StringTrimming.Word);

        // if we still don't have a cell format, create an empty
        if (null == cellformat)
            cellformat = new StringFormat(CellFormatFlags);

        return cellformat;
    }

    /// <summary>
    ///     Deprecated - use GetCellFormat
    ///     Allow the user to override the cell string alignment. Default value is
    ///     Alignment - Near;
    /// </summary>
    public StringAlignment CellAlignment { get; set; }

    /// <summary>
    ///     Deprecated - use GetCellFormat
    ///     Allow the user to override the cell string format flags. Default values
    ///     are: FormatFlags - NoWrap, LineLimit, NoClip
    /// </summary>
    public StringFormatFlags CellFormatFlags { get; set; }

    /// <summary>
    ///     allow the user to override the column width calcs with their own defaults
    /// </summary>
    private readonly List<float> colwidthsoverride = new();

    public Dictionary<string, float> ColumnWidths { get; } = new();

    /// <summary>
    ///     Allow per column style overrides
    /// </summary>
    public Dictionary<string, DataGridViewCellStyle> ColumnStyles { get; } = new();

    /// <summary>
    ///     Allow per column style overrides
    /// </summary>
    public Dictionary<string, DataGridViewCellStyle> AlternatingRowColumnStyles { get; } = new();

    /// <summary>
    ///     Allow the user to set columns that appear on every pageset. Only used when
    ///     the printout is wider than one page.
    /// </summary>
    private readonly List<int> fixedcolumns = new();

    public List<string> FixedColumns { get; } = new();

    /// <summary>
    ///     List of columns to not display in the grid view printout.
    /// </summary>
    public List<string> HideColumns { get; } = new();

    /// <summary>
    ///     Insert a page break when the value in this column changes
    /// </summary>
    private object oldvalue;

    public string BreakOnValueChange { get; set; }

    #endregion cell properties

    // Page Level Properties

    #region page level properties

    /// <summary>
    ///     Page margins override. Default is (60, 60, 40, 40)
    /// </summary>
    public Margins PrintMargins
    {
        get => PageSettings.Margins;
        set => PageSettings.Margins = value;
    }

    /// <summary>
    ///     Expose the printdocument default page settings to the caller
    /// </summary>
    public PageSettings PageSettings => printDocument.DefaultPageSettings;

    /// <summary>
    ///     Spread the columns porportionally accross the page. Default is false.
    ///     Deprecated. Please use the ColumnWidth property
    /// </summary>
    private bool porportionalcolumns;

    public bool PorportionalColumns
    {
        get => porportionalcolumns;
        set
        {
            porportionalcolumns = value;
            if (porportionalcolumns)
                ColumnWidth = ColumnWidthSetting.Porportional;
            else
                ColumnWidth = ColumnWidthSetting.CellWidth;
        }
    }

    /// <summary>
    ///     Center the table on the page.
    /// </summary>
    public Alignment TableAlignment { get; set; } = Alignment.NotSet;

    /// <summary>
    ///     Change the default row height to either the height of the string or the size of
    ///     the cell. Added for image cell handling; set to CellHeight for image cells
    /// </summary>
    public enum RowHeightSetting
    {
        DataHeight,
        CellHeight
    }

    public RowHeightSetting RowHeight { get; set; } = RowHeightSetting.DataHeight;

    /// <summary>
    ///     Change the default column width to be spread porportionally accross the page,
    ///     to the size of the grid cell or the size of the formatted data string.
    ///     Set to CellWidth for image cells.
    /// </summary>
    public enum ColumnWidthSetting
    {
        DataWidth,
        CellWidth,
        Porportional
    }

    private ColumnWidthSetting _rowwidth = ColumnWidthSetting.CellWidth;

    public ColumnWidthSetting ColumnWidth
    {
        get => _rowwidth;
        set
        {
            _rowwidth = value;
            if (value == ColumnWidthSetting.Porportional)
                porportionalcolumns = true;
            else
                porportionalcolumns = false;
        }
    }

    #endregion page level properties

    // Utility Functions

    #region

    /// <summary>
    ///     calculate the print preview window width to show the entire page
    /// </summary>
    /// <returns></returns>
    private int PreviewDisplayWidth()
    {
        double displayWidth = printDocument.DefaultPageSettings.Bounds.Width
                              + 3 * printDocument.DefaultPageSettings.HardMarginY;
        return (int)(displayWidth * PrintPreviewZoom);
    }

    /// <summary>
    ///     calculate the print preview window height to show the entire page
    /// </summary>
    /// <returns></returns>
    private int PreviewDisplayHeight()
    {
        double displayHeight = printDocument.DefaultPageSettings.Bounds.Height
                               + 3 * printDocument.DefaultPageSettings.HardMarginX;

        return (int)(displayHeight * PrintPreviewZoom);
    }

    /// <summary>
    ///     Invoke any provided cell owner draw routines
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnCellOwnerDraw(DGVCellDrawingEventArgs e)
    {
        if (null != OwnerDraw)
            OwnerDraw(this, e);
    }

    /// <summary>
    ///     Given a row and column, get the current grid cell style, including our local
    ///     overrides
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    protected DataGridViewCellStyle GetStyle(DataGridViewRow row, DataGridViewColumn col)
    {
        // set initial default
        var colstyle = row.Cells[col.Index].InheritedStyle.Clone();

        // check for our override
        if (ColumnStyles.ContainsKey(col.Name)) colstyle = ColumnStyles[col.Name];

        // check for alternating row override
        if (0 != (row.Index & 1) && AlternatingRowColumnStyles.ContainsKey(col.Name))
            colstyle = AlternatingRowColumnStyles[col.Name];

        return colstyle;
    }

    /// <summary>
    ///     Skim the colstoprint list for a column name and return it's index
    /// </summary>
    /// <param name="colname">Name of column to find</param>
    /// <returns>index of column</returns>
    protected int GetColumnIndex(string colname)
    {
        var i = 0;
        foreach (DataGridViewColumn col in colstoprint)
            if (col.Name != colname)
                i++;
            else
                break;

        // catch unknown column names
        if (i >= colstoprint.Count) throw new Exception("Unknown Column Name: " + colname);

        return i;
    }

    #endregion properties

    #endregion
}