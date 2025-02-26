using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Utility.Interface;
using MrDAL.Utility.Server;
using System.Data;
using System.Windows.Forms;

namespace MrDAL.Utility.Config;

public class ClsUtilityTools : IUtility
{
    public DataGridView ReturnSummaryAccountTransactionDesign(DataGridView rGrid)
    {
        if (rGrid.ColumnCount > 0)
        {
            rGrid.DataSource = null;
            rGrid.Columns.Clear();
        }

        rGrid.ReadOnly = true;
        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtModule", "Module", "Module", 0, 2, false);
        rGrid.AddColumn("GTxtEntryType", "ENTRY_TYPE", "Source", 120, 2, DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtDebit", "DEBIT (Dr)", "Debit", 150, 2, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtCredit", "CREDIT (Cr)", "Credit", 150, 2, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtDifference", "DIFFERENCE", "Difference", 150, 2, true,
            DataGridViewContentAlignment.MiddleRight);

        return rGrid;
    }

    public DataGridView ReturnDetailsAccountTransactionDesign(DataGridView rGrid)
    {
        if (rGrid.ColumnCount > 0)
        {
            rGrid.DataSource = null;
            rGrid.Columns.Clear();
        }

        rGrid.ReadOnly = true;
        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtModule", "Module", "Module", 0, 2, false);
        rGrid.AddColumn("GTxtEntryType", "ENTRY_TYPE", "Source", 100, 2, DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtVoucherDate", "VOUCHER_DATE", "VoucherDate", 120, 2);
        rGrid.AddColumn("GTxtVoucherNo", "VOUCHER_NO", "VoucherNo", 150, 2);
        rGrid.AddColumn("GTxtDebit", "DEBIT (Dr)", "Debit", 120, 2, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtCredit", "CREDIT (Cr)", "Credit", 120, 2, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtDifference", "DIFFERENCE", "Difference", 120, 2, true,
            DataGridViewContentAlignment.MiddleRight);

        return rGrid;
    }

    public DataTable CheckSummaryAccountTransactionPosting()
    {
        var cmdString = @$"
	          SELECT Module, CASE WHEN Module='OB' THEN 'OPENING BALANCE'
			               WHEN Module='LOB' THEN 'LEDGER OPENING BALANCE'
			               WHEN Module='OPL' THEN 'PURCHASE LEDGER OPENING BALANCE'
			               WHEN Module='OPL' THEN 'PURCHASE LEDGER OPENING BALANCE'
			               WHEN Module='N' THEN 'OPENING BALANCE'
			               WHEN Module='PDC' THEN 'POST DATED CHEQUE'
			               WHEN Module='CB' THEN 'CASH BANK VOUCHER'
			               WHEN Module='JV' THEN 'JOURNAL VOUCHER'
			               WHEN Module='SB' THEN 'SALES INVOICE'
			               WHEN Module='SR' THEN 'SALES RETURN INVOICE'
			               WHEN Module='PB' THEN 'PURCHASE INVOICE'
			               WHEN Module='PR' THEN 'PURCHASE RETURN INVOICE'
			               WHEN Module='DN' THEN 'DEBIT NOTES'
			               WHEN Module='CN' THEN 'CREDIT NOTES' END Source, CAST(SUM(ad.LocalDebit_Amt) AS DECIMAL(18, 2)) Debit, CAST(SUM(ad.LocalCredit_Amt) AS DECIMAL(18, 2)) Credit, CAST(ABS(SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt)) AS DECIMAL(18, 2)) Difference
			FROM AMS.AccountDetails ad
			WHERE ad.FiscalYearId = {ObjGlobal.SysFiscalYearId}
			GROUP BY ad.Module
			HAVING CAST(ABS(SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt)) AS DECIMAL(18, 2))>0
			ORDER BY ad.Module;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable CheckDetailsAccountTransactionPosting(string getModule)
    {
        var cmdString = @$"
            SELECT Module, CASE WHEN Module='OB' THEN 'OPENING BALANCE'
			               WHEN Module='LOB' THEN 'OPENING BALANCE'
			               WHEN Module='N' THEN 'OPENING BALANCE'
			               WHEN Module='PDC' THEN 'POST DATED CHEQUE'
			               WHEN Module='CB' THEN 'CASH BANK VOUCHER'
			               WHEN Module='JV' THEN 'JOURNAL VOUCHER'
			               WHEN Module='SB' THEN 'SALES INVOICE'
			               WHEN Module='SR' THEN 'SALES RETURN INVOICE'
			               WHEN Module='PB' THEN 'PURCHASE INVOICE'
			               WHEN Module='PR' THEN 'PURCHASE RETURN INVOICE'
			               WHEN Module='DN' THEN 'DEBIT NOTES'
			               WHEN Module='CN' THEN 'CREDIT NOTES' END Source, ad.Voucher_No VoucherNo,ad.Voucher_Date VoucherDate, CAST(SUM(ad.LocalDebit_Amt) AS DECIMAL(18, 2)) Debit, CAST(SUM(ad.LocalCredit_Amt) AS DECIMAL(18, 2)) Credit, CAST(ABS(SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt)) AS DECIMAL(18, 2)) Difference
			FROM AMS.AccountDetails ad
			WHERE ad.FiscalYearId = {ObjGlobal.SysFiscalYearId} ";
        if (!string.IsNullOrEmpty(getModule))
            cmdString += $@"
        		AND ad.Module = '{getModule}'";
        cmdString += @"
            GROUP BY ad.Voucher_No, Module,ad.Voucher_Date
            HAVING CAST(ABS(SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt)) AS DECIMAL(18, 2)) > 0
            ORDER BY VoucherNo  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }
}