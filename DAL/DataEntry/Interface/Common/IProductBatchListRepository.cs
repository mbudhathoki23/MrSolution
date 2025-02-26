using System.Data;

namespace MrDAL.DataEntry.Interface.Common;

public interface IProductBatchListRepository
{
    // RETURN VALUE IN DATA TABLE
    DataTable GetProductBatchFormat();
    DataTable GetProductBatchFromBatchRate(string batch, string rate);
}