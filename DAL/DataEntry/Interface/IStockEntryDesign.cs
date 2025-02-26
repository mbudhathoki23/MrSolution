using System.Windows.Forms;

namespace MrDAL.DataEntry.Interface;

public interface IStockEntryDesign
{
    DataGridView GetBillOfMaterialsEntryDesign(DataGridView dGrid);

    DataGridView GetStockAdjustmentEntryDesign(DataGridView dGrid);

    DataGridView GetPhysicalStockEntry(DataGridView dGrid);

    DataGridView GetProductionEntryDesign(DataGridView dGrid);

    DataGridView GetProductOpeningEntry(DataGridView dGrid);
}