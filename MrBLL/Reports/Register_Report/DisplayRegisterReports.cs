﻿using DevExpress.XtraSplashScreen;
using MrBLL.DataEntry.PurchaseMaster;
using MrBLL.DataEntry.SalesMaster;
using MrBLL.Utility.Common;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Reports.Design;
using MrDAL.Reports.Interface;
using MrDAL.Reports.Register;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PrintControl.PrintClass.Domains;

namespace MrBLL.Reports.Register_Report;

public partial class DisplayRegisterReports : MrForm
{
    //DISPLAY REGISTER REPORTS

    #region --------------- DISPLAY REGISTER REPORTS ---------------
    public DisplayRegisterReports()
    {
        InitializeComponent();
        _getReport = new ClsRegisterReport();
        var dialog = new FrmPrintDialog();
    }

    private void DisplayRegisterReports_Shown(object sender, EventArgs e)
    {
        RGrid.Focus();
    }

    private void DisplayRegisterReports_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.Alt)
        {
            case true when e.KeyCode == Keys.P:
            {
                TsBtnPrint_Click(sender, e);
                break;
            }
            case true when e.KeyCode == Keys.E:
            {
                TsBtnExport_Click(sender, e);
                break;
            }
            case true when e.KeyCode == Keys.R:
            {
                TsBtnRefresh_Click(sender, e);
                break;
            }
            case true when e.KeyCode == Keys.F:
            {
                TsBtnSearch_Click(sender, e);
                break;
            }
        }
    }

    private void DisplayRegisterReports_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
            {
                Close();
            }
        }
    }

    private void DisplayRegisterReports_Load(object sender, EventArgs e)
    {
        ComponentValue();
        HeaderText();
        GenerateRegisterReports();
    }

    private void DisplayRegisterReports_Resize(object sender, EventArgs e)
    {
        var difference = PageWidth - RGrid.Width;
        if (RGrid.ColumnCount == 0) return;
        if (WindowState == FormWindowState.Maximized)
        {
            RGrid.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.Width += PageWidth > 0 ? difference / PageWidth * 100 : 0b0);
        }
        else
        {
            RGrid.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.Width -= PageWidth > 0 ? difference / PageWidth * 100 : 0b0);
        }
    }

    #endregion --------------- DISPLAY REGISTER REPORTS ---------------

    //REPORT ZOOMING FUNCTION

    #region --------------- Zooming Function ---------------

    private void ZoomingRpt(string station)
    {
        switch (RptName)
        {
            case "PURCHASE REGISTER" or "PURCHASE VAT REGISTER":
            {
                if (IsSummary)
                {
                    var result = new DisplayRegisterReports
                    {
                        RptMode = "DATE WISE",
                        RptType = "NORMAL",
                        RptName = RptName,
                        RptDate = RptDate,
                        FromAdDate = FromAdDate,
                        ToAdDate = ToAdDate,
                        IsSummary = false,
                        IsAdditionalTerm = IsAdditionalTerm,
                        IsHorizon = IsHorizon,
                        IncludeSalesOrder = IncludeSalesOrder,
                        IncludeSalesChallan = IncludeSalesChallan,
                        IncludeGodown = IncludeGodown,
                        IncludeAltQty = IncludeAltQty,
                        IncludeFreeQty = IncludeFreeQty,
                        IncludeRemarks = IncludeRemarks,
                        IsDate = IsDate,
                        LedgerId = LedgerId,
                        ProductId = ProductId,
                        PGroupId = PGroupId,
                        PSubGroupId = PSubGroupId,
                        CounterId = CounterId,
                        AgentId = AgentId,
                        AreaId = AreaId,
                        DepartmentId = DepartmentId,
                        FilterValue = FilterValue,
                        Module = Module,
                        EntryUser = EntryUser,
                        InvoiceType = InvoiceType,
                        VatTermId = VatTermId,
                        AdditionalVatTermId = AdditionalVatTermId,
                        BranchId = BranchId,
                        FiscalYearId = FiscalYearId,
                        CompanyUnitId = CompanyUnitId
                    };
                    result.ShowDialog();
                }
                else
                {
                    var frm = station switch
                    {
                        "PO" => new FrmPurchaseOrderEntry(true, FilterValue),
                        "PC" => new FrmPurchaseChallanEntry(true, FilterValue),
                        "PB" => new FrmPurchaseInvoiceEntry(true, FilterValue),
                        "PR" => new FrmPurchaseReturnEntry(true, FilterValue),
                        _ => new Form()
                    };
                    if (frm.Text.IsBlankOrEmpty())
                    {
                        frm.Dispose();
                    }
                    else
                    {
                        frm.ShowDialog();
                    }
                }

                break;
            }
            case "SALES REGISTER" or "SALES VAT REGISTER":
            {
                if (IsSummary)
                {
                    var result = new DisplayRegisterReports
                    {
                        RptMode = "DATE WISE",
                        RptType = "NORMAL",
                        RptName = RptName,
                        RptDate = RptDate,
                        FromAdDate = FromAdDate,
                        ToAdDate = ToAdDate,
                        IsSummary = false,
                        IsAdditionalTerm = IsAdditionalTerm,
                        IsHorizon = IsHorizon,
                        IncludeSalesOrder = IncludeSalesOrder,
                        IncludeSalesChallan = IncludeSalesChallan,
                        IncludeGodown = IncludeGodown,
                        IncludeAltQty = IncludeAltQty,
                        IncludeFreeQty = IncludeFreeQty,
                        IncludeRemarks = IncludeRemarks,
                        IsDate = IsDate,
                        LedgerId = LedgerId,
                        ProductId = ProductId,
                        PGroupId = PGroupId,
                        PSubGroupId = PSubGroupId,
                        CounterId = CounterId,
                        AgentId = AgentId,
                        AreaId = AreaId,
                        DepartmentId = DepartmentId,
                        FilterValue = FilterValue,
                        Module = Module,
                        EntryUser = EntryUser,
                        InvoiceType = InvoiceType,
                        BranchId = BranchId,
                        FiscalYearId = FiscalYearId,
                        CompanyUnitId = CompanyUnitId,
                        VatTermId = VatTermId
                    };
                    result.ShowDialog();
                }
                else
                {
                    if (station == "SB" && ObjGlobal.IsIrdRegister)
                    {
                        CustomMessageBox.Information("ONLINE BILLING CAN NOT BE EDIT && DELETE..!!");
                        return;
                    }
                    var frm = station switch
                    {
                        "SO" => new FrmSalesOrderEntry(true, FilterValue),
                        "SC" => new FrmSalesChallanEntry(true, FilterValue),
                        "SB" => new FrmSalesInvoiceEntry(true, FilterValue),
                        "SR" => new FrmSalesReturnEntry(true, FilterValue),
                        _ => new Form()
                    };
                    if (frm.Text.IsBlankOrEmpty())
                    {
                        frm.Dispose();
                    }
                    else frm.ShowDialog();
                }

                break;
            }
            case "AGING":
            {
                var display = new DisplayRegisterReports
                {
                    Text = Text + @" REPORT",
                    RptType = "NORMAL",
                    RptName = "OUTSTANDING",
                    RptMode = RptMode,
                    RptDate = RptDate,
                    FromAdDate = FromAdDate,
                    FromBsDate = FromBsDate,
                    ToAdDate = ToAdDate,
                    ToBsDate = ToBsDate,
                    BranchId = BranchId,
                    CompanyUnitId = CompanyUnitId,
                    FiscalYearId = FiscalYearId,
                    LedgerId = LedgerId,
                    AgentId = AgentId,
                    AreaId = AreaId,
                    IncludePdc = IncludePdc,
                    IncludeAdjustment = IncludeAdjustment,
                    IsCustomer = IsCustomer,
                    GroupBy = "Ledger",
                    AccountType = AccountType,
                };
                display.ShowDialog();
                break;
            }
        }
    }

    #endregion --------------- Zooming Function ---------------

    //ANALYSIS REGISTER REPORTS

    #region --------------- ANALYSIS REPORT ---------------

    private bool GetTopNAnalysisRegisterReport()
    {
        try
        {
            _design.GetTopNRegisterDesign(RGrid, Module);
            Text = Module switch
            {
                "P" => $"TOP {RptMode.ToUpper()} PRODUCT REGISTER REPORTS",
                "V" => $"{RptMode.ToUpper()} VENDOR REGISTER REPORTS",
                "C" => $"{RptMode.ToUpper()} CUSTOMER REGISTER REPORTS",
                _ => Text
            };
            LblReportName.Text = Text;
            var dtOutStanding = _getReport.GetTopNRegisterReport();
            if (dtOutStanding.Rows.Count is 0)
            {
                return false;
            }
            var iRows = 0;
            RGrid.DataSource = dtOutStanding;
            foreach (DataRow drReport in dtOutStanding.Rows)
            {
                var reportType = drReport["IsGroup"].GetInt();
                RGrid.Rows[iRows].DefaultCellStyle.ForeColor = reportType switch
                {
                    1 => Color.BlueViolet,
                    10 => Color.DarkSalmon,
                    11 => Color.IndianRed,
                    22 => Color.BlueViolet,
                    _ => Color.Black
                };
                RGrid.Rows[iRows].DefaultCellStyle.Alignment = reportType switch
                {
                    11 => DataGridViewContentAlignment.MiddleRight,
                    22 => DataGridViewContentAlignment.MiddleRight,
                    99 => DataGridViewContentAlignment.MiddleRight,
                    _ => RGrid.Rows[iRows].DefaultCellStyle.Alignment
                };
                RGrid.Rows[iRows].DefaultCellStyle.Font = reportType switch
                {
                    10 => new Font("Bookman Old Style", 10, FontStyle.Italic),
                    11 => new Font("Bookman Old Style", 10, FontStyle.Italic),
                    22 => new Font("Bookman Old Style", 10, FontStyle.Bold),
                    99 => new Font("Bookman Old Style", 10, FontStyle.Bold),
                    _ => new Font("Bookman Old Style", 10, FontStyle.Regular)
                };
                RGrid.Rows[iRows].Cells["GTxtAltQty"].Value = RGrid.Rows[iRows].Cells["GTxtAltQty"].Value.GetDecimalQtyString();
                RGrid.Rows[iRows].Cells["GTxtQty"].Value = RGrid.Rows[iRows].Cells["GTxtQty"].Value.GetDecimalQtyString();
                RGrid.Rows[iRows].Cells["GTxtAdjustQty"].Value = RGrid.Rows[iRows].Cells["GTxtAdjustQty"].Value.GetDecimalQtyString();
                RGrid.Rows[iRows].Cells["GTxtBalanceQty"].Value = RGrid.Rows[iRows].Cells["GTxtBalanceQty"].Value.GetDecimalQtyString();
                RGrid.Rows[iRows].Cells["GTxtRate"].Value = RGrid.Rows[iRows].Cells["GTxtRate"].Value.GetDecimalComma();
                RGrid.Rows[iRows].Cells["GTxtAmount"].Value = RGrid.Rows[iRows].Cells["GTxtAmount"].Value.GetDecimalComma();
                iRows++;
            }

            // enable row wrap
            RGrid.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            RGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            return RGrid.RowCount > 0;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            this.NotifyError(e.Message);
            return false;
        }
    }

    #endregion --------------- ANALYSIS REPORT ---------------

    //METHOD FOR REGISTER REPORTS

    #region --------------- Method   ---------------

    private void HeaderText()
    {
        lbl_Currency.Visible = false;
        LblComanyName.Text = ObjGlobal.CompanyPrintDesc;
        LblCompanyAddress.Text = ObjGlobal.CompanyAddress;
        LblCompanyPANVATNo.Text = @"PAN/VATNo : [" + ObjGlobal.CompanyPanNo + @"]";
        if (ObjGlobal.SysDateType is "M" && !IsDate || ObjGlobal.SysDateType is "D" && IsDate)
        {
            LblAccPeriodDate.Text = $@"{ObjGlobal.CfStartBsDate} - {ObjGlobal.CfEndBsDate}";
            lbl_DateTime.Text = $@"{DateTime.Now}";
        }
        else
        {
            LblAccPeriodDate.Text = $@"{ObjGlobal.CfStartAdDate.GetDateString()} - {ObjGlobal.CfEndAdDate.GetDateString()}";
            lbl_DateTime.Text = DateTime.Now.ToShortDateString();
        }
        lbl_PageNo.Text = @"Page 1 of 1";
        LblReportName.Text = !string.IsNullOrEmpty(RptMode) ? $"{RptMode.ToUpper()} {RptName}" : RptName.ToUpper();
        LblReportDate.Text = RptDate;
    }

    private void FitGridColumn()
    {
        try
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            RGrid.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells);
            SplashScreenManager.CloseForm(true);
        }
        catch
        {
            SplashScreenManager.CloseForm(true);
        }
    }

    private void ComponentValue()
    {
        try
        {
            if (string.IsNullOrEmpty(RptName))
            {
                return;
            }
            _getReport.GetReports.RptMode = RptMode;
            _getReport.GetReports.EntryUser = EntryUser;
            _getReport.GetReports.RptType = RptType;
            _getReport.GetReports.RptName = RptName;
            _getReport.GetReports.FromAdDate = FromAdDate;
            _getReport.GetReports.ToAdDate = ToAdDate;
            _getReport.GetReports.AccountType = AccountType;
            _getReport.GetReports.InvoiceType = InvoiceType;
            _getReport.GetReports.InvoiceCategory = InvoiceCategory;
            _getReport.GetReports.LedgerId = LedgerId;
            _getReport.GetReports.BranchId = BranchId;
            _getReport.GetReports.FiscalYearId = FiscalYearId;
            _getReport.GetReports.VatTermId = VatTermId;
            _getReport.GetReports.RptType = RptType;
            _getReport.GetReports.RptName = RptName;
            _getReport.GetReports.RptDate = RptDate;
            _getReport.GetReports.GroupBy = 1;
            _getReport.GetReports.IsDetails = IsSummary;
            _getReport.GetReports.IncludeRemarks = IncludeRemarks;
            _getReport.GetReports.IncludePdc = IncludePdc;
            _getReport.GetReports.IsDate = IsDate;
            _getReport.GetReports.LedgerId = LedgerId;
            _getReport.GetReports.ProductId = ProductId;
            _getReport.GetReports.PGroupId = PGroupId;
            _getReport.GetReports.PSubGroupId = PSubGroupId;
            _getReport.GetReports.FilterValue = FilterValue;
            _getReport.GetReports.FilterMode = FilterMode;
            _getReport.GetReports.Module = Module;
            _getReport.GetReports.BranchId = BranchId;
            _getReport.GetReports.DepartmentId = DepartmentId;
            _getReport.GetReports.IncludeAdjustment = IncludeAdjustment;
            _getReport.GetReports.IncludeNarration = IncludeNarration;
            _getReport.GetReports.IncludeReturn = IncludeSalesReturn;
            _getReport.GetReports.IsHorizon = IsHorizon;
            _getReport.GetReports.ColumnsNo = ColumnsNo;
            _getReport.GetReports.AgingDays = AgingDays;
        }
        catch
        {
            // ignored
        }
    }

    private void CalculateTotalPages()
    {
        const int pgSize = 40;
        var rowCount = RGrid.RowCount;
        // if any row left after calculated pages, add one more page
        lbl_PageNo.Text = rowCount % pgSize > 0 ? @$"Page 1 of {rowCount / pgSize}" : @"Page 1 of 1";
    }

    private void GenerateRegisterReports()
    {
        try
        {
            RGrid.SuspendLayout();
            SplashScreenManager.ShowForm(typeof(PleaseWait));

            RptType = RptType.ToUpper();
            RptName = RptName.ToUpper();

            if (RptType.Equals("NORMAL"))
            {
                var result = RptName switch
                {
                    "MATERIALIZE VIEW" => GetMaterializeViewRegister(),
                    "ENTRY LOG REGISTER" => GetEntryLogRegister(),
                    "PURCHASE REGISTER" when IsSummary => GetPurchaseRegisterSummaryReports(),
                    "PURCHASE REGISTER" when !IsSummary => GetPurchaseRegisterDetailsReports(),
                    "PURCHASE VAT REGISTER" when IsSummary => GetPurchaseSalesVatSummaryRegister(),
                    "PURCHASE VAT REGISTER" when !IsSummary => GetPurchaseVatRegisterVoucherWise(),
                    "SALES REGISTER" when IsSummary => GetSalesRegisterSummaryReports(),
                    "SALES REGISTER" when !IsSummary => GetSalesRegisterDetailsReports(),
                    "SALES VAT REGISTER" when IsSummary => GetPurchaseSalesVatSummaryRegister(),
                    "SALES VAT REGISTER" when !IsSummary => GetSalesVatRegister(),
                    "SALES OUTSTANDING" when IsSummary => GetSalesPurchaseOutstandingRegisterReportSummary(),
                    "SALES OUTSTANDING" when !IsSummary => GetSalesPurchaseOutstandingRegisterReportDetails(),
                    "OUTSTANDING" when !IsSummary => GetOutstandingLedgerReports(),
                    "OUTSTANDING" when IsSummary => GetOutstandingLedgerReports(),
                    "AGING" when !IsSummary => GetCustomerVendorAgingReport(),
                    "TOP N ANALYSIS" => GetTopNAnalysisRegisterReport(),
                    "VAT REGISTER" when !IsSummary => GetNormalVatRegister(),
                    "VAT REGISTER" when IsSummary => GetVatRegisterTransactionAbove(),
                    "SALES ANALYSIS" => GetSalesAnalysisReport(),
                    _ => false
                };
                CalculateTotalPages();
                if (result)
                {
                    RGrid.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                    RGrid.ResumeLayout();
                    SplashScreenManager.CloseForm(false);
                }
                else
                {
                    SplashScreenManager.CloseForm(false);
                    MessageBox.Show($@"{RptName} REPORT NOT FOUND..!!", ObjGlobal.Caption);
                    Close();
                    return;
                }
            }
            else
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show($@"{RptName} REPORT NOT FOUND..!!", ObjGlobal.Caption);
                Close();
            }
        }
        catch (Exception ex)
        {
            SplashScreenManager.CloseForm(false);
            ex.DialogResult();
            Close();
        }
    }

    public static void InvokeIfRequired2<T>(T control, Action<T> action) where T : Control
    {
        if (control.InvokeRequired)
        {
            control.Invoke(new Action(() => action(control)));
        }
        else
        {
            action(control);
        }
    }

    #endregion --------------- Method   ---------------

    //EXPORT REPORT TO EXCEL FUNCTION

    #region --------------- Export Event/Method ---------------

    private void TsBtnExport_Click(object sender, EventArgs e)
    {
        var result = ExportReport();
        MessageBox.Show(result ? $" {RptName} REPORT EXPORT COMPLETED..!!" : "SORRY..! REPORT CANNOT EXPORT");
    }

    private bool ExportReport()
    {
        try
        {
            saveFileDialog1.Title = @"Save";
            saveFileDialog1.Filter = @"Excel File 1997-07 |*.Xls|Excel File 2010-13 |*.xlsx|Word |*.Doc|Html Page |*.Html";
            saveFileDialog1.ShowDialog();
            var fileName = saveFileDialog1.FileName;
            using var writer = new StreamWriter(fileName);
            writer.Write("<html>");
            //-----------Report Heading------------
            writer.Write("<head>");
            writer.Write("<title>" + LblComanyName.Text.GetUpper() + "</title>");
            writer.Write("</head>");
            writer.Write("<body>");
            writer.Write("<P align=Right>");
            writer.Write("<font color=#FF0000>");
            writer.Write($"<align=right><font face=Times New Roman size=2><i>{lbl_AccountingPeriod.Text}</i> </font> <BR>");
            writer.Write($"<align=right><font face=Times New Roman size=2><i>{LblAccPeriodDate.Text}</i></font></font><BR>");
            writer.Write("</P>");
            writer.Write("<P align=center>");
            writer.Write($"<align=center><font face=Times New Roman size=5 color=#800000><b>{LblComanyName.Text}</b></font><BR>");
            writer.Write($"<align=center><font face=Times New Roman size=3 color=#0000FF><b>{LblCompanyAddress.Text}</b></font><BR>");
            writer.Write($"<align=center><font face=Times New Roman size=3 color=#0000FF><b>{LblCompanyPANVATNo.Text}</b></font><BR></P>");
            writer.Write("<p align=center>");
            writer.Write($"<align=center><u><font face=Times New Roman size=3 color=#0000FF><b>{LblReportName.Text}</b></font></u><BR>");
            writer.Write($"<align=center><u><font face=Times New Roman size=3 color=#0000FF><b>{LblReportDate.Text}</b></font></u>");
            writer.Write("<table border=1 width=100% cellspacing=0 bgcolor=#FFFFFF cellpadding=.5>");

            //-----------Report Column Heading------------
            writer.Write("<tr>");
            for (var i = 0; i < RGrid.Columns.Count; i++)
            {
                if (!RGrid.Columns[i].Visible)
                {
                    continue;
                }
                writer.Write($"<td align=center><b><font size=2>{RGrid.Columns[i].HeaderText.ToUpper()}</font></b></td>");
            }
            writer.Write("</tr>");

            //-----------Report Details--------------------

            int ro;
            for (ro = 0; ro < RGrid.Rows.Count; ro++)
            {
                writer.Write("<tr>");
                int index;
                for (index = 0; index < RGrid.Columns.Count; index++)
                {
                    var fontBold = string.Empty;
                    if (RGrid.Rows[ro].Cells[index].InheritedStyle.Font.Bold) fontBold = "<B>";
                    if (RGrid.Rows[ro].Cells[index].InheritedStyle.Font.Italic) fontBold = "<I>";
                    var alignCenter = RGrid.Rows[ro].Cells[index].InheritedStyle.Alignment switch
                    {
                        DataGridViewContentAlignment.MiddleCenter => "Align = Center",
                        DataGridViewContentAlignment.MiddleLeft => "Align = Left",
                        DataGridViewContentAlignment.MiddleRight => "Align = Right",
                        _ => "Align = Left"
                    };
                    if (RGrid.Rows[ro].Cells[index].Visible)
                    {
                        writer.Write($"<td {alignCenter}>{fontBold}<font face=Courier New size=2>{RGrid.Rows[ro].Cells[index].Value}</font></td></B>");
                    }
                }
                writer.Write("</tr>");
            }
            writer.Write("</table>");
            writer.Write("</body>");
            writer.Write("</html>");
            writer.Close();

            //-----------End Report Exporting------------
            return true;
        }
        catch
        {
            return false;
        }
    }

    #endregion --------------- Export Event/Method ---------------

    //BUTTON CLICK EVENTS

    #region --------------- BUTTON CLICK EVENTS ---------------

    private void TsBtnSearch_Click(object sender, EventArgs e)
    {
        RGrid.ClearSelection();
        var rptSearch = new FrmRptSearch(RGrid);
        if (rptSearch.ShowDialog() != DialogResult.OK)
        {
        }
    }

    private void TsBtnRefresh_Click(object sender, EventArgs e)
    {
        GenerateRegisterReports();
    }

    private void TsBtnFitColumn_Click(object sender, EventArgs e)
    {
        FitGridColumn();
    }

    private void TsBtnPrint_Click(object sender, EventArgs e)
    {
        CustomPrintFunction();
    }

    private void CustomPrintFunction()
    {
        _control.RGrid = RGrid;
        _control.AccPeriodDate = LblAccPeriodDate.Text;
        _control.CompanyAddress = LblCompanyAddress.Text;
        _control.CompanyPanNo = LblCompanyPANVATNo.Text;
        _control.ReportDate = LblReportDate.Text;
        _control.ReportName = LblReportName.Text;
        var printDialog = new PrintDialog
        {
            Document = printDocument1,
            UseEXDialog = true
        };
        if (DialogResult.OK == printDialog.ShowDialog())
        {
            printDocument1.DocumentName = RptName;
            printDocument1.DefaultPageSettings.Margins = new Margins(20, 20, 50, 20);
            printDocument1.Print();
            var result = printDialog.AllowPrintToFile;
            if (result.GetHashCode() > 0)
            {
                MessageBox.Show($@"{RptName.ToUpper()} PRINT SUCCESSFULLY..!!", ObjGlobal.Caption);
            }
        }
    }

    #endregion --------------- BUTTON CLICK EVENTS ---------------

    //PRINT REPORT FUNCTION

    #region --------------- BEGIN PRINT EVENT HANDLER ---------------

    private void PrintDocument1_BeginPrint(object sender, System.ComponentModel.CancelEventArgs e)
    {
        _control.PrintDocument1_BeginPrint(sender, e);
    }

    private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
    {
        _control.PrintDocument1_PrintPage(sender, e);
    }

    #endregion --------------- BEGIN PRINT EVENT HANDLER ---------------

    //GRID EVENTS

    #region --------------- GRID EVENT ---------------

    private void RGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void RGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        _rowIndex = e.RowIndex;
    }

    private void RGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
        {
            return;
        }
        RGrid.Rows[_rowIndex].Selected = true;
        e.SuppressKeyPress = true;
        if (RptName is null) return;
        switch (RptName.ToUpper())
        {
            case "PURCHASE REGISTER":
            {
                if (RptMode.GetUpper() is "PRODUCT WISE" or "PRODUCT GROUP WISE" or "PRODUCT SUBGROUP WISE")
                {
                    ProductId = RGrid.CurrentRow?.Cells["GTxtLedgerId"].Value?.ToString();
                }
                else
                {
                    FilterValue = RGrid.CurrentRow?.Cells["GTxtVoucherNo"].Value?.ToString();
                }
                if (FilterValue.IsBlankOrEmpty())
                {
                    return;
                }
                ZoomingRpt(Module);
                break;
            }
            case "SALES REGISTER":
            {
                if (RptMode.GetUpper() is "PRODUCT WISE" or "PRODUCT GROUP WISE" or "PRODUCT SUBGROUP WISE")
                {
                    ProductId = RGrid.CurrentRow?.Cells["GTxtLedgerId"].Value?.ToString();
                }
                else
                {
                    FilterValue = RGrid.CurrentRow?.Cells["GTxtVoucherNo"].Value?.ToString();
                }

                if (FilterValue.IsBlankOrEmpty())
                {
                    return;
                }
                if (Module.Equals("SB") && !IsSummary)
                {
                    var dtSales = GetConnection.SelectDataTableQuery($"SELECT sm.Invoice_Mode FROM AMS.SB_Master sm WHERE sm.SB_Invoice = '{FilterValue}'AND sm.Invoice_Mode NOT IN ('POS','AVT','RESTRO','RSB'); ");
                    if (dtSales.Rows.Count <= 0)
                    {
                        MessageBox.Show(@"THIS INVOICE CAN'T BE EDITED OR LOCK BY SYSTEM..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                ZoomingRpt(Module);
                break;
            }
            case "MATERIALIZE VIEW":
            {
                FilterValue = RGrid.CurrentRow?.Cells["GTxtVoucherNo"].Value?.ToString();
                if (FilterValue.IsBlankOrEmpty())
                {
                    return;
                }
                ZoomingRpt(Module);
                break;
            }
            case "SALES VAT REGISTER" or "PURCHASE VAT REGISTER" when IsSummary:
            {
                LedgerId = RGrid.CurrentRow?.Cells["GTxtLedgerId"].Value?.ToString();
                if (FilterValue.IsBlankOrEmpty())
                {
                    return;
                }
                ZoomingRpt(Module);
                break;
            }
            case "SALES VAT REGISTER" or "PURCHASE VAT REGISTER" when !IsSummary:
            {
                FilterValue = RGrid.CurrentRow?.Cells["GTxtVoucherNo"].Value?.ToString();
                if (FilterValue.IsBlankOrEmpty())
                {
                    return;
                }
                ZoomingRpt(Module);
                break;
            }
            case "OUTSTANDING VENDOR" or "OUTSTANDING CUSTOMER":
            {
                FilterValue = RGrid.CurrentRow?.Cells[7].Value?.ToString();
                if (FilterValue.IsBlankOrEmpty())
                {
                    return;
                }
                if (RGrid.CurrentRow != null)
                {
                    ZoomingRpt(RGrid.CurrentRow.Cells[8].Value.ToString());
                }
                break;
            }
            case "AGING":
            {
                FilterValue = RGrid.CurrentRow?.Cells["GTxtLedgerId"].Value?.ToString();
                if (FilterValue.IsBlankOrEmpty())
                {
                    return;
                }
                if (RGrid.CurrentRow != null)
                {
                    ZoomingRpt("");
                }
                break;
            }
        }
    }

    private void RGrid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
    }

    private void RGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        RGrid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
    }

    private void RGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void RGrid_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
    {
        var rtHeader = RGrid.DisplayRectangle;
        rtHeader.Height = RGrid.ColumnHeadersHeight / 2;
        RGrid.Invalidate(rtHeader);
    }

    private void RGrid_Scroll(object sender, ScrollEventArgs e)
    {
        var rtHeader = RGrid.DisplayRectangle;
        rtHeader.Height = RGrid.ColumnHeadersHeight / 2;
        RGrid.Invalidate(rtHeader);
    }

    private void RGrid_Paint(object sender, PaintEventArgs e)
    {
    }

    private void RGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
    {
        if (e.RowIndex == -1 && e.ColumnIndex > -1)
        {
            e.PaintBackground(e.CellBounds, false);
            var r2 = e.CellBounds;
            r2.Y += e.CellBounds.Height / 2;
            r2.Height = e.CellBounds.Height / 2;
            e.PaintContent(r2);
            e.Handled = true;
        }
    }

    private void RGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
    }

    #endregion --------------- GRID EVENT ---------------

    //PURCHASE REGISTER REPORT FUNCTION

    #region --------------- PURCHASE REGISTER ---------------

    //Summary

    #region **---------- SUMMARY ----------**

    private bool GetPurchaseRegisterSummaryReports()
    {
        try
        {
            var isProduct = RptMode.GetUpper() is "PRODUCT WISE" or "PRODUCT GROUP" or "PRODUCT SUBGROUP WISE";
            _design.GetPurchaseSalesSummaryRegisterDesign(RGrid, Module, isProduct);

            var result = ObjGlobal.SysDateType is "M" && !IsDate || ObjGlobal.SysDateType is "D" && IsDate;
            RGrid.Columns["GTxtMiti"].Visible = result && !isProduct;
            RGrid.Columns["GTxtDate"].Visible = !RGrid.Columns["GTxtMiti"].Visible && !isProduct;

            if (isProduct)
            {
                RGrid.Columns["GTxtQty"].Visible = isProduct;
            }

            Text = Module switch
            {
                "PIN" => $"{RptMode.ToUpper()} PURCHASE INDENT SUMMARY REGISTER REPORTS",
                "PO" => $"{RptMode.ToUpper()} PURCHASE ORDER SUMMARY REGISTER REPORTS",
                "PC" => $"{RptMode.ToUpper()} PURCHASE CHALLAN SUMMARY REGISTER REPORTS",
                "PB" => $"{RptMode.ToUpper()} PURCHASE INVOICE SUMMARY REGISTER REPORTS",
                "APB" => $"{RptMode.ToUpper()} ABBREVIATE PURCHASE INVOICE SUMMARY REGISTER REPORTS",
                "PR" => $"{RptMode.ToUpper()} PURCHASE RETURN SUMMARY REGISTER REPORTS",
                "PAB" => $"{RptMode.ToUpper()} PURCHASE ADDITIONAL SUMMARY REGISTER REPORTS",
                "PBC" => $"{RptMode.ToUpper()} PURCHASE INVOICE CANCEL SUMMARY REGISTER REPORTS",
                "PBT" => $"{RptMode.ToUpper()} PURCHASE TRAVEL & TOUR SUMMARY REGISTER REPORTS",
                _ => $"{RptMode.ToUpper()} PURCHASE INVOICE SUMMARY REGISTER REPORTS"
            };
            if (("RETURN", "CANCEL").ToString().Contains(InvoiceType))
            {
                Text += $@" [{InvoiceType}]";
            }
            LblReportName.Text = Text;
            var dtPurchase = Module switch
            {
                "PC" => _getReport.GetPurchaseChallanRegisterSummary(),
                "PB" => _getReport.GetPurchaseInvoiceRegisterSummary(),
                "PR" => _getReport.GetPurchaseReturnRegisterSummary(),
                _ => _getReport.GetPurchaseInvoiceRegisterSummary()
            };
            if (dtPurchase.Rows.Count <= 0)
            {
                return false;
            }
            RGrid.DataSource = dtPurchase;

            return RGrid.RowCount > 0;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            MessageBox.Show(e.Message, ObjGlobal.Caption);
            return false;
        }
    }

    #endregion **---------- SUMMARY ----------**

    //Details

    #region **---------- DETAILS ----------**

    private bool GetPurchaseRegisterDetailsReports()
    {
        try
        {
            _design.GetPurchaseSalesDetailsRegisterDesign(RGrid, Module, IsHorizon);
            Text = Module switch
            {
                "PIN" => $"{RptMode.ToUpper()} PURCHASE INDENT SUMMARY REGISTER REPORTS",
                "PO" => $"{RptMode.ToUpper()} PURCHASE ORDER SUMMARY REGISTER REPORTS",
                "PC" => $"{RptMode.ToUpper()} PURCHASE CHALLAN SUMMARY REGISTER REPORTS",
                "PB" => $"{RptMode.ToUpper()} PURCHASE INVOICE SUMMARY REGISTER REPORTS",
                "APB" => $"{RptMode.ToUpper()} ABBREVIATE PURCHASE INVOICE SUMMARY REGISTER REPORTS",
                "PR" => $"{RptMode.ToUpper()} PURCHASE RETURN SUMMARY REGISTER REPORTS",
                "PAB" => $"{RptMode.ToUpper()} PURCHASE ADDITIONAL SUMMARY REGISTER REPORTS",
                "PBC" => $"{RptMode.ToUpper()} PURCHASE INVOICE CANCEL SUMMARY REGISTER REPORTS",
                "PBT" => $"{RptMode.ToUpper()} PURCHASE TRAVEL & TOUR SUMMARY REGISTER REPORTS",
                _ => $"{RptMode.ToUpper()} PURCHASE INVOICE SUMMARY REGISTER REPORTS"
            };
            LblReportName.Text = Text;
            var dtPurchase = Module switch
            {
                "PC" => _getReport.GetPurchaseChallanRegisterDetails(),
                "PB" => _getReport.GetPurchaseInvoiceRegisterDetails(),
                "PR" => _getReport.GetPurchaseReturnRegisterDetails(),
                _ => _getReport.GetPurchaseInvoiceRegisterDetails()
            };
            var result = ObjGlobal.SysDateType is "M" && !IsDate || ObjGlobal.SysDateType is "D" && IsDate;
            RGrid.Columns["GTxtMiti"].Visible = result;
            RGrid.Columns["GTxtDate"].Visible = !RGrid.Columns["GTxtMiti"].Visible;
            RGrid.Columns["GTxtAltQty"].Visible = RGrid.Columns["GTxtAltUOM"].Visible = IncludeAltQty;

            if (dtPurchase.Rows.Count is 0)
            {
                return false;
            }

            RGrid.DataSource = dtPurchase;

            return RGrid.RowCount > 0;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            this.NotifyError(e.Message);
            return false;
        }
    }

    #endregion **---------- DETAILS ----------**

    #endregion --------------- PURCHASE REGISTER ---------------

    //SALES REGISTER REPORT FUNCTION

    #region --------------- SALES REGISTER ---------------

    //Summary

    #region **---------- SUMMARY ----------**

    private bool GetSalesRegisterSummaryReports()
    {
        var isProduct = RptMode.GetUpper() is "PRODUCT WISE" or "PRODUCT GROUP" or "PRODUCT SUBGROUP WISE";

        _design.GetPurchaseSalesSummaryRegisterDesign(RGrid, Module, isProduct);
        var dtCheckTerm = _getReport.GetPurchaseSalesTermName(false);
        if (!isProduct)
        {
            RGrid.Columns["GTxtMiti"].Visible = ObjGlobal.SysDateType is "M" && !IsDate || ObjGlobal.SysDateType is "D" && IsDate;
            RGrid.Columns["GTxtDate"].Visible = !RGrid.Columns["GTxtMiti"].Visible;
        }

        //if (RptMode.ToUpper() is "CUSTOMER WISE")
        //{
        //    RGrid.Columns["GTxtInvoiceNo"].Visible = false;
        //}
        if (isProduct)
        {
            RGrid.Columns["GTxtQty"].Visible = isProduct;
        }

        if (RGrid == null)
        {
            return RGrid.RowCount > 0;
        }
        RGrid.Columns["GTxtBasic"].Visible = !InvoiceCategory.GetUpper().Equals("PARTIAL");
        if (InvoiceCategory.GetUpper().Equals("PARTIAL"))
        {
            if (dtCheckTerm != null && dtCheckTerm.Rows.Count > 0 && InvoiceCategory.GetUpper().Equals("PARTIAL"))
            {
                foreach (DataRow row in dtCheckTerm.Rows)
                {
                    var column = row["TermDesc"].ToString();
                    RGrid.Columns[column].Visible = !InvoiceCategory.GetUpper().Equals("PARTIAL");
                }
            }
        }

        Text = Module switch
        {
            "SQ" => $"{RptMode.ToUpper()} SALES QUOTATION SUMMARY REGISTER REPORTS",
            "SO" => $"{RptMode.ToUpper()} SALES ORDER SUMMARY REGISTER REPORTS",
            "SC" => $"{RptMode.ToUpper()} SALES CHALLAN SUMMARY REGISTER REPORTS",
            "SB" => $"{RptMode.ToUpper()} SALES INVOICE SUMMARY REGISTER REPORTS",
            "PSB" => $"{RptMode.ToUpper()} POINT OF SALES INVOICE SUMMARY REGISTER REPORTS",
            "ASB" => $"{RptMode.ToUpper()} ABBREVIATE SALES INVOICE SUMMARY REGISTER REPORTS",
            "SR" => $"{RptMode.ToUpper()} SALES RETURN SUMMARY REGISTER REPORTS",
            "SAB" => $"{RptMode.ToUpper()} SALES ADDITIONAL SUMMARY REGISTER REPORTS",
            "SBC" => $"{RptMode.ToUpper()} SALES INVOICE CANCEL SUMMARY REGISTER REPORTS",
            "SBT" => $"{RptMode.ToUpper()} SALES TRAVEL & TOUR SUMMARY REGISTER REPORTS",
            _ => $"{RptMode.ToUpper()} SALES INVOICE SUMMARY REGISTER REPORTS"
        };
        if (("RETURN", "CANCEL").ToString().Contains(InvoiceType))
        {
            Text += $@" [{InvoiceType}]";
        }
        LblReportName.Text = Text;

        var dtSales = Module switch
        {
            "SQ" => _getReport.GetSalesQuotationRegisterSummary(),
            "SO" => _getReport.GetSalesOrderRegisterSummary(),
            "SC" => _getReport.GetSalesChallanRegisterSummary(),
            "SB" when !InvoiceCategory.Equals("NORMAL") => _getReport.GetSalesInvoiceRegisterInvoiceTypeSummary(),
            "SB" when InvoiceCategory.Equals("NORMAL") => _getReport.GenerateSalesInvoiceRegisterSummaryReports(),
            "SR" when !InvoiceCategory.Equals("NORMAL") => _getReport.GetSalesReturnRegisterSummary(),
            "SR" when InvoiceCategory.Equals("NORMAL") => _getReport.GetSalesReturnRegisterSummary(),
            _ => _getReport.GenerateSalesInvoiceRegisterSummaryReports()
        };
        if (dtSales.Rows.Count is 0)
        {
            return false;
        }
        RGrid.DataSource = dtSales;
        return RGrid.RowCount > 0;
    }

    #endregion **---------- SUMMARY ----------**

    //Details

    #region **---------- DETAILS ----------**

    private bool GetSalesRegisterDetailsReports()
    {
        try
        {
            var isProduct = RptMode.GetUpper() is "PRODUCT WISE" or "PRODUCT GROUP" or "PRODUCT SUBGROUP WISE";
            _design.GetPurchaseSalesDetailsRegisterDesign(RGrid, Module, IsHorizon, isProduct);
            Text = Module switch
            {
                "SQ" => $"SALES QUOTATION {RptMode.ToUpper()} DETAILS REGISTER REPORTS",
                "SO" => $"SALES ORDER {RptMode.ToUpper()} DETAILS REGISTER REPORTS",
                "SC" => $"SALES CHALLAN {RptMode.ToUpper()} DETAILS REGISTER REPORTS",
                "SB" => $"SALES INVOICE {RptMode.ToUpper()} DETAILS REGISTER REPORTS",
                "PSB" => $"POINT OF SALES INVOICE {RptMode.ToUpper()} DETAILS REGISTER REPORTS",
                "ASB" => $"ABBREVIATION SALES INVOICE {RptMode.ToUpper()} DETAILS REGISTER REPORTS",
                "SR" => $"SALES RETURN {RptMode.ToUpper()} DETAILS REGISTER REPORTS",
                "SAB" => $"SALES QUOTATION {RptMode.ToUpper()} DETAILS REGISTER REPORTS",
                "SBC" => $"SALES QUOTATION {RptMode.ToUpper()} DETAILS REGISTER REPORTS",
                "SBT" => $"SALES QUOTATION {RptMode.ToUpper()} DETAILS REGISTER REPORTS",
                _ => $"SALES INVOICE {RptMode.ToUpper()} DETAILS REGISTER REPORTS"
            };
            LblReportName.Text = Text;
            var dtSales = Module switch
            {
                "SQ" => _getReport.GetSalesQuotationRegisterDetails(),
                "SO" => _getReport.GetSalesOrderRegisterDetails(),
                "SC" => _getReport.GetSalesChallanRegisterDetails(),
                "SB" => _getReport.GetSalesInvoiceRegisterDetails(),
                "SR" => _getReport.GetSalesReturnRegisterDetails(),
                _ => _getReport.GetSalesInvoiceRegisterDetails()
            };
            if (dtSales.Rows.Count is 0)
            {
                return false;
            }
            RGrid.DataSource = dtSales;
            if (RGrid != null)
            {
                var result = ObjGlobal.SysDateType is "M" && !IsDate || ObjGlobal.SysDateType is "D" && IsDate;
                RGrid.Columns["GTxtMiti"].Visible = result;
                RGrid.Columns["GTxtDate"].Visible = !RGrid.Columns["GTxtMiti"].Visible;
                RGrid.Columns["GTxtAltQty"].Visible = RGrid.Columns["GTxtAltUOM"].Visible = IncludeAltQty;
            }
            return RGrid.RowCount > 0;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            return false;
        }
    }

    #endregion **---------- DETAILS ----------**

    #endregion --------------- SALES REGISTER ---------------

    //OUTSTANDING REPORT FUNCTION

    #region --------------- OUTSTANDING REPORT ---------------

    private bool GetOutstandingLedgerReports()
    {
        try
        {
            _design.GetPartyOutstandingDesign(RGrid);
            var dtOutstanding = IsSummary
                ? _getReport.GetPartyOutstandingSummaryReport(IsSummary)
                : _getReport.GetPartyOutstandingDetailsReport(IsCustomer);
            if (dtOutstanding.Rows.Count is 0)
            {
                return false;
            }
            var result = ObjGlobal.SysDateType is "M" && !IsDate || ObjGlobal.SysDateType is "D" && IsDate;
            RGrid.Columns["GTxtMiti"].Visible = result;
            RGrid.Columns["GTxtDate"].Visible = !result;
            RGrid.Columns["GTxtAdjMiti"].Visible = result && IncludeAdjustment;
            RGrid.Columns["GTxtAdjDate"].Visible = !result && IncludeAdjustment;
            RGrid.Columns["GTxtAdjVoucher"].Visible = IncludeAdjustment;
            RGrid.DataSource = dtOutstanding;
            return RGrid.RowCount > 0;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            return false;
        }
    }

    private bool GetSalesPurchaseOutstandingRegisterReportSummary()
    {
        try
        {
            _design.GetOutstandingRegisterDesign(RGrid, Module);
            Text = Module switch
            {
                "SQ" => $"{RptMode.ToUpper()} SALES QUOTATION SUMMARY OUTSTANDING REGISTER REPORTS",
                "SO" => $"{RptMode.ToUpper()} SALES ORDER SUMMARY OUTSTANDING  REGISTER REPORTS",
                "SC" => $"{RptMode.ToUpper()} SALES CHALLAN SUMMARY OUTSTANDING  REGISTER REPORTS",
                "PIN" => $"{RptMode.ToUpper()} PURCHASE INDENT SUMMARY OUTSTANDING REGISTER REPORTS",
                "PO" => $"{RptMode.ToUpper()} PURCHASE ORDER SUMMARY OUTSTANDING  REGISTER REPORTS",
                "PC" => $"{RptMode.ToUpper()} PURCHASE CHALLAN SUMMARY OUTSTANDING  REGISTER REPORTS",
                _ => Text
            };
            LblReportName.Text = Text;
            var dtOutStanding = _getReport.GetOutstandingSummaryReport();
            if (dtOutStanding.Rows.Count is 0)
            {
                return false;
            }
            RGrid.DataSource = dtOutStanding;
            if (RGrid == null)
            {
                return false;
            }
            var result = ObjGlobal.SysDateType is "M" && !IsDate || ObjGlobal.SysDateType is "D" && IsDate;
            RGrid.Columns["GTxtMiti"].Visible = result;
            RGrid.Columns["GTxtAdjustMiti"].Visible = result;

            RGrid.Columns["GTxtDate"].Visible = !result;
            RGrid.Columns["GTxtAdjustDate"].Visible = !result;

            RGrid.Columns["GTxtAdjustMiti"].Visible = IncludeAdjustment && result;
            RGrid.Columns["GTxtAdjustDate"].Visible = IncludeAdjustment && !result;

            RGrid.Columns["GTxtAdjustVoucher"].Visible = IncludeAdjustment;
            RGrid.Columns["GTxtAdjustQty"].Visible = IncludeAdjustment;
            RGrid.Columns["GTxtAdjustQty"].Visible = IncludeAdjustment;
            RGrid.Columns["GTxtBalanceQty"].Visible = IncludeAdjustment;
            return RGrid.RowCount > 0;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            e.DialogResult();
            return false;
        }
    }

    private bool GetSalesPurchaseOutstandingRegisterReportDetails()
    {
        try
        {
            _design.GetOutstandingRegisterDesign(RGrid, Module);
            Text = Module switch
            {
                "SQ" => $"{RptMode.ToUpper()} SALES QUOTATION SUMMARY OUTSTANDING REGISTER REPORTS",
                "SO" => $"{RptMode.ToUpper()} SALES ORDER SUMMARY OUTSTANDING REGISTER REPORTS",
                "SC" => $"{RptMode.ToUpper()} SALES CHALLAN SUMMARY OUTSTANDING REGISTER REPORTS",
                "PIN" => $"{RptMode.ToUpper()} PURCHASE INDENT SUMMARY OUTSTANDING REGISTER REPORTS",
                "PO" => $"{RptMode.ToUpper()} PURCHASE ORDER SUMMARY OUTSTANDING REGISTER REPORTS",
                "PC" => $"{RptMode.ToUpper()} PURCHASE CHALLAN SUMMARY OUTSTANDING REGISTER REPORTS",
                _ => Text
            };

            LblReportName.Text = Text;
            var dtOutStanding = _getReport.GetOutstandingDetailsReport();
            if (dtOutStanding.Rows.Count is 0)
            {
                return false;
            }
            RGrid.DataSource = dtOutStanding;

            if (RGrid != null)
            {
                var result = ObjGlobal.SysDateType is "M" && !IsDate || ObjGlobal.SysDateType is "D" && IsDate;
                RGrid.Columns["GTxtMiti"].Visible = result;
                RGrid.Columns["GTxtAdjustMiti"].Visible = result;
                RGrid.Columns["GTxtDate"].Visible = !RGrid.Columns["GTxtMiti"].Visible;
                RGrid.Columns["GTxtAdjustDate"].Visible = !RGrid.Columns["GTxtAdjustMiti"].Visible;
                RGrid.Columns["GTxtAdjustMiti"].Visible = IncludeAdjustment;
                RGrid.Columns["GTxtAdjustDate"].Visible = IncludeAdjustment;
                RGrid.Columns["GTxtAdjustVoucher"].Visible = IncludeAdjustment;
                RGrid.Columns["GTxtAdjustQty"].Visible = IncludeAdjustment;
                RGrid.Columns["GTxtAdjustQty"].Visible = IncludeAdjustment;
                RGrid.Columns["GTxtBalanceQty"].Visible = IncludeAdjustment;
            }
            return RGrid.RowCount > 0;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            this.NotifyError(e.Message);
            return false;
        }
    }

    #endregion --------------- OUTSTANDING REPORT ---------------

    //ANALYSIS REPORT

    #region --------------- ANALYSIS REPORT ---------------

    private bool GetSalesAnalysisReport()
    {
        try
        {
            _design.GetSalesAnalysisDesign(RGrid);
            var dtAnalysis = _getReport.GetSalesAnalysisReport();
            if (dtAnalysis.Rows.Count is 0)
            {
                return false;
            }
            RGrid.DataSource = dtAnalysis;
            return RGrid.RowCount > 0;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            return false;
        }
    }

    #endregion --------------- ANALYSIS REPORT ---------------

    //AGING REPORT

    #region --------------- AGING REPORT ---------------

    private bool GetCustomerVendorAgingReport()
    {
        try
        {
            _design.GetPartyAgingReportDesign(RGrid, AgingDays, ColumnsNo);
            var dtAnalysis = _getReport.GetPartyAgingReport(IsCustomer);
            if (dtAnalysis.Rows.Count is 0)
            {
                return false;
            }
            RGrid.DataSource = dtAnalysis;
            return RGrid.RowCount > 0;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            return false;
        }
    }

    #endregion --------------- AGING REPORT ---------------

    //VAT REGISTER REPORT

    #region --------------- VAT REPORT ---------------

    private bool GetNormalVatRegister()
    {
        try
        {
            _design.GetNormalVatRegisterDesign(RGrid, !NepaliColumn);

            if (ObjGlobal.SysDateType is "M")
            {
                RGrid.Columns["GTxtSalesMiti"].Visible = !IsDate;
                RGrid.Columns["GTxtPurchaseMiti"].Visible = !IsDate;
                RGrid.Columns["GTxtSalesDate"].Visible = IsDate;
                RGrid.Columns["GTxtPurchaseDate"].Visible = IsDate;
            }
            else
            {
                RGrid.Columns["GTxtSalesMiti"].Visible = IsDate;
                RGrid.Columns["GTxtPurchaseMiti"].Visible = IsDate;
                RGrid.Columns["GTxtSalesDate"].Visible = !IsDate;
                RGrid.Columns["GTxtPurchaseDate"].Visible = !IsDate;
            }

            var dtVat = _getReport.GetVatRegisterNormal();
            LblReportName.Text = Module switch
            {
                _ => $" VAT REGISTER REPORT [{RptMode}]"
            };
            if (dtVat.Rows.Count is 0)
            {
                return false;
            }
            RGrid.DataSource = dtVat;
            return RGrid.RowCount > 0;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return false;
        }
    }

    private bool GetVatRegisterTransactionAbove()
    {
        try
        {
            _design.GetVatRegisterTransactionDesign(RGrid, !NepaliColumn);
            var dtVat = _getReport.GetVatRegisterTransactionValue();
            LblReportName.Text = Module switch
            {
                _ => $" VAT REGISTER REPORT [{RptMode}]"
            };
            if (dtVat.Rows.Count is 0)
            {
                return false;
            }
            RGrid.DataSource = dtVat;
            return RGrid.RowCount > 0;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return false;
        }
    }

    private bool GetSalesVatRegister()
    {
        try
        {
            _design.GetSalesVatDetailsRegisterDesign(RGrid, !NepaliColumn);
            RGrid.Columns["GTxtNoOfBills"].Visible = false;

            if (ObjGlobal.SysDateType is "M")
            {
                RGrid.Columns["GTxtVoucherMiti"].Visible = !IsDate;
                RGrid.Columns["GTxtVoucherDate"].Visible = IsDate;
            }
            else
            {
                RGrid.Columns["GTxtVoucherMiti"].Visible = IsDate;
                RGrid.Columns["GTxtVoucherDate"].Visible = !IsDate;
            }

            var dtVat = Module switch
            {
                "SB" when RptMode.Equals("DATE WISE") => _getReport.GetSalesVatRegisterDateWiseReport(),
                "SB" when RptMode.Equals("VOUCHER WISE") => _getReport.GetSalesVatRegisterVoucherWise(),
                "SB" when RptMode.Equals("MONTH WISE") => _getReport.GetSalesVatRegisterReportMonthly(),
                "SB" when RptMode.Equals("PRODUCT WISE") => _getReport.GetSalesVatRegisterProductWise(),
                "SB" when RptMode.Equals("CUSTOMER WISE") => _getReport.GetSalesVatRegisterCustomer(),
                "SR" when RptMode.Equals("DATE WISE") => _getReport.GetSalesVatRegisterDateWiseReport(),
                _ => _getReport.GetSalesVatRegisterDateWiseReport()
            };
            LblReportName.Text = Module switch
            {
                "SB" when !IncludeSalesReturn => $"SALES INVOICE VAT REGISTER [{RptMode}]",
                "SB" when IncludeSalesReturn => $"SALES INVOICE VAT REGISTER [{RptMode}] INCLUDE SALES RETURN",
                "SR" => $"SALES RETURN VAT REGISTER [{RptMode}]",
                _ => $"SALES INVOICE VAT REGISTER [{RptMode}]"
            };
            if (dtVat.Rows.Count is 0)
            {
                return false;
            }
            RGrid.DataSource = dtVat;
            return RGrid.RowCount > 0;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return false;
        }
    }

    private bool GetPurchaseSalesVatSummaryRegister()
    {
        try
        {
            if (RptMode.Equals("DATE WISE") && IsSummary)
            {
                if (Module.Equals("SB"))
                {
                    _design.GetSalesVatDetailsRegisterDesign(RGrid, !NepaliColumn);
                    if (ObjGlobal.SysDateType is "M")
                    {
                        RGrid.Columns["GTxtVoucherMiti"].Visible = !IsDate;
                        RGrid.Columns["GTxtVoucherDate"].Visible = IsDate;
                    }
                    else
                    {
                        RGrid.Columns["GTxtVoucherMiti"].Visible = IsDate;
                        RGrid.Columns["GTxtVoucherDate"].Visible = !IsDate;
                    }
                    RGrid.Columns["GTxtLedger"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    RGrid.Columns["GTxtVoucherNo"].Visible = false;
                    RGrid.Columns["GTxtNoOfBills"].Visible = false;
                    RGrid.Columns["GTxtPanNo"].Visible = false;
                    RGrid.Columns["GTxtInvoiceType"].Visible = false;
                    RGrid.Columns["GTxtExportSales"].Visible = false;
                    RGrid.Columns["GTxtExportCountry"].Visible = false;
                    RGrid.Columns["GTxtExportVoucherNo"].Visible = false;
                    RGrid.Columns["GTxtExportMiti"].Visible = false;
                }
                else if (Module.Equals("PB"))
                {
                    _design.GetPurchaseVatDetailsRegisterDesign(RGrid, !NepaliColumn);
                    if (ObjGlobal.SysDateType is "M")
                    {
                        RGrid.Columns["GTxtMiti"].Visible = !IsDate;
                        RGrid.Columns["GTxtDate"].Visible = IsDate;
                    }
                    else
                    {
                        RGrid.Columns["GTxtMiti"].Visible = IsDate;
                        RGrid.Columns["GTxtDate"].Visible = !IsDate;
                    }
                    RGrid.Columns["GTxtLedger"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    RGrid.Columns["GTxtVoucherNo"].Visible = false;
                    RGrid.Columns["GTxtRefVno"].Visible = false;
                    RGrid.Columns["GTxtPanNo"].Visible = false;
                    RGrid.Columns["GTxtInvoiceType"].Visible = false;
                }
            }
            else
            {
                _design.GetPurchaseSalesVatRegisterSummary(RGrid, Module.Equals("PB"));
            }

            Text = Module switch
            {
                "PB" => $"[{RptMode}] PURCHASE VAT REGISTER SUMMARY",
                "SB" => $"[{RptMode}] SALES VAT REGISTER SUMMARY",
                _ => "VAT REGISTER SUMMARY REPORT"
            };
            LblReportName.Text = Text;
            var dtVat = Module switch
            {
                "PB" => _getReport.GetPurchaseVatRegisterDetailsReport(),
                "SB" => _getReport.GetSalesVatRegisterSummary(),
                _ => new DataTable()
            };
            if (dtVat.Rows.Count is 0)
            {
                return false;
            }
            RGrid.DataSource = dtVat;
            return RGrid.RowCount > 0;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError(ex.Message);
            return false;
        }
    }

    private bool GetPurchaseVatRegisterVoucherWise()
    {
        try
        {
            _design.GetPurchaseVatDetailsRegisterDesign(RGrid, !NepaliColumn);
            var result = ObjGlobal.SysDateType.Equals("M") && !_getReport.GetReports.IsDate || ObjGlobal.SysDateType.Equals("D") && _getReport.GetReports.IsDate;
            RGrid.Columns["GTxtMiti"].Visible = result;
            RGrid.Columns["GTxtDate"].Visible = !RGrid.Columns["GTxtMiti"].Visible;
            var dtPurchaseVat = _getReport.GetPurchaseVatRegisterDetailsReport();
            LblReportName.Text = _getReport.GetReports.Module switch
            {
                "PR" => "PURCHASE RETURN VAT REGISTER - VOUCHER WISE",
                _ => "PURCHASE VAT REGISTER - VOUCHER WISE"
            };
            if (dtPurchaseVat.Rows.Count <= 0)
            {
                return false;
            }
            RGrid.DataSource = dtPurchaseVat;
            return RGrid.RowCount > 0;
        }
        catch (Exception ex)
        {
            ex.DialogResult();
            return false;
        }
    }

    private bool GetMaterializeViewRegister()
    {
        try
        {
            _getReport.GetReports.FromAdDate = FromAdDate;
            _getReport.GetReports.ToAdDate = ToAdDate;
            _design.GetMaterializeViewRegisterDesign(RGrid);
            var dtRegister = _getReport.GetMaterializeViewRegister();
            if (dtRegister.Rows.Count <= 0)
            {
                return false;
            }
            RGrid.DataSource = dtRegister;

            return RGrid.RowCount > 0;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            this.NotifyError($@"{RptName} RECORDS NOT FOUND..!!");
            return false;
        }
    }

    private bool GetEntryLogRegister()
    {
        try
        {
            _getReport.GetReports.FromAdDate = FromAdDate;
            _getReport.GetReports.ToAdDate = ToAdDate;
            _design.GetEntryLogRegisterDesign(RGrid);
            var dtVat = _getReport.GetEntryLogRegister();
            var iRows = 0;
            RGrid.DataSource = dtVat.Rows.Count > 0 ? dtVat : new DataTable("EMPTY");
            return RGrid.RowCount > 0;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            this.NotifyError($@"{RptName} RECORDS NOT FOUND..!!");
            return false;
        }
    }

    #endregion --------------- VAT REPORT ---------------

    //OBJECT FOR THIS FORM

    #region --------------- OBJECT FOR THIS FORM   ---------------

    public int PageWidth;

    private int _rowIndex;
    public int PageNo;
    public int AgingDays;
    public int ColumnsNo;

    public bool IncludePdc = false;
    public bool IsCustomer = false;
    public bool IsSummary;
    public bool IncludeSalesOrder;
    public bool IncludeSalesChallan;
    public bool IncludeGodown;
    public bool IncludeAdjustment = false;
    public bool IncludeAltQty;
    public bool IsHorizon;
    public bool IsAdditionalTerm;
    public bool AgentOnly = false;
    public bool BillAgent = false;
    public bool CreditLimit = false;
    public bool IsDate;
    public bool NepaliColumn;
    public bool IncludeFreeQty;
    public bool IncludeRemarks;
    public bool IncludeNarration = false;
    public bool IncludeSalesReturn = false;
    private bool _firstTime = true;

    public short Slab = 0;
    public short Columns = 0;

    public string RptMode = string.Empty;
    public string RptName = string.Empty;
    public string RptDate = string.Empty;
    public string RptType = string.Empty;
    public string FromAdDate = string.Empty;
    public string FromBsDate = string.Empty;
    public string ToAdDate = string.Empty;
    public string ToBsDate = string.Empty;
    public string Query = string.Empty;
    public string Str = string.Empty;
    public string AccountType = string.Empty;
    public string SetReportName = string.Empty;
    public string SetReportDate = string.Empty;

    public string LedgerId = string.Empty;
    public string ProductId = string.Empty;
    public string PGroupId = string.Empty;
    public string PSubGroupId = string.Empty;
    public string SubLedgerId = string.Empty;
    public string BranchId = ObjGlobal.SysBranchId.ToString();
    public string CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString();
    public string FiscalYearId = ObjGlobal.SysFiscalYearId.ToString();
    public string AgentId = string.Empty;
    public string AreaId = string.Empty;
    public string CounterId = string.Empty;
    public string CurrencyId = string.Empty;
    public string[] VatTermId { get; set; }
    public string AdditionalVatTermId = string.Empty;
    public string DepartmentId = string.Empty;

    public string PartyType = string.Empty;
    public string Opening = string.Empty;

    public string GroupBy = string.Empty;
    public string DateOn = string.Empty;
    public string SlabType = string.Empty;
    public string Module = string.Empty;
    public string FilterMode = string.Empty;
    public string FilterValue = string.Empty;
    public string EntryUser = string.Empty;
    public string InvoiceType = string.Empty;
    public string InvoiceCategory = string.Empty;
    public string CurrencyType = string.Empty;
    public string FirstSlab = string.Empty;
    public string ForthSlab = string.Empty;

    private readonly IRegisterReport _getReport;
    private readonly ClsPrintControl _control = new();

    private readonly IRegisterDesign _design = new RegisterReportDesign();

    [DllImport("kernel32.dll")]
    public static extern bool Beep(int beepFreq, int beepDuration);

    #endregion --------------- OBJECT FOR THIS FORM   ---------------
}