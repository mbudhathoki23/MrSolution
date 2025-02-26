using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using MrDAL.Global.Common;
using PrintControl.PrintFunction;

namespace PrintControl.PrintClass.DirectPrint;

public class ClsPrintDocument
{
    private static bool result;
    private static readonly PrintPreviewDialog printPreviewDialog1 = new();
    private readonly ObjGlobal _global = new();
#pragma warning disable CS0414 // The field 'ClsPrintDocument.dataGridView1' is assigned but its value is never used
    private readonly DataGridView dataGridView1 = null;
#pragma warning restore CS0414 // The field 'ClsPrintDocument.dataGridView1' is assigned but its value is never used

    public ClsPrintDocument(DataGridView dataGridView1, string TransationDate)
    {
    }

    public static PageSettings MyPageSettings { get; set; } = new();

    public static bool PrintPreview(string Mode, string IsPrint, DataGridView dataGridView1, string TransationDate)
    {
        try
        {
            if (dataGridView1 != null)
            {
                dataGridView1.Dock = DockStyle.None;
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                //dataGridView1.Size = new Size(763, 1019);
                //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);//dataGridView1.Width = 826;//dataGridView1.Height = 1169;//dataGridView1.Size = new Size(826, 1169);

                dataGridView1.Font = new Font("Arial", 9F);
                for (var i = 0; i < dataGridView1.Columns.Count - 1; i++)
                    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[dataGridView1.Columns.Count - 1].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                for (var i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    var colw = dataGridView1.Columns[i].Width;
                    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridView1.Columns[i].Width = colw;
                }
            }

            var printer = new DGVPrinter
            {
                Title = ObjGlobal.LogInCompany
            };
            var strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(ObjGlobal.LogInCompany)) strSql.Append(ObjGlobal.LogInCompany + "\n");

            strSql.Append("\n");
            printer.SubTitle = strSql + TransationDate;
            printer.SubTitleSpacing = 5;
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = $"FY :- {ObjGlobal.SysFiscalYear}              PrintedBy : MrSolution";
            if (null != MyPageSettings)
            {
                MyPageSettings.PaperSize = new PaperSize("PapaerA4", 763, 1019);
                printer.printDocument.DefaultPageSettings = MyPageSettings;
            }

            printer.PageSettings.Landscape = Mode != "Portrait";
            MyPageSettings.Margins = new Margins(20, 40, 20, 20);
            printer.FooterSpacing = 5;
            printer.PreviewDialog = printPreviewDialog1;

            if (string.Compare(IsPrint, IsPrint, StringComparison.Ordinal) == 0)
            {
                printer.PrintDataGridView(dataGridView1);
                result = true;
            }
            else
            {
                printer.PrintPreviewDataGridView(dataGridView1);
                result = false;
            }

            dataGridView1.Dock = DockStyle.Fill;
            return result;
        }
        catch
        {
            return result;
        }
    }

    public static bool NewPrintPreview(string Mode, string IsPrint, DataGridView dataGridView1, string TransationDate)
    {
        var result = false;
        //dataGridView1 = dgv_DisplayReport;
        //dataGridView1.Dock = DockStyle.None;
        dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        if (dataGridView1 != null)
        {
            for (var i = 0; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            for (var i = 0; i < dataGridView1.Columns.Count; i++)
            {
                var colw = dataGridView1.Columns[i].Width;
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridView1.Columns[i].Width = colw;
            }
        }

        dataGridView1.Font = new Font("Arial", 9F);
        var printDocument = new System.Drawing.Printing.PrintDocument();
        printDocument.DefaultPageSettings.PaperSize = new PaperSize("PaperA4", 763, 1019);

        ////------------ PRINT PREVIEW
        var printer = new DGVPrinter
        {
            Title = ObjGlobal.LogInCompany
        };
        var strSql = new StringBuilder();
        if (!string.IsNullOrEmpty(ObjGlobal.CompanyAddress)) strSql.Append(ObjGlobal.CompanyAddress + "\n");
        if (!string.IsNullOrEmpty(ObjGlobal.CompanyPhoneNo)) strSql.Append(ObjGlobal.CompanyPhoneNo + "\n");
        strSql.Append("\n");
        printer.SubTitle = strSql + TransationDate;
        printer.SubTitleSpacing = 5;
        printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
        printer.PageNumbers = true;
        printer.PageNumberInHeader = false;
        printer.PorportionalColumns = true;
        printer.HeaderCellAlignment = StringAlignment.Near;
        printer.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
        printer.HeaderCellAlignment = StringAlignment.Near;
        printer.Footer = $"FY :- {ObjGlobal.SysFiscalYear}              PrintedBy : MrSolution";
        if (null != MyPageSettings)
        {
            MyPageSettings.PaperSize = new PaperSize("PapaerA4", 763, 1019);
            printer.printDocument.DefaultPageSettings = MyPageSettings;
        }

        printer.PageSettings.Landscape = false;
        if (MyPageSettings != null)
        {
            MyPageSettings.Margins = new Margins(20, 60, 20, 20);
        }

        printer.FooterSpacing = 5;
        printer.PreviewDialog = printPreviewDialog1;
        printer.PrintDataGridView(dataGridView1);
        result = true;

        //this.Width = 1019;
        dataGridView1.Dock = DockStyle.Fill;
        return true;
    }

    public static bool PrintA4()
    {
        //if (GetPRT1().ShowDialog() == DialogResult.OK)
        //{
        //	if (GetPRT1().No_Copy > 0)
        //	{
        //		for (int i = 1; i <= GetPRT1().No_Copy; i++)
        //		{
        //			PageNo = 0;
        //			if (PartyLedger == false)
        //			{
        //				printDocument1.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
        //				if (GetPRT1().Paper_Size == "A4 FULL")
        //				{
        //					printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 387, 750);
        //					printDocument1.DefaultPageSettings.PaperSize.PaperName = PaperKind.A4.ToString();
        //				}
        //				else if (GetPRT1().Paper_Size == "LETTER FULL")
        //				{
        //					printDocument1.DefaultPageSettings.PaperSize.PaperName = System.Drawing.Printing.PaperKind.Letter.ToString();
        //				}

        //				printDocument1.DefaultPageSettings.Landscape = GetPRT1().Landscape;
        //				printDocument1.Print();
        //			}
        //		}
        //		if (GetPRT1().N_Line <= 0)
        //		{
        //			return;
        //		}
        //	}
        //}
        return true;
    }

    public void printDocument1_BeginPrint(object sender, CancelEventArgs e)
    {
        //try
        //{
        //	strFormat = new StringFormat
        //	{
        //		Alignment = StringAlignment.Near,
        //		LineAlignment = StringAlignment.Center,
        //		Trimming = StringTrimming.EllipsisCharacter
        //	};

        //	arrColumnLefts.Clear();
        //	arrColumnWidths.Clear();
        //	iCellHeight = 0;
        //	iCount = 0;
        //	bFirstPage = true;
        //	bNewPage = true;

        //	// Calculating Total Widths
        //	GridWidth = 0;
        //	foreach (DataGridViewColumn dgvGridCol in dgv_DisplayReport.Columns)
        //	{
        //		GridWidth += dgvGridCol.Width;
        //	}
        //}
        //catch (Exception ex)
        //{
        //	MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //}
    }

    public void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
    {
        //PrintFunction(e);
        //HSK(e);
        //PrintRpt(e);
    }

    public void PrintHeaderFunction(PrintPageEventArgs e)
    {
        //try
        //{
        //	// ---------Start Report Header ----------------
        //	int LeftMargin = 0;
        //	LeftMargin = iLeftMargin + 650;
        //	if (PRT._Landscape == true)
        //	{
        //		LeftMargin = LeftMargin + 200;
        //	}

        //	myFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
        //	strFormat.Alignment = StringAlignment.Near;
        //	e.Graphics.DrawString(lbl_ComanyName.Text, myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
        //	myFont = new System.Drawing.Font("Arial", 9, FontStyle.Regular);
        //	strFormat.Alignment = StringAlignment.Center;
        //	e.Graphics.DrawString(lbl_AccountingPeriod.Text, myFont, Brushes.Black, new RectangleF(LeftMargin, iTopMargin, lbl_AccountingPeriod.Width, iCellHeight), strFormat);
        //	iTopMargin += (int)(e.Graphics.MeasureString(lbl_ComanyName.Text, myFont).Height) + 5;

        //	strFormat.Alignment = StringAlignment.Near;
        //	e.Graphics.DrawString(lbl_CompanyAddress.Text, myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
        //	strFormat.Alignment = StringAlignment.Center;
        //	e.Graphics.DrawString(lbl_AccPeriodDate.Text, myFont, Brushes.Black, new RectangleF(LeftMargin, iTopMargin, lbl_AccPeriodDate.Width, iCellHeight), strFormat);
        //	iTopMargin += (int)(e.Graphics.MeasureString(lbl_AccPeriodDate.Text, myFont).Height) + 5;

        //	strFormat.Alignment = StringAlignment.Near;
        //	e.Graphics.DrawString(lbl_CompanyPANVATNo.Text, myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
        //	if (ObjGlobal._Printing_DateTime == true)
        //	{
        //		strFormat.Alignment = StringAlignment.Center;
        //		myFont = new System.Drawing.Font("Arial", 8, FontStyle.Regular);
        //		string PrintDate = "Date/Time : " + System.DateTime.Now;
        //		e.Graphics.DrawString(PrintDate, myFont, Brushes.Black, new RectangleF(LeftMargin - 25, iTopMargin, lbl_DateTime.Width + 50, iCellHeight), strFormat);
        //	}
        //	iTopMargin += (int)(e.Graphics.MeasureString(lbl_ReportName.Text, myFont).Height) + 5;

        //	//    if (lbl_Branch.Text.Trim() != "")
        //	//    {
        //	//        e.Graphics.DrawString(lbl_Branch.Text, myFont, Brushes.Black, lbl_Branch.Location.X, lbl_AccPeriodDate.Location.Y);
        //	//    }

        //	//    if (lbl_Branch.Text.Trim() != "")
        //	//    {
        //	//        e.Graphics.DrawString(lbl_Branch.Text, myFont, Brushes.Black, lbl_Branch.Location.X, lbl_AccPeriodDate.Location.Y);
        //	//    }

        //	myFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
        //	strFormat.Alignment = StringAlignment.Center;
        //	e.Graphics.DrawString(lbl_ReportName.Text, myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
        //	iTopMargin += (int)(e.Graphics.MeasureString(lbl_ReportName.Text, myFont).Height) + 5;

        //	strFormat.Alignment = StringAlignment.Center;
        //	myFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
        //	e.Graphics.DrawString(lbl_ReportDate.Text, myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
        //	if (PageNo < 1)
        //	{
        //		if (PRT.Start_Page > 0)
        //		{
        //			PageNo = PRT.Start_Page - 1;
        //		}
        //		else
        //		{
        //			PageNo = 0;
        //		}
        //	}
        //	PageNo = PageNo + 1;
        //	string PNo;
        //	PNo = "Page No. " + PageNo;
        //	strFormat.Alignment = StringAlignment.Center;
        //	e.Graphics.DrawString(PNo, myFont, Brushes.Black, new RectangleF(LeftMargin, iTopMargin, lbl_PageNo.Width, iCellHeight), strFormat);
        //	// iTopMargin += (int)(e.Graphics.MeasureString(lbl_ReportDate.Text, myFont).Height);

        //	//Draw Grid Header
        //	e.Graphics.DrawString("Items Ordered", new System.Drawing.Font(dgv_DisplayReport.Font, FontStyle.Bold), Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top - e.Graphics.MeasureString("Items Ordered", new System.Drawing.Font(dgv_DisplayReport.Font, FontStyle.Bold), e.MarginBounds.Width).Height - 13);
        //	string strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
        //	//Draw Date
        //	e.Graphics.DrawString(strDate, new System.Drawing.Font(dgv_DisplayReport.Font, FontStyle.Bold), Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width - e.Graphics.MeasureString(strDate, new System.Drawing.Font(dgv_DisplayReport.Font, FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top - e.Graphics.MeasureString("Items Ordered", new System.Drawing.Font(new System.Drawing.Font(dgv_DisplayReport.Font, FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);
        //	//Draw Columns
        //	iTopMargin += 20;
        //	myFont = new System.Drawing.Font("Arial", 9, FontStyle.Bold);
        //	if (bFirstPage)
        //	{
        //		foreach (DataGridViewColumn GridCol in dgv_DisplayReport.Columns)
        //		{
        //			if (GridCol.Visible == true)
        //			{
        //				iTmpWidth = (int)(Math.Floor(GridCol.Width / (double)GridWidth * PageWidth));
        //				//iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width / (double)GridWidth * (double)GridWidth * ((double)e.MarginBounds.Width / (double)GridWidth))));
        //				iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText, GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;
        //				// Save width and height of headres
        //				arrColumnLefts.Add(iLeftMargin);
        //				arrColumnWidths.Add(iTmpWidth);
        //				iLeftMargin += iTmpWidth;
        //			}
        //		}
        //	}
        //	iCount = 0;
        //	foreach (DataGridViewColumn GridCol in dgv_DisplayReport.Columns)
        //	{
        //		if (GridCol.Visible == true)
        //		{
        //			e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), new System.Drawing.Rectangle((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount], iHeaderHeight));
        //			e.Graphics.DrawRectangle(Pens.Black, new System.Drawing.Rectangle((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount], iHeaderHeight));
        //			e.Graphics.DrawString(GridCol.HeaderText, myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
        //			iCount++;
        //		}
        //	}
        //	iTopMargin += iHeaderHeight;

        //	//---------End Report Header ----------------
        //}
        //catch (Exception exc)
        //{
        //	MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //}
    }

    public void PrintFunction(PrintPageEventArgs e)
    {
        //	try
        //	{
        //		PageWidth = e.MarginBounds.Width - ((e.MarginBounds.Width * 4) / 100);
        //		iLeftMargin = ((e.MarginBounds.Width * 3) / 100); //Set the left margin
        //		iRightMargin = (PageWidth - iLeftMargin - 20); //Set the Right Margin
        //		iTopMargin = e.MarginBounds.Top; //Set the top margin
        //		iTopMargin = 10;
        //		long LCnt;
        //		LCnt = 0;
        //		//if (dgv_DisplayReport.FixedRows = 2 )
        //		//    LCnt = 1;

        //		////stto = dgv_DisplayReport.FixedRow;
        //		//stto = 1;
        //		//if (PRT.Start_Page > 0)
        //		//    stto = stto + ((PRT.Start_Page - 1) * PRT.N_Line);

        //		//iRow = stto;
        //		//Whether more pages have to print or not
        //		bMorePagesToPrint = false;

        //		System.Drawing.Font myFont = new System.Drawing.Font("Arial", 9, FontStyle.Regular);

        //		//Loop till all the grid rows not get printed

        //		while (iRow <= dgv_DisplayReport.Rows.Count - 1)
        //		{
        //			LCnt = LCnt + 1;
        //			DataGridViewRow GridRow = dgv_DisplayReport.Rows[Convert.ToInt16(iRow)];
        //			//Set the cell height
        //			iCellHeight = GridRow.Height;//+ 5
        //			int iCount = 0;
        //			//Check whether the current page settings allo more rows to print
        //			if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
        //			{
        //				bNewPage = true;
        //				bFirstPage = false;
        //				bMorePagesToPrint = true;
        //				break;
        //			}
        //			else
        //			{
        //				if (bNewPage)
        //				{
        //					PrintHeaderFunction(e);
        //					bNewPage = false;
        //				}
        //				iCount = 0;
        //				//Draw Columns Contents
        //				if ((ObjGlobal._Font_Name != "" && ObjGlobal._Font_Name != null) && (ObjGlobal._Font_Size != 0 && ObjGlobal._Font_Size != null))
        //				{
        //					myFont = new System.Drawing.Font(ObjGlobal._Font_Name, Convert.ToInt16(ObjGlobal._Font_Size), FontStyle.Regular);
        //				}
        //				else
        //				{
        //					myFont = new System.Drawing.Font("Arial", 9, FontStyle.Regular);
        //				}

        //				foreach (DataGridViewCell Cel in GridRow.Cells)
        //				{
        //					if (Cel.Value != null)
        //					{
        //						if (Cel.InheritedStyle.Alignment == DataGridViewContentAlignment.MiddleLeft)
        //						{
        //							strFormat.Alignment = StringAlignment.Near;
        //						}
        //						else if (Cel.InheritedStyle.Alignment == DataGridViewContentAlignment.MiddleCenter)
        //						{
        //							strFormat.Alignment = StringAlignment.Center;
        //						}
        //						else if (Cel.InheritedStyle.Alignment == DataGridViewContentAlignment.MiddleRight)
        //						{
        //							strFormat.Alignment = StringAlignment.Far;
        //						}
        //						//if (Cel.InheritedStyle.Font==FontStyle())
        //						//Cel.InheritedStyle.Font, new SolidBrush(Cel.InheritedStyle.ForeColor),
        //						e.Graphics.DrawString(Cel.Value.ToString(), Cel.InheritedStyle.Font, Brushes.Black,
        //						new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount], iCellHeight), strFormat);
        //					}
        //					iCount++;
        //				}
        //			}
        //			iRow++;
        //			iTopMargin += iCellHeight;
        //		}
        //		//If more lines exist, print another page.
        //		if (bMorePagesToPrint)
        //		{
        //			e.HasMorePages = true;
        //		}
        //		else
        //		{
        //			e.HasMorePages = false;
        //		}

        //		//if (LCnt >= PRT.N_Line && iRow < dgv_DisplayReport.Rows.Count - 1)
        //		//{
        //		//    if (PRT.End_Page != 0)
        //		//    {
        //		//        if (PageNo == PRT.End_Page)
        //		//        {
        //		//            PageNo = 0;
        //		//            return;
        //		//        }
        //		//    }
        //		//    //Printer.NewPage;
        //		//    e.HasMorePages = true;
        //		//    PrintHeaderFunction(e);
        //		//    LCnt = 0;
        //		//    //if (dgv_DisplayReport.FixedRows == 2 )
        //		//    //    LCnt = 1;

        //		//}
        //	}
        //	catch (Exception exc)
        //	{
        //		MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //	}
    }

    private void printDocument2_PrintPage(object sender, PrintPageEventArgs e)
    {
        //	var myPaintArgs = new PaintEventArgs
        //	(
        //		e.Graphics,
        //		new Rectangle(new Point(0, 0), this.Size)
        //	);

        //	this.InvokePaint(dataGridView1, myPaintArgs);
    }
}