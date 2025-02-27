﻿using DevExpress.XtraSplashScreen;
using MrBLL.DataEntry.FinanceMaster;
using MrBLL.DataEntry.PurchaseMaster;
using MrBLL.DataEntry.SalesMaster;
using MrBLL.Reports.Inventory_Report;
using MrBLL.Utility.Common;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Reports.Design;
using MrDAL.Reports.Finance;
using MrDAL.Reports.Interface;
using MrDAL.Utility.GridControl;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PrintControl.PrintClass.Domains;
using static System.Drawing.Color;

namespace MrBLL.Reports.Finance_Report;

public partial class DisplayFinanceReports : MrForm
{
    // DISPLAY FINANCE REPORTS FOR THIS FORM

    #region **--------------- DisplayFinanceReports ---------------**

    public DisplayFinanceReports()
    {
        InitializeComponent();
        _dtTemp = new DataTable();
        var arrayList = new ArrayList();
        _report = new ClsFinanceReport();
        _design = new FinanceReportDesign();
    }

    private void DisplayFinanceReports_Shown(object sender, EventArgs e)
    {
        HeaderValue();

        BringToFront();
        RGrid.Focus();
    }

    private void RptReportDisplay_Load(object sender, EventArgs e)
    {
        PageWidth = RGrid.Width;
        InitializeObjectValue();
        GenerateFinanceReports();
    }

    private void RptReportDisplay_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != (char)Keys.Escape)
        {
            return;
        }
        if (CustomMessageBox.ExitActiveForm() is DialogResult.Yes)
        {
            Close();
            return;
        }
    }

    private void RptReportDisplay_Resize(object sender, EventArgs e)
    {
        var difference = PageWidth - RGrid.Width;
        if (RGrid.ColumnCount == 0)
        {
            return;
        }
        if (WindowState == FormWindowState.Maximized)
        {
            RGrid.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.Width += PageWidth > 0 ? difference / PageWidth * 100 : 0b0);
        }
        else
        {
            RGrid.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.Width -= PageWidth > 0 ? difference / PageWidth * 100 : 0b0);
            StartPosition = FormStartPosition.CenterScreen;
        }
    }

    #endregion **--------------- DisplayFinanceReports ---------------**

    // METHOD FOR THIS FORM

    #region **--------------- Method ---------------**

    private void HeaderValue()
    {
        LblAccPeriodDate.Text = $@"{ObjGlobal.CfStartBsDate} to {ObjGlobal.CfEndBsDate}";
        lbl_DateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:MM");
        lbl_ComanyName.Text = ObjGlobal.CompanyPrintDesc;
        LblCompanyAddress.Text = ObjGlobal.CompanyAddress;
        LblCompanyPANVATNo.Text = $@"PAN/VATNo : {ObjGlobal.CompanyPanNo}";

        if (ObjGlobal.SysDateType is "M" && !IsDate || ObjGlobal.SysDateType is "D" && IsDate)
        {
            LblAccPeriodDate.Text = $@"{ObjGlobal.CfStartBsDate} - {ObjGlobal.CfEndBsDate}";
            lbl_DateTime.Text = $@"{DateTime.Now.GetNepaliDate()}";
        }
        else
        {
            LblAccPeriodDate.Text = $@"{ObjGlobal.CfStartAdDate.GetDateString()} - {ObjGlobal.CfEndAdDate.GetDateString()}";
            lbl_DateTime.Text = DateTime.Now.GetDateString();
        }

        lbl_PageNo.Text = @"Page 1 of 1";
        LblReportName.Text = RptName.ToUpper() + @" REPORTS";
        if (ObjGlobal.SysDateType is "M" && !IsDate || ObjGlobal.SysDateType is "D" && IsDate)
        {
            LblReportDate.Text = $@"FROM {FromAdDate.GetNepaliDate()} TO {ToAdDate.GetNepaliDate()}";
        }
        else
        {
            LblReportDate.Text = $@"FROM {FromAdDate} TO {ToAdDate}";
        }
    }

    private void GenerateFinanceReports()
    {
        try
        {
            RGrid.SuspendLayout();
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            RptName = RptName.ToUpper();
            RptType = RptType.ToUpper();
            var reportResult = RptType switch
            {
                "NORMAL" => RptName switch
                {
                    "MONTHLY LEDGER" => GenerateLedgerReportMonthly(),
                    "LEDGER" when IsDetails => GenerateLedgerReportDetails(),
                    "LEDGER" when !IsDetails => GenerateLedgerReportSummary(),
                    "OPENING BALANCE" when IsBalanceSheet => GenerateOpeningBalanceSheetNormal(),
                    "OPENING BALANCE" when !IsBalanceSheet => GenerateOpeningTrialBalanceNormal(),
                    "TRIAL BALANCE" => GenerateTrialBalanceNormal(),
                    "BALANCE SHEET" => GenerateBalanceSheetNormal(),
                    "PROFIT & LOSS" => GenerateProfitLossNormal(),
                    "CASH FLOW" => GenerateCashFlowReportNormal(),
                    "JOURNAL BOOK" => GenerateJournalVoucherReports(),
                    "DAY BOOK" when IsDetails => GenerateDayBookReports(),
                    "DAY BOOK" when !IsDetails => GenerateDayBookReportsSummary(),
                    "CASH/BANK BOOK" when IsDetails => GenerateCashBankReportNormal(),
                    "CASH/BANK BOOK" when !IsDetails => GenerateCashBankReportSummary(),
                    _ => false
                },
                "PERIODIC" => RptName switch
                {
                    "TRIAL BALANCE" => GenerateTrialBalancePeriodic(),
                    "BALANCE SHEET" => GenerateBalanceSheetPeriodic(),
                    "PROFIT & LOSS" => GenerateProfitLossPeriodic(),
                    _ => false
                },
                "LIST_OF_MASTER" => GenerateListOfMaster(),
                "T FORMAT" => RptName switch
                {
                    "LEDGER" when IsDetails => GenerateLedgerReportDetails(),
                    "LEDGER" when !IsDetails => GenerateLedgerReportDetails(),
                    "OPENING BALANCE" when IsBalanceSheet => GenerateOpeningBalanceSheetNormalTFormat(),
                    "OPENING BALANCE" when IsTrialBalance => GenerateOpeningTrialBalanceTFormat(),
                    "TRIAL BALANCE" => GenerateTrialBalanceTFormat(),
                    "BALANCE SHEET" => GenerateBalanceSheetTFormat(),
                    "PERIODIC BALANCE SHEET" => GenerateBalanceSheetPeriodicTFormat(),
                    "PROFIT & LOSS" => GenerateProfitLossNormalTFormat(),
                    "PERIODIC PROFIT & LOSS" => GenerateProfitLossPeriodicTFormat(),
                    "CASH FLOW" => GenerateCashFlowReportNormalTFormat(),
                    "JOURNAL VOUCHER" => GenerateJournalVoucherReportsTFormat(),
                    "DAY BOOK" => GenerateDayBookReportsTFormat(),
                    "CASH/BANK BOOK" when IsDetails => GenerateCashBankReportNormalTFormat(),
                    "CASH/BANK BOOK" when !IsDetails => GenerateCashBankReportSummaryTFormat(),
                    _ => false
                },
                _ => false
            };
            if (reportResult)
            {
                var result = RGrid.Columns.Cast<DataGridViewColumn>().ToList();
                result.ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
                RGrid.ResumeLayout();
                ObjGlobal.DGridColorCombo(RGrid);
                SplashScreenManager.CloseForm(false);
            }
            else
            {
                MessageBox.Show($@"{RptName} REPORT NOT FOUND..!!", ObjGlobal.Caption);
                SplashScreenManager.CloseForm(false);
                Close();
            }
        }
        catch (Exception ex)
        {
            SplashScreenManager.CloseForm(false);
            var errMsg = ex.Message;
            ex.ToNonQueryErrorResult(ex.StackTrace);
            MessageBox.Show($@"{ex.Message} ..!!", ObjGlobal.Caption);
            Close();
        }
    }

    #endregion **--------------- Method ---------------**

    // GRID METHOD OF THIS FORM

    #region **--------------- Grid Method ---------------**

    private void RGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void RGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        _rowIndex = e.RowIndex;
    }

    private void RGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            RGrid.MultiSelect = !RGrid.MultiSelect && RGrid.MultiSelect;
            if (RGrid.Rows.Count is 0) return;
            RGrid.Rows[_rowIndex].Selected = true;
            e.SuppressKeyPress = true;
            ReportZooming();
        }
        else if (e.Control && e.KeyCode is Keys.A)
        {
            RGrid.MultiSelect = true;
            RGrid.SelectAll();
        }
        else if (e.Control && e.KeyCode is Keys.C)
        {
            RGrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
        }

        RGrid.MultiSelect = false;
    }

    private void RGrid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        ReportZooming();
    }

    private void ReportZooming()
    {
        try
        {
            switch (RptName)
            {
                case "JOURNAL BOOK":
                {
                    if (RGrid.CurrentRow != null)
                    {
                        Module = RGrid.CurrentRow.Cells["GTxtModule"].Value?.ToString().Trim();
                        _voucherNo = RGrid.CurrentRow.Cells["GTxtVoucherNo"].Value?.ToString().Trim();
                        ZoomToEntryLevel();
                    }

                    break;
                }
                case "DAY BOOK":
                {
                    if (RGrid.CurrentRow != null)
                    {
                        Module = RGrid.CurrentRow.Cells["GTxtModule"].Value?.ToString().Trim();
                        _voucherNo = RGrid.CurrentRow.Cells["GTxtVoucherNo"].Value?.ToString().Trim();
                        ZoomToEntryLevel();
                    }

                    break;
                }
                case "CASH/BANK BOOK":
                {
                    if (RGrid.CurrentRow != null)
                    {
                        Module = RGrid.CurrentRow.Cells["GTxtModule"].Value?.ToString().Trim();
                        _voucherNo = RGrid.CurrentRow.Cells["GTxtVoucherNo"].Value?.ToString().Trim();
                        ZoomToEntryLevel();
                    }

                    break;
                }
                case "RECEIPT/PAYMENT BOOK":
                {
                    if (RGrid.CurrentRow != null)
                    {
                        Module = RGrid.CurrentRow.Cells["GTxtModule"].Value?.ToString().Trim();
                        _voucherNo = RGrid.CurrentRow.Cells["GTxtVoucherNo"].Value?.ToString().Trim();
                        ZoomToEntryLevel();
                    }

                    break;
                }
                case "CASH FLOW":
                {
                    if (RGrid.CurrentRow != null)
                    {
                        Module = RGrid.CurrentRow.Cells["GTxtModule"].Value?.ToString().Trim();
                        _voucherNo = RGrid.CurrentRow.Cells["GTxtVoucherNo"].Value?.ToString().Trim();
                        ZoomToEntryLevel();
                    }

                    break;
                }
                case "TRIAL BALANCE":
                case "BALANCE SHEET":
                case "PROFIT & LOSS":
                case "LEDGER":
                {
                    if (IsDetails)
                    {
                        if (RGrid.CurrentRow != null)
                        {
                            Module = RGrid.CurrentRow.Cells["GTxtModule"].Value?.ToString().Trim();
                            _voucherNo = RGrid.CurrentRow.Cells["GTxtVoucherNo"].Value?.ToString().Trim();
                            ZoomToEntryLevel();
                        }
                    }
                    else
                    {
                        ZoomingReport();
                    }

                    break;
                }
                case "MONTHLY LEDGER":
                {
                    if (RGrid.CurrentRow != null)
                    {
                        var value = RGrid?.CurrentRow?.Cells[0].Value;
                        var currentRow = RGrid!.CurrentRow;
                        if (currentRow != null)
                        {
                            currentRow.Cells[8].Value.ToString();
                            RGrid.CurrentRow.Cells[9].Value.ToString().Trim();
                        }

                        ZoomingReport();
                    }

                    break;
                }
                case "OPENING BALANCE":
                case "LIST_OF_MASTER":
                {
                    break;
                }
                default:
                {
                    var value = RGrid?.CurrentRow?.Cells["GTxtModule"].Value;
                    if (value != null)
                    {
                        Module = RGrid.CurrentRow.Cells["GTxtModule"].Value?.ToString().Trim();
                        _voucherNo = RGrid.CurrentRow.Cells["GTxtVoucherNo"].Value?.ToString().Trim();
                        ZoomToEntryLevel();
                    }

                    break;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption);
        }
    }

    #endregion **--------------- Grid Method ---------------**

    // GENERATE REPORT ON THIS FORM

    #region --------------- FINANCE REPORTS ---------------

    // LIST OF MASTER

    #region ** Master of List

    private bool GenerateListOfMaster()
    {
        var dt = ReportOption switch
        {
            "Account Group" => _report.GetAccountGroupListMaster(),
            "Account Group/Ledger" => _report.GetAccountGroupListMaster(),
            "Account Group/Sub Group" => _report.GetAccountGroupListMaster(),
            "Account Group/Sub Group/Ledger" => _report.GetAccountGroupListMaster(),

            "SubLedger" => _report.GetAccountGroupListMaster(),
            "SubLedger/Ledger" => _report.GetAccountGroupListMaster(),
            "Department" => _report.GetAccountGroupListMaster(),
            "Department/Ledger" => _report.GetAccountGroupListMaster(),

            "Agent" => _report.GetAccountGroupListMaster(),
            "Agent/Ledger" => _report.GetAccountGroupListMaster(),
            "Main Agent/Ledger" => _report.GetAccountGroupListMaster(),

            "Area" => _report.GetAccountGroupListMaster(),
            "Area/Ledger" => _report.GetAccountGroupListMaster(),
            "Main Area/Ledger" => _report.GetAccountGroupListMaster(),
            _ => _report.GetGeneralLedgerListMaster()
        };
        RGrid.DataSource = dt;
        RGrid.AutoResizeColumns();
        if (RGrid.ColumnCount > 0)
        {
            RGrid.Columns["IsGroup"]!.Visible = false;
        }
        return RGrid.RowCount > 0;
    }

    #endregion ** Master of List

    // GENERAL LEDGER REPORTS

    #region ** GENERAL LEDGER

    private bool GenerateLedgerReportSummary()
    {
        RGrid.DataSource = null;
        _design.GetGeneralLedgerSummaryDesign(RGrid);
        if (RGrid == null) return false;
        RGrid.Columns["GTxtLedger"].HeaderText = _report.GetReports.FilterFor switch
        {
            "Account Group" => @"ACCOUNT_GROUP",
            "SubLedger/Ledger" => @"SUB_LEDGER/LEDGER",
            "Ledger/SubLedger" => @"LEDGER/SUB_LEDGER",
            "Ledger/Agent" => @"LEDGER/AGENT",
            "Agent/Ledger" => @"AGENT/LEDGER",
            "Ledger/Department" => @"LEDGER/DEPARTMENT",
            "Department/Ledger" => @"DEPARTMENT/LEDGER",
            "Account Group/Ledger" => @"ACCOUNT_GROUP/LEDGER",
            "Account Group/Sub Group" => @"ACCOUNT_GROUP/SUB_GROUP",
            "Account Group/Sub Group/Ledger" => @"ACCOUNT_GROUP/SUB_GROUP/LEDGER",
            _ => RGrid.Columns["GTxtLedger"].HeaderText
        };
        var dtLedger = _report.GetGeneralLedgerSummaryReport();
        if (dtLedger.Rows.Count > 0)
        {
            RGrid.DataSource = dtLedger;
        }

        return RGrid.RowCount > 0;
    }

    private bool GenerateLedgerReportDetails()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetGeneralLedgerNormalDesign(RGrid);

            RGrid.Columns[@"GTxtCurrency"].Visible = CurrencyType is "Both" or "Foreign";
            RGrid.Columns[@"GTxtExchangeRate"].Visible = CurrencyType is "Both" or "Foreign";

            RGrid.Columns[@"GTxtLocalBalance"].Visible = CurrencyType is "Both";

            RGrid.Columns[@"GTxtActualDebit"].Visible = CurrencyType is "Both" or "Foreign";
            RGrid.Columns[@"GTxtActualCredit"].Visible = CurrencyType is "Both" or "Foreign";

            RGrid.Columns[@"GTxtCredit"].Visible = CurrencyType is not "Foreign";
            RGrid.Columns[@"GTxtDebit"].Visible = CurrencyType is not "Foreign";

            RGrid.Columns["GTxtMiti"].Visible = ObjGlobal.SysDateType.Equals("M") && !IsDate || ObjGlobal.SysDateType.Equals("D") && IsDate;
            RGrid.Columns["GTxtDate"].Visible = !RGrid.Columns["GTxtMiti"].Visible;

            RGrid.Columns[@"GTxtLedger"]!.HeaderText = _report.GetReports.FilterFor switch
            {
                "Account Group" => @"ACCOUNT_GROUP",
                "SubLedger/Ledger" => @"SUB_LEDGER/LEDGER",
                "Ledger/SubLedger" => @"LEDGER/SUB_LEDGER",
                "Ledger/Agent" => @"LEDGER/AGENT",
                "Agent/Ledger" => @"AGENT/LEDGER",
                "Ledger/Department" => @"LEDGER/DEPARTMENT",
                "Department/Ledger" => @"DEPARTMENT/LEDGER",
                "Account Group/Ledger" => @"ACCOUNT_GROUP/LEDGER",
                "Account Group/Sub Group" => @"ACCOUNT_GROUP/SUB_GROUP",
                "Account Group/Sub Group/Ledger" => @"ACCOUNT_GROUP/SUB_GROUP/LEDGER",
                _ => RGrid.Columns[@"GTxtLedger"].HeaderText
            };
            LblReportName.Text = ReportOption.ToUpper() != "LEDGER" ? $"{ReportOption.ToUpper()} {ReportOption.ToUpper()} REPORTS" : $"{ReportOption.ToUpper()} REPORTS";
            var dtLedger = _report.GetGeneralLedgerDetailsReport();
            if (dtLedger.Rows.Count <= 0)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < dtLedger.Rows.Count; i++)
                {
                    if (dtLedger.Rows[i]["DebitAmount"].ToString() != "")
                    {
                        double DAmount = Convert.ToDouble(dtLedger.Rows[i]["DebitAmount"].ToString());
                        dtLedger.Rows[i]["DebitAmount"] = DAmount.ToString("N");

                    }
                    if (dtLedger.Rows[i]["CreditAmount"].ToString() != "")
                    {
                        double DAmount = Convert.ToDouble(dtLedger.Rows[i]["CreditAmount"].ToString());
                        dtLedger.Rows[i]["CreditAmount"] = DAmount.ToString("N");

                    }
                }
            }
            RGrid.DataSource = dtLedger;
            return RGrid.RowCount > 0;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            MessageBox.Show(e.Message, ObjGlobal.Caption);
            return false;
        }
    }

    #endregion ** GENERAL LEDGER

    // DAY BOOK REPORTS

    #region ** DAY/JOURNAL BOOK

    private bool GenerateDayBookReports()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetDayBookNormalReportDesign(RGrid);
            RGrid.Columns["GTxtLocalDebit"].Visible = IsMultiCurrency;
            RGrid.Columns["GTxtLocalCredit"].Visible = IsMultiCurrency;
            RGrid.Columns["GTxtCurrency"].Visible = IsMultiCurrency;
            RGrid.Columns["GTxtExchangeRate"].Visible = IsMultiCurrency;
            RGrid.Columns["GTxtMiti"].Visible = ObjGlobal.SysDateType is "M" && IsDate is false ||
                                                ObjGlobal.SysDateType is "D" && IsDate is true;
            RGrid.Columns["GTxtDate"].Visible = !RGrid.Columns["GTxtMiti"].Visible;

            var dtDayBook = _report.GetDayBookReport();
            if (dtDayBook.Rows.Count <= 0)
            {
                MessageBox.Show(@"DAY BOOK REPORTS NOT FOUND..!!", ObjGlobal.Caption);
                Close();
            }
            else
            {
                var rows = 0;
                RGrid.DataSource = dtDayBook;
                foreach (DataRow dr in dtDayBook.Rows)
                {
                    var reportType = dr["IsGroup"].GetInt();
                    RGrid.Rows[rows].DefaultCellStyle.ForeColor = reportType switch
                    {
                        1 => Blue,
                        3 => BlueViolet,
                        2 => DarkSlateBlue,
                        10 => Blue,
                        11 => IndianRed,
                        33 => BlueViolet,
                        22 => DarkSlateBlue,
                        _ => Black
                    };
                    RGrid.Rows[rows].DefaultCellStyle.Alignment = reportType switch
                    {
                        10 => DataGridViewContentAlignment.MiddleRight,
                        11 => DataGridViewContentAlignment.MiddleRight,
                        33 => DataGridViewContentAlignment.MiddleRight,
                        22 => DataGridViewContentAlignment.MiddleRight,
                        99 => DataGridViewContentAlignment.MiddleRight,
                        _ => RGrid.Rows[rows].DefaultCellStyle.Alignment
                    };
                    RGrid.Rows[rows].DefaultCellStyle.Font = reportType switch
                    {
                        10 => new Font("Bookman Old Style", 9, FontStyle.Italic),
                        11 => new Font("Bookman Old Style", 10, FontStyle.Regular),
                        99 => new Font("Bookman Old Style", 10, FontStyle.Bold),
                        _ => new Font("Bookman Old Style", 10, FontStyle.Regular)
                    };
                    RGrid.Rows[rows].Cells["GTxtExchangeRate"].Value = RGrid.Rows[rows].Cells["GTxtExchangeRate"]
                        .Value.GetDecimalComma();
                    RGrid.Rows[rows].Cells["GTxtDebit"].Value =
                        RGrid.Rows[rows].Cells["GTxtDebit"].Value.GetDecimalComma();
                    RGrid.Rows[rows].Cells["GTxtCredit"].Value =
                        RGrid.Rows[rows].Cells["GTxtCredit"].Value.GetDecimalComma();
                    RGrid.Rows[rows].Cells["GTxtLocalDebit"].Value =
                        RGrid.Rows[rows].Cells["GTxtLocalDebit"].Value.GetDecimalComma();
                    RGrid.Rows[rows].Cells["GTxtLocalCredit"].Value = RGrid.Rows[rows].Cells["GTxtLocalCredit"]
                        .Value.GetDecimalComma();
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                        RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                    rows++;
                }
            }

            return RGrid.RowCount > 0;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError(ex.Message);
            return false;
        }
    }

    private bool GenerateDayBookReportsTFormat()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetDayBookTFormatReportDesign(RGrid);
            RGrid.Columns["GTxtLocalDebit"].Visible = IsMultiCurrency;
            RGrid.Columns["GTxtLocalCredit"].Visible = IsMultiCurrency;
            RGrid.Columns["GTxtMiti1"].Visible = ObjGlobal.SysDateType is "M" && IsDate is false ||
                                                 ObjGlobal.SysDateType is "D" && IsDate is true;
            RGrid.Columns["GTxtMiti2"].Visible = RGrid.Columns["GTxtMiti1"].Visible;
            RGrid.Columns["GTxtDate1"].Visible = !RGrid.Columns["GTxtMiti1"].Visible;
            RGrid.Columns["GTxtDate2"].Visible = RGrid.Columns["GTxtDate1"].Visible;
            var dtDayBook = _report.GetDayBookReport();
            if (dtDayBook.Rows.Count <= 0)
            {
                MessageBox.Show(@"DAY BOOK REPORTS NOT FOUND..!!", ObjGlobal.Caption);
                Close();
            }
            else
            {
                var rows = 0;
                RGrid.DataSource = dtDayBook;
                foreach (DataRow dr in dtDayBook.Rows)
                {
                    var reportType = dr["IsGroup"].GetInt();
                    RGrid.Rows[rows].DefaultCellStyle.ForeColor = reportType switch
                    {
                        1 => Blue,
                        3 => BlueViolet,
                        2 => DarkSlateBlue,
                        10 => Blue,
                        11 => IndianRed,
                        33 => BlueViolet,
                        22 => DarkSlateBlue,
                        _ => Black
                    };
                    RGrid.Rows[rows].DefaultCellStyle.Alignment = reportType switch
                    {
                        10 => DataGridViewContentAlignment.MiddleRight,
                        11 => DataGridViewContentAlignment.MiddleRight,
                        33 => DataGridViewContentAlignment.MiddleRight,
                        22 => DataGridViewContentAlignment.MiddleRight,
                        99 => DataGridViewContentAlignment.MiddleRight,
                        _ => RGrid.Rows[rows].DefaultCellStyle.Alignment
                    };
                    RGrid.Rows[rows].DefaultCellStyle.Font = reportType switch
                    {
                        10 => new Font("Bookman Old Style", 10, FontStyle.Italic),
                        11 => new Font("Bookman Old Style", 10, FontStyle.Regular),
                        99 => new Font("Bookman Old Style", 10, FontStyle.Bold),
                        _ => new Font("Bookman Old Style", 10, FontStyle.Regular)
                    };
                    RGrid.Rows[rows].Cells["GTxtDebit"].Value =
                        RGrid.Rows[rows].Cells["GTxtDebit"].Value.GetDecimalComma();
                    RGrid.Rows[rows].Cells["GTxtCredit"].Value =
                        RGrid.Rows[rows].Cells["GTxtCredit"].Value.GetDecimalComma();
                    RGrid.Rows[rows].Cells["GTxtLocalDebit"].Value =
                        RGrid.Rows[rows].Cells["GTxtLocalDebit"].Value.GetDecimalComma();
                    RGrid.Rows[rows].Cells["GTxtLocalCredit"].Value =
                        RGrid.Rows[rows].Cells["GTxtLocalCredit"].Value.GetDecimalComma();
                    rows++;
                }
            }

            return RGrid.RowCount > 0;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            MessageBox.Show(ex.Message, ObjGlobal.Caption);
            return false;
        }
    }

    private bool GenerateDayBookReportsSummary()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetDayBookNormalReportDesign(RGrid);
            RGrid.Columns["GTxtLocalDebit"].Visible = IsMultiCurrency;
            RGrid.Columns["GTxtLocalCredit"].Visible = IsMultiCurrency;
            RGrid.Columns["GTxtCurrency"].Visible = IsMultiCurrency;
            RGrid.Columns["GTxtExchangeRate"].Visible = IsMultiCurrency;
            RGrid.Columns["GTxtMiti"].Visible = ObjGlobal.SysDateType is "M" && IsDate is false ||
                                                ObjGlobal.SysDateType is "D" && IsDate is true;
            RGrid.Columns["GTxtDate"].Visible = !RGrid.Columns["GTxtMiti"].Visible;
            var dtDayBook = _report.GetDayBookReport();
            if (dtDayBook.Rows.Count <= 0) return false;
            RGrid.DataSource = dtDayBook;
            return RGrid.RowCount > 0;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError(ex.Message);
            return false;
        }
    }

    private bool GenerateJournalVoucherReports()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetDayBookNormalReportDesign(RGrid);
            RGrid.Columns["GTxtLocalDebit"].Visible = IsMultiCurrency;
            RGrid.Columns["GTxtLocalCredit"].Visible = IsMultiCurrency;
            RGrid.Columns["GTxtCurrency"].Visible = IsMultiCurrency;
            RGrid.Columns["GTxtExchangeRate"].Visible = IsMultiCurrency;
            RGrid.Columns["GTxtMiti"].Visible = ObjGlobal.SysDateType is "M" && IsDate is false ||
                                                ObjGlobal.SysDateType is "D" && IsDate is true;
            RGrid.Columns["GTxtDate"].Visible = !RGrid.Columns["GTxtMiti"].Visible;

            var dtDayBook = _report.GetJournalVoucherReport();
            if (dtDayBook.Rows.Count <= 0) return false;
            var rows = 0;
            RGrid.DataSource = dtDayBook;
            foreach (DataRow dr in dtDayBook.Rows)
            {
                var reportType = dr["IsGroup"].GetInt();
                RGrid.Rows[rows].DefaultCellStyle.ForeColor = reportType switch
                {
                    1 => Blue,
                    3 => BlueViolet,
                    2 => DarkSlateBlue,
                    10 => Blue,
                    11 => IndianRed,
                    33 => BlueViolet,
                    22 => DarkSlateBlue,
                    _ => Black
                };
                RGrid.Rows[rows].DefaultCellStyle.Alignment = reportType switch
                {
                    10 => DataGridViewContentAlignment.MiddleRight,
                    11 => DataGridViewContentAlignment.MiddleRight,
                    33 => DataGridViewContentAlignment.MiddleRight,
                    22 => DataGridViewContentAlignment.MiddleRight,
                    99 => DataGridViewContentAlignment.MiddleRight,
                    _ => RGrid.Rows[rows].DefaultCellStyle.Alignment
                };
                RGrid.Rows[rows].DefaultCellStyle.Font = reportType switch
                {
                    10 => new Font("Bookman Old Style", 9, FontStyle.Italic),
                    11 => new Font("Bookman Old Style", 10, FontStyle.Regular),
                    99 => new Font("Bookman Old Style", 10, FontStyle.Bold),
                    _ => new Font("Bookman Old Style", 10, FontStyle.Regular)
                };
                RGrid.Rows[rows].Cells["GTxtExchangeRate"].Value =
                    RGrid.Rows[rows].Cells["GTxtExchangeRate"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtDebit"].Value =
                    RGrid.Rows[rows].Cells["GTxtDebit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtCredit"].Value =
                    RGrid.Rows[rows].Cells["GTxtCredit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtLocalDebit"].Value =
                    RGrid.Rows[rows].Cells["GTxtLocalDebit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtLocalCredit"].Value =
                    RGrid.Rows[rows].Cells["GTxtLocalCredit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                rows++;
            }

            return RGrid.RowCount > 0;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError(ex.Message);
            return false;
        }
    }

    private bool GenerateJournalVoucherReportsTFormat()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetDayBookNormalReportDesign(RGrid);
            RGrid.Columns["GTxtLocalDebit"].Visible = IsMultiCurrency;
            RGrid.Columns["GTxtLocalCredit"].Visible = IsMultiCurrency;
            RGrid.Columns["GTxtCurrency"].Visible = IsMultiCurrency;
            RGrid.Columns["GTxtExchangeRate"].Visible = IsMultiCurrency;
            RGrid.Columns["GTxtMiti"].Visible = ObjGlobal.SysDateType is "M" && IsDate is false ||
                                                ObjGlobal.SysDateType is "D" && IsDate is true;
            RGrid.Columns["GTxtDate"].Visible = !RGrid.Columns["GTxtMiti"].Visible;

            var dtDayBook = _report.GetJournalVoucherReport();
            if (dtDayBook.Rows.Count <= 0) return false;
            var rows = 0;
            RGrid.DataSource = dtDayBook;
            foreach (DataRow dr in dtDayBook.Rows)
            {
                var reportType = dr["IsGroup"].GetInt();
                RGrid.Rows[rows].DefaultCellStyle.ForeColor = reportType switch
                {
                    1 => Blue,
                    3 => BlueViolet,
                    2 => DarkSlateBlue,
                    10 => Blue,
                    11 => IndianRed,
                    33 => BlueViolet,
                    22 => DarkSlateBlue,
                    _ => Black
                };
                RGrid.Rows[rows].DefaultCellStyle.Alignment = reportType switch
                {
                    10 => DataGridViewContentAlignment.MiddleRight,
                    11 => DataGridViewContentAlignment.MiddleRight,
                    33 => DataGridViewContentAlignment.MiddleRight,
                    22 => DataGridViewContentAlignment.MiddleRight,
                    99 => DataGridViewContentAlignment.MiddleRight,
                    _ => RGrid.Rows[rows].DefaultCellStyle.Alignment
                };
                RGrid.Rows[rows].DefaultCellStyle.Font = reportType switch
                {
                    10 => new Font("Bookman Old Style", 9, FontStyle.Italic),
                    11 => new Font("Bookman Old Style", 10, FontStyle.Regular),
                    99 => new Font("Bookman Old Style", 10, FontStyle.Bold),
                    _ => new Font("Bookman Old Style", 10, FontStyle.Regular)
                };
                RGrid.Rows[rows].Cells["GTxtExchangeRate"].Value =
                    RGrid.Rows[rows].Cells["GTxtExchangeRate"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtDebit"].Value =
                    RGrid.Rows[rows].Cells["GTxtDebit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtCredit"].Value =
                    RGrid.Rows[rows].Cells["GTxtCredit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtLocalDebit"].Value =
                    RGrid.Rows[rows].Cells["GTxtLocalDebit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtLocalCredit"].Value =
                    RGrid.Rows[rows].Cells["GTxtLocalCredit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                rows++;
            }

            return RGrid.RowCount > 0;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError(ex.Message);
            return false;
        }
    }

    #endregion ** DAY/JOURNAL BOOK

    // CASH & BANK REPORTS

    #region ** CASH BOOK

    private bool GenerateCashBankReportNormal()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetCashBankJournalVoucherDesign(RGrid);
            if (RGrid == null) return false;
            RGrid.Columns["GTxtDebitAmount"].HeaderText = @"RECEIPT";
            RGrid.Columns["GTxtCreditAmount"].HeaderText = @"PAYMENT";

            RGrid.Columns["GTxtDate"].Visible = ObjGlobal.SysDateType is "M" && IsDate || ObjGlobal.SysDateType is "D" && !IsDate;
            RGrid.Columns["GTxtMiti"].Visible = !RGrid.Columns["GTxtDate"].Visible;

            var dtCashBank = _report.GetCashBankDetailsReports();
            if (dtCashBank.Rows.Count <= 0)
            {
                MessageBox.Show(@"CASH BANK REPORT RECORD NOT FOUND..!!", ObjGlobal.Caption);
                Close();
                return false;
            }

            RGrid.DataSource = dtCashBank;

            return RGrid.RowCount > 0;
        }
        catch (Exception ex)
        {
            var errMsg = ex.Message;
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError(errMsg);
            return false;
        }
    }

    private bool GenerateCashBankReportNormalTFormat()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetCashBankJournalVoucherTFormatDesign(RGrid);
            if (RGrid == null) return false;
            RGrid.Columns["GTxtDebitAmount"].HeaderText = @"RECEIPT";
            RGrid.Columns["GTxtCreditAmount"].HeaderText = @"PAYMENT";

            RGrid.Columns["GTxtDate1"].Visible =
                ObjGlobal.SysDateType is "M" && IsDate || ObjGlobal.SysDateType is "D" && !IsDate;
            RGrid.Columns["GTxtDate2"].Visible = RGrid.Columns["GTxtDate1"].Visible;
            RGrid.Columns["GTxtMiti1"].Visible = !RGrid.Columns["GTxtDate1"].Visible;
            RGrid.Columns["GTxtMiti2"].Visible = RGrid.Columns["GTxtMiti1"].Visible;

            var dtCashBank = _report.GetCashBankDetailsReports();
            if (dtCashBank.Rows.Count <= 0)
            {
                MessageBox.Show(@"CASH BANK REPORT RECORD NOT FOUND..!!", ObjGlobal.Caption);
                Close();
            }
            else
            {
                var rows = 0;
                RGrid.DataSource = dtCashBank;
                foreach (DataRow ro in dtCashBank.Rows)
                {
                    var reportType = ro["IsGroup"].GetInt();
                    RGrid.Rows[rows].DefaultCellStyle.ForeColor = reportType switch
                    {
                        1 => Blue,
                        2 => BlueViolet,
                        11 => Blue,
                        22 => BlueViolet,
                        33 => IndianRed,
                        44 => Blue,
                        _ => Black
                    };
                    RGrid.Rows[rows].DefaultCellStyle.Alignment = reportType switch
                    {
                        11 => DataGridViewContentAlignment.MiddleRight,
                        22 => DataGridViewContentAlignment.MiddleRight,
                        33 => DataGridViewContentAlignment.MiddleRight,
                        44 => DataGridViewContentAlignment.MiddleRight,
                        _ => RGrid.Rows[rows].DefaultCellStyle.Alignment
                    };
                    RGrid.Rows[rows].DefaultCellStyle.Font = reportType switch
                    {
                        10 => new Font("Bookman Old Style", 9, FontStyle.Italic),
                        11 => new Font("Bookman Old Style", 10, FontStyle.Regular),
                        99 => new Font("Bookman Old Style", 10, FontStyle.Bold),
                        _ => new Font("Bookman Old Style", 10, FontStyle.Regular)
                    };
                    RGrid.Rows[rows].Cells["GTxtExchangeRate"].Value =
                        RGrid.Rows[rows].Cells["GTxtExchangeRate"].Value.GetDecimalComma();
                    RGrid.Rows[rows].Cells["GTxtDebitAmount"].Value = RGrid.Rows[rows].Cells["GTxtDebitAmount"]
                        .Value.GetDecimalComma();
                    RGrid.Rows[rows].Cells["GTxtCreditAmount"].Value =
                        RGrid.Rows[rows].Cells["GTxtCreditAmount"].Value.GetDecimalComma();
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                        RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                    rows++;
                }
            }

            return RGrid.RowCount > 0;
        }
        catch (Exception ex)
        {
            var errMsg = ex.Message;
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError(errMsg);
            return false;
        }
    }

    private bool GenerateCashBankReportSummary()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetCashBankSummaryJournalVoucherDesign(RGrid);
            RGrid.Columns["GTxtDate"].Visible =
                ObjGlobal.SysDateType is "M" && IsDate || ObjGlobal.SysDateType is "D" && IsDate;
            RGrid.Columns["GTxtMiti"].Visible = !RGrid.Columns["GTxtDate"].Visible;
            var dtCashBank = _report.GetCashBankSummaryReports();
            if (dtCashBank.Rows.Count <= 0)
            {
                MessageBox.Show(@"CASH BANK SUMMARY REPORT RECORD NOT FOUND..!!", ObjGlobal.Caption);
                Close();
            }
            else
            {
                var rows = 0;
                RGrid.DataSource = dtCashBank;
                foreach (DataRow dr in dtCashBank.Rows)
                {
                    var reportType = dr["IsGroup"].GetInt();
                    RGrid.Rows[rows].DefaultCellStyle.ForeColor = reportType switch
                    {
                        1 => Blue,
                        3 => BlueViolet,
                        2 => DarkSlateBlue,
                        10 => Blue,
                        11 => IndianRed,
                        33 => BlueViolet,
                        22 => DarkSlateBlue,
                        _ => Black
                    };
                    RGrid.Rows[rows].DefaultCellStyle.Alignment = reportType switch
                    {
                        10 => DataGridViewContentAlignment.MiddleRight,
                        11 => DataGridViewContentAlignment.MiddleRight,
                        33 => DataGridViewContentAlignment.MiddleRight,
                        22 => DataGridViewContentAlignment.MiddleRight,
                        99 => DataGridViewContentAlignment.MiddleRight,
                        _ => RGrid.Rows[rows].DefaultCellStyle.Alignment
                    };
                    RGrid.Rows[rows].DefaultCellStyle.Font = reportType switch
                    {
                        10 => new Font("Bookman Old Style", 9, FontStyle.Italic),
                        11 => new Font("Bookman Old Style", 10, FontStyle.Regular),
                        99 => new Font("Bookman Old Style", 10, FontStyle.Bold),
                        _ => new Font("Bookman Old Style", 10, FontStyle.Regular)
                    };
                    RGrid.Rows[rows].Cells["GTxtOpening"].Value =
                        RGrid.Rows[rows].Cells["GTxtOpening"].Value.GetDecimalComma();
                    RGrid.Rows[rows].Cells["GTxtReceipt"].Value =
                        RGrid.Rows[rows].Cells["GTxtReceipt"].Value.GetDecimalComma();
                    RGrid.Rows[rows].Cells["GTxtPayment"].Value =
                        RGrid.Rows[rows].Cells["GTxtPayment"].Value.GetDecimalComma();
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                        RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                    rows++;
                }
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

    private bool GenerateCashBankReportSummaryTFormat()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetCashBankSummaryJournalVoucherDesign(RGrid);
            RGrid.Columns["GTxtDate"].Visible =
                ObjGlobal.SysDateType is "M" && IsDate || ObjGlobal.SysDateType is "D" && IsDate;
            RGrid.Columns["GTxtMiti"].Visible = !RGrid.Columns["GTxtDate"].Visible;
            var dtCashBank = _report.GetCashBankSummaryReports();
            if (dtCashBank.Rows.Count <= 0)
            {
                MessageBox.Show(@"CASH BANK SUMMARY REPORT RECORD NOT FOUND..!!", ObjGlobal.Caption);
                Close();
            }
            else
            {
                var rows = 0;
                RGrid.DataSource = dtCashBank;
                foreach (DataRow dr in dtCashBank.Rows)
                {
                    var reportType = dr["IsGroup"].GetInt();
                    RGrid.Rows[rows].DefaultCellStyle.ForeColor = reportType switch
                    {
                        1 => Blue,
                        3 => BlueViolet,
                        2 => DarkSlateBlue,
                        10 => Blue,
                        11 => IndianRed,
                        33 => BlueViolet,
                        22 => DarkSlateBlue,
                        _ => Black
                    };
                    RGrid.Rows[rows].DefaultCellStyle.Alignment = reportType switch
                    {
                        10 => DataGridViewContentAlignment.MiddleRight,
                        11 => DataGridViewContentAlignment.MiddleRight,
                        33 => DataGridViewContentAlignment.MiddleRight,
                        22 => DataGridViewContentAlignment.MiddleRight,
                        99 => DataGridViewContentAlignment.MiddleRight,
                        _ => RGrid.Rows[rows].DefaultCellStyle.Alignment
                    };
                    RGrid.Rows[rows].DefaultCellStyle.Font = reportType switch
                    {
                        10 => new Font("Bookman Old Style", 9, FontStyle.Italic),
                        11 => new Font("Bookman Old Style", 10, FontStyle.Regular),
                        99 => new Font("Bookman Old Style", 10, FontStyle.Bold),
                        _ => new Font("Bookman Old Style", 10, FontStyle.Regular)
                    };
                    RGrid.Rows[rows].Cells["GTxtOpening"].Value = RGrid.Rows[rows].Cells["GTxtOpening"]
                        .Value.GetDecimalComma();
                    RGrid.Rows[rows].Cells["GTxtReceipt"].Value =
                        RGrid.Rows[rows].Cells["GTxtReceipt"].Value.GetDecimalComma();
                    RGrid.Rows[rows].Cells["GTxtPayment"].Value =
                        RGrid.Rows[rows].Cells["GTxtPayment"].Value.GetDecimalComma();
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                        RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                    rows++;
                }
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

    private bool GenerateCashFlowReportNormal()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetCashBankSummaryJournalVoucherDesign(RGrid);
            RGrid.Columns["GTxtDate"].Visible =
                ObjGlobal.SysDateType is "M" && IsDate || ObjGlobal.SysDateType is "D" && IsDate;
            RGrid.Columns["GTxtMiti"].Visible = !RGrid.Columns["GTxtDate"].Visible;
            var dtCashBank = _report.GetCashBankSummaryReports();
            if (dtCashBank.Rows.Count <= 0) return false;
            var rows = 0;
            RGrid.DataSource = dtCashBank;
            foreach (DataRow dr in dtCashBank.Rows)
            {
                var reportType = dr["IsGroup"].GetInt();
                RGrid.Rows[rows].DefaultCellStyle.ForeColor = reportType switch
                {
                    1 => Blue,
                    3 => BlueViolet,
                    2 => DarkSlateBlue,
                    10 => Blue,
                    11 => IndianRed,
                    33 => BlueViolet,
                    22 => DarkSlateBlue,
                    _ => Black
                };
                RGrid.Rows[rows].DefaultCellStyle.Alignment = reportType switch
                {
                    10 => DataGridViewContentAlignment.MiddleRight,
                    11 => DataGridViewContentAlignment.MiddleRight,
                    33 => DataGridViewContentAlignment.MiddleRight,
                    22 => DataGridViewContentAlignment.MiddleRight,
                    99 => DataGridViewContentAlignment.MiddleRight,
                    _ => RGrid.Rows[rows].DefaultCellStyle.Alignment
                };
                RGrid.Rows[rows].DefaultCellStyle.Font = reportType switch
                {
                    10 => new Font("Bookman Old Style", 9, FontStyle.Italic),
                    11 => new Font("Bookman Old Style", 10, FontStyle.Regular),
                    99 => new Font("Bookman Old Style", 10, FontStyle.Bold),
                    _ => new Font("Bookman Old Style", 10, FontStyle.Regular)
                };
                RGrid.Rows[rows].Cells["GTxtOpening"].Value =
                    RGrid.Rows[rows].Cells["GTxtOpening"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtReceipt"].Value =
                    RGrid.Rows[rows].Cells["GTxtReceipt"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtPayment"].Value =
                    RGrid.Rows[rows].Cells["GTxtPayment"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                rows++;
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

    private bool GenerateCashFlowReportNormalTFormat()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetCashBankSummaryJournalVoucherDesign(RGrid);
            RGrid.Columns["GTxtDate"].Visible =
                ObjGlobal.SysDateType is "M" && IsDate || ObjGlobal.SysDateType is "D" && IsDate;
            RGrid.Columns["GTxtMiti"].Visible = !RGrid.Columns["GTxtDate"].Visible;
            var dtCashBank = _report.GetCashBankSummaryReports();
            if (dtCashBank.Rows.Count <= 0) return false;
            var rows = 0;
            RGrid.DataSource = dtCashBank;
            foreach (DataRow dr in dtCashBank.Rows)
            {
                var reportType = dr["IsGroup"].GetInt();
                RGrid.Rows[rows].DefaultCellStyle.ForeColor = reportType switch
                {
                    1 => Blue,
                    3 => BlueViolet,
                    2 => DarkSlateBlue,
                    10 => Blue,
                    11 => IndianRed,
                    33 => BlueViolet,
                    22 => DarkSlateBlue,
                    _ => Black
                };
                RGrid.Rows[rows].DefaultCellStyle.Alignment = reportType switch
                {
                    10 => DataGridViewContentAlignment.MiddleRight,
                    11 => DataGridViewContentAlignment.MiddleRight,
                    33 => DataGridViewContentAlignment.MiddleRight,
                    22 => DataGridViewContentAlignment.MiddleRight,
                    99 => DataGridViewContentAlignment.MiddleRight,
                    _ => RGrid.Rows[rows].DefaultCellStyle.Alignment
                };
                RGrid.Rows[rows].DefaultCellStyle.Font = reportType switch
                {
                    10 => new Font("Bookman Old Style", 9, FontStyle.Italic),
                    11 => new Font("Bookman Old Style", 10, FontStyle.Regular),
                    99 => new Font("Bookman Old Style", 10, FontStyle.Bold),
                    _ => new Font("Bookman Old Style", 10, FontStyle.Regular)
                };
                RGrid.Rows[rows].Cells["GTxtOpening"].Value =
                    RGrid.Rows[rows].Cells["GTxtOpening"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtReceipt"].Value =
                    RGrid.Rows[rows].Cells["GTxtReceipt"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtPayment"].Value =
                    RGrid.Rows[rows].Cells["GTxtPayment"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                rows++;
            }

            return RGrid.RowCount > 0;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError(ex.Message);
            return false;
        }
    }

    #endregion ** CASH BOOK

    // TRIAL BALANCE REPORT

    #region ** --------------- TRIAL BALANCE --------------- **

    private bool GenerateOpeningTrialBalanceNormal()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetTrialBalanceNormalDesign(RGrid);
            var dtTrial = _report.GetOpeningTrialBalanceReport();
            if (dtTrial.Rows.Count <= 0)
            {
                return false;
            }
            RGrid.DataSource = dtTrial;
            RGrid.Columns["GTxtLedger"].HeaderText = _report.GetReports.FilterFor switch
            {
                "Account Group" => @"ACCOUNT_GROUP",
                "Account Group/Ledger" => @"ACCOUNT_GROUP/LEDGER",
                "Account Group/Sub Group" => @"ACCOUNT_GROUP/SUB_GROUP",
                "Account Group/Sub Group/Ledger" => @"ACCOUNT_GROUP/SUB_GROUP/LEDGER",
                _ => RGrid.Columns["GTxtLedger"].HeaderText
            };
            return RGrid.RowCount > 0;
        }
        catch (Exception ex)
        {
            ex.DialogResult();
            return false;
        }
    }

    private bool GenerateOpeningTrialBalanceTFormat()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetTrialBalanceNormalDesign(RGrid);
            var dtTrial = _report.GetNormalTrialBalanceReport();
            if (dtTrial.Rows.Count <= 0)
            {
                return false;
            }
            var rows = 0;
            RGrid.DataSource = dtTrial;
            RGrid.Columns["GTxtLedger"]!.HeaderText = _report.GetReports.FilterFor switch
            {
                "Account Group" => @"ACCOUNT_GROUP",
                "Account Group/Ledger" => @"ACCOUNT_GROUP/LEDGER",
                "Account Group/Sub Group" => @"ACCOUNT_GROUP/SUB_GROUP",
                "Account Group/Sub Group/Ledger" => @"ACCOUNT_GROUP/SUB_GROUP/LEDGER",
                _ => RGrid.Columns["GTxtLedger"].HeaderText
            };
            foreach (DataRow dr in dtTrial.Rows)
            {
                var reportType = dr["IsGroup"].GetInt();
                RGrid.Rows[rows].DefaultCellStyle.ForeColor = reportType switch
                {
                    1 => Blue,
                    3 => BlueViolet,
                    2 => DarkSlateBlue,
                    10 => Blue,
                    11 => IndianRed,
                    33 => BlueViolet,
                    22 => DarkSlateBlue,
                    _ => Black
                };
                RGrid.Rows[rows].DefaultCellStyle.Alignment = reportType switch
                {
                    10 => DataGridViewContentAlignment.MiddleRight,
                    11 => DataGridViewContentAlignment.MiddleRight,
                    33 => DataGridViewContentAlignment.MiddleRight,
                    22 => DataGridViewContentAlignment.MiddleRight,
                    99 => DataGridViewContentAlignment.MiddleRight,
                    _ => RGrid.Rows[rows].DefaultCellStyle.Alignment
                };
                RGrid.Rows[rows].DefaultCellStyle.Font = reportType switch
                {
                    10 => new Font("Bookman Old Style", 9, FontStyle.Italic),
                    11 => new Font("Bookman Old Style", 10, FontStyle.Regular),
                    99 => new Font("Bookman Old Style", 10, FontStyle.Bold),
                    _ => new Font("Bookman Old Style", 10, FontStyle.Regular)
                };
                RGrid.Rows[rows].Cells["GTxtDebit"].Value = RGrid.Rows[rows].Cells["GTxtDebit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtCredit"].Value = RGrid.Rows[rows].Cells["GTxtCredit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtPayment"].Value = RGrid.Rows[rows].Cells["GTxtPayment"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtBalance"].Value = RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                rows++;
            }
            return RGrid.RowCount > 0;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError(ex.Message);
            return false;
        }
    }

    private bool GenerateTrialBalanceNormal()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetTrialBalanceNormalDesign(RGrid);
            var dtTrial = _report.GetNormalTrialBalanceReport();
            if (dtTrial.Rows.Count <= 0)
            {
                return false;
            }
            RGrid.DataSource = dtTrial;
            RGrid.Columns["GTxtLedger"]!.HeaderText = _report.GetReports.FilterFor switch
            {
                "Account Group" => @"ACCOUNT_GROUP",
                "Account Group/Ledger" => @"ACCOUNT_GROUP/LEDGER",
                "Account Group/Sub Group" => @"ACCOUNT_GROUP/SUB_GROUP",
                "Account Group/Sub Group/Ledger" => @"ACCOUNT_GROUP/SUB_GROUP/LEDGER",
                _ => RGrid.Columns["GTxtLedger"].HeaderText
            };
            return RGrid.RowCount > 0;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError(ex.Message);
            return false;
        }
    }

    private bool GenerateTrialBalancePeriodic()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetTrialBalancePeriodicDesign(RGrid);
            var dtTrial = _report.GetPeriodicTrialBalanceReport();
            RGrid.Columns["GTxtLedger"]!.HeaderText = _report.GetReports.FilterFor switch
            {
                "Account Group/Ledger" => @"ACCOUNT_GROUP/LEDGER",
                "Account Group/Sub Group/Ledger" => @"ACCOUNT_GROUP/SUB_GROUP/LEDGER",
                _ => RGrid.Columns["GTxtLedger"].HeaderText
            };
            if (dtTrial.Rows.Count is 0) return false;
            RGrid.DataSource = dtTrial;
            var rows = 0;
            foreach (DataRow dr in dtTrial.Rows)
            {
                var reportType = dr["IsGroup"].GetInt();
                RGrid.Rows[rows].DefaultCellStyle.ForeColor = reportType switch
                {
                    1 => Blue,
                    3 => BlueViolet,
                    2 => DarkSlateBlue,
                    10 => Blue,
                    11 => IndianRed,
                    33 => BlueViolet,
                    22 => DarkSlateBlue,
                    _ => Black
                };
                RGrid.Rows[rows].DefaultCellStyle.Alignment = reportType switch
                {
                    10 => DataGridViewContentAlignment.MiddleRight,
                    11 => DataGridViewContentAlignment.MiddleRight,
                    33 => DataGridViewContentAlignment.MiddleRight,
                    22 => DataGridViewContentAlignment.MiddleRight,
                    99 => DataGridViewContentAlignment.MiddleRight,
                    88 => DataGridViewContentAlignment.MiddleRight,
                    _ => RGrid.Rows[rows].DefaultCellStyle.Alignment
                };
                RGrid.Rows[rows].DefaultCellStyle.Font = reportType switch
                {
                    10 => new Font("Bookman Old Style", 9, FontStyle.Italic),
                    11 => new Font("Bookman Old Style", 10, FontStyle.Regular),
                    88 => new Font("Bookman Old Style", 10, FontStyle.Italic),
                    99 => new Font("Bookman Old Style", 10, FontStyle.Bold),
                    _ => new Font("Bookman Old Style", 10, FontStyle.Regular)
                };
                RGrid.Rows[rows].Cells["GTxtOpeningDebit"].Value =
                    RGrid.Rows[rows].Cells["GTxtOpeningDebit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtOpeningCredit"].Value =
                    RGrid.Rows[rows].Cells["GTxtOpeningCredit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtDebit"].Value =
                    RGrid.Rows[rows].Cells["GTxtDebit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtCredit"].Value =
                    RGrid.Rows[rows].Cells["GTxtCredit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();

                RGrid.Rows[rows].Cells["GTxtClosingDebit"].Value =
                    RGrid.Rows[rows].Cells["GTxtClosingDebit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtClosingCredit"].Value =
                    RGrid.Rows[rows].Cells["GTxtClosingCredit"].Value.GetDecimalComma();
                rows++;
            }

            return RGrid.RowCount > 0;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError(ex.Message);
            return false;
        }
    }

    private bool GenerateTrialBalanceTFormat()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetTrialBalanceNormalTFormatDesign(RGrid);
            var dtTrial = _report.GetNormalTrialBalanceReport();
            if (dtTrial.Rows.Count <= 0)
            {
                return false;
            }
            RGrid.DataSource = dtTrial;
            RGrid.Columns["GTxtLedger"]!.HeaderText = _report.GetReports.FilterFor switch
            {
                "Account Group" => @"ACCOUNT_GROUP",
                "Account Group/Ledger" => @"ACCOUNT_GROUP/LEDGER",
                "Account Group/Sub Group" => @"ACCOUNT_GROUP/SUB_GROUP",
                "Account Group/Sub Group/Ledger" => @"ACCOUNT_GROUP/SUB_GROUP/LEDGER",
                _ => RGrid.Columns["GTxtLedger"].HeaderText
            };
            var rows = 0;
            foreach (DataRow dr in dtTrial.Rows)
            {
                var reportType = dr["IsGroup"].GetInt();
                RGrid.Rows[rows].DefaultCellStyle.ForeColor = reportType switch
                {
                    1 => Blue,
                    3 => BlueViolet,
                    2 => DarkSlateBlue,
                    10 => Blue,
                    11 => IndianRed,
                    33 => BlueViolet,
                    22 => DarkSlateBlue,
                    _ => Black
                };
                RGrid.Rows[rows].DefaultCellStyle.Alignment = reportType switch
                {
                    10 => DataGridViewContentAlignment.MiddleRight,
                    11 => DataGridViewContentAlignment.MiddleRight,
                    33 => DataGridViewContentAlignment.MiddleRight,
                    22 => DataGridViewContentAlignment.MiddleRight,
                    99 => DataGridViewContentAlignment.MiddleRight,
                    _ => RGrid.Rows[rows].DefaultCellStyle.Alignment
                };
                RGrid.Rows[rows].DefaultCellStyle.Font = reportType switch
                {
                    10 => new Font("Bookman Old Style", 9, FontStyle.Italic),
                    11 => new Font("Bookman Old Style", 10, FontStyle.Regular),
                    99 => new Font("Bookman Old Style", 10, FontStyle.Bold),
                    _ => new Font("Bookman Old Style", 10, FontStyle.Regular)
                };
                RGrid.Rows[rows].Cells["GTxtDebit"].Value =
                    RGrid.Rows[rows].Cells["GTxtDebit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtCredit"].Value =
                    RGrid.Rows[rows].Cells["GTxtCredit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtPayment"].Value =
                    RGrid.Rows[rows].Cells["GTxtPayment"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                rows++;
            }

            return RGrid.RowCount > 0;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError(ex.Message);
            return false;
        }
    }

    #endregion ** --------------- TRIAL BALANCE --------------- **

    // BALANCE SHEET REPORTS

    #region ** --------------- BALANCE SHEET --------------- **

    private bool GenerateOpeningBalanceSheetNormal()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetProfitLossBalanceSheetNormalDesign(RGrid);
            var dtBalance = _report.GetNormalBalanceSheetReport();
            if (dtBalance.Rows.Count <= 0) return false;
            RGrid.DataSource = dtBalance;
            RGrid.Columns["GTxtLedger"]!.HeaderText = _report.GetReports.FilterFor switch
            {
                "Account Group" => "ACCOUNT_GROUP",
                "Account Group/Ledger" => "ACCOUNT_GROUP/LEDGER",
                "Account Group/Sub Group" => @"ACCOUNT_GROUP/SUB_GROUP",
                "Account Group/Sub Group/Ledger" => @"ACCOUNT_GROUP/SUB_GROUP/LEDGER",
                _ => RGrid.Columns["GTxtLedger"]?.HeaderText
            };
            var rows = 0;
            foreach (DataRow dr in dtBalance.Rows)
            {
                var reportType = dr["IsGroup"].GetInt();
                RGrid.Rows[rows].DefaultCellStyle.ForeColor = reportType switch
                {
                    1 => Blue,
                    3 => BlueViolet,
                    2 => DarkSlateBlue,
                    10 => Blue,
                    11 => IndianRed,
                    33 => BlueViolet,
                    22 => DarkSlateBlue,
                    _ => Black
                };
                RGrid.Rows[rows].DefaultCellStyle.Alignment = reportType switch
                {
                    10 => DataGridViewContentAlignment.MiddleRight,
                    11 => DataGridViewContentAlignment.MiddleRight,
                    33 => DataGridViewContentAlignment.MiddleRight,
                    22 => DataGridViewContentAlignment.MiddleRight,
                    99 => DataGridViewContentAlignment.MiddleRight,
                    _ => RGrid.Rows[rows].DefaultCellStyle.Alignment
                };
                RGrid.Rows[rows].DefaultCellStyle.Font = reportType switch
                {
                    10 => new Font("Bookman Old Style", 9, FontStyle.Italic),
                    11 => new Font("Bookman Old Style", 10, FontStyle.Regular),
                    99 => new Font("Bookman Old Style", 10, FontStyle.Bold),
                    _ => new Font("Bookman Old Style", 10, FontStyle.Regular)
                };
                RGrid.Rows[rows].Cells["GTxtDebit"].Value =
                    RGrid.Rows[rows].Cells["GTxtDebit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtCredit"].Value =
                    RGrid.Rows[rows].Cells["GTxtCredit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtPayment"].Value =
                    RGrid.Rows[rows].Cells["GTxtPayment"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                rows++;
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

    private bool GenerateOpeningBalanceSheetNormalTFormat()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetProfitLossBalanceSheetNormalDesign(RGrid);
            var dtBalance = _report.GetNormalBalanceSheetReport();
            if (dtBalance.Rows.Count <= 0) return false;
            RGrid.DataSource = dtBalance;
            RGrid.Columns["GTxtLedger"]!.HeaderText = _report.GetReports.FilterFor switch
            {
                "Account Group" => "ACCOUNT_GROUP",
                "Account Group/Ledger" => "ACCOUNT_GROUP/LEDGER",
                "Account Group/Sub Group" => @"ACCOUNT_GROUP/SUB_GROUP",
                "Account Group/Sub Group/Ledger" => @"ACCOUNT_GROUP/SUB_GROUP/LEDGER",
                _ => RGrid.Columns["GTxtLedger"]?.HeaderText
            };
            var rows = 0;
            foreach (DataRow dr in dtBalance.Rows)
            {
                var reportType = dr["IsGroup"].GetInt();
                RGrid.Rows[rows].DefaultCellStyle.ForeColor = reportType switch
                {
                    1 => Blue,
                    3 => BlueViolet,
                    2 => DarkSlateBlue,
                    10 => Blue,
                    11 => IndianRed,
                    33 => BlueViolet,
                    22 => DarkSlateBlue,
                    _ => Black
                };
                RGrid.Rows[rows].DefaultCellStyle.Alignment = reportType switch
                {
                    10 => DataGridViewContentAlignment.MiddleRight,
                    11 => DataGridViewContentAlignment.MiddleRight,
                    33 => DataGridViewContentAlignment.MiddleRight,
                    22 => DataGridViewContentAlignment.MiddleRight,
                    99 => DataGridViewContentAlignment.MiddleRight,
                    _ => RGrid.Rows[rows].DefaultCellStyle.Alignment
                };
                RGrid.Rows[rows].DefaultCellStyle.Font = reportType switch
                {
                    10 => new Font("Bookman Old Style", 9, FontStyle.Italic),
                    11 => new Font("Bookman Old Style", 10, FontStyle.Regular),
                    99 => new Font("Bookman Old Style", 10, FontStyle.Bold),
                    _ => new Font("Bookman Old Style", 10, FontStyle.Regular)
                };
                RGrid.Rows[rows].Cells["GTxtDebit"].Value =
                    RGrid.Rows[rows].Cells["GTxtDebit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtCredit"].Value =
                    RGrid.Rows[rows].Cells["GTxtCredit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtPayment"].Value =
                    RGrid.Rows[rows].Cells["GTxtPayment"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                rows++;
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

    private bool GenerateBalanceSheetNormal()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetProfitLossBalanceSheetNormalDesign(RGrid);
            var dtBalance = _report.GetNormalBalanceSheetReport();
            if (dtBalance.Rows.Count <= 0) return false;
            RGrid.DataSource = dtBalance;
            RGrid.Columns["GTxtLedger"]!.HeaderText = _report.GetReports.FilterFor switch
            {
                "Account Group" => "ACCOUNT_GROUP",
                "Ledger/SubLedger" => "LEDGER/SUB_LEDGER",
                "Account Group/Ledger" => "ACCOUNT_GROUP/LEDGER",
                "Account Group/Sub Group" => @"ACCOUNT_GROUP/SUB_GROUP",
                "Account Group/Sub Group/Ledger" => @"ACCOUNT_GROUP/SUB_GROUP/LEDGER",
                _ => RGrid.Columns["GTxtLedger"]?.HeaderText
            };
            RGrid.Columns["GTxtShortName"]!.Visible = IncludeShortName;
            return RGrid.RowCount > 0;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            this.NotifyError(e.Message);
            return false;
        }
    }

    private bool GenerateBalanceSheetTFormat()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetProfitLossBalanceSheetNormalTFormatDesign(RGrid);
            var dtBalance = _report.GetNormalBalanceSheetReport();
            if (dtBalance.Rows.Count <= 0) return false;
            RGrid.DataSource = dtBalance;
            RGrid.Columns["GTxtLedger"]!.HeaderText = _report.GetReports.FilterFor switch
            {
                "Account Group" => "ACCOUNT_GROUP",
                "Account Group/Ledger" => "ACCOUNT_GROUP/LEDGER",
                "Account Group/Sub Group" => @"ACCOUNT_GROUP/SUB_GROUP",
                "Account Group/Sub Group/Ledger" => @"ACCOUNT_GROUP/SUB_GROUP/LEDGER",
                _ => RGrid.Columns["GTxtLedger"]?.HeaderText
            };
            var rows = 0;
            foreach (DataRow dr in dtBalance.Rows)
            {
                var reportType = dr["IsGroup"].GetInt();
                RGrid.Rows[rows].DefaultCellStyle.ForeColor = reportType switch
                {
                    1 => Blue,
                    3 => BlueViolet,
                    2 => DarkSlateBlue,
                    10 => Blue,
                    11 => IndianRed,
                    33 => BlueViolet,
                    22 => DarkSlateBlue,
                    _ => Black
                };
                RGrid.Rows[rows].DefaultCellStyle.Alignment = reportType switch
                {
                    10 => DataGridViewContentAlignment.MiddleRight,
                    11 => DataGridViewContentAlignment.MiddleRight,
                    33 => DataGridViewContentAlignment.MiddleRight,
                    22 => DataGridViewContentAlignment.MiddleRight,
                    99 => DataGridViewContentAlignment.MiddleRight,
                    _ => RGrid.Rows[rows].DefaultCellStyle.Alignment
                };
                RGrid.Rows[rows].DefaultCellStyle.Font = reportType switch
                {
                    10 => new Font("Bookman Old Style", 9, FontStyle.Italic),
                    11 => new Font("Bookman Old Style", 10, FontStyle.Regular),
                    99 => new Font("Bookman Old Style", 10, FontStyle.Bold),
                    _ => new Font("Bookman Old Style", 10, FontStyle.Regular)
                };
                RGrid.Rows[rows].Cells["GTxtDebit"].Value =
                    RGrid.Rows[rows].Cells["GTxtDebit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtCredit"].Value =
                    RGrid.Rows[rows].Cells["GTxtCredit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtPayment"].Value =
                    RGrid.Rows[rows].Cells["GTxtPayment"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                rows++;
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

    private bool GenerateBalanceSheetPeriodic()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetProfitLossBalanceSheetPeriodicDesign(RGrid);
            var dtBalance = _report.GetPeriodicBalanceSheetReport();
            if (dtBalance.Rows.Count <= 0) return false;
            RGrid.DataSource = dtBalance;
            RGrid.Columns["GTxtLedger"]!.HeaderText = _report.GetReports.FilterFor switch
            {
                "Account Group" => "ACCOUNT_GROUP",
                "Account Group/Ledger" => "ACCOUNT_GROUP/LEDGER",
                "Account Group/Sub Group" => @"ACCOUNT_GROUP/SUB_GROUP",
                "Account Group/Sub Group/Ledger" => @"ACCOUNT_GROUP/SUB_GROUP/LEDGER",
                _ => RGrid.Columns["GTxtLedger"]?.HeaderText
            };
            var rows = 0;
            foreach (DataRow ro in dtBalance.Rows)
            {
                var reportType = ro["IsGroup"].GetInt();
                RGrid.Rows[rows].DefaultCellStyle.ForeColor = reportType switch
                {
                    1 => Blue,
                    3 => BlueViolet,
                    2 => DarkSlateBlue,
                    10 => Blue,
                    11 => IndianRed,
                    33 => BlueViolet,
                    22 => DarkSlateBlue,
                    88 => IndianRed,
                    _ => Black
                };
                RGrid.Rows[rows].DefaultCellStyle.Alignment = reportType switch
                {
                    10 => DataGridViewContentAlignment.MiddleRight,
                    11 => DataGridViewContentAlignment.MiddleRight,
                    33 => DataGridViewContentAlignment.MiddleRight,
                    22 => DataGridViewContentAlignment.MiddleRight,
                    88 => DataGridViewContentAlignment.MiddleRight,
                    99 => DataGridViewContentAlignment.MiddleRight,
                    _ => RGrid.Rows[rows].DefaultCellStyle.Alignment
                };
                RGrid.Rows[rows].DefaultCellStyle.Font = reportType switch
                {
                    11 => new Font("Bookman Old Style", 10, FontStyle.Regular),
                    88 => new Font("Bookman Old Style", 10, FontStyle.Italic),
                    99 => new Font("Bookman Old Style", 10, FontStyle.Bold),
                    _ => new Font("Bookman Old Style", 10, FontStyle.Regular)
                };
                RGrid.Rows[rows].Cells["GTxtOpening"].Value =
                    RGrid.Rows[rows].Cells["GTxtOpening"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtDebit"].Value =
                    RGrid.Rows[rows].Cells["GTxtDebit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtCredit"].Value =
                    RGrid.Rows[rows].Cells["GTxtCredit"].Value.GetDecimalComma();
                RGrid.Rows[rows].Cells["GTxtBalance"].Value =
                    RGrid.Rows[rows].Cells["GTxtBalance"].Value.GetDecimalComma();
                rows++;
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

    private bool GenerateBalanceSheetPeriodicTFormat()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetProfitLossBalanceSheetPeriodicDesign(RGrid);
            var dtBalance = _report.GetNormalBalanceSheetReport();
            if (dtBalance.Rows.Count <= 0) return false;
            RGrid.DataSource = dtBalance;
            RGrid.Columns["GTxtLedger"]!.HeaderText = _report.GetReports.FilterFor switch
            {
                "Account Group" => "ACCOUNT_GROUP",
                "Account Group/Ledger" => "ACCOUNT_GROUP/LEDGER",
                "Account Group/Sub Group" => @"ACCOUNT_GROUP/SUB_GROUP",
                "Account Group/Sub Group/Ledger" => @"ACCOUNT_GROUP/SUB_GROUP/LEDGER",
                _ => RGrid.Columns["GTxtLedger"]?.HeaderText
            };
            foreach (DataRow ro in dtBalance.Rows)
            {
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

    #endregion ** --------------- BALANCE SHEET --------------- **

    // PROFIT & LOSS REPORT

    #region ** ---------------PROFIT && LOSS --------------- **

    private bool GenerateProfitLossNormal()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetProfitLossBalanceSheetNormalDesign(RGrid);
            var dtProfit = _report.GetNormalProfitLossReport();
            RGrid.Columns["GTxtLedger"]!.HeaderText = _report.GetReports.FilterFor switch
            {
                "Account Group" => "ACCOUNT_GROUP",
                "Account Group/Ledger" => "ACCOUNT_GROUP/LEDGER",
                "Account Group/Sub Group" => @"ACCOUNT_GROUP/SUB_GROUP",
                "Account Group/Sub Group/Ledger" => @"ACCOUNT_GROUP/SUB_GROUP/LEDGER",
                _ => RGrid.Columns["GTxtLedger"]?.HeaderText
            };
            if (dtProfit.Rows.Count <= 0)
            {
                return false;
            }
            RGrid.DataSource = dtProfit;
            return RGrid.RowCount > 0;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            this.NotifyError(e.Message);
            return false;
        }
    }

    private bool GenerateProfitLossNormalTFormat()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetProfitLossBalanceSheetNormalTFormatDesign(RGrid);
            var dtProfit = _report.GetNormalProfitLossReport();
            RGrid.Columns["GTxtLedger"]!.HeaderText = _report.GetReports.FilterFor switch
            {
                "Account Group" => "ACCOUNT_GROUP",
                "Account Group/Ledger" => "ACCOUNT_GROUP/LEDGER",
                "Account Group/Sub Group" => @"ACCOUNT_GROUP/SUB_GROUP",
                "Account Group/Sub Group/Ledger" => @"ACCOUNT_GROUP/SUB_GROUP/LEDGER",
                _ => RGrid.Columns["GTxtLedger"]?.HeaderText
            };
            if (dtProfit.Rows.Count <= 0)
            {
                return false;
            }
            RGrid.DataSource = dtProfit;
            return RGrid.RowCount > 0;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            this.NotifyError(e.Message);
            return false;
        }
    }

    private bool GenerateProfitLossPeriodic()
    {
        RGrid.DataSource = null;
        _design.GetProfitLossBalanceSheetPeriodicDesign(RGrid);
        RGrid.Columns["GTxtLedger"]!.HeaderText = _report.GetReports.FilterFor switch
        {
            "Account Group" => "ACCOUNT_GROUP",
            "Account Group/Ledger" => "ACCOUNT_GROUP/LEDGER",
            "Account Group/Sub Group" => @"ACCOUNT_GROUP/SUB_GROUP",
            "Account Group/Sub Group/Ledger" => @"ACCOUNT_GROUP/SUB_GROUP/LEDGER",
            _ => RGrid.Columns["GTxtLedger"]?.HeaderText
        };
        var dtProfit = _report.GetPeriodicProfitLossReport();
        if (dtProfit.Rows.Count <= 0)
        {
            return false;
        }
        RGrid.DataSource = dtProfit;
        return RGrid.RowCount > 0;
    }

    private bool GenerateProfitLossPeriodicTFormat()
    {
        try
        {
            RGrid.DataSource = null;
            _design.GetProfitLossBalanceSheetNormalTFormatDesign(RGrid);
            var dtProfit = _report.GetPeriodicProfitLossReport();
            if (dtProfit?.Rows.Count <= 0) return false;
            var rows = 0;
            RGrid.DataSource = dtProfit;
            RGrid.Columns["GTxtLedger"]!.HeaderText = _report.GetReports.FilterFor switch
            {
                "Account Group" => "ACCOUNT_GROUP",
                "Account Group/Ledger" => "ACCOUNT_GROUP/LEDGER",
                "Account Group/Sub Group" => @"ACCOUNT_GROUP/SUB_GROUP",
                "Account Group/Sub Group/Ledger" => @"ACCOUNT_GROUP/SUB_GROUP/LEDGER",
                _ => RGrid.Columns["GTxtLedger"]?.HeaderText
            };
            return RGrid.RowCount > 0;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            this.NotifyError(e.Message);
            return false;
        }
    }

    #endregion ** ---------------PROFIT && LOSS --------------- **

    // MONTHLY LEDGER

    #region ** --------------- MONTHLY LEDGER --------------- **

    private bool GenerateLedgerReportMonthly()
    {
        RGrid.DataSource = null;
        return false;
    }

    #endregion ** --------------- MONTHLY LEDGER --------------- **

    // ZOOMING FUNCTION

    #region ** --------------- Zooming Function ---------------

    private void ZoomingReport()
    {
        if (RptName is "TRIAL BALANCE" or "BALANCE SHEET" or "PROFIT & LOSS" || RptName is "LEDGER" && !IsDetails)
        {
            if (RGrid.CurrentRow != null)
            {
                if (ReportOption.GetUpper() is "ACCOUNT GROUP" && RGrid.CurrentRow.Cells["IsGroup"].Value.GetInt() is 0)
                {
                    GroupId = RGrid.CurrentRow.Cells["GTxtLedgerId"].Value.ToString();
                    IsDetails = false;
                    ReportOption = "Account Group/Ledger";
                }
                else if (ReportOption.GetUpper() is "Account Group/Sub Group")
                {
                    if (RGrid.CurrentRow.Cells["IsGroup"].Value.GetInt() is 0)
                    {
                        GroupId = RGrid.CurrentRow.Cells["GTxtLedgerId"].Value.ToString();
                        IsDetails = false;
                        ReportOption = "Account Group/Ledger";
                    }
                    else if (RGrid.CurrentRow.Cells["IsGroup"].Value.GetInt() is 1)
                    {
                        SubGroupId = RGrid.CurrentRow.Cells["GTxtLedgerId"].Value.ToString();
                        IsDetails = false;
                        ReportOption = "Account Group/Sub Group/Ledger";
                    }
                }
                else
                {
                    LedgerId = RGrid.CurrentRow.Cells["GTxtLedgerId"].Value.ToString();
                }
            }
            if (LedgerId.GetLong() == ObjGlobal.StockStockInHandLedgerId || LedgerId.GetLong() == ObjGlobal.StockClosingStockLedgerId)
            {
                var display = new DisplayInventoryReports
                {
                    Text = RptName + @" REPORTS",
                    RptType = "NORMAL",
                    RptName = "STOCK VALUATION",
                    RptDate = RptDate,
                    RptMode = "PRODUCT WISE",
                    IsSummary = true,
                    FromBsDate = FromBsDate,
                    ToBsDate = ToBsDate,
                    FromAdDate = FromAdDate,
                    ToAdDate = ToAdDate,
                    IsShortName = false,
                    SortOn = SortBy,
                    BranchId = BranchId,
                    FiscalYearId = FiscalYearId,
                    CompanyUnitId = CompanyUnitId,
                    IsDate = IsDate,
                    RePostValue = IsRePostValue
                };
                display.ShowDialog();
            }
            else if (LedgerId.GetLong() == ObjGlobal.StockOpeningStockLedgerId)
            {
                var display = new DisplayInventoryReports
                {
                    Text = RptName + @" REPORTS",
                    RptType = "NORMAL",
                    RptName = "PRODUCT OPENING",
                    RptDate = RptDate,
                    RptMode = "PRODUCT WISE",
                    IsSummary = true,
                    FromBsDate = FromBsDate,
                    ToBsDate = ToBsDate,
                    FromAdDate = FromAdDate,
                    ToAdDate = ToAdDate,
                    IsShortName = false,
                    SortOn = SortBy,
                    BranchId = BranchId,
                    FiscalYearId = FiscalYearId,
                    CompanyUnitId = CompanyUnitId,
                    IsDate = IsDate,
                    RePostValue = IsRePostValue
                };
                display.ShowDialog();
            }
            else
            {
                var getDisplay = new DisplayFinanceReports
                {
                    Text = RptName + @" REPORT",
                    RptType = "NORMAL",
                    RptName = LedgerId.GetLong() == ObjGlobal.FinanceProfitLossLedgerId && RptName.Equals("BALANCE SHEET") ? "PROFIT & LOSS" : "LEDGER",
                    RptDate = RptDate,
                    FromAdDate = FromAdDate,
                    ToAdDate = ToAdDate,
                    IsDetails = true,
                    CurrencyId = CurrencyId,
                    IsSubLedger = IsSubLedger,
                    IsPostingDetails = IsPostingDetails,
                    IsProductDetails = IsProductDetails,
                    IncludeUdf = IncludeUdf,
                    IsDnCnDetails = IsDnCnDetails,
                    IncludeNarration = IncludeNarration,
                    IncludeRemarks = IncludeRemarks,
                    IsDate = IsDate,
                    LedgerId = LedgerId,
                    SubLedgerId = SubLedgerId,
                    BranchId = BranchId,
                    AgentId = AgentId,
                    ReportOption = "Ledger",
                    IncludeRefVno = IncludeRefVno,
                    IncludePdc = IncludePdc,
                    IsClosingStock = IsClosingStock,
                    IsRePostValue = IsRePostValue,
                    CurrencyType = CurrencyType,
                    AccountType = AccountType
                };
                getDisplay.Show();
            }
        }
        else if (!LedgerId.IsBlankOrEmpty())
        {
            var getDisplay = new DisplayFinanceReports
            {
                Text = RptName + @" REPORT",
                RptType = RptType,
                RptName = RptName,
                RptDate = RptDate,
                FromAdDate = FromAdDate,
                ToAdDate = ToAdDate,
                IsDetails = true,
                CurrencyId = CurrencyId,
                IsSubLedger = IsSubLedger,
                IsPostingDetails = IsPostingDetails,
                IsProductDetails = IsProductDetails,
                IncludeUdf = IncludeUdf,
                IsDnCnDetails = IsDnCnDetails,
                IncludeNarration = IncludeNarration,
                IncludeRemarks = IncludeRemarks,
                IsDate = IsDate,
                LedgerId = LedgerId,
                SubLedgerId = SubLedgerId,
                BranchId = BranchId,
                AgentId = AgentId,
                ReportOption = ReportOption,
                IncludeRefVno = IncludeRefVno,
                IncludePdc = IncludePdc,
                CurrencyType = CurrencyType,
                AccountType = AccountType
            };
            getDisplay.Show();
            getDisplay.BringToFront();
            getDisplay.Activate();
        }
    }

    private void ZoomToEntryLevel()
    {
        if (_voucherNo.IsBlankOrEmpty()) return;
        {
            switch (Module.ToUpper())
            {
                case "JV":
                {
                    var frm = new FrmJournalVoucherEntry(true, _voucherNo)
                    {
                        MaximizeBox = false,
                        Owner = this
                    };
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        tsBtnRefreshReport.PerformClick();
                    }

                    break;
                }
                case "CB":
                {
                    if (RGrid.CurrentRow != null)
                    {
                        var rowIndex = RGrid.CurrentRow.Index;
                        var frm = new FrmCashBankEntry(true, _voucherNo)
                        {
                            MaximizeBox = false,
                            Owner = this
                        };
                        frm.ShowDialog();
                        if (frm.DialogResult != DialogResult.OK) return;
                        tsBtnRefreshReport.PerformClick();
                        try
                        {
                            RGrid.CurrentCell = RGrid.Rows[rowIndex].Cells["GTxtVoucherNo"];
                        }
                        catch
                        {
                            RGrid.CurrentCell = RGrid.Rows[rowIndex - 1].Cells["GTxtVoucherNo"];
                        };
                    }

                    break;
                }
                case "DN":
                case "CN":
                {
                    switch (Module.ToUpper())
                    {
                        case "DN":
                        {
                            var frm = new FrmVoucherNotesEntry(true, _voucherNo)
                            {
                                MaximizeBox = false,
                                Owner = this
                            };
                            frm.ShowDialog();
                            if (frm.DialogResult != DialogResult.OK) return;
                            tsBtnRefreshReport.PerformClick();
                            try
                            {
                                RGrid.CurrentCell = RGrid.Rows[_rowIndex].Cells["GTxtVoucherNo"];
                            }
                            catch
                            {
                                RGrid.CurrentCell = RGrid.Rows[_rowIndex - 1].Cells["GTxtVoucherNo"];
                            };
                            break;
                        }
                        case "CN":
                        {
                            var frm = new FrmVoucherNotesEntry(true, _voucherNo)
                            {
                                MaximizeBox = false,
                                Owner = this
                            };
                            frm.ShowDialog();
                            if (frm.DialogResult != DialogResult.OK) return;
                            tsBtnRefreshReport.PerformClick();
                            try
                            {
                                RGrid.CurrentCell = RGrid.Rows[_rowIndex].Cells["GTxtVoucherNo"];
                            }
                            catch
                            {
                                RGrid.CurrentCell = RGrid.Rows[_rowIndex - 1].Cells["GTxtVoucherNo"];
                            };
                            break;
                        }
                    }

                    break;
                }
                case "PB":
                {
                    var frm = new FrmPurchaseInvoiceEntry(true, _voucherNo)
                    {
                        MaximizeBox = false,
                        Owner = this
                    };
                    frm.ShowDialog();
                    if (frm.DialogResult != DialogResult.OK) return;
                    tsBtnRefreshReport.PerformClick();
                    try
                    {
                        RGrid.CurrentCell = RGrid.Rows[_rowIndex].Cells["GTxtVoucherNo"];
                    }
                    catch
                    {
                        RGrid.CurrentCell = RGrid.Rows[_rowIndex - 1].Cells["GTxtVoucherNo"];
                    };
                    break;
                }
                case "PAB":
                {
                    //new FrmPurchaseAdditionalInvoice(true, voucherNo, false)
                    //{
                    //    MaximizeBox = false,
                    //    Owner = this
                    //}.ShowDialog();
                    break;
                }
                case "PR":
                {
                    var frm = new FrmPurchaseReturnEntry(true, _voucherNo)
                    {
                        MaximizeBox = false,
                        Owner = this
                    };
                    frm.ShowDialog();
                    if (frm.DialogResult != DialogResult.OK) return;
                    tsBtnRefreshReport.PerformClick();
                    try
                    {
                        RGrid.CurrentCell = RGrid.Rows[_rowIndex].Cells["GTxtVoucherNo"];
                    }
                    catch
                    {
                        RGrid.CurrentCell = RGrid.Rows[_rowIndex - 1].Cells["GTxtVoucherNo"];
                    };
                    break;
                }
                case "PEB":
                {
                    //new FrmPurchaseExpBrk(true, voucherNo)
                    //{
                    //    MaximizeBox = false,
                    //    Owner = this
                    //}.ShowDialog();
                    break;
                }
                case "SB":
                {
                    _query =
                        $"Select * from AMS.SB_Master where SB_Invoice = '{_voucherNo}' and CBranch_Id={ObjGlobal.SysBranchId} ";
                    _dtTemp = _query.GetQueryDataTable();
                    if (_dtTemp.Rows.Count <= 0) return;
                    var invoiceMode = _dtTemp.Rows[0]["Invoice_Mode"].ToString().ToUpper();
                    switch (invoiceMode)
                    {
                        case "POS":
                        case "RSB":
                        case "ATI":
                        case "HSB":
                        {
                            this.NotifyWarning(@"BILL IS ONLINE YOU DON'T HAVE RIGHT TO MODIFY..!!");
                            break;
                        }
                        default:
                        {
                            var frm = new FrmSalesInvoiceEntry(true, _voucherNo)
                            {
                                MinimizeBox = false,
                                Owner = this
                            };
                            frm.ShowDialog();
                            if (frm.DialogResult == DialogResult.OK)
                            {
                                tsBtnRefreshReport.PerformClick();
                            }

                            break;
                        }
                    }

                    break;
                }
                case "SBT":
                {
                    //new FrmSalesTours(true, voucherNo)
                    //{
                    //    MinimizeBox = false,
                    //    Owner = this
                    //}.ShowDialog();
                    break;
                }
                case "SAB":
                {
                    //new FrmSalesAdditional(true, voucherNo)
                    //{
                    //    MinimizeBox = false,
                    //    Owner = this
                    //}.ShowDialog();

                    break;
                }
                case "SR":
                {
                    new FrmSalesReturnEntry(true, _voucherNo)
                    {
                        MinimizeBox = false,
                        Owner = this
                    }.ShowDialog();
                    break;
                }
                case "PDC":
                {
                    new FrmPDCVoucher(true, _voucherNo)
                    {
                        MinimizeBox = false,
                        Owner = this
                    }.ShowDialog();
                    break;
                }
            }
        }
    }

    #endregion ** --------------- Zooming Function ---------------

    #endregion --------------- FINANCE REPORTS ---------------

    // FUNCTION FOR THIS FORM

    #region --------------- FUNCTION  FOR THIS FOMR ---------------

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

    private void FitGridColumn()
    {
        try
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var column = RGrid.Columns.Cast<DataGridViewColumn>().ToList();
            column.ForEach(f => f.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells);
            SplashScreenManager.CloseForm(true);
        }
        catch
        {
            SplashScreenManager.CloseForm(true);
        }
    }

    private void ExportReport()
    {
        saveFileDialog1.Title = @"Save";
        saveFileDialog1.Filter = @"Excel File 1997-07 |*.Xls|Excel File 2010-13 |*.xlsx|Word |*.Doc|Html Page |*.Html";
        saveFileDialog1.ShowDialog();
        var fileName = saveFileDialog1.FileName;
        var reports = new ClsExportReports
        {
            FileName = fileName,
            CompanyName = lbl_ComanyName.Text,
            AccountingPeriod = lbl_AccountingPeriod.Text,
            AccPeriodDate = LblAccPeriodDate.Text,
            CompanyAddress = LblCompanyAddress.Text,
            CompanyPanVatNo = LblCompanyPANVATNo.Text,
            ReportName = LblReportName.Text,
            ReportDate = LblReportDate.Text
        };
        reports.ExportReport(RGrid);
        this.NotifySuccess($"{RptName} EXPORT SUCCESSFULLY..!!");
        if (CustomMessageBox.Question(@"DO YOU WANT TO OPEN THE EXPORT FILE..??") is DialogResult.Yes)
        {
            Process.Start(fileName);
        }
    }

    private void PrintDocument1_BeginPrint(object sender, System.ComponentModel.CancelEventArgs e)
    {
        _control.PrintDocument1_BeginPrint(sender, e);
    }

    private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
    {
        _control.PrintDocument1_PrintPage(sender, e);
    }

    #endregion --------------- FUNCTION  FOR THIS FOMR ---------------

    // BUTTON CLICK EVENT OF THIS FORM

    #region --------------- BUTTON CLICK EVENT ---------------

    private void TsBtnPrint_Click(object sender, EventArgs e)
    {
        CustomPrintFunction();
    }

    private void TsBtnExport_Click(object sender, EventArgs e)
    {
        ExportReport();
    }

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
        GenerateFinanceReports();
    }

    private void TsBtnFitColumnGrid_Click(object sender, EventArgs e)
    {
        FitGridColumn();
    }

    #endregion --------------- BUTTON CLICK EVENT ---------------

    // GRID METHODS FOR THIS FORM TO BIND REPORTS

    #region --------------- GRID METHODS ---------------

    private void RGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        RGrid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
    }

    private void RGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.Value != null)
        {
            RGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = FloralWhite;
        }
    }

    private void RGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void RGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
    {
    }

    private void RGrid_Leave(object sender, EventArgs e)
    {
    }

    #endregion --------------- GRID METHODS ---------------

    // REQUIRED OBJECT ASSIGN TO VALUE

    #region --------------- OBJECT ASSIGN ---------------

    private void InitializeObjectValue()
    {
        try
        {
            if (RptName == null || RptName.IsBlankOrEmpty())
            {
                return;
            }
            _report.GetReports.ReportType = RptType;
            _report.GetReports.ReportDesc = RptName;
            _report.GetReports.ReportDate = RptDate;
            _report.GetReports.FromDate = FromAdDate;
            _report.GetReports.ToDate = ToAdDate;
            _report.GetReports.FilterFor = ReportOption;
            _report.GetReports.IsGroupWise = IsGroupWise;
            _report.GetReports.IsDetails = IsDetails;
            _report.GetReports.Currency = CurrencyId;
            _report.GetReports.IsTFormat = IsTFormat;
            _report.GetReports.IsSubLedger = IsSubLedger;
            _report.GetReports.IsClosingStock = IsClosingStock;
            _report.GetReports.IsRePostValue = IsRePostValue;
            _report.GetReports.IsDate = IsDate;
            _report.GetReports.Source = Source;
            _report.GetReports.BranchId = BranchId;
            _report.GetReports.CompanyUnitId = CompanyUnitId;
            _report.GetReports.FiscalYearId = FiscalYearId;
            _report.GetReports.IsProfitLoss = IsProfitLoss;
            _report.GetReports.IsBalanceSheet = IsBalanceSheet;
            _report.GetReports.IsTrialBalance = IsTrialBalance;
            _report.GetReports.IsPostingDetails = IsPostingDetails;
            _report.GetReports.IsProductDetails = IsProductDetails;
            _report.GetReports.IncludeUdf = IncludeUdf;
            _report.GetReports.IsIncludePdc = IncludePdc;
            _report.GetReports.IsDnCnDetails = IsDnCnDetails;
            _report.GetReports.IsNarration = IncludeNarration;
            _report.GetReports.IsRemarks = IncludeRemarks;
            _report.GetReports.AccountType = AccountType;
            _report.GetReports.AccountGroupId = GroupId;
            _report.GetReports.AccountSubGroupId = SubGroupId;
            _report.GetReports.LedgerId = LedgerId;
            _report.GetReports.SubLedgerId = SubLedgerId;
            _report.GetReports.IsZeroBalance = IsZeroBalance;
            _report.GetReports.IsCombineSales = IsCombineSales;
            _report.GetReports.IsCombineCustomerVendor = IsCombineCustomerVendor;
            _report.GetReports.IncludeVoucherTotal = IncludeVoucherTotal;
            _report.GetReports.IncludeLedger = IncludeLedger;
            _report.GetReports.IncludeShortName = IncludeShortName;
            _report.GetReports.CurrencyType = CurrencyType;
            _report.GetReports.SortOn = SortBy;
            _report.GetReports.IsLedgerContactDetails = IsLedgerContactDetails;
            _report.GetReports.IsLedgerScheme = IsLedgerScheme;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            ex.DialogResult();
            // ignored
        }
    }

    #endregion --------------- OBJECT ASSIGN ---------------

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT ---------------

    public int PageWidth;
    private int _j = 0;
    private int _k = 0;
    private int _m = 0;
    private int _n = 0;
    private int _iTotalWidth; //

    private int _count = 0;
    private int _iHeaderHeight;
    private int _leftMargin = 0;
    private int _rightMargin = 0;
    private int _topMargin = 0;
    private int _tmpWidth = 0;
    private int _rowIndex;
    private int _iRow;
    public int PageNo;

    public int GroupBy = 0;

    public short Slab = 0;
    public short Columns = 0;

    private bool _bFirstPage;
    private bool _bNewPage;
    private bool _bMorePagesToPrint = false;
    private bool _result = false;

    public bool IsDetails;
    public bool IsCombineSales = false;
    public bool IsCombineCustomerVendor = false;
    public bool IsAgentOnly = false;
    public bool IsDate;
    public bool IsGroupWise = false;
    public bool IsTFormat = false;
    public bool IsSubLedger;
    public bool IsPostingDetails;
    public bool IsClosingStock;
    public bool IsRePostValue;
    public bool IsOpeningOnly = false;
    public bool IsZeroBalance = false;
    public bool IsProfitLoss = false;
    public bool IsBalanceSheet = false;
    public bool IsTrialBalance = false;
    public bool IsMultiCurrency = false;
    public bool PartyLedger;
    public bool IsProductDetails;
    public bool IncludeUdf;
    public bool IsDnCnDetails;
    public bool IncludeNarration;
    public bool IncludeAdjustment;
    public bool IncludeRemarks;
    public bool IncludeRefVno;
    public bool IncludePdc;
    public bool IsFirstTime = true;
    public bool IncludeVoucherTotal = false;
    public bool IncludeLedger = false;
    public bool IncludeShortName = false;
    public bool IsLedgerContactDetails = false;
    public bool IsLedgerScheme = false;

    public string ReportOption = string.Empty;
    public string Source = string.Empty;
    public string SortBy = string.Empty;
    public string RptType = string.Empty;
    public string RptName = string.Empty;
    public string RptDate = string.Empty;
    public string FromAdDate = string.Empty;
    public string FromBsDate = string.Empty;
    public string ToAdDate = string.Empty;
    public string ToBsDate = string.Empty;
    public string CurrencyId = string.Empty;
    public string AccountType = string.Empty;
    public string LedgerId = string.Empty;
    public string SubLedgerId = string.Empty;
    public string GroupId = string.Empty;
    public string SubGroupId = string.Empty;
    public string Module = string.Empty;
    public string BranchId = string.Empty;
    public string AgentId = string.Empty;
    public string AreaId = string.Empty;
    public string CurrencyType = string.Empty;
    public string CompanyUnitId = string.Empty;
    public string FiscalYearId = string.Empty;
    private string _query = string.Empty;
    private string _voucherNo;

    public DateTime FromDate = DateTime.Now;
    public DateTime ToDate = DateTime.Now;
    private DataTable _dtTemp;

    private readonly IFinanceReport _report;
    private readonly IFinanceDesign _design;
    private readonly ClsPrintControl _control = new();

    [DllImport("kernel32.dll")]
    public static extern bool Beep(int beepFreq, int beepDuration);

    #endregion --------------- OBJECT ---------------
}