using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Reports.Finance;
using MrDAL.Reports.Interface;
using System.Drawing;
using System.Windows.Forms;

namespace MrDAL.Reports.Design;

public class FinanceReportDesign : IFinanceDesign
{
    // OBJECT FOR THIS FORM
    private readonly IFinanceReport _objFinance = new ClsFinanceReport();
    // FINANCE REPORTS FORMAT

    #region --------------- FINANCE REPORT DESIGN ---------------

    public DataGridView GetCashBankJournalVoucherDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0) rGrid.Columns.Clear();
        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtDate", "DATE", "Voucher_Date", 120, 100, true);
        rGrid.AddColumn("GTxtMiti", "MITI", "Voucher_Miti", 120, 100, true);
        rGrid.AddColumn("GTxtVoucherNo", "VOUCHER_NO", "Voucher_No", 130, 100, true,
            DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader);
        rGrid.AddColumn("GTxtLedger", "DESCRIPTION", "PartyLedger",
            _objFinance.GetReports.CurrencyType.Equals("Foreign") ? 325
            : _objFinance.GetReports.CurrencyType.Equals("Both") ? 280 : 395, 100, true,
            DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtCurr", "CUR", "CCode", 130, 100, false);
        rGrid.AddColumn("GTxtExchangeRate", "CUR_RATE", "Currency_Rate", 130, 100, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtDebitAmount", "DEBIT (Dr)", "Receipt", 130, 100, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtCreditAmount", "CREDIT (Cr)", "Payment", 130, 100, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalance", "BALANCE", "Balance", 130, 100, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalanceType", string.Empty, "BalanceType", 40, 20, true,
            DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtModule", "Module", "Module", 0, 2, false);
        rGrid.AddColumn("IsGroup", "IsGroup", "IsGroup", 0, 2, false);
        return rGrid;
    }

    public DataGridView GetCashBankJournalVoucherTFormatDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0) rGrid.Columns.Clear();
        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtDate1", "DATE", "dt_Date", 120, 100, true);
        rGrid.AddColumn("GTxtMiti1", "MITI", "dt_Miti", 120, 100, true);
        rGrid.AddColumn("GTxtVoucherNo1", "VOUCHER_NO", "dt_VoucherNo", 130, 100, true);
        rGrid.AddColumn("GTxtLedger1", "DESCRIPTION", "dt_Desc", 250, 100, true,
            DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtDebitAmount", "DEBIT (Dr)", "dt_Receipt", 130, 100, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtDate2", "DATE", "dt_Date", 120, 100, true);
        rGrid.AddColumn("GTxtMiti2", "MITI", "dt_Miti", 120, 100, true);
        rGrid.AddColumn("GTxtVoucherNo2", "VOUCHER_NO", "dt_VoucherNo", 130, 100, true);
        rGrid.AddColumn("GTxtLedger2", "DESCRIPTION", "dt_Desc", 250, 100, true,
            DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtCreditAmount", "CREDIT (Cr)", "dt_Payment", 130, 100, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtModule", "Module", "dt_Module", 0, 2, false);
        rGrid.AddColumn("IsGroup", "IsGroup", "IsGroup", 0, 2, false);
        return rGrid;
    }

    public DataGridView GetCashBankSummaryJournalVoucherDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0) rGrid.Columns.Clear();
        rGrid.AutoGenerateColumns = false;
        DataGridViewColumn column;

        column = new DataGridViewTextBoxColumn
        {
            HeaderText = "DATE",
            Name = "GTxtDate",
            Width = 100,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft }
        };
        column.HeaderCell.Style.Font = new Font("Bookman Old Style", 11, FontStyle.Regular);
        rGrid.Columns.Add(column);

        column = new DataGridViewTextBoxColumn
        {
            HeaderText = "MITI",
            Name = "GTxtMiti",
            Width = 100,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft }
        };
        column.HeaderCell.Style.Font = new Font("Bookman Old Style", 11, FontStyle.Regular);
        rGrid.Columns.Add(column);

        column = new DataGridViewTextBoxColumn
        {
            HeaderText = "OPENING",
            Name = "GTxtOpening",
            Width = 130,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
        };
        column.HeaderCell.Style.Font = new Font("Bookman Old Style", 11, FontStyle.Regular);
        rGrid.Columns.Add(column);

        column = new DataGridViewTextBoxColumn
        {
            HeaderText = "RECEIPT",
            Name = "GTxtReceipt",
            Width = 130,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
        };
        column.HeaderCell.Style.Font = new Font("Bookman Old Style", 11, FontStyle.Regular);
        rGrid.Columns.Add(column);

        column = new DataGridViewTextBoxColumn
        {
            HeaderText = "PAYMENT",
            Name = "GTxtPayment",
            Width = 130,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
        };
        column.HeaderCell.Style.Font = new Font("Bookman Old Style", 11, FontStyle.Regular);
        rGrid.Columns.Add(column);

        column = new DataGridViewTextBoxColumn
        {
            HeaderText = "BALANCE",
            Name = "GTxtBalance",
            Width = 130,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
        };
        column.HeaderCell.Style.Font = new Font("Bookman Old Style", 11, FontStyle.Regular);
        rGrid.Columns.Add(column);

        return rGrid;
    }

    public DataGridView GetCashBankSummaryJournalVoucherTFormatDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        rGrid.Rows.Clear();
        rGrid.Columns.Clear();
        DataGridViewColumn column;

        column = new DataGridViewTextBoxColumn
        {
            HeaderText = "DATE",
            Name = "GTxtDate",
            Width = 100,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft }
        };
        column.HeaderCell.Style.Font = new Font("Bookman Old Style", 11, FontStyle.Regular);
        rGrid.Columns.Add(column);

        column = new DataGridViewTextBoxColumn
        {
            HeaderText = "MITI",
            Name = "GTxtMiti",
            Width = 100,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft }
        };
        column.HeaderCell.Style.Font = new Font("Bookman Old Style", 11, FontStyle.Regular);
        rGrid.Columns.Add(column);

        column = new DataGridViewTextBoxColumn
        {
            HeaderText = "OPENING",
            Name = "GTxtOpening",
            Width = 130,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
        };
        column.HeaderCell.Style.Font = new Font("Bookman Old Style", 11, FontStyle.Regular);
        rGrid.Columns.Add(column);

        column = new DataGridViewTextBoxColumn
        {
            HeaderText = "RECEIPT",
            Name = "GTxtReceipt",
            Width = 130,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
        };
        column.HeaderCell.Style.Font = new Font("Bookman Old Style", 11, FontStyle.Regular);
        rGrid.Columns.Add(column);

        column = new DataGridViewTextBoxColumn
        {
            HeaderText = "PAYMENT",
            Name = "GTxtPayment",
            Width = 130,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
        };
        column.HeaderCell.Style.Font = new Font("Bookman Old Style", 11, FontStyle.Regular);
        rGrid.Columns.Add(column);

        column = new DataGridViewTextBoxColumn
        {
            HeaderText = "BALANCE",
            Name = "GTxtBalance",
            Width = 130,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
        };
        column.HeaderCell.Style.Font = new Font("Bookman Old Style", 11, FontStyle.Regular);
        rGrid.Columns.Add(column);

        return rGrid;
    }

    #endregion --------------- FINANCE REPORT DESIGN ---------------

    // TRIAL BALANCE SHEET FORMAT

    #region --------------- TRIAL BALANCE ---------------

    public DataGridView GetTrialBalanceNormalDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0) rGrid.Columns.Clear();
        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtLedgerId", "LEDGER_ID", "LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtShortName", @"SHORTNAME", "ShortName", 110, 110, true,
            DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.AddColumn("GTxtLedger", @"PARTICULARS", "LedgerName", 430, 400, true,
            DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtDebit", @"DEBIT", "DebitAmount", 160, 160, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtCredit", @"CREDIT", "CreditAmount", 160, 160, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("IsGroup", @"IsGroup", "IsGroup", 0, 2, false);
        return rGrid;
    }

    public DataGridView GetTrialBalanceNormalTFormatDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0) rGrid.Columns.Clear();
        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtLedgerId", "GTxtLedgerId", "dt_LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtShortName", @"SHORTNAME", "dt_ShortName", 110, 110, true);
        rGrid.AddColumn("GTxtLedger", @"PARTICULARS", "dt_Desc", 430, 400, true,
            DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtDebit", @"DEBIT", "dt_Debit", 160, 160, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtCredit", @"CREDIT", "dt_Credit", 160, 160, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("IsGroup", @"IsGroup", "IsGroup", 0, 2, false);
        return rGrid;
    }

    public DataGridView GetTrialBalancePeriodicDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0) rGrid.Columns.Clear();
        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtLedgerId", @"LedgerId", "dt_LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtShortName", @"SHORTNAME", "dt_ShortName", 150, 110, false);
        rGrid.AddColumn("GTxtLedger", @"PARTICULARS", "dt_Ledger", 430, 350, true,
            DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtOpeningDebit", @"OB_DEBIT", "dt_OpeningDebit", 120, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtOpeningCredit", @"OB_CREDIT", "dt_OpeningCredit", 120, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtDebit", @"P_DEBIT", "dt_PeriodicDebit", 120, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtCredit", @"P_CREDIT", "dt_PeriodicCredit", 120, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalance", @"BALANCE", "dt_Balance", 120, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalanceType", @" ", "dt_BalanceType", 45, 2, true,
            DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtClosingDebit", @"CB_DEBIT", "dt_ClosingDebit", 120, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtClosingCredit", @"CB_CREDIT", "dt_ClosingCredit", 120, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtPanNo", @"PAN_NO", "dt_PanNo", 120, 90, false);
        rGrid.AddColumn("GTxtPhoneNo", @"PHONE_NO", "dt_PhoneNo", 120, 90, false);
        rGrid.AddColumn("GTxtPanNo", @"PAN_NO", "dt_PanNo", 120, 90, false);
        rGrid.AddColumn("IsGroup", @"IsGroup", "IsGroup", 0, 2, false);
        return rGrid;
    }

    public DataGridView GetTrialBalancePeriodicTFormatDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0) rGrid.Columns.Clear();
        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtLedgerId", @"LedgerId", "dt_LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtShortName", @"SHORTNAME", "dt_ShortName", 150, 110, false);
        rGrid.AddColumn("GTxtLedger", @"PARTICULARS", "dt_Desc", 430, 350, true,
            DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtCurrency", @"CUR", "dt_Currency", 150, 110, false);
        rGrid.AddColumn("GTxtExchangeRate", @"CUR_RATE", "dt_CurrencyRate", 150, 110, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtOpeningDebit", @"OB_DEBIT", "OB_DEBIT", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtLocalOpeningDebit", @"LOCAL_DEBIT", "OB_LOCAL_DEBIT", 150, 110, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtOpeningCredit", @"OB_CREDIT", "OB_CREDIT", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtLocalOpeningCredit", @"LOCAL_CREDIT", "OB_LOCAL_CREDIT", 150, 110, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtDebit", @"P_DEBIT", "DEBIT", 150, 110, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtLocalDebit", @"LOCAL_DEBIT", "LOCAL_DEBIT", 150, 110, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtCredit", @"P_CREDIT", "CREDIT", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtLocalCredit", @"LOCAL_CREDIT", "LOCAL_CREDIT", 150, 110, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalance", @"BALANCE", "BALANCE", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtLocalBalance", @"LOCAL_BALANCE", "LOCAL_BALANCE", 150, 110, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalanceType", @" ", "dt_Type", 150, 110, true,
            DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtClosingDebit", @"CB_DEBIT", "CB_DEBIT", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtLocalClosingDebit", @"LOCAL_DEBIT", "CB_LOCAL_DEBIT", 150, 110, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtClosingCredit", @"CB_CREDIT", "CB_CREDIT", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtLocalClosingCredit", @"LOCAL_CREDIT", "CB_LOCAL_CREDIT", 150, 110, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtPanNo", @"PAN_NO", "dt_PanNo", 150, 110, false);
        rGrid.AddColumn("GTxtPhoneNo", @"PHONE_NO", "dt_PhoneNo", 150, 110, false);
        rGrid.AddColumn("GTxtPanNo", @"PAN_NO", "dt_PanNo", 150, 110, false);
        rGrid.AddColumn("IsGroup", @"IsGroup", "IsGroup", 0, 2, false);
        return rGrid;
    }

    #endregion --------------- TRIAL BALANCE ---------------

    // PROFIT AND LOSS REPORTS FORMAT

    #region --------------- PROFIT & LOSS AND BALANCE SHEET ---------------

    public DataGridView GetProfitLossBalanceSheetPeriodicDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0) rGrid.Columns.Clear();
        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtLedgerId", "GTxtLedgerId", "dt_LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtShortName", @"SHORTNAME", "dt_ShortName", 110, 110, true);
        rGrid.AddColumn("GTxtLedger", @"PARTICULARS", "dt_Desc", 430, 400, true,
            DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtOpening", @"OPENING", "dt_Opening", 150, 150, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtDebit", @"P_DEBIT", "dt_PeriodicDebit", 150, 150, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtCredit", @"P_CREDIT", "dt_PeriodicCredit", 150, 150, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalance", @"CLOSING", "dt_Balance", 150, 150, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("IsGroup", @"IsGroup", "IsGroup", 0, 2, false);
        rGrid.AddColumn("dt_Type", string.Empty, "dt_Type", 35, 2, true);
        return rGrid;
    }

    public DataGridView GetProfitLossBalanceSheetPeriodicTFormatDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0) rGrid.Columns.Clear();
        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtLedgerId", "GTxtLedgerId", "dt_LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtShortName", @"SHORTNAME", "dt_ShortName", 110, 110, true);
        rGrid.AddColumn("GTxtLedger", @"PARTICULARS", "dt_Desc", 430, 400, true, DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtOpeningDebit", @"OB_DEBIT", "dt_OpeningDebit", 150, 150, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtOpeningCredit", @"OB_DEBIT", "dt_OpeningCredit", 150, 150, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtDebit", @"P_DEBIT", "dt_ClosingDebit", 150, 150, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtCredit", @"P_CREDIT", "dt_ClosingCredit", 150, 150, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalanceDebit", @"CB_DEBIT", "dt_BalanceDebit", 150, 150, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalanceCredit", @"CB_CREDIT", "dt_BalanceCredit", 150, 150, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("IsGroup", @"IsGroup", "IsGroup", 0, 2, false);
        rGrid.AddColumn("dt_GroupBy", @"dt_GroupBy", "dt_GroupBy", 0, 2, false);
        return rGrid;
    }

    public DataGridView GetProfitLossBalanceSheetNormalDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0) rGrid.Columns.Clear();
        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtLedgerId", "GTxtLedgerId", "LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtShortName", @"SHORTNAME", "ShortName", 110, 110, true);
        rGrid.AddColumn("GTxtLedger", @"PARTICULARS", "LedgerName", 430, 400, true,
            DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtAmount", @"AMOUNT", "Balance", 150, 150, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("IsGroup", @"IsGroup", "IsGroup", 0, 2, false);
        rGrid.AddColumn("dt_Type", string.Empty, "BalanceType", 35, 2, true);
        return rGrid;
    }

    public DataGridView GetProfitLossBalanceSheetNormalTFormatDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0) rGrid.Columns.Clear();
        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtLedgerId", "GTxtLedgerId", "dt_LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtShortName", @"SHORTNAME", "dt_ShortName", 110, 110, true);
        rGrid.AddColumn("GTxtLedger", @"PARTICULARS", "dt_Desc", 430, 400, true, DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtAmount", @"AMOUNT", "dt_Amount", 150, 150, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtAmount", @"AMOUNT", "dt_Amount", 150, 150, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("IsGroup", @"IsGroup", "IsGroup", 0, 2, false);
        rGrid.AddColumn("dt_Type", @"dt_Type", "dt_Type", 0, 2, false);
        return rGrid;
    }

    #endregion --------------- PROFIT & LOSS AND BALANCE SHEET ---------------

    // LEDGER REPORT FORMAT

    #region --------------- LEDGER REPORT ---------------

    public DataGridView GetGeneralLedgerNormalDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0) rGrid.Columns.Clear();
        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtDate", @"DATE", "VoucherDate", 110, 110, true);
        rGrid.AddColumn("GTxtMiti", @"MITI", "VoucherMiti", 110, 110, true);
        rGrid.AddColumn("GTxtLedgerId", @"LedgerId", "LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtLedger", @"PARTICULARS", "PartyLedger", 430, 350, true,
            DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtCurrency", @"CUR", "CCode", 150, 110, false);
        rGrid.AddColumn("GTxtExchangeRate", @"CUR_RATE", "CurrencyRate", 150, 110, false,
            DataGridViewContentAlignment.MiddleRight);

        rGrid.AddColumn("GTxtActualDebit", @"DEBIT (Dr)", "ActualDebitAmount", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtDebit", @"DEBIT (Dr)", "DebitAmount", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);

        rGrid.AddColumn("GTxtActualCredit", @"CREDIT (Cr)", "ActualCreditAmount", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtCredit", @"CREDIT (Cr)", "CreditAmount", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);

        rGrid.AddColumn("GTxtBalance", @"BALANCE", "Balance", 150, 110, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtLocalBalance", @"BALANCE (NPR)", "Balance", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtModule", @"Module", "Module", 150, 110, false);
        rGrid.AddColumn("GTxtVoucherNo", @"VOUCHER_NO", "VoucherNo", 150, 110, false);
        rGrid.AddColumn("GTxtAmountType", string.Empty, "BalanceType", 30, 30, true);
        rGrid.AddColumn("IsGroup", @"IsGroup", "IsGroup", 0, 2, false);
        rGrid.AddColumn("dt_Type", @"dt_Type", "dt_Type", 0, 2, false);
        return rGrid;
    }

    public DataGridView GetGeneralLedgerNormalTFormatDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0) rGrid.Columns.Clear();
        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtDate", @"DATE", "dt_Date", 110, 110, true);
        rGrid.AddColumn("GTxtMiti", @"MITI", "dt_Miti", 110, 110, true);
        rGrid.AddColumn("GTxtLedgerId", @"LedgerId", "dt_LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtLedger", @"PARTICULARS", "dt_Desc", 430, 350, true, DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtCurrency", @"CUR", "dt_Currency", 150, 110, false);
        rGrid.AddColumn("GTxtExchangeRate", @"CUR_RATE", "dt_CurrencyRate", 150, 110, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtDebit", @"DEBIT (Dr)", "dt_Debit", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtCredit", @"CREDIT (Cr)", "dt_Credit", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtLocalDebit", @"LOCAL_DEBIT (Dr)", "dt_LocalDebit", 150, 110, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtLocalCredit", @"LOCAL_CREDIT (Cr)", "dt_LocalCredit", 150, 110, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalance", @"BALANCE", "dt_Balance", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtModule", @"Module", "dt_Module", 150, 110, false);
        rGrid.AddColumn("GTxtVoucherNo", @"VOUCHER_NO", "dt_VoucherNo", 150, 110, false);
        rGrid.AddColumn("GTxtAmountType", string.Empty, "dt_AmountType", 30, 30, true);
        rGrid.AddColumn("IsGroup", @"IsGroup", "IsGroup", 0, 2, false);
        rGrid.AddColumn("dt_Type", @"dt_Type", "dt_Type", 0, 2, false);
        return rGrid;
    }

    public DataGridView GetGeneralLedgerSummaryDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0) rGrid.Columns.Clear();
        rGrid.AutoGenerateColumns = false;
        rGrid.ColumnHeadersHeight = 65;
        rGrid.AddColumn("GTxtLedgerId", @"LedgerId", "LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtShortName", @"SHORTNAME", "ShortName", 120, 110, true);
        rGrid.AddColumn("GTxtLedger", @"PARTICULARS", "LedgerDesc", 350, 275, true,
            DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtOpeningDebit", @"OPENING DEBIT", "OpeningDebit", 150, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtOpeningCredit", @"OPENING CREDIT", "OpeningCredit", 150, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtDebit", @"DEBIT", "LocalDebit", 150, 90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtCredit", @"CREDIT", "LocalCredit", 150, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalance", @"BALANCE", "Balance", 150, 90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalanceType", @" ", "BalanceType", 35, 2, true, DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtClosingDebit", @"CLOSING DEBIT", "ClosingDebit", 150, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtClosingCredit", @"CLOSING CREDIT", "ClosingCredit", 150, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtPanNo", @"PAN NO", "PanNo", 110, 90, false);
        rGrid.AddColumn("GTxtPhoneNo", @"PHONE NO", "PhoneNo", 110, 90, false);
        rGrid.AddColumn("GTxtAddress", @"ADDRESS", "GLAddress", 110, 90, false);
        rGrid.AddColumn("IsGroup", @"IsGroup", "IsGroup", 0, 2, false);
        rGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        foreach (DataGridViewColumn column in rGrid.Columns)
        {
            if (!column.Visible) continue;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.DefaultCellStyle.Alignment = column.DefaultCellStyle.Alignment switch
            {
                DataGridViewContentAlignment.MiddleRight => DataGridViewContentAlignment.MiddleRight,
                _ => DataGridViewContentAlignment.MiddleLeft
            };
        }

        rGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        return rGrid;
    }

    public DataGridView GetGeneralLedgerSummaryTFormatDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0) rGrid.Columns.Clear();
        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtLedgerId", @"LedgerId", "dt_LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtShortName", @"SHORTNAME", "dt_ShortName", 150, 110, false);
        rGrid.AddColumn("GTxtLedger", @"PARTICULARS", "dt_Desc", 430, 350, true, DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtCurrency", @"CUR", "dt_Currency", 150, 110, false);
        rGrid.AddColumn("GTxtExchangeRate", @"CUR_RATE", "dt_CurrencyRate", 150, 110, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtOpeningDebit", @"OB_DEBIT", "OB_DEBIT", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtOpeningCredit", @"OB_CREDIT", "OB_CREDIT", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtDebit", @"P_DEBIT", "DEBIT", 150, 110, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtCredit", @"P_CREDIT", "CREDIT", 150, 110, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalance", @"BALANCE", "BALANCE", 150, 110, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalanceType", @" ", "dt_Type", 150, 110, true, DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtClosingDebit", @"CB_DEBIT", "CB_DEBIT", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtClosingCredit", @"CB_CREDIT", "CB_CREDIT", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtPanNo", @"PAN_NO", "dt_PanNo", 150, 110, false);
        rGrid.AddColumn("GTxtPhoneNo", @"PHONE_NO", "dt_PhoneNo", 150, 110, false);
        rGrid.AddColumn("IsGroup", @"IsGroup", "IsGroup", 0, 2, false);
        return rGrid;
    }

    public DataGridView GetPartyConfirmationDesign(DataGridView rGrid, bool includeReturn)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0) rGrid.Columns.Clear();
        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtLedgerId", @"LedgerId", "LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtLedger", @"PARTICULARS", "LedgerName", 430, 350, true,
            DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtLedgerPan", @"PAN NO", "LedgerPanNo", 120, 120, true,
            DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtOpening", @"OPENING BALANCE", "OpeningBalance", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtOpeningType", @" ", "OpeningType", 65, 65, true,
            DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtNetAmount", @"NET AMOUNT", "NetAmount", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtTaxExempted", @"TAX EXEMPTED", "TaxExempted", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtTaxableAmount", @"TAXABLE AMOUNT", "TaxableAmount", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtVatAmount", @"VAT AMOUNT", "TaxAmount", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtNetReturnAmount", @"NET RETURN AMOUNT", "NetReturnAmount", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtTaxReturnExempted", @"TAX RETURN EXEMPTED", "TaxReturnExempted", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtTaxableReturnAmount", @"TAXABLE RETURN AMOUNT", "TaxableReturn", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtVatReturnAmount", @"VAT RETURN AMOUNT", "TaxReturnAmount", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);

        rGrid.AddColumn("GTxtClosingBalance", @"CLOSING BALANCE", "ClosingBalance", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtClosingType", @" ", "ClosingType", 65, 65, true,
            DataGridViewContentAlignment.MiddleCenter);

        rGrid.AddColumn("IsGroup", @"IsGroup", "IsGroup", 0, 2, false);

        rGrid.ColumnHeadersHeight = 65;
        foreach (DataGridViewColumn column in rGrid.Columns)
        {
            if (!column.Visible) continue;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.DefaultCellStyle.Alignment = column.DefaultCellStyle.Alignment switch
            {
                DataGridViewContentAlignment.MiddleRight => DataGridViewContentAlignment.MiddleRight,
                _ => DataGridViewContentAlignment.MiddleLeft
            };
        }

        rGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        return rGrid;
    }

    #endregion --------------- LEDGER REPORT ---------------

    // DAY BOOK REPORT FORMAT

    #region --------------- DAY BOOK ---------------

    public DataGridView GetDayBookNormalReportDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0) rGrid.Columns.Clear();
        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtDate", @"DATE", "dt_Date", 130, 90, true);
        rGrid.AddColumn("GTxtMiti", @"MITI", "dt_Miti", 130, 90, true);
        rGrid.AddColumn("GTxtVoucherNo", @"VOUCHER_NO", "dt_VoucherNo", 150, 130, true);
        rGrid.AddColumn("GTxtLedger", @"PARTICULARS", "dt_Desc", 400, 375, true, DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtCurrency", @"CUR", "dt_Currency", 90, 90, false);
        rGrid.AddColumn("GTxtExchangeRate", @"CUR_RATE", "dt_CurrencyRate", 90, 90, false);
        rGrid.AddColumn("GTxtDebit", @"DEBIT (Dr)", "dt_Debit", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtCredit", @"CREDIT (Cr)", "dt_Credit", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtLocalDebit", @"LOCAL_DEBIT (Dr)", "dt_LocalDebit", 150, 110, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtLocalCredit", @"LOCAL_CREDIT (Cr)", "dt_LocalCredit", 150, 110, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalance", @"BALANCE", "dt_Balance", 150, 110, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtModule", @"MODULE", "dt_Module", 0, 2, false);
        rGrid.AddColumn("dtFilterDate", @"FILTER_DATE", "dtFilterDate", 0, 2, false);
        rGrid.AddColumn("IsGroup", @"IS_GROUP", "IsGroup", 0, 2, false);
        return rGrid;
    }

    public DataGridView GetDayBookTFormatReportDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0) rGrid.Columns.Clear();
        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtDate1", @"DATE", "dt_Date", 130, 90, ObjGlobal.SysDateType.Equals("D"));
        rGrid.AddColumn("GTxtMiti1", @"MITI", "dt_Miti", 130, 90, ObjGlobal.SysDateType.Equals("M"));
        rGrid.AddColumn("GTxtVoucherNo1", @"VOUCHER_NO", "dt_VoucherNo1", 150, 130, true);
        rGrid.AddColumn("GTxtLedger1", @"PARTICULARS", "dt_Desc1", 400, 375, true, DataGridViewAutoSizeColumnMode.Fill);

        rGrid.AddColumn("GTxtDebit", @"DEBIT (Dr)", "dt_Debit", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtLocalDebit", @"LOCAL_DEBIT (Dr)", "dt_LocalDebit", 150, 110, false,
            DataGridViewContentAlignment.MiddleRight);

        rGrid.AddColumn("GTxtDate2", @"DATE", "dt_Date", 130, 90, ObjGlobal.SysDateType.Equals("D"));
        rGrid.AddColumn("GTxtMiti2", @"MITI", "dt_Miti", 130, 90, ObjGlobal.SysDateType.Equals("M"));
        rGrid.AddColumn("GTxtVoucherNo2", @"VOUCHER_NO", "dt_VoucherNo2", 150, 130, true);
        rGrid.AddColumn("GTxtLedger2", @"PARTICULARS", "dt_Desc2", 400, 375, true, DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtCredit", @"CREDIT (Cr)", "dt_Credit", 150, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtLocalCredit", @"LOCAL_CREDIT (Cr)", "dt_LocalCredit", 150, 110, false,
            DataGridViewContentAlignment.MiddleRight);

        rGrid.AddColumn("GTxtModule", @"MODULE", "dt_Module", 0, 2, false);
        rGrid.AddColumn("GTxtVoucherNo", @"VOUCHER_NO", "dt_VoucherNo", 0, 2, false);
        rGrid.AddColumn("dtFilterDate", @"FILTER_DATE", "dtFilterDate", 0, 2, false);
        rGrid.AddColumn("IsGroup", @"IS_GROUP", "IsGroup", 0, 2, false);
        return rGrid;
    }

    #endregion --------------- DAY BOOK ---------------
}