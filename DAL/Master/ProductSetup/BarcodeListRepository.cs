using DatabaseModule.Master.ProductSetup;
using MrDAL.Global.Common;
using MrDAL.Master.Interface.ProductSetup;
using MrDAL.Utility.Server;
using System.Data;

namespace MrDAL.Master.ProductSetup;

public class BarcodeListRepository : IBarcodeListRepository
{
    public BarcodeListRepository()
    {
        BarcodeList = new BarcodeList();
    }


    // INSERT UPDATE DELETE

    public int SaveBarcodeListAuditLog(string actionTag)
    {
        var cmdString = $@"
            INSERT INTO AUD.AUDIT_BARCODE_LIST(ProductId, Barcode, SalesRate, MRP, Trade, Wholesale, Retail, Dealer, Resellar, UnitId, AltUnitId, ModifyAction, ModifyBy, ModifyDate)
            SELECT ProductId, Barcode, SalesRate, MRP, Trade, Wholesale, Retail, Dealer, Resellar, UnitId, AltUnitId,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate
            FROM AMS.BarcodeList
            WHERE ProductId='{BarcodeList.ProductId}'
            ";
        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return exe;
    }


    // RETURN VALUE IN DATA TABLE

    public DataTable GetProductBarcodeList(long productId)
    {
        var cmdString = $@"
			SELECT bl.ProductId, bl.Barcode, bl.SalesRate, bl.MRP, bl.Trade, bl.Wholesale, bl.Retail, bl.Dealer, bl.Resellar, ISNULL(bl.UnitId, bl.AltUnitId) UnitId,ISNULL(pu.UnitCode,pu1.UnitCode) UOM, CASE WHEN  bl.AltUnitId > 0 THEN 1 ELSE 0 END isAltUnit FROM AMS.BarcodeList bl
			LEFT OUTER JOIN AMS.ProductUnit pu ON bl.UnitId = pu.UID
			LEFT OUTER JOIN AMS.ProductUnit pu1 ON bl.AltUnitId = pu1.UID
			WHERE bl.ProductId = {productId}; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }


    // OBJECT FOR THIS FORM

    public BarcodeList BarcodeList { get; set; }

}