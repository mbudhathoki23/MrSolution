using System.Windows.Forms;

namespace MrDAL.DataEntry.Interface;

public interface IPurchaseDesign
{
    #region *---------------- DATA ENTRY GRID DESIGN ----------------*

    DataGridView GetPurchaseEntryDesign(DataGridView rGrid, string module);

    #endregion *---------------- DATA ENTRY GRID DESIGN ----------------*
}