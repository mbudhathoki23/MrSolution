using DevExpress.XtraSplashScreen;
using MoreLinq.Extensions;
using MrBLL.DataEntry.OpeningMaster;
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
using MrDAL.Reports.Stock;
using MrDAL.Utility.GridControl;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PrintControl.PrintClass.Domains;

namespace MrBLL.Reports.Inventory_Report;

public partial class DisplayInventoryReports : MrForm
{
    // DISPLAY INVENTORY REPORTS

    #region --------------- INVENTORY REPORT FORM  ---------------

    public DisplayInventoryReports()
    {
        InitializeComponent();
        RGrid.RowHeadersWidth = 20;
    }

    private void FrmInventoryReport_Load(object sender, EventArgs e)
    {
        InitializeObject();
        MenuHeaderText();
        GenerateReports();
        RGrid.Focus();
    }

    private void FrmInventoryReport_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != (char)Keys.Escape) return;
        if (MessageBox.Show(@"ARE YOU SURE WANT TO CLOSE FORM..!!", ObjGlobal.Caption, MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            Dispose();
        }
    }

    private void FrmInventoryReport_FormClosed(object sender, FormClosedEventArgs e)
    {
    }

    private void FrmInventoryReport_Resize(object sender, EventArgs e)
    {
        var difference = _pageWidth - RGrid.Width;
        if (RGrid.ColumnCount == 0)
        {
            return;
        }

        if (WindowState == FormWindowState.Maximized)
        {
            RGrid.Columns.Cast<DataGridViewColumn>().ToList()
                .ForEach(f => f.Width += _pageWidth > 0 ? difference / _pageWidth * 100 : 0b0);
        }
        else
        {
            RGrid.Columns.Cast<DataGridViewColumn>().ToList()
                .ForEach(f => f.Width -= _pageWidth > 0 ? difference / _pageWidth * 100 : 0b0);
        }
    }

    #endregion --------------- INVENTORY REPORT FORM  ---------------

    // METHOD FOR THIS FORM

    #region --------------- METHOD  ---------------

    private void MenuHeaderText()
    {
        lbl_ComanyName.Text = ObjGlobal.CompanyPrintDesc;
        LblCompanyAddress.Text = ObjGlobal.CompanyAddress;
        if (ObjGlobal.SysDateType is "M" && !IsDate || ObjGlobal.SysDateType is "D" && IsDate ||
            ObjGlobal.SysDateType is "D" && IsDate || ObjGlobal.SysDateType is "D" && IsDate)
        {
            LblAccPeriodDate.Text = $@"{ObjGlobal.CfStartBsDate} - {ObjGlobal.CfEndBsDate}";
            lbl_DateTime.Text = $@"{DateTime.Now.GetNepaliDate()} {DateTime.Now.ToShortTimeString()}";
        }
        else
        {
            LblAccPeriodDate.Text = $@"{ObjGlobal.CfStartAdDate.GetDateString()} - {ObjGlobal.CfEndAdDate.GetDateString()}";
            lbl_DateTime.Text = $@"{DateTime.Now.GetDateString()} {DateTime.Now.ToShortTimeString()}";
        }
        LblReportName.Text = RptName;
        LblReportDate.Text = RptDate;
    }

    private void GenerateReports()
    {
        try
        {
            RGrid.SuspendLayout();
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            switch (RptType.ToUpper())
            {
                case "NORMAL":
                {
                    switch (RptName.ToUpper())
                    {
                        case "PRODUCT OPENING":
                        {
                            GenerateProductOpeningListReports();
                            break;
                        }
                        case "STOCK LEDGER":
                        {
                            if (IsSummary)
                            {
                                GenerateStockLedgerSummeryReports();
                            }
                            else
                            {
                                GenerateStockLedgerDetailsReports();
                            }

                            break;
                        }
                        case "STOCK VALUATION":
                        {
                            GenerateStockValuationReports();
                            break;
                        }
                        case "BILL OF MATERIALS":
                        {
                            GenerateBillOfMaterialsReports();
                            break;
                        }
                        case "PROFITABILITY":
                        {
                            GenerateProfitabilityReports();
                            break;
                        }
                    }

                    break;
                }
                case "ADVANCE":
                {
                    if (RptName == "STOCK ANALYSIS")
                    {
                        GenerateStockAnalysisReports();
                    }
                    break;
                }
                case "LIST_OF_MASTER":
                {
                    GenerateProductListReports();
                    break;
                }

                case "":
                {
                    MessageBox.Show(RptName.ToUpper() + @" REPORT NOT FOUND..!!", ObjGlobal.Caption);
                    Close();
                    break;
                }
            }
            GeneratePageNo();
            RGrid.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            RGrid.ResumeLayout();
            SplashScreenManager.CloseForm(false);
        }
        catch (Exception ex)
        {
            SplashScreenManager.CloseForm(false);
            ex.ToNonQueryErrorResult(ex.StackTrace);
            ex.DialogResult();
        }
    }

    private string GeneratePageNo()
    {
        const int totalRecords = 43;
        if (RGrid.RowCount > 0)
        {
            var result = RGrid.RowCount;
            result /= totalRecords;
            lbl_PageNo.Text = @$"Page 1 of {result}";
        }
        else
        {
            lbl_PageNo.Text = @"Page 1 of 1";
        }

        return lbl_PageNo.Text;
    }

    private void InitializeObject()
    {
        try
        {
            if (RptType.IsBlankOrEmpty()) return;
            _objStock.GetReports.RptType = RptType;
            _objStock.GetReports.RptName = RptName;
            _objStock.GetReports.RptDate = RptDate;
            _objStock.GetReports.RptMode = RptMode;
            _objStock.GetReports.FromDate = FromAdDate;
            _objStock.GetReports.ToDate = ToAdDate;
            _objStock.GetReports.GroupBy = GroupBy;
            _objStock.GetReports.ProductSubGroupId = ProductSubGroupId;
            _objStock.GetReports.ProductGroupId = ProductGroupId;
            _objStock.GetReports.ProductId = ProductId;
            _objStock.GetReports.Godown = Godown;
            _objStock.GetReports.GodownId = GodownId;
            _objStock.GetReports.IncludeValue = IncludeValue;
            _objStock.GetReports.IsSummary = IsSummary;
            _objStock.GetReports.IsRemarks = IncludeRemarks;
            _objStock.GetReports.IsDate = IsDate;
            _objStock.GetReports.FilterValue = FilterValue;
            _objStock.GetReports.BranchId = BranchId;
            _objStock.GetReports.FiscalYearId = FiscalYearId;
            _objStock.GetReports.CompanyUnitId = CompanyUnitId;
            _objStock.GetReports.IncludeNegative = NegativeStock;
            _objStock.GetReports.NegativeStockOnly = NegativeStockOnly;
            _objStock.GetReports.ExcludeNegative = ExcludeNegative;
            _objStock.GetReports.IncludeAltQty = IncludeAltQty;
            _objStock.GetReports.IncludeVAT = IncludeVat;
            _objStock.GetReports.RePostValue = RePostValue;
            _objStock.GetReports.IncludeZeroBalance = IncludeZero;
            _objStock.GetReports.IncludeRefBill = IncludeRefBill;
            _objStock.GetReports.CostRatio = CostRatio;
            _objStock.GetReports.SortOn = SortOn;
        }
        catch (Exception ex)
        {
            var errMsg = ex.Message;
            this.NotifyError(ex.Message);
            ex.ToNonQueryErrorResult(ex.StackTrace);
            //ignore
        }
    }

    #endregion --------------- METHOD  ---------------

    // GRID CONTROL EVENTS

    #region --------------- Grid Details ---------------

    private void RGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void RGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        _rowIndex = e.RowIndex;
        if (RGrid.Rows.Count > 0)
            lbl_PageNo.Text = _rowIndex > 46
                ? $@"Page {Math.Ceiling(Convert.ToDouble(_rowIndex / 46))} of  {_totalNoPage}"
                : $@"Page 1 of  {_totalNoPage}";
    }

    private void RGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            RGrid.Rows[_rowIndex].Selected = true;
            e.SuppressKeyPress = true;
            if (IsSummary)
            {
                if (RptName is "STOCK LEDGER" or "STOCK VALUATION" or "PROFITABILITY")
                {
                    ZoomingRpt();
                }
            }
            else
            {
                if (RptType is "LIST_OF_MASTER")
                {
                    return;
                }
                var module = RGrid.CurrentRow?.Cells["GTxtModule"].Value.GetString();
                var voucherNo = RGrid.CurrentRow?.Cells["GTxtVoucherNo"].Value.GetString();
                ZoomingRpt(module, voucherNo);
            }
        }
    }

    private void RGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        RGrid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
    }

    private void RGrid_SizeChanged(object sender, EventArgs e)
    {
    }

    private void RGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
    {
    }

    #endregion --------------- Grid Details ---------------

    // EXPORT IN EXCEL FUNCTION

    #region --------------- Export Event/Method ---------------

    private void TsBtnExport_Click(object sender, EventArgs e)
    {
        ExportReport();
    }

    private void ExportReport()
    {
        saveFileDialog1.Title = "Save";
        saveFileDialog1.Filter = @"Excel File 1997-07 |*.Xls|Excel File 2010-13 |*.xlsx|Word |*.Doc|Html Page |*.Html";
        saveFileDialog1.ShowDialog();
        var fileName = saveFileDialog1.FileName;
        ClsExportReports clsExportReports = new()
        {
            FileName = fileName,
            CompanyName = lbl_ComanyName.Text,
            AccountingPeriod = lbl_AccountingPeriod.Text,
            AccPeriodDate = LblAccPeriodDate.Text,
            CompanyAddress = LblCompanyAddress.Text,
            CompanyPanVatNo = LblCompanyPANVATNo.Text,
            ReportName = LblReportName.Text,
            ReportDate = LblReportDate.Text,
        };
        clsExportReports.ExportReport(RGrid);
        this.NotifySuccess($"{RptName} EXPORT SUCCESSFULLY..!!");
        if (CustomMessageBox.Question(@"DO YOU WANT TO OPEN THE EXPORT FILE..??") is DialogResult.Yes)
        {
            if (fileName.Length > 0)
            {
                Process.Start(fileName);
            }
        }
    }

    #endregion --------------- Export Event/Method ---------------

    // PRINT FUNCTION FOR THIS FORM

    #region --------------- Print Event/Method ---------------

    private void PrintDocument1_BeginPrint(object sender, System.ComponentModel.CancelEventArgs e)
    {
        _control.PrintDocument1_BeginPrint(sender, e);
    }

    private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
    {
        _control.PrintDocument1_PrintPage(sender, e);
    }

    #endregion --------------- Print Event/Method ---------------

    // METHOD FOR THIS FORM

    #region --------------- Function ---------------

    private void BtnFitColumn_Click(object sender, EventArgs e)
    {
        try
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            RGrid.Columns.Cast<DataGridViewColumn>().ToList()
                .ForEach(f => f.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells);
            SplashScreenManager.CloseForm(true);
        }
        catch
        {
            SplashScreenManager.CloseForm(true);
        }
    }

    private void TsBtnPrint_Click(object sender, EventArgs e)
    {
        CustomPrintFunction();
    }

    private void TsBtnRefresh_Click(object sender, EventArgs e)
    {
        GenerateReports();
    }

    private void TsBtnSearch_Click(object sender, EventArgs e)
    {
        RGrid.ClearSelection();
        using var rptSearch = new FrmRptSearch(RGrid);
        if (rptSearch.ShowDialog() == DialogResult.OK)
        {
        }
    }

    public void ZoomingRpt(string module = "", string voucherNo = "")
    {
        if (IsSummary)
        {
            if (RptName is "STOCK LEDGER" or "STOCK VALUATION" or "PROFITABILITY")
            {
                ProductId = RGrid.CurrentRow?.Cells["GTxtProductId"].Value.GetString();
                if (ProductId.IsBlankOrEmpty())
                {
                    return;
                }
                var dr = new DisplayInventoryReports
                {
                    Text = RptName + @" REPORTS",
                    RptType = "NORMAL",
                    RptName = "STOCK LEDGER",
                    RptDate = RptDate,
                    IsSummary = false,
                    IncludeValue = IncludeValue || RptName.Equals("STOCK VALUATION"),
                    IncludeVat = IncludeVat,
                    FromBsDate = RptName.Equals("STOCK VALUATION") ? ObjGlobal.CfStartBsDate.GetDateString() : FromBsDate,
                    ToBsDate = ToBsDate,
                    FromAdDate = RptName.Equals("STOCK VALUATION") ? ObjGlobal.CfStartAdDate.GetDateString() : FromAdDate,
                    ToAdDate = ToAdDate,
                    ProductSubGroupId = ProductSubGroupId,
                    ProductGroupId = ProductGroupId,
                    ProductId = ProductId,
                    BranchId = BranchId,
                    FiscalYearId = FiscalYearId,
                    CompanyUnitId = CompanyUnitId
                };
                dr.ShowDialog();
            }
            else if (RptName == "STOCK VALUATION")
            {
                ProductId = RGrid.CurrentRow?.Cells["GTxtProductId"].Value.GetString();
                var dr = new DisplayInventoryReports
                {
                    Text = RptName + @" REPORTS",
                    RptType = "NORMAL",
                    RptName = "STOCK LEDGER",
                    RptDate = RptDate,
                    IsSummary = false,
                    IncludeValue = IncludeValue,
                    IncludeVat = IncludeVat,
                    FromBsDate = FromBsDate,
                    ToBsDate = ToBsDate,
                    FromAdDate = FromAdDate,
                    ToAdDate = ToAdDate,
                    ProductSubGroupId = ProductSubGroupId,
                    ProductGroupId = ProductGroupId,
                    ProductId = ProductId,
                    BranchId = BranchId,
                    FiscalYearId = FiscalYearId,
                    CompanyUnitId = CompanyUnitId
                };
                dr.ShowDialog();
            }
            else if (RptName == "OPENING STOCK")
            {
            }
        }
        else
        {
            switch (module.ToUpper())
            {
                case "SC":
                {
                    new FrmSalesChallanEntry(true, voucherNo).ShowDialog();
                    break;
                }
                case "SB":
                {
                    var cmdString =
                        $" SELECT Invoice_Mode FROM AMS.SB_Master WHERE SB_Invoice='{voucherNo}' AND Invoice_Mode <> 'POS'";
                    var table = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    if (table.Rows.Count is 0)
                        this.NotifyWarning($"{voucherNo} INVOICE NUMBER CANNOT BE EDITED OR MODIFY");
                    else
                        new FrmSalesInvoiceEntry(true, voucherNo).ShowDialog();
                    break;
                }
                case "SR":
                {
                    new FrmSalesReturnEntry(true, voucherNo).ShowDialog();
                    break;
                }
                case "PC":
                {
                    new FrmPurchaseChallanEntry(true, voucherNo).ShowDialog();
                    break;
                }
                case "PB":
                {
                    new FrmPurchaseInvoiceEntry(true, voucherNo).ShowDialog();
                    break;
                }
                case "PR":
                {
                    new FrmPurchaseReturnEntry(true, voucherNo).ShowDialog();
                    break;
                }
                case "POB":
                {
                    new FrmProductOpeningEntry(true, voucherNo).ShowDialog();
                    break;
                }
            }
        }
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

    #endregion --------------- Function ---------------

    // STOCK LEDGER REPORTS

    #region --------------- Stock Ledger Summary ---------------

    private void GenerateProductOpeningListReports()
    {
        LblReportDate.Text = ObjGlobal.SysDateType.Equals("M") ? $"AS ON {ObjGlobal.CfStartBsDate}" : $"AS ON {ObjGlobal.CfStartAdDate.GetDateString()}";
        _objDesign.GetStockValuationDesign(RGrid);
        _objStock.GetReports.BranchId = ObjGlobal.SysBranchId.ToString();
        _objStock.GetReports.CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString();
        _objStock.GetReports.FiscalYearId = ObjGlobal.SysFiscalYearId.ToString();
        _objStock.GetReports.ToDate = Convert.ToDateTime(ObjGlobal.CfStartAdDate).ToString("yyyy-MM-dd");
        var dtOpening = _objStock.GetProductOpeningOnly();
        try
        {
            if (dtOpening.Rows.Count > 0)
            {
                RGrid.DataSource = dtOpening;
            }
            else
            {
                MessageBox.Show($@"{RptName} REPORT NOT FOUND", ObjGlobal.Caption);
                Close();
                return;
            }
        }
        catch (Exception ex)
        {
            var erMsg = ex.Message;
            ex.ToNonQueryErrorResult(ex.StackTrace);
            MessageBox.Show(ex.Message, ObjGlobal.Caption);
        }
    }

    private void GenerateStockLedgerSummeryReports()
    {
        try
        {
            _objDesign.GetStockLedgerSummaryDesign(RGrid);
            RGrid.Columns["GTxtOpeningValue"].Visible = IncludeValue;
            RGrid.Columns["GTxtInValue"].Visible = IncludeValue;
            RGrid.Columns["GTxtIssueValue"].Visible = IncludeValue;
            RGrid.Columns["GTxtSalesValue"].Visible = IncludeValue;
            RGrid.Columns["GTxtBalanceAmount"].Visible = IncludeValue;
            RGrid.Columns["GTxtBalanceRate"].Visible = IncludeValue;
            var dtStock = _objStock.GetStockLedgerSummaryReport();
            if (dtStock.Rows.Count > 0)
            {
                RGrid.DataSource = dtStock;
            }
            else
            {
                MessageBox.Show(@$"{RptName} REPORT NOT FOUND..!!", ObjGlobal.Caption);
                Close();
                return;
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            ex.DialogResult();
            Close();
            return;
        }
    }

    private void GenerateStockLedgerDetailsReports()
    {
        try
        {
            _objDesign.GetStockLedgerDetailsDesign(RGrid);
            RGrid.Columns["GTxtDate"].Visible = ObjGlobal.SysDateType.Equals("M") && IsDate || ObjGlobal.SysDateType.Equals("D") && !IsDate;
            RGrid.Columns["GTxtMiti"].Visible = !RGrid.Columns["GTxtDate"].Visible;

            RGrid.Columns["GTxtOpeningAltQty"].Visible = IncludeAltQty;
            RGrid.Columns["GTxtReceiptAltQty"].Visible = IncludeAltQty;
            RGrid.Columns["GTxtIssueAltQty"].Visible = IncludeAltQty;

            RGrid.Columns["GTxtOpeningValue"].Visible = IncludeValue;
            RGrid.Columns["GTxtReceiptValue"].Visible = IncludeValue;
            RGrid.Columns["GTxtIssueValue"].Visible = IncludeValue;
            RGrid.Columns["GTxtBalanceValue"].Visible = IncludeValue;
            RGrid.Columns["GTxtBalanceRate"].Visible = IncludeValue;
            RGrid.Columns["GTxtProduct"].HeaderText = RptMode switch
            {
                "GROUP WISE" => "GROUP WISE/PRODUCT",
                "SUBGROUP WISE" => "GROUP/SUBGROUP WISE PRODUCT",
                _ => RGrid.Columns["GTxtProduct"].HeaderText
            };

            Text = RptMode switch
            {
                "GROUP WISE" => "PRODUCT GROUP WISE - STOCK LEDGER DETAILS REPORT",
                "SUBGROUP WISE" => "PRODUCT SUBGROUP WISE - STOCK LEDGER DETAILS REPORT",
                _ => "STOCK LEDGER DETAILS REPORT"
            };
            var dtStock = _objStock.GetStockLedgerDetailsReport();
            if (dtStock == null || dtStock.Rows.Count <= 0)
            {
                MessageBox.Show(@"STOCK LEDGER DETAILS REPORT NOT FOUND..!!", ObjGlobal.Caption);
                Close();
            }
            else
            {
                RGrid.DataSource = dtStock;
            }
        }
        catch (Exception ex)
        {
            ex.DialogResult();
            Close();
            return;
        }
    }

    private void GenerateStockValuationReports()
    {
        try
        {
            _objDesign.GetStockValuationDesign(RGrid, IncludeVat);
            RGrid.Columns["GTxtStockAltQty"].Visible = IncludeAltQty;
            RGrid.Columns["GTxtAltUnit"].Visible = IncludeAltQty;
            RGrid.Columns["GTxtShortName"].Visible = IsShortName;
            var dtStock = _objStock.GetClosingStockValue();
            if (dtStock.Rows.Count > 0)
            {
                RGrid.DataSource = dtStock;
            }
            else
            {
                MessageBox.Show(@$"{RptName} REPORT NOT FOUND..!!", ObjGlobal.Caption);
                Close();
                return;
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            MessageBox.Show(@$"{RptName} REPORT NOT FOUND..!!", ObjGlobal.Caption);
            Close();
            return;
        }
    }

    #endregion --------------- Stock Ledger Summary ---------------

    // STOCK ANALYSIS REPORTS

    #region --------------- StockAnalysisReport ---------------

    private void GenerateStockAnalysisReports()
    {
    }

    private void GenerateBillOfMaterialsReports()
    {
        try
        {
            _objDesign.GetBillOfMaterialsDesign(RGrid);
            RGrid.Columns["GTxtDate"].Visible = ObjGlobal.SysDateType.Equals("M") && IsDate || ObjGlobal.SysDateType.Equals("D") && !IsDate;
            RGrid.Columns["GTxtMiti"].Visible = !RGrid.Columns["GTxtDate"].Visible;
            RGrid.Columns["GTxtSalesRate"].Visible = CostRatio;
            RGrid.Columns["GTxtCostRatio"].Visible = CostRatio;
            var dtStock = _objStock.GetBillOfMaterialsReports();
            if (dtStock.Rows.Count > 0)
            {
                RGrid.DataSource = dtStock;
            }
            else
            {
                MessageBox.Show(@$"{RptName} REPORT NOT FOUND..!!", ObjGlobal.Caption);
                Close();
                return;
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            MessageBox.Show(@$"{RptName} REPORT NOT FOUND..!!", ObjGlobal.Caption);
            Close();
            return;
        }
    }

    private void GenerateProfitabilityReports()
    {
        try
        {
            _objDesign.GetProfitabilityReportDesign(RGrid);
            var dtStock = _objStock.GetProfitabilityReports();
            if (dtStock.Rows.Count > 0)
            {
                RGrid.DataSource = dtStock;
            }
            else
            {
                MessageBox.Show(@$"{RptName} REPORT NOT FOUND..!!", ObjGlobal.Caption);
                Close();
                return;
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            MessageBox.Show(@$"{RptName} REPORT NOT FOUND..!!", ObjGlobal.Caption);
            Close();
            return;
        }
    }

    #endregion --------------- StockAnalysisReport ---------------

    // LIST OF PRODUCT

    #region --------------- List of Master ---------------

    private void GenerateProductListReports()
    {
        try
        {
            _objDesign.GetMasterProductListDesign(RGrid);
            _objStock.GetReports.BranchId = ObjGlobal.SysBranchId.ToString();
            _objStock.GetReports.CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString();
            _objStock.GetReports.FiscalYearId = ObjGlobal.SysFiscalYearId.ToString();
            _objStock.GetReports.ToDate = Convert.ToDateTime(ObjGlobal.CfStartAdDate).ToString("yyyy-MM-dd");
            var dtOpening = _objStock.GetMasterProductList();
            if (dtOpening.Rows.Count > 0)
            {
                RGrid.Columns["GTxtSalesLedger"].Visible = IncludeLedgerInfo;
                RGrid.Columns["GTxtSalesLedger"].Visible = IncludeLedgerInfo;
                RGrid.Columns["GTxtSalesReturnLedger"].Visible = IncludeLedgerInfo;
                RGrid.Columns["GTxtPurchaseLedger"].Visible = IncludeLedgerInfo;
                RGrid.Columns["GTxtPurchaseReturnLedger"].Visible = IncludeLedgerInfo;
                RGrid.Columns["GTxtPLOpeningLedger"].Visible = IncludeLedgerInfo;
                RGrid.Columns["GTxtPlClosingLedger"].Visible = IncludeLedgerInfo;
                RGrid.Columns["GTxtBsClosingLedger"].Visible = IncludeLedgerInfo;
                RGrid.Columns["GTxtMinQty"].Visible = IncludeReOrderInfo;
                RGrid.Columns["GTxtMinQty"].Visible = IncludeReOrderInfo;
                RGrid.Columns["GTxtMinQty"].Visible = IncludeBonusFreeQty;
                RGrid.Columns["GTxtSalesRate"].Visible = IncludePriceInfo;
                RGrid.Columns["GTxtBuyRate"].Visible = IncludePriceInfo;
                RGrid.Columns["GTxtMRP"].Visible = IncludePriceInfo;
                RGrid.Columns["GTxtShortName"].Visible = IsShortName;
                RGrid.DataSource = dtOpening;
                RGrid.Rows.OfType<DataGridViewRow>().ForEach(row =>
                {
                    row.DefaultCellStyle.ForeColor = row.Cells["IsGroup"].Value.GetInt() switch
                    {
                        1 => Color.DodgerBlue,
                        2 => Color.BlueViolet,
                        3 => Color.Aquamarine,
                        22 => Color.BlueViolet,
                        _ => Color.Black
                    };
                    row.DefaultCellStyle.Font = row.Cells["IsGroup"].Value.GetInt() switch
                    {
                        11 => new Font("Bookman Old Style", 11, FontStyle.Bold),
                        22 => new Font("Bookman Old Style", 11, FontStyle.Bold),
                        99 => new Font("Bookman Old Style", 11, FontStyle.Bold),
                        _ => new Font("Bookman Old Style", 10, FontStyle.Regular)
                    };
                    row.DefaultCellStyle.Alignment = row.Cells["IsGroup"].Value.GetInt() switch
                    {
                        11 => DataGridViewContentAlignment.MiddleRight,
                        22 => DataGridViewContentAlignment.MiddleRight,
                        99 => DataGridViewContentAlignment.MiddleRight,
                        _ => row.DefaultCellStyle.Alignment
                    };
                });
            }
            else
            {
                MessageBox.Show($@"{RptName} REPORT NOT FOUND", ObjGlobal.Caption);
                Close();
                return;
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            ex.DialogResult();
            Close();
            return;
        }
    }

    #endregion --------------- List of Master ---------------

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT ---------------

    private int _pageWidth;
    private int _rowIndex;

    public bool Godown;
    public bool IsSummary;
    public bool Class;
    public bool IncludeProduct;
    public bool IncludeReturn;
    public bool IncludeValue;
    public bool IncludeBatch;
    public bool RePostValue;
    public bool IncludeRemarks;
    public bool IncludeRefBill;
    public bool IsDate;
    public bool IsShortName;
    public bool IncludeZero;
    public bool IncludeAltQty;
    public bool NegativeStockOnly;
    public bool NegativeStock;
    public bool ExcludeNegative;
    public bool IncludeLedgerInfo;
    public bool IncludeReOrderInfo;
    public bool IncludePriceInfo;
    public bool IncludeBonusFreeQty;
    public bool IncludeVat;

    private string _totalNoPage;
    public string FilterFor;
    public string RptName;
    public string RptDate;
    public string RptMode;
    public string RptType;
    public string FromAdDate;
    public string FromBsDate;
    public string ToAdDate;
    public string ToBsDate;
    public string Str;
    public string Query = string.Empty;
    public string[] Code;
    public string CodeList;
    public string ProductSubGroupId;
    public string ProductGroupId;
    public string ProductId;
    public string BranchId = ObjGlobal.SysBranchId.ToString();
    public string FiscalYearId = ObjGlobal.SysFiscalYearId.ToString();
    public string CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString();
    public string CostCenterId;
    public string GodownId;
    public string ClassCode;
    public string FilterValue;
    public string SortOn;
    public string GroupBy;
    public bool CostRatio;

    [DllImport("kernel32.dll")]
    public static extern bool Beep(int beepFreq, int beepDuration);

    private readonly IStockReport _objStock = new ClsStockReport();
    private readonly IStockReportDesign _objDesign = new StockReportDesign();
    private readonly ClsPrintControl _control = new();
    public static DataTable ReportTable;

    #endregion --------------- OBJECT ---------------
}