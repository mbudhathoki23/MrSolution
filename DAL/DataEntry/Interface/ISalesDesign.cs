using System.Windows.Forms;

namespace MrDAL.DataEntry.Interface;

public interface ISalesDesign
{
    #region *---------------- DATA ENTRY GRID DESIGN ----------------*

    DataGridView GetSalesEntryGridDesign(DataGridView rGrid, string module);

    DataGridView GetPointOfSalesInvoiceDesign(DataGridView rGrid, string module);

    DataGridView GetPointOfSalesDesign(DataGridView rGrid, string module);

    DataGridView GetRestroInvoiceDesign(DataGridView dGrid, string module);

    #endregion *---------------- DATA ENTRY GRID DESIGN ----------------*
}