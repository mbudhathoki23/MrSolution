using System.Data;

namespace MrDAL.DataEntry.Interface.Common;

public interface IUserDefinedFieldRepository
{
    // RETURN VALUE IN DATA TABLE
    DataTable GetDataTable();
}