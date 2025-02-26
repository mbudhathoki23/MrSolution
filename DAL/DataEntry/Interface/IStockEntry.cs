using DatabaseModule.DataEntry.OpeningMaster;
using DatabaseModule.DataEntry.ProductionSystem.BillOfMaterials;
using DatabaseModule.DataEntry.ProductionSystem.Production;
using DatabaseModule.DataEntry.ProductionSystem.RawMaterialsIssue;
using DatabaseModule.DataEntry.StockTransaction.StockAdjustment;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.Interface;

public interface IStockEntry
{
    // INSERT, UPDATE, DELETE FUNCTION

    #region --------------- I_U_D_FUNCTION ---------------
    Task<int> SyncProductAddInfoAsync(string actionTag);

    int SaveBillOfMaterialsSetup(string actionTag);

    int UpdateStockAdjustmentStockValue();

    int SaveProductionSetup(string actionTag);
    
    int SaveStockAdjustment(string actionTag);
    
    Task<int> SyncStockAdjustmentAsync(string actionTag);
    
    int SaveRawMaterialReturn(string actionTag);


    // STOCK DETAILS UPDATE
    int ProductOpeningStockPosting();
    int ProductionStockPosting();
    int StockAdjustmentStockPosting();


    string NumberSchema { get; set; }
    string Module { get; set; }

    #endregion --------------- I_U_D_FUNCTION ---------------

    // RETURN VALUE IN DATA TABLE

    #region --------------- DATA TABLE ---------------



    DataTable GetProductOpeningVoucher(string branchId, string companyUnitId, string fiscalYearId);

    DataTable GetAutoConsumptionProductInvoiceDetails();

    DataTable GetProductBomDetails(long productId);

    #endregion --------------- DATA TABLE ---------------

    // RETURN VOUCHER DETAILS IN DATA SET

    #region --------------- RETURN VOUCHER DETAILS ---------------

    DataTable CheckVoucherNo(string tableName, string tableVoucherNo, string inputVoucherNo);
    DataSet GetBomVoucherDetails(string voucherNo);



    DataSet GetStockAdjustmentVoucherDetails(string voucherNo);


    #endregion --------------- RETURN VOUCHER DETAILS ---------------

    // OBJECT FOR THIS CLASS

    #region ---------------  VIEW MODULE ---------------

    ProductOpening VmProductOpening { get; set; }
    STA_Master VmStaMaster { get; set; }
    BOM_Master VmBomMaster { get; set; }
    Production_Master VmProductionMaster { get; set; }
    StockIssue_Master IssueMaster { get; set; }
    #endregion ---------------  VIEW MODULE ---------------
}