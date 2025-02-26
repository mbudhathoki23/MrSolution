using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface.Common;
using MrDAL.Utility.Server;
using System.Data;

namespace MrDAL.DataEntry.CommonSetup;

public class ProductBatchListRepository : IProductBatchListRepository
{
    public ProductBatchListRepository()
    {

    }

    // RETURN VALUE IN DATA TABLE
    public DataTable GetProductBatchFormat()
    {
        var dtBatch = new DataTable();
        dtBatch.AddStringColumns(new[]
        {
            "SNo",
            "ProductId",
            "BatchNo",
            "MfDate",
            "ExpDate",
            "Qty",
            "MRP",
            "Rate",
            "ProductSno"
        });
        return dtBatch;
    }
    public DataTable GetProductBatchFromBatchRate(string batch, string rate)
    {
        var cmdString = $"SELECT * FROM AMS.ProductAddInfo WHERE BatchNo = '{batch}' AND Rate = {rate.GetDecimal()}";
        var dtProduct = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dtProduct;
    }
}