using MrDAL.DataEntry.Interface.Common;
using System.Data;

namespace MrDAL.DataEntry.CommonSetup;

public class UserDefinedFieldRepository : IUserDefinedFieldRepository
{
    public UserDefinedFieldRepository()
    {

    }

    // RETURN VALUE IN DATA TABLE
    public DataTable GetDataTable()
    {
        var dt = new DataTable();
        dt.Columns.Add("Des");
        dt.Rows.Add("Opening Master", "Opening Master");
        dt.Rows.Add("Opening Detail", "Opening Detail");
        dt.Rows.Add("Purchase Master", "Purchase Master");
        dt.Rows.Add("Purchase Detail", "Purchase Detail");
        return dt;
    }
}