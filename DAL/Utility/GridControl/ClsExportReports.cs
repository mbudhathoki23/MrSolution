using Microsoft.Office.Interop.Excel;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace MrDAL.Utility.GridControl;

public class ClsExportReports
{
    // OBJECT FOR THIS CLASS
    public string FileName { get; set; }

    public string CompanyName { get; set; }
    public string CompanyAddress { get; set; }
    public string CompanyPanVatNo { get; set; }
    public string ReportName { get; set; }
    public string ReportDate { get; set; }
    public string AccountingPeriod { get; set; }
    public string AccPeriodDate { get; set; }

    public void ExportReport(DataGridView rGrid)
    {
        try
        {
            var writer = new StreamWriter(FileName, true);
            writer.Write("<html>");
            //-----------Report Heading------------
            writer.Write("<head>");
            writer.Write($"<title>{CompanyName}</title>");
            writer.Write("</head>");
            writer.Write("<body>");
            writer.Write("<P align=Right>");
            writer.Write("<font color=#FF0000>");
            writer.Write($"<align=right><font face=Bookman Old Style size=2><i>{AccountingPeriod}</i> </font> <BR>");
            writer.Write($"<align=right><font face=Bookman Old Style size=2><i>{AccPeriodDate}</i></font></font><BR>");
            writer.Write("</P>");
            writer.Write("<P align=center>");
            writer.Write(
                $"<align=center><font face=Bookman Old Style size=5 color=#800000><b>{CompanyName}</b></font><BR>");
            writer.Write(
                $"<align=center><font face=Bookman Old Style size=3 color=#0000FF><b>{CompanyAddress}</b></font><BR>");
            writer.Write(
                $"<align=center><font face=Bookman Old Style size=3 color=#0000FF><b>{CompanyPanVatNo}</b></font><BR></P>");
            writer.Write("<p align=center>");
            writer.Write(
                $"<align=center><u><font face=Bookman Old Style size=3 color=#0000FF><b>{ReportName}</b></font></u><BR>");
            writer.Write(
                $"<align=center><u><font face=Bookman Old Style size=3 color=#0000FF><b>{ReportDate}</b></font></u>");
            writer.Write("<table border=1 width=100% cellspacing=0 bgcolor=#FFFFFF cellpadding=.5>");

            //-----------Report Column Heading------------
            writer.Write("<tr>");
            for (var i = 0; i < rGrid.Columns.Count; i++)
                if (rGrid.Columns[i].Visible)
                    writer.Write(
                        $"<td align=center><b><font size=2>{rGrid.Columns[i].HeaderText.ToUpper()}</font></b></td>");
            writer.Write("</tr>");
            //-----------Report Details--------------------

            int ro;
            for (ro = 0; ro < rGrid.Rows.Count; ro++)
            {
                writer.Write("<tr>");
                int co;
                for (co = 0; co < rGrid.Columns.Count; co++)
                {
                    var fontBol = string.Empty;
                    if (rGrid.Rows[ro].Cells[co].InheritedStyle.Font.Bold is true) fontBol = "<B>";
                    if (rGrid.Rows[ro].Cells[co].InheritedStyle.Font.Italic is true) fontBol = "<I>";
                    var fontAlign = string.Empty;
                    var fontName = string.Empty;
                    var colorName = string.Empty;
                    if (rGrid.Rows[ro].Cells[co].InheritedStyle.Alignment is DataGridViewContentAlignment.MiddleCenter)
                    {
                        fontAlign = "Align = Center";
                    }
                    else
                    {
                        var result = rGrid.Columns[co].Name;

                        var dataGridViewCellStyle = rGrid.Columns[co].InheritedStyle;
                        var cellStyle = rGrid.Rows[ro].Cells[co].InheritedStyle.Alignment;
                        fontAlign = cellStyle switch
                        {
                            DataGridViewContentAlignment.MiddleRight => "Align = Right",
                            DataGridViewContentAlignment.MiddleLeft => "Align = Left",
                            DataGridViewContentAlignment.MiddleCenter => "Align = Center",
                            _ => fontAlign
                        };
                        fontName = rGrid.Rows[ro].DefaultCellStyle.Font.Name;
                        colorName = rGrid.Rows[ro].DefaultCellStyle.ForeColor.Name;

                        //if (dataGridViewCellStyle?.Alignment is DataGridViewContentAlignment.MiddleLeft)
                        //{
                        //    fontAlign = "Align = Left";
                        //}
                        //else
                        //{
                        //}
                    }

                    if (rGrid.Rows[ro].Cells[co].Visible)
                        writer.Write(
                            $"<td {fontAlign}>{fontBol}<font face={fontName} size=3 color={colorName}>{rGrid.Rows[ro].Cells[co].Value}</font></td></B>");
                }

                writer.Write("</tr>");
            }

            writer.Write("</table>");
            writer.Write("</body>");
            writer.Write("</html>");
            writer.Close();
            //-----------End Report Exporting------------
        }
        catch
        {
            // ignored
        }
    }

    private DataGridView CopyDataGridView(DataGridView dgv_org)
    {
        DataGridView dgv_copy = new();
        try
        {
            if (dgv_copy.Columns.Count == 0)
                foreach (DataGridViewColumn dgvc in dgv_org.Columns)
                    dgv_copy.Columns.Add(dgvc.Clone() as DataGridViewColumn);

            DataGridViewRow row = new();

            for (var i = 0; i < dgv_org.Rows.Count; i++)
            {
                row = (DataGridViewRow)dgv_org.Rows[i].Clone();
                var intColIndex = 0;
                foreach (DataGridViewCell cell in dgv_org.Rows[i].Cells)
                {
                    row.Cells[intColIndex].Value = cell.Value;
                    intColIndex++;
                }

                dgv_copy.Rows.Add(row);
            }

            dgv_copy.AllowUserToAddRows = false;
            dgv_copy.Refresh();
        }
        catch (Exception ex)
        {
            var result = ex.ToString();
        }

        return dgv_copy;
    }

    public void ImportDataGridViewDataToExcelSheet(DataGridView RGrid)
    {
        object misValue = Missing.Value;
        var xlApp = new Application();
        var xlWorkBook = xlApp.Workbooks.Add(misValue);
        var xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
        xlWorkSheet.PageSetup.RightHeader = $@"
            {AccountingPeriod}
            {AccPeriodDate}";
        xlWorkSheet.PageSetup.CenterHeader = $@"
            {CompanyName}"; //{CompanyAddress}            { CompanyPanVatNo}  { ReportName}            { ReportDate}
        for (var x = 1; x < RGrid.Columns.Count + 1; x++) xlWorkSheet.Cells[1, x] = RGrid.Columns[x - 1].HeaderText;
        for (var i = 0; i < RGrid.Rows.Count; i++)
            for (var j = 0; j < RGrid.Columns.Count; j++)
                xlWorkSheet.Cells[i + 2, j + 1] = RGrid.Rows[i].Cells[j].Value.ToString();
        var fileDialog = new SaveFileDialog
        {
            FileName = FileName,
            DefaultExt = ".xlsx"
        };
        if (fileDialog.ShowDialog() == DialogResult.OK)
            xlWorkBook.SaveAs(fileDialog.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        xlWorkBook.Close(true, misValue, misValue);
        xlApp.Quit();
        ReleaseObject(xlWorkSheet);
        ReleaseObject(xlWorkBook);
        ReleaseObject(xlApp);
    }

    private void ReleaseObject(object obj)
    {
        try
        {
            Marshal.ReleaseComObject(obj);
            obj = null;
        }
        catch (Exception ex)
        {
            obj = null;
            MessageBox.Show($@"Exception Occurred while releasing object {ex}");
        }
        finally
        {
            GC.Collect();
        }
    }
}