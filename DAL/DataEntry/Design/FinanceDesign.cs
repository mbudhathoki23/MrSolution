using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface;
using System.Windows.Forms;

namespace MrDAL.DataEntry.Design;

public class FinanceDesign : IFinanceDesign
{
    public DataGridView GetCashBankDesign(DataGridView dGrid, string module)
    {
        if (dGrid.ColumnCount > 0)
        {
            dGrid.DataSource = null;
            dGrid.Columns.Clear();
        }

        dGrid.AddColumn("GTxtSNo", "SNo.", "GTxtSNo", 50, 50, true, DataGridViewContentAlignment.MiddleCenter);
        dGrid.AddColumn("GTxtLedgerId", "LedgerId", "GTxtLedgerId", 0, 2, false);
        dGrid.AddColumn("GTxtLedger", "PARTICULARS", "GTxtLedger", 325, 300, DataGridViewAutoSizeColumnMode.Fill);
        dGrid.AddColumn("GTxtSubledgerId", "SubLedgerId", "GTxtSubledgerId", 0, 2, false);
        dGrid.AddColumn("GTxtSubLedger", "SUB_LEDGER", "GTxtSubLedger", 125, 100);
        dGrid.AddColumn("GTxtAgentId", "AgentId", "GTxtAgentId", 0, 2, false);
        dGrid.AddColumn("GTxtAgent", "AGENT", "GTxtAgent", 125, 100);
        dGrid.AddColumn("GTxtDepartmentId", "DepartmentId", "GTxtDepartmentId", 0, 2, false);
        dGrid.AddColumn("GTxtDepartment", "DEPARTMENT", "GTxtDepartment", 125, 100);
        dGrid.AddColumn("GTxtCurrencyId", "CurrencyId", "GTxtCurrencyId", 0, 2, false);
        dGrid.AddColumn("GTxtCurrency", "CURR", "GTxtCurrency", 100, 100, false);
        dGrid.AddColumn("GTxtExchangeRate", "RATE", "GTxtExchangeRate", 100, 100, false,
            DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtReceipt", "RECEIPT", "GTxtReceipt", 110, 100, true,
            DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtLocalReceipt", "N_RECEIPT", "GTxtLocalReceipt", 0, 100, false,
            DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtPayment", "PAYMENT", "GTxtPayment", 110, 100, true,
            DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtLocalPayment", "N_PAYMENT", "GTxtLocalPayment", 0, 100, false,
            DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtNarration", "NARRATION", "GTxtNarration", 150, 100, true,
            DataGridViewAutoSizeColumnMode.Fill);
        return dGrid;
    }

    public DataGridView GetNotesDesign(DataGridView dGrid, string module)
    {
        if (dGrid.ColumnCount > 0)
        {
            dGrid.DataSource = null;
            dGrid.Columns.Clear();
        }

        dGrid.AddColumn("GTxtSNo", "SNo.", "GTxtSNo", 50, 50, true, DataGridViewContentAlignment.MiddleCenter);
        dGrid.AddColumn("GTxtLedgerId", "LedgerId", "GTxtLedgerId", 0, 2, false);
        dGrid.AddColumn("GTxtLedger", "PARTICULARS", "GTxtLedger", 325, 300, DataGridViewAutoSizeColumnMode.Fill);
        dGrid.AddColumn("GTxtSubledgerId", "SubLedgerId", "GTxtSubledgerId", 0, 2, false);
        dGrid.AddColumn("GTxtSubLedger", "SUB_LEDGER", "GTxtSubLedger", 125, 100);
        dGrid.AddColumn("GTxtAgentId", "AgentId", "GTxtAgentId", 0, 2, false);
        dGrid.AddColumn("GTxtAgent", "AGENT", "GTxtAgent", 125, 100);
        dGrid.AddColumn("GTxtDepartmentId", "DepartmentId", "GTxtDepartmentId", 0, 2, false);
        dGrid.AddColumn("GTxtDepartment", "DEPARTMENT", "GTxtDepartment", 125, 100);
        dGrid.AddColumn("GTxtCurrencyId", "CurrencyId", "GTxtCurrencyId", 0, 2, false);
        dGrid.AddColumn("GTxtCurrency", "CURR", "GTxtCurrency", 65, 100, false);
        dGrid.AddColumn("GTxtExchangeRate", "RATE", "GTxtExchangeRate", 65, 100, false,
            DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtAmount", "AMOUNT", "GTxtAmount", 110, 100, true,
            DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtLocalAmount", "N_AMOUNT", "GTxtLocalAmount", 0, 100, false,
            DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtNarration", "NARRATION", "GTxtNarration", 150, 100, true);
        return dGrid;
    }

    public DataGridView GetJournalVoucherDesign(DataGridView rGrid, string module)
    {
        if (rGrid.ColumnCount > 0)
        {
            rGrid.DataSource = null;
            rGrid.Columns.Clear();
        }

        rGrid.AddColumn("GTxtSNo", "SNO.", "Sno", 65, 50, true, DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtLedgerId", "LedgerId", "LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtLedger", "PARTICULARS", "Ledger", 375, 300);
        rGrid.AddColumn("GTxtSubledgerId", "SubLedgerId", "SubledgerId", 0, 2, false);
        rGrid.AddColumn("GTxtSubLedger", "SUB_LEDGER", "SubLedger", 150, 50);
        rGrid.AddColumn("GTxtAgentId", "AgentId", "AgentId", 0, 2, false);
        rGrid.AddColumn("GTxtAgent", "AGENT", "Agent", 150, 50);
        rGrid.AddColumn("GTxtDepartmentId", "DepartmentId", "DepartmentId", 0, 2, false);
        rGrid.AddColumn("GTxtDepartment", "DEPARTMENT", "Department", 150, 50);
        rGrid.AddColumn("GTxtCurrencyId", "CurrencyId", "CurrencyId", 0, 2, false);
        rGrid.AddColumn("GTxtCurrency", "CURR", "Currency", 75, 50, false);
        rGrid.AddColumn("GTxtExchangeRate", "RATE", "ExchangeRate", 75, 50, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtDebit", "DEBIT", "Debit", 120, 50, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtLocalDebit", "N_DEBIT", "LocalDebit", 90, 50, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtCredit", "CREDIT", "Credit", 120, 100, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtLocalCredit", "N_CREDIT", "LocalCredit", 90, 50, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtNarration", "NARRATION", "Narration", 200, 50, true);
        return rGrid;
    }

    public DataGridView GetRemittanceDesign(DataGridView dGrid, string module)
    {
        if (dGrid.ColumnCount > 0)
        {
            dGrid.DataSource = null;
            dGrid.Columns.Clear();
        }

        if (module == "P")
        {
            dGrid.AddColumn("GTxtPSNo", "SNo.", "GTxtSNo", 50, 50, true, DataGridViewContentAlignment.MiddleCenter);
            dGrid.AddColumn("GTxtPLedgerId", "LedgerId", "GTxtLedgerId", 0, 2, false);
            dGrid.AddColumn("GTxtPLedger", "PARTICULARS", "GTxtLedger", 325, 300, DataGridViewAutoSizeColumnMode.Fill);
            dGrid.AddColumn("GTxtPCurrencyId", "CurrencyId", "GTxtCurrencyId", 0, 2, false);
            dGrid.AddColumn("GTxtPCurrency", "CURR", "GTxtCurrency", 100, 100, true);
            dGrid.AddColumn("GTxtPExchangeRate", "RATE", "GTxtExchangeRate", 100, 100, true,
                DataGridViewContentAlignment.MiddleRight);
            dGrid.AddColumn("GTxtPayment", "PAYMENT", "GTxtPayment", 110, 100, true,
                DataGridViewContentAlignment.MiddleRight);
            dGrid.AddColumn("GTxtLocalPayment", "N_PAYMENT", "GTxtLocalPayment", 0, 100, false,
                DataGridViewContentAlignment.MiddleRight);
            dGrid.AddColumn("GTxtPNarration", "NARRATION", "GTxtNarration", 150, 100, true,
                DataGridViewAutoSizeColumnMode.Fill);
        }
        else if (module == "R")
        {
            dGrid.AddColumn("GTxtRSNo", "SNo.", "GTxtSNo", 50, 50, true, DataGridViewContentAlignment.MiddleCenter);
            dGrid.AddColumn("GTxtRLedgerId", "LedgerId", "GTxtLedgerId", 0, 2, false);
            dGrid.AddColumn("GTxtRLedger", "PARTICULARS", "GTxtLedger", 325, 300, DataGridViewAutoSizeColumnMode.Fill);
            dGrid.AddColumn("GTxtRCurrencyId", "CurrencyId", "GTxtCurrencyId", 0, 2, false);
            dGrid.AddColumn("GTxtRCurrency", "CURR", "GTxtCurrency", 100, 100, true);
            dGrid.AddColumn("GTxtRExchangeRate", "RATE", "GTxtExchangeRate", 100, 100, true,
                DataGridViewContentAlignment.MiddleRight);
            dGrid.AddColumn("GTxtReceipt", "RECEIPT", "GTxtReceipt", 110, 100, true,
                DataGridViewContentAlignment.MiddleRight);
            dGrid.AddColumn("GTxtLocalReceipt", "N_RECEIPT", "GTxtLocalReceipt", 0, 100, false,
                DataGridViewContentAlignment.MiddleRight);
            dGrid.AddColumn("GTxtRNarration", "NARRATION", "GTxtNarration", 150, 100, true,
                DataGridViewAutoSizeColumnMode.Fill);
        }
        else
        {
            dGrid.AddColumn("GTxtSNo", "SNo.", "GTxtSNo", 50, 50, true, DataGridViewContentAlignment.MiddleCenter);
            dGrid.AddColumn("GTxtLedgerId", "LedgerId", "GTxtLedgerId", 0, 2, false);
            dGrid.AddColumn("GTxtLedger", "PARTICULARS", "GTxtLedger", 325, 300, DataGridViewAutoSizeColumnMode.Fill);
            dGrid.AddColumn("GTxtSubledgerId", "SubLedgerId", "GTxtSubledgerId", 0, 2, false);
            dGrid.AddColumn("GTxtSubLedger", "SUB_LEDGER", "GTxtSubLedger", 125, 100);
            dGrid.AddColumn("GTxtAgentId", "AgentId", "GTxtAgentId", 0, 2, false);
            dGrid.AddColumn("GTxtAgent", "AGENT", "GTxtAgent", 125, 100);
            dGrid.AddColumn("GTxtDepartmentId", "DepartmentId", "GTxtDepartmentId", 0, 2, false);
            dGrid.AddColumn("GTxtDepartment", "DEPARTMENT", "GTxtDepartment", 125, 100);
            dGrid.AddColumn("GTxtCurrencyId", "CurrencyId", "GTxtCurrencyId", 0, 2, false);
            dGrid.AddColumn("GTxtCurrency", "CURR", "GTxtCurrency", 100, 100, false);
            dGrid.AddColumn("GTxtExchangeRate", "RATE", "GTxtExchangeRate", 100, 100, false, DataGridViewContentAlignment.MiddleRight);
            dGrid.AddColumn("GTxtReceipt", "RECEIPT", "GTxtReceipt", 110, 100, true, DataGridViewContentAlignment.MiddleRight);
            dGrid.AddColumn("GTxtLocalReceipt", "N_RECEIPT", "GTxtLocalReceipt", 0, 100, false, DataGridViewContentAlignment.MiddleRight);
            dGrid.AddColumn("GTxtPayment", "PAYMENT", "GTxtPayment", 110, 100, true, DataGridViewContentAlignment.MiddleRight);
            dGrid.AddColumn("GTxtLocalPayment", "N_PAYMENT", "GTxtLocalPayment", 0, 100, false, DataGridViewContentAlignment.MiddleRight);
            dGrid.AddColumn("GTxtNarration", "NARRATION", "GTxtNarration", 150, 100, true, DataGridViewAutoSizeColumnMode.Fill);
        }

        return dGrid;
    }

    public DataGridView GetCurrencyExchangeDesign(DataGridView dGrid)
    {
        if (dGrid.ColumnCount > 0)
        {
            dGrid.DataSource = null;
            dGrid.Columns.Clear();
        }

        var column = new[] { "GTxtSNo", "SNo", "GTxtSNo" };
        dGrid.AddColumn(column[0], column[1], column[2], 50, 50, true, DataGridViewContentAlignment.MiddleCenter);

        column = ["GTxtCurrencyId", "CurrencyId", "GTxtCurrencyId"];
        dGrid.AddColumn(column[0], column[1], column[2], 0, 2, false);

        column = ["GTxtCurrency", "DESCRIPTION", "GTxtCurrency"];
        dGrid.AddColumn(column[0], column[1], column[2], 100, 100, true, DataGridViewAutoSizeColumnMode.Fill);

        column = ["GTxtAmount", "AMOUNT", "GTxtAmount"];
        dGrid.AddColumn(column[0], column[1], column[2], 100, 100, true, DataGridViewContentAlignment.MiddleRight);

        column = ["GTxtBuyRate", "BUY", "GTxtBuyRate"];
        dGrid.AddColumn(column[0], column[1], column[2], 100, 100, true, DataGridViewContentAlignment.MiddleRight);

        column = ["GTxtSalesRate", "SALES", "GTxtSalesRate"];
        dGrid.AddColumn(column[0], column[1], column[2], 100, 100, true, DataGridViewContentAlignment.MiddleRight);

        column = ["GTxtNarration", "NARRATION", "GTxtNarration"];
        dGrid.AddColumn(column[0], column[1], column[2], 150, 100, true, DataGridViewAutoSizeColumnMode.Fill);
        return dGrid;
    }

    public DataGridView GetCurrencyPurchaseEntryDesign(DataGridView dGrid)
    {
        if (dGrid.ColumnCount > 0)
        {
            dGrid.DataSource = null;
            dGrid.Columns.Clear();
        }

        var column = new[] { "GTxtSNo", "SNo", "GTxtSNo" };
        dGrid.AddColumn(column[0], column[1], column[2], 50, 50, true, DataGridViewContentAlignment.MiddleCenter);

        column = ["GTxtCurrencyId", "CurrencyId", "GTxtCurrencyId"];
        dGrid.AddColumn(column[0], column[1], column[2], 0, 2, false);

        column = ["GTxtShortName", "CURRENCY", "GTxtShortName"];
        dGrid.AddColumn(column[0], column[1], column[2], 100, 100, true, DataGridViewAutoSizeColumnMode.Fill);

        column = ["GTxtCurrency", "DESCRIPTION", "GTxtCurrency"];
        dGrid.AddColumn(column[0], column[1], column[2], 100, 100, true, DataGridViewAutoSizeColumnMode.Fill);

        column = ["GTxtQty", "Qty", "GTxtQty"];
        dGrid.AddColumn(column[0], column[1], column[2], 100, 100, true, DataGridViewContentAlignment.MiddleRight);

        column = ["GTxtBuyRate", "BUY", "GTxtBuyRate"];
        dGrid.AddColumn(column[0], column[1], column[2], 100, 100, true, DataGridViewContentAlignment.MiddleRight);

        column = ["GTxLocalRate", "L RATE", "GTxLocalRate"];
        dGrid.AddColumn(column[0], column[1], column[2], 100, 100, true, DataGridViewContentAlignment.MiddleRight);
        
        column = ["GTxNetAmount", "NET AMOUNT", "GTxNetAmount"];
        dGrid.AddColumn(column[0], column[1], column[2], 150, 100, true, DataGridViewContentAlignment.MiddleRight);

        column = ["GTxLocalNetAmount", "LOCAL AMOUNT", "GTxLocalNetAmount"];
        dGrid.AddColumn(column[0], column[1], column[2], 150, 100, true, DataGridViewContentAlignment.MiddleRight);

        column = ["GTxtNarration", "NARRATION", "GTxtNarration"];
        dGrid.AddColumn(column[0], column[1], column[2], 150, 100, true, DataGridViewAutoSizeColumnMode.Fill);
        return dGrid;
    }

    public DataGridView GetCurrencySalesEntryDesign(DataGridView dGrid)
    {
        if (dGrid.ColumnCount > 0)
        {
            dGrid.DataSource = null;
            dGrid.Columns.Clear();
        }

        var column = new[] { "GTxtSNo", "SNo", "GTxtSNo" };
        dGrid.AddColumn(column[0], column[1], column[2], 50, 50, true, DataGridViewContentAlignment.MiddleCenter);

        column = ["GTxtCurrencyId", "CurrencyId", "GTxtCurrencyId"];
        dGrid.AddColumn(column[0], column[1], column[2], 0, 2, false);

        column = ["GTxtShortName", "CURRENCY", "GTxtShortName"];
        dGrid.AddColumn(column[0], column[1], column[2], 100, 100, true, DataGridViewAutoSizeColumnMode.Fill);

        column = ["GTxtCurrency", "DESCRIPTION", "GTxtCurrency"];
        dGrid.AddColumn(column[0], column[1], column[2], 100, 100, true, DataGridViewAutoSizeColumnMode.Fill);

        column = ["GTxtQty", "Qty", "GTxtQty"];
        dGrid.AddColumn(column[0], column[1], column[2], 100, 100, true, DataGridViewContentAlignment.MiddleRight);

        column = ["GTxtSalesRate", "BUY", "GTxtSalesRate"];
        dGrid.AddColumn(column[0], column[1], column[2], 100, 100, true, DataGridViewContentAlignment.MiddleRight);

        column = ["GTxLocalRate", "SALES", "GTxLocalRate"];
        dGrid.AddColumn(column[0], column[1], column[2], 100, 100, true, DataGridViewContentAlignment.MiddleRight);

        column = ["GTxtDifference", "DIFF", "GTxtDifference"];
        dGrid.AddColumn(column[0], column[1], column[2], 5, 5, false, DataGridViewContentAlignment.MiddleRight);

        column = ["GTxNetAmount", "NET AMOUNT", "GTxNetAmount"];
        dGrid.AddColumn(column[0], column[1], column[2], 150, 100, true, DataGridViewContentAlignment.MiddleRight);

        column = ["GTxLocalNetAmount", "LOCAL AMOUNT", "GTxLocalNetAmount"];
        dGrid.AddColumn(column[0], column[1], column[2], 150, 100, true, DataGridViewContentAlignment.MiddleRight);

        column = ["GTxtProfitLoss", "Loss", "GTxtProfitLoss"];
        dGrid.AddColumn(column[0], column[1], column[2], 5, 5, false, DataGridViewContentAlignment.MiddleRight);

        column = ["GTxtNarration", "NARRATION", "GTxtNarration"];
        dGrid.AddColumn(column[0], column[1], column[2], 150, 100, true, DataGridViewAutoSizeColumnMode.Fill);
        return dGrid;
    }
}