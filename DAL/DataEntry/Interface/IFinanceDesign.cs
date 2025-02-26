using System.Windows.Forms;

namespace MrDAL.DataEntry.Interface;

public interface IFinanceDesign
{
    #region *---------------- DATA ENTRY GRID DESIGN ----------------*

    DataGridView GetCashBankDesign(DataGridView rGrid, string module);

    DataGridView GetNotesDesign(DataGridView rGrid, string module);

    DataGridView GetJournalVoucherDesign(DataGridView rGrid, string module);

    DataGridView GetRemittanceDesign(DataGridView rGrid, string module);

    DataGridView GetCurrencyExchangeDesign(DataGridView dGrid);
    DataGridView GetCurrencyPurchaseEntryDesign(DataGridView dGrid);
    DataGridView GetCurrencySalesEntryDesign(DataGridView dGrid);

    #endregion *---------------- DATA ENTRY GRID DESIGN ----------------*
}