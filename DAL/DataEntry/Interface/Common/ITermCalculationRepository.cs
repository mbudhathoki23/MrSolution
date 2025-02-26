using System.Data;
using System.Windows.Forms;

namespace MrDAL.DataEntry.Interface.Common;

public interface ITermCalculationRepository
{

    // RETURN VALUE IN DATA TABLE
    DataGridView GetBillingTermDesign(DataGridView dGrid);
    DataTable GetTermCalculationForVoucher(string module, string termType = "B");
}