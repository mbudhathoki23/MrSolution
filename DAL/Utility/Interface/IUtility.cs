using System.Data;
using System.Windows.Forms;

namespace MrDAL.Utility.Interface;

public interface IUtility
{
    DataGridView ReturnSummaryAccountTransactionDesign(DataGridView rGrid);

    DataGridView ReturnDetailsAccountTransactionDesign(DataGridView rGrid);

    DataTable CheckSummaryAccountTransactionPosting();

    DataTable CheckDetailsAccountTransactionPosting(string getModule);
}