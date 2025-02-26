using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.OpeningMaster;
using DatabaseModule.DataEntry.ProductionSystem.BillOfMaterials;
using DatabaseModule.DataEntry.ProductionSystem.Production;
using DatabaseModule.DataEntry.ProductionSystem.RawMaterialsIssue;
using DatabaseModule.DataEntry.StockTransaction.StockAdjustment;
using DatabaseModule.Master.ProductSetup;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Interface;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrDAL.DataEntry.TransactionClass;

public class ClsStockEntry : IStockEntry
{
    // STOCK ENTRY
    
    #region -------------- AUDIT_LOG --------------

    // SAVE , UPDATE, DELETE FUNCTION
    public async Task<int> SyncProductAddInfoAsync(string actionTag)
    {
        //sync
        try
        {
            var configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!configParams.Success || configParams.Model.Item1 == null)
                return 1;

            var injectData = new DbSyncRepoInjectData
            {
                ExternalConnectionString = null,
                DateTime = DateTime.Now,
                LocalOriginId = configParams.Model.Item1,
                LocalCompanyUnitId = ObjGlobal.SysCompanyUnitId,
                Username = ObjGlobal.LogInUser,
                LocalConnectionString = GetConnection.ConnectionString,
                LocalBranchId = ObjGlobal.SysBranchId,
                ApiConfig = DataSyncHelper.GetConfig()
            };
            var deleteUrl = string.Empty;
            var getUrl = string.Empty;
            var insertUrl = string.Empty;
            var updateUrl = string.Empty;

            getUrl = @$"{configParams.Model.Item2}ProductAddInfo/GetProductAddInfoById";
            insertUrl = @$"{configParams.Model.Item2}ProductAddInfo/InsertProductAddInfoList";
            updateUrl = @$"{configParams.Model.Item2}ProductAddInfo/UpdateProductAddInfoList";
            deleteUrl = $@"{configParams.Model.Item2}ProductAddInfo/DeleteProductAddInfoAsync?id={VmProductOpening.OpeningId}";

            var apiConfig = new SyncApiConfig
            {
                BaseUrl = configParams.Model.Item2,
                Apikey = configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = getUrl,
                InsertUrl = insertUrl,
                UpdateUrl = updateUrl,
                DeleteUrl = deleteUrl
            };

            DataSyncHelper.SetConfig(apiConfig);
            injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(injectData);

            var productAddInfoRepo =
                DataSyncProviderFactory.GetRepository<ProductAddInfo>(DataSyncManager.GetGlobalInjectData());

            var poList = (from DataRow dr in VmProductOpening.ProductBatch.Rows
                          select new ProductAddInfo
                          {
                              Module = "OB",
                              VoucherNo = VmProductOpening.Voucher_No,
                              VoucherType = "I",
                              ProductId = dr["ProductId"].GetLong(),
                              SerialNo = dr["ProductSno"].GetString(),
                              SizeNo = null,
                              BatchNo = dr["BatchNo"].IsValueExits() ? dr["BatchNo"].GetString() : null,
                              MFDate = Convert.ToDateTime(dr["MfDate"].GetSystemDate()),
                              ExpDate = Convert.ToDateTime(dr["ExpDate"].GetSystemDate()),
                              Mrp = dr["MRP"].GetDecimal(),
                              Rate = dr["Rate"].GetDecimal(),
                              AltQty = 0,
                              Qty = dr["Qty"].GetDecimal(),
                              BranchId = ObjGlobal.SysBranchId,
                              CompanyUnitId = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null,
                              EnterBy = ObjGlobal.LogInUser,
                              EnterDate = DateTime.Now,
                              SyncRowVersion = VmProductOpening.SyncRowVersion
                          }).ToList();

            var result = actionTag.ToUpper() switch
            {
                "SAVE" => await productAddInfoRepo?.PushNewListAsync(poList)!,
                "UPDATE" => await productAddInfoRepo?.PutNewListAsync(poList)!,
                "DELETE" => await productAddInfoRepo?.DeleteNewAsync()!,
                _ => await productAddInfoRepo?.PushNewListAsync(poList)!
            };

            return 1;
        }
        catch (Exception ex)
        {
            return 1;
        }
    }

    public int SaveBillOfMaterialsSetup(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag is "UPDATE" or "DELETE" or "REVERSE")
        {
            cmdString.Append(
                $"DELETE AMS.StockDetails WHERE Module='BOM' AND Voucher_No='{VmBomMaster.VoucherNo}'; \n");
            if (actionTag != "REVERSE")
                cmdString.Append($"DELETE INV.BOM_Details WHERE VoucherNo ='{VmBomMaster.VoucherNo}'; \n");
            if (actionTag is "DELETE")
                cmdString.Append($"DELETE INV.BOM_Master WHERE VoucherNo ='{VmBomMaster.VoucherNo}'; \n");
        }

        if (actionTag is "SAVE" or "NEW")
        {
            cmdString.Append(
                "INSERT INTO INV.BOM_Master(VoucherNo, VDate, VMiti, VTime, FinishedGoodsId, FinishedGoodsQty, DepartmentId, CostCenterId, Amount, InWords, Remarks, IsAuthorized, AuthorizeBy, AuthDate, IsReconcile, ReconcileBy, ReconcileDate, IsPosted, PostedBy, PostedDate, OrderNo, OrderDate, EnterBy, EnterDate, BranchId, CompanyUnitId, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)\n");
            cmdString.Append("VALUES \n");
            cmdString.Append(
                $"(N'{VmBomMaster.VoucherNo}', '{VmBomMaster.VDate:yyyy-MM-dd}', N'{VmBomMaster.VMiti}', GETDATE(), {VmBomMaster.FinishedGoodsId}, {VmBomMaster.FinishedGoodsQty},");
            cmdString.Append(VmBomMaster.DepartmentId > 0 ? $"{VmBomMaster.DepartmentId}," : "NULL,");
            cmdString.Append(VmBomMaster.CostCenterId > 0 ? $"{VmBomMaster.CostCenterId}," : "NULL,");
            cmdString.Append(VmBomMaster.Amount > 0 ? $"{VmBomMaster.Amount}," : "0,");
            cmdString.Append(VmBomMaster.InWords.IsValueExits() ? $"N'{VmBomMaster.InWords}'," : "NULL,");
            cmdString.Append(VmBomMaster.Remarks.IsValueExits()
                ? $"N'{VmBomMaster.Remarks.Trim().Replace("'", "''")}',"
                : "NULL,");
            cmdString.Append("0, NULL, NULL, 0, NULL, NULL, 0, NULL, NULL,");
            cmdString.Append(VmBomMaster.OrderNo.IsValueExits()
                ? $"'{VmBomMaster.OrderNo}','{VmBomMaster.OrderDate}',"
                : "NULL,NULL,");
            cmdString.Append($"N'{ObjGlobal.LogInUser}', GETDATE(), {ObjGlobal.SysBranchId},");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"{ObjGlobal.SysCompanyUnitId}," : "NULL,");
            cmdString.Append($"{ObjGlobal.SysFiscalYearId},");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
            cmdString.Append($"{VmBomMaster.SyncRowVersion});\n");
        }
        else if (actionTag is "UPDATE")
        {
            cmdString.Append("UPDATE INV.BOM_Master SET \n");
            cmdString.Append(
                $" VDate= '{VmBomMaster.VDate:yyyy-MM-dd}', VMiti = N'{VmBomMaster.VMiti}',FinishedGoodsId= {VmBomMaster.FinishedGoodsId},FinishedGoodsQty= {VmBomMaster.FinishedGoodsQty},");
            cmdString.Append(VmBomMaster.DepartmentId > 0
                ? $"DepartmentId = {VmBomMaster.DepartmentId},"
                : "DepartmentId = NULL,");
            cmdString.Append(VmBomMaster.CostCenterId > 0
                ? $"CostCenterId = {VmBomMaster.CostCenterId},"
                : "CostCenterId= NULL,");
            cmdString.Append(VmBomMaster.Amount > 0 ? $"Amount = {VmBomMaster.Amount}," : "Amount= 0,");
            cmdString.Append(VmBomMaster.InWords.IsValueExits()
                ? $"InWords = N'{VmBomMaster.InWords}',"
                : "InWords = NULL,");
            cmdString.Append(VmBomMaster.Remarks.IsValueExits()
                ? $"Remarks = N'{VmBomMaster.Remarks.Trim().Replace("'", "''")}',"
                : "Remarks= NULL,");
            cmdString.Append(VmBomMaster.OrderNo.IsValueExits()
                ? $"OrderNo = '{VmBomMaster.OrderNo}',OrderDate = '{VmBomMaster.OrderDate}'"
                : "OrderNo= NULL,OrderDate= NULL");
            cmdString.Append($" WHERE VoucherNo = N'{VmBomMaster.VoucherNo}';\n");
        }

        if (actionTag != "DELETE" && actionTag != "REVERSE")
        {
            var iRow = 0;
            if (VmBomMaster.GetView != null)
            {
                cmdString.Append(
                    " INSERT INTO INV.BOM_Details(VoucherNo, SerialNo, ProductId, GodownId, CostCenterId, OrderNo, OrderSNo, AltQty, AltUnitId, Qty, UnitId, Rate, Amount, Narration, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn,SyncBaseId,SyncRowVersion) \n");
                cmdString.Append("VALUES \n");
                foreach (DataGridViewRow roDetail in VmBomMaster.GetView.Rows)
                {
                    cmdString.Append(
                        $"('{VmBomMaster.VoucherNo}',{roDetail.Cells["GTxtSNo"].Value.GetDecimal()},{roDetail.Cells["GTxtProductId"].Value},");
                    cmdString.Append(roDetail.Cells["GTxtGodownId"].Value.GetInt() > 0
                        ? $"{roDetail.Cells["GTxtGodownId"].Value},"
                        : "NULL,");
                    cmdString.Append(roDetail.Cells["GTxtCostCenterId"].Value.GetInt() > 0
                        ? $"{roDetail.Cells["GTxtCostCenterId"].Value},"
                        : "NULL,");
                    cmdString.Append(roDetail.Cells["GTxtOrderNo"].Value.IsValueExits()
                        ? $"'{roDetail.Cells["GTxtOrderNo"].Value}',"
                        : "NULL,");
                    cmdString.Append(
                        $"{roDetail.Cells["GTxtOrderSNo"].Value.GetInt()},{roDetail.Cells["GTxtAltQty"].Value.GetDecimal()},");
                    cmdString.Append(roDetail.Cells["GTxtAltUOMId"].Value.GetInt() > 0
                        ? $"{roDetail.Cells["GTxtAltUOMId"].Value},"
                        : "NULL,");
                    cmdString.Append($"{roDetail.Cells["GTxtQty"].Value.GetDecimal(true)},");
                    cmdString.Append(roDetail.Cells["GTxtUOMId"].Value.GetInt() > 0
                        ? $"{roDetail.Cells["GTxtUOMId"].Value},"
                        : "NULL,");
                    cmdString.Append(
                        $"{roDetail.Cells["GTxtRate"].Value.GetDecimal()},{roDetail.Cells["GTxtAmount"].Value.GetDecimal()},");
                    cmdString.Append(roDetail.Cells["GTxtNarration"].Value.IsValueExits()
                        ? $"'{roDetail.Cells["GTxtNarration"].Value}',"
                        : "NULL,");
                    cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
                    cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
                    cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
                    cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
                    cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
                    cmdString.Append($"{VmBomMaster.SyncRowVersion} ");
                    cmdString.Append(iRow < VmBomMaster.GetView.Rows.Count - 1 ? "),\n" : ");\n");
                    iRow++;
                }
            }
        }

        var result = SqlExtensions.ExecuteNonTrans(cmdString.ToString());
        if (result <= 0) return result;
        if (actionTag is "SAVE" or "UPDATE")
        {
            var cmd =
                $"UPDATE AMS.Product SET PBuyRate ={VmBomMaster.Amount} WHERE PID = '{VmBomMaster.FinishedGoodsId}'";
            var update = SqlExtensions.ExecuteNonQuery(cmd);
        }

        if (actionTag is "SAVE") UpdateDocumentNumbering("BOM", NumberSchema);
        return result;
    }

    public int UpdateStockAdjustmentStockValue()
    {
        var result = 0;
        var cmdString = $@"
        SELECT sm.VDate,sd.StockAdjust_No ,sd.ProductId,sm.FiscalYearId FROM AMS.STA_Details  sd LEFT OUTER JOIN AMS.STA_Master sm ON sm.StockAdjust_No = sd.StockAdjust_No
        WHERE sm.FiscalYearId = '{ObjGlobal.SysFiscalYearId}' 
        ORDER BY sd.ProductId";
        var product = SqlExtensions.GetDataTable(cmdString);
        if (product == null || product.RowsCount() <= 0)
        {
            return result;
        }

        foreach (DataRow row in product.Rows)
        {
            var productId = row["ProductId"].GetLong();
            var voucherNo = row["StockAdjust_No"].GetString();
            var voucherDate = row["VDate"].GetString();
            cmdString = $@"
            SELECT TOP(1)(StockVal / StockQty) Rate FROM AMS.StockDetails WHERE Product_Id={productId} AND EntryType = 'O' AND Voucher_Date <='{voucherDate.GetSystemDate()}' AND FiscalYearId= {ObjGlobal.SysFiscalYearId}  AND SD.Module <> 'SA'
            ORDER BY Voucher_Date DESC ";
            var value = cmdString.GetQueryData().GetDecimal();
            if (value <= 0)
            {
                continue;
            }
            cmdString = $@"
            UPDATE AMS.StockDetails
            SET StockVal = StockQty * IsNULL({value} ,0)
            WHERE Product_Id={productId} AND Module='SA' AND Voucher_No = '{voucherNo}';";
            result += Math.Abs(SqlExtensions.ExecuteNonQuery(cmdString));
        }

        return result;
    }

    public int SaveProductionSetup(string actionTag)
    {
        var cmdTxt = new StringBuilder();
        if (actionTag is "UPDATE" || _tagStrings.Contains(actionTag))
        {
            cmdTxt.Append(
                $"DELETE AMS.StockDetails WHERE Module='IBOM' AND Voucher_No='{VmProductionMaster.VoucherNo}'; \n");
            if (actionTag != "REVERSE")
                cmdTxt.Append($"DELETE INV.Production_Details WHERE VoucherNo ='{VmProductionMaster.VoucherNo}'; \n");

            if (actionTag is "DELETE")
                cmdTxt.Append($"DELETE INV.Production_Master WHERE VoucherNo ='{VmProductionMaster.VoucherNo}'; \n");
        }

        if (actionTag is "SAVE" or "NEW")
        {
            cmdTxt.Append(
                "INSERT INTO INV.Production_Master (VoucherNo, BOMVNo, BOMDate, VDate, VMiti, VTime, FinishedGoodsId, FinishedGoodsQty, Costing, Machine, DepartmentId, CostCenterId, Amount, InWords, Remarks, IsAuthorized, AuthorizeBy, AuthDate, IsCancel, IsReturn, IsReconcile, ReconcileBy, ReconcileDate, IsPosted, PostedBy, PostedDate, IssueNo, IssueDate, OrderNo, OrderDate, EnterBy, EnterDate, BranchId, CompanyUnitId, FiscalYearId, Source, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5,SyncGlobalId, SyncOriginId, SyncBaseId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)\n");
            cmdTxt.Append("VALUES \n");
            cmdTxt.Append($"(N'{VmProductionMaster.VoucherNo}',");
            cmdTxt.Append(!string.IsNullOrWhiteSpace(VmProductionMaster.BOMVNo)
                ? $"N'{VmProductionMaster.BOMVNo}',N'{VmProductionMaster.BOMDate:yyyy-MM-dd}',"
                : "NULL,NULL,");
            cmdTxt.Append(
                $"'{VmProductionMaster.VDate:yyyy-MM-dd}', N'{VmProductionMaster.VMiti}', GETDATE(), {VmProductionMaster.FinishedGoodsId}, {VmProductionMaster.FinishedGoodsQty},");
            cmdTxt.Append(VmProductionMaster.Costing > 0 ? $"{VmProductionMaster.Costing}," : "100,");
            cmdTxt.Append(!string.IsNullOrWhiteSpace(VmProductionMaster.Machine)
                ? $"N'{VmProductionMaster.Machine}',"
                : "NULL,");
            cmdTxt.Append(VmProductionMaster.DepartmentId > 0 ? $"{VmProductionMaster.DepartmentId}," : "NULL,");
            cmdTxt.Append(VmProductionMaster.CostCenterId > 0 ? $"{VmProductionMaster.CostCenterId}," : "NULL,");
            cmdTxt.Append(VmProductionMaster.Amount > 0 ? $"{VmProductionMaster.Amount}," : "0,");
            cmdTxt.Append(!string.IsNullOrWhiteSpace(VmProductionMaster.InWords)
                ? $"N'{VmProductionMaster.InWords}',"
                : "NULL,");
            cmdTxt.Append(!string.IsNullOrWhiteSpace(VmProductionMaster.Remarks.Trim())
                ? $"N'{VmProductionMaster.Remarks.Trim().Replace("'", "''")}',"
                : "NULL,");
            cmdTxt.Append("0, NULL, NULL, 0,0,0,NULL, NULL, 0, NULL, NULL,");
            cmdTxt.Append(!string.IsNullOrWhiteSpace(VmProductionMaster.IssueNo)
                ? $"'{VmProductionMaster.IssueNo}','{VmProductionMaster.IssueDate:yyyy-MM-dd}',"
                : "NULL,NULL,");
            cmdTxt.Append(!string.IsNullOrWhiteSpace(VmProductionMaster.OrderNo)
                ? $"'{VmProductionMaster.OrderNo}','{VmProductionMaster.OrderDate:yyyy-MM-dd}',"
                : "NULL,NULL,");
            cmdTxt.Append($"N'{ObjGlobal.LogInUser}', GETDATE(), {ObjGlobal.SysBranchId},");
            cmdTxt.Append(ObjGlobal.ReturnInt(ObjGlobal.SysCompanyUnitId.ToString()) > 0 ? "0," : "NULL,");
            cmdTxt.Append(
                $"{ObjGlobal.SysFiscalYearId},'{VmProductionMaster.Source}',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL, ");
            cmdTxt.Append("GETDATE(), 1 ); \n");
        }
        else if (actionTag is "UPDATE")
        {
            cmdTxt.Append("UPDATE INV.Production_Master SET \n");
            cmdTxt.Append(!string.IsNullOrWhiteSpace(VmProductionMaster.BOMVNo)
                ? $"BOMVNo = '{VmProductionMaster.BOMVNo}', BOMDate = '{VmProductionMaster.BOMDate:yyyy-MM-dd}',"
                : "BOMVNo = NULL,BOMDate = NULL,");
            cmdTxt.Append(
                $" VDate= '{VmProductionMaster.VDate:yyyy-MM-dd}', VMiti = N'{VmProductionMaster.VMiti}',FinishedGoodsId= {VmProductionMaster.FinishedGoodsId},FinishedGoodsQty= {VmProductionMaster.FinishedGoodsQty},");
            cmdTxt.Append(
                VmProductionMaster.Costing > 0 ? $"Costing = {VmProductionMaster.Costing}," : "Costing = 100,");
            cmdTxt.Append(!string.IsNullOrWhiteSpace(VmProductionMaster.Machine)
                ? $"Machine = '{VmProductionMaster.Machine}',"
                : "Machine = NULL,");
            cmdTxt.Append(VmProductionMaster.DepartmentId > 0
                ? $"DepartmentId = {VmProductionMaster.DepartmentId},"
                : "DepartmentId = NULL,");
            cmdTxt.Append(VmProductionMaster.CostCenterId > 0
                ? $"CostCenterId = {VmProductionMaster.CostCenterId},"
                : "CostCenterId= NULL,");
            cmdTxt.Append(VmProductionMaster.Amount > 0 ? $"Amount = {VmProductionMaster.Amount}," : "Amount= 0,");
            cmdTxt.Append(!string.IsNullOrWhiteSpace(VmProductionMaster.InWords)
                ? $"InWords = N'{VmProductionMaster.InWords}',"
                : "InWords = NULL,");
            cmdTxt.Append(!string.IsNullOrWhiteSpace(VmProductionMaster.Remarks.Trim())
                ? $"Remarks = N'{VmProductionMaster.Remarks.Trim().Replace("'", "''")}',"
                : "Remarks= NULL,");
            cmdTxt.Append(!string.IsNullOrWhiteSpace(VmProductionMaster.OrderNo)
                ? $"OrderNo = '{VmProductionMaster.OrderNo}',OrderDate = '{VmProductionMaster.OrderDate}'"
                : "OrderNo= NULL,OrderDate= NULL");
            cmdTxt.Append($" WHERE VoucherNo = N'{VmProductionMaster.VoucherNo}';\n");
        }

        if (!_tagStrings.Contains(actionTag))
        {
            var irow = 0;
            if (VmProductionMaster.GetView != null)
            {
                cmdTxt.Append(
                    " INSERT INTO INV.Production_Details (VoucherNo, SerialNo, ProductId, GodownId, CostCenterId, BOMNo, BOMSNo, BOMQty, IssueNo, IssueSNo, OrderNo, OrderSNo, AltQty, AltUnitId, Qty, UnitId, Rate, Amount, Narration, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
                cmdTxt.Append(" VALUES \n");
                foreach (DataGridViewRow roDetail in VmProductionMaster.GetView.Rows)
                {
                    cmdTxt.Append(
                        $"('{VmProductionMaster.VoucherNo}',{roDetail.Cells["GTxtSNo"].Value},{roDetail.Cells["GTxtProductId"].Value},");
                    cmdTxt.Append(roDetail.Cells["GTxtGodownId"].Value.GetInt() > 0
                        ? $"{roDetail.Cells["GTxtGodownId"].Value},"
                        : "NULL,");
                    cmdTxt.Append(roDetail.Cells["GTxtCostCenterId"].Value.GetInt() > 0
                        ? $"{roDetail.Cells["GTxtCostCenterId"].Value},"
                        : "NULL,");
                    cmdTxt.Append(roDetail.Cells["GTxtBOMVno"].Value.IsValueExits()
                        ? $"'{roDetail.Cells["GTxtBOMVno"].Value}', {roDetail.Cells["GTxtBOMSNo"].Value},"
                        : "NULL,0,");
                    cmdTxt.Append($"{roDetail.Cells["GTxtActualQty"].Value.GetDecimal()},");
                    cmdTxt.Append(roDetail.Cells["GTxtIssueVno"].Value.IsValueExits()
                        ? $"'{roDetail.Cells["GTxtIssueVno"].Value}', {roDetail.Cells["GTxtIssueSNo"].Value},"
                        : "NULL,0,");
                    cmdTxt.Append(roDetail.Cells["GTxtOrderNo"].Value.IsValueExits()
                        ? $"'{roDetail.Cells["GTxtOrderNo"].Value}', {roDetail.Cells["GTxtOrderSNo"].Value},"
                        : "NULL,0,");
                    cmdTxt.Append($"{roDetail.Cells["GTxtAltQty"].Value.GetDecimal()},");
                    cmdTxt.Append(roDetail.Cells["GTxtAltUOMId"].Value.GetInt() > 0
                        ? $"{roDetail.Cells["GTxtAltUOMId"].Value},"
                        : "NULL,");
                    cmdTxt.Append($"{roDetail.Cells["GTxtQty"].Value.GetDecimal(true)},");
                    cmdTxt.Append(roDetail.Cells["GTxtUOMId"].Value.GetInt() > 0
                        ? $"{roDetail.Cells["GTxtUOMId"].Value},"
                        : "NULL,");
                    cmdTxt.Append($"{roDetail.Cells["GTxtRate"].Value.GetDecimal()},");
                    cmdTxt.Append($"{roDetail.Cells["GTxtAmount"].Value.GetDecimal()},");
                    cmdTxt.Append(roDetail.Cells["GTxtNarration"].Value.IsValueExits()
                        ? $"'{roDetail.Cells["GTxtNarration"].Value}',"
                        : "NULL,");
                    cmdTxt.Append("NULL,NULL,NULL,NULL,GETDATE(),1");
                    cmdTxt.Append(irow < VmProductionMaster.GetView.Rows.Count - 1 ? "),\n" : ");\n");
                    irow++;
                }
            }
        }

        var result = SqlExtensions.ExecuteNonTrans(cmdTxt.ToString());
        if (result <= 0) return result;
        if (actionTag is "SAVE")
        {
            // UpdateDocumentNumbering("IBOM", VmProductionMaster.Schema);
        }

        if (actionTag != "DELETE") ProductionStockPosting();
        if (actionTag == "DELETE") return result;
        //VmStockImage.VoucherNo = VmProductionMaster.VoucherNo;
        //VmStockImage.PAttachment1 = VmProductionMaster.PAttachment1;
        //VmStockImage.PAttachment2 = VmProductionMaster.PAttachment2;
        //VmStockImage.PAttachment3 = VmProductionMaster.PAttachment3;
        //VmStockImage.PAttachment4 = VmProductionMaster.PAttachment4;
        //VmStockImage.PAttachment5 = VmProductionMaster.PAttachment5;
        UpdateProductionImage("INV.Production_Master", "VoucherNo");
        return result;
    }

    public int SaveStockAdjustment(string actionTag)
    {
        var cmdTxt = new StringBuilder();
        if (_tagStrings.Contains(actionTag) || actionTag.Equals("UPDATE"))
        {
            cmdTxt.Append(
                $"DELETE AMS.StockDetails WHERE Voucher_No='{VmStaMaster.StockAdjust_No}' AND Module='SA' \n");
            if (!actionTag.Equals("REVERSE"))
                cmdTxt.Append($"DELETE AMS.STA_Details WHERE StockAdjust_No='{VmStaMaster.StockAdjust_No}'; \n");
            if (actionTag.Equals("DELETE"))
                cmdTxt.Append($"DELETE AMS.STA_Master WHERE StockAdjust_No='{VmStaMaster.StockAdjust_No}'; \n");
        }

        if (actionTag.Equals("SAVE"))
        {
            cmdTxt.Append(
                " INSERT INTO AMS.STA_Master(StockAdjust_No, VDate, VMiti, Vtime, DepartmentId, BarCode, PhyStockNo, Posting, Export, ReconcileBy, AuditBy, AuditDate, AuthorizeBy, AuthorizeDate, AuthorizeRemarks, PostedBy, PostedDate, Remarks, Status, EnterBy, EnterDate, PrintValue, IsReverse, CancelBy, CancelDate, CancelReason, BranchId, CompanyUnitId, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
            cmdTxt.Append(
                $" VALUES (N'{VmStaMaster.StockAdjust_No}', '{VmStaMaster.VDate:yyyy-MM-dd}','{VmStaMaster.VMiti}',GETDATE(),");
            cmdTxt.Append(VmStaMaster.DepartmentId > 0 ? $"{VmStaMaster.DepartmentId}," : "NUll,");
            cmdTxt.Append(VmStaMaster.BranchId > 0 ? $"'{VmStaMaster.BranchId}'," : "NULL,");
            cmdTxt.Append(VmStaMaster.PhyStockNo.IsValueExits() ? $" N'{VmStaMaster.PhyStockNo}'," : "NULL,");
            cmdTxt.Append("NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,");
            cmdTxt.Append(VmStaMaster.Remarks.IsValueExits() ? $" N'{VmStaMaster.Remarks}'," : "NULL,");
            cmdTxt.Append($" NULL, N'{ObjGlobal.LogInUser}', GETDATE(),0,0,NULL,NULL,NULL,{ObjGlobal.SysBranchId},");
            cmdTxt.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
            cmdTxt.Append(
                $" {ObjGlobal.SysFiscalYearId},NULL,NULL,NULL,NULL,GETDATE(),{VmStaMaster.SyncRowVersion.GetDecimal(true)} ); \n");
        }
        else if (actionTag.Equals("UPDATE"))
        {
            cmdTxt.Append(
                $" UPDATE AMS.STA_Master SET VDate= '{VmStaMaster.VDate:yyyy-MM-dd}', VMiti = '{VmStaMaster.VMiti}',");
            cmdTxt.Append(VmStaMaster.DepartmentId > 0
                ? $"DepartmentId = {VmStaMaster.DepartmentId},"
                : "DepartmentId = NUll,");
            cmdTxt.Append(VmStaMaster.BranchId > 0
                ? $"BranchId = '{VmStaMaster.BranchId}',"
                : $"BranchId = {ObjGlobal.SysBranchId},");
            cmdTxt.Append(VmStaMaster.PhyStockNo.IsValueExits()
                ? $"PhyStockNo =  N'{VmStaMaster.PhyStockNo}',"
                : "PhyStockNo = NULL,");
            cmdTxt.Append(VmStaMaster.Remarks.IsValueExits()
                ? $" Remarks = N'{VmStaMaster.Remarks}' ,"
                : "Remarks = NULL ,");
            cmdTxt.Append(" IsSynced = 0 ");
            cmdTxt.Append($" WHERE StockAdjust_No = N'{VmStaMaster.StockAdjust_No}'; \n");
        }

        if (!actionTag.Equals("DELETE") && !actionTag.Equals("REVERSE"))
            if (VmStaMaster.GetView is { RowCount: > 0 })
            {
                cmdTxt.Append(
                    " INSERT INTO AMS.STA_Details(StockAdjust_No, Sno, ProductId, GodownId, DepartmentId, AdjType, AltQty, AltUnitId, Qty, UnitId, AltStockQty, StockQty, Rate, NetAmount, AddDesc, StConvRatio, PhyStkNo, PhyStkSno) \n");
                cmdTxt.Append(" VALUES \n");
                foreach (DataGridViewRow dr in VmStaMaster.GetView.Rows)
                {
                    var godownId = dr.Cells["GTxtGodownId"].Value.GetInt();
                    var altUnitId = dr.Cells["GTxtAltUOMId"].Value.GetInt();
                    var unitId = dr.Cells["GTxtUOMId"].Value.GetInt();
                    var altQty = dr.Cells["GTxtAltQty"].Value.GetDecimal();
                    var qty = dr.Cells["GTxtQty"].Value.GetDecimal();
                    var rate = dr.Cells["GTxtRate"].Value.GetDecimal();
                    var amount = dr.Cells["GTxtAmount"].Value.GetDecimal();

                    cmdTxt.Append($"('{VmStaMaster.StockAdjust_No}',");
                    cmdTxt.Append($"{dr.Cells["GTxtSNo"].Value.GetInt()},");
                    cmdTxt.Append($"{dr.Cells["GTxtProductId"].Value.GetLong()},");
                    cmdTxt.Append(godownId > 0 ? $"{godownId}," : "NULL,");
                    cmdTxt.Append(VmStaMaster.DepartmentId > 0 ? $"{VmStaMaster.DepartmentId}," : "NULL,");
                    cmdTxt.Append(dr.Cells["GTxtType"].Value.Equals("In") ? "'I'," : "'O',");
                    cmdTxt.Append(altQty > 0 ? $"{altQty}," : "0,");
                    cmdTxt.Append(altUnitId > 0 ? $"{altUnitId}," : "NULL,");
                    cmdTxt.Append(qty > 0 ? $"{qty}," : "0,");
                    cmdTxt.Append(unitId > 0 ? $"{unitId}," : "NULL,");
                    cmdTxt.Append(altQty > 0 ? $"{altQty}," : "0,");
                    cmdTxt.Append(qty > 0 ? $"{qty}," : "0,");
                    cmdTxt.Append(rate > 0 ? $"{rate}," : "0,");
                    cmdTxt.Append(amount > 0 ? $"{amount}," : "0,");
                    cmdTxt.Append(dr.Cells["GTxtNarration"].Value.IsValueExits()
                        ? $"N'{dr.Cells["GTxtNarration"].Value}',"
                        : "NULL,");
                    cmdTxt.Append("0,NULL,NULL");
                    cmdTxt.Append(VmStaMaster.GetView.RowCount - 1 == VmStaMaster.GetView.Rows.IndexOf(dr)
                        ? " ); \n"
                        : "), \n");
                }
            }

        var result = SqlExtensions.ExecuteNonTrans(cmdTxt.ToString());
        if (result == 0) return result;
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncStockAdjustmentAsync(actionTag));
        }
        StockAdjustmentStockPosting();
        return result;
    }

    public async Task<int> SyncStockAdjustmentAsync(string actionTag)
    {
        //sync
        try
        {
            var configParams =
                DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!configParams.Success || configParams.Model.Item1 == null)
                //MessageBox.Show(@"Error fetching local-origin information. " + configParams.ErrorMessage,
                //    configParams.ResultType.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //configParams.ShowErrorDialog();
                return 1;

            var injectData = new DbSyncRepoInjectData
            {
                ExternalConnectionString = null,
                DateTime = DateTime.Now,
                LocalOriginId = configParams.Model.Item1,
                LocalCompanyUnitId = ObjGlobal.SysCompanyUnitId,
                Username = ObjGlobal.LogInUser,
                LocalConnectionString = GetConnection.ConnectionString,
                LocalBranchId = ObjGlobal.SysBranchId,
                ApiConfig = DataSyncHelper.GetConfig()
            };
            var deleteUrl = string.Empty;
            var getUrl = string.Empty;
            var insertUrl = string.Empty;
            var updateUrl = string.Empty;

            getUrl = @$"{configParams.Model.Item2}StockAdjustment/GetStockAdjustmentById";
            insertUrl = @$"{configParams.Model.Item2}StockAdjustment/InsertStockAdjustment";
            updateUrl = @$"{configParams.Model.Item2}StockAdjustment/UpdateStockAdjustment";
            deleteUrl = @$"{configParams.Model.Item2}StockAdjustment/DeleteStockAdjustmentAsync?id=" +
                        VmStaMaster.StockAdjust_No;

            var apiConfig = new SyncApiConfig
            {
                BaseUrl = configParams.Model.Item2,
                Apikey = configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = getUrl,
                InsertUrl = insertUrl,
                UpdateUrl = updateUrl,
                DeleteUrl = deleteUrl
            };

            DataSyncHelper.SetConfig(apiConfig);
            injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(injectData);

            var staRepo = DataSyncProviderFactory.GetRepository<STA_Master>(DataSyncManager.GetGlobalInjectData());
            var sta = new STA_Master
            {
                StockAdjust_No = VmStaMaster.StockAdjust_No,
                VDate = Convert.ToDateTime(VmStaMaster.VDate.ToString("yyyy-MM-dd")),
                VMiti = VmStaMaster.VMiti,
                Vtime = DateTime.Now,
                DepartmentId = VmStaMaster.DepartmentId > 0 ? VmStaMaster.DepartmentId : null,
                BarCode = VmStaMaster.BarCode,
                PhyStockNo = VmStaMaster.PhyStockNo.IsValueExits() ? VmStaMaster.PhyStockNo : null,
                Posting = null,
                Export = null,
                ReconcileBy = null,
                AuditBy = null,
                AuditDate = null,
                AuthorizeBy = null,
                AuthorizeDate = null,
                AuthorizeRemarks = null,
                PostedBy = null,
                PostedDate = null,
                Remarks = VmStaMaster.Remarks.IsValueExits() ? VmStaMaster.Remarks : null,
                Status = null,
                EnterBy = ObjGlobal.LogInUser,
                EnterDate = DateTime.Now,
                PrintValue = 0,
                IsReverse = false,
                CancelBy = null,
                CancelDate = null,
                CancelReason = null,
                BranchId = ObjGlobal.SysBranchId,
                CompanyUnitId = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null,
                FiscalYearId = ObjGlobal.SysFiscalYearId,
                SyncRowVersion = VmStaMaster.SyncRowVersion
            };
            var staDetails = VmStaMaster.DetailsList;

            if (!actionTag.Equals("DELETE") && !actionTag.Equals("REVERSE"))
                if (VmStaMaster.GetView is { RowCount: > 0 })
                    foreach (DataGridViewRow dr in VmStaMaster.GetView.Rows)
                    {
                        var godownId = dr.Cells["GTxtGodownId"].Value.GetInt();
                        var altUnitId = dr.Cells["GTxtAltUOMId"].Value.GetInt();
                        var unitId = dr.Cells["GTxtUOMId"].Value.GetInt();
                        var altQty = dr.Cells["GTxtAltQty"].Value.GetDecimal();
                        var qty = dr.Cells["GTxtQty"].Value.GetDecimal();
                        var rate = dr.Cells["GTxtRate"].Value.GetDecimal();
                        var amount = dr.Cells["GTxtAmount"].Value.GetDecimal();

                        var details = new STA_Details
                        {
                            StockAdjust_No = VmStaMaster.StockAdjust_No,
                            Sno = dr.Cells["GTxtSNo"].Value.GetInt(),
                            ProductId = dr.Cells["GTxtProductId"].Value.GetLong(),
                            GodownId = godownId > 0 ? godownId : null,
                            DepartmentId = VmStaMaster.DepartmentId > 0 ? VmStaMaster.DepartmentId : null,
                            AdjType = dr.Cells["GTxtType"].Value.Equals("In") ? "I" : "O",
                            AltQty = altQty > 0 ? altQty : 0,
                            AltUnitId = altUnitId > 0 ? altUnitId : null,
                            Qty = qty > 0 ? qty : 0,
                            UnitId = unitId > 0 ? unitId : null,
                            AltStockQty = altQty,
                            StockQty = qty > 0 ? qty : 0,
                            Rate = rate > 0 ? rate : 0,
                            NetAmount = amount > 0 ? amount : 0,
                            AddDesc = dr.Cells["GTxtNarration"].Value.IsValueExits()
                                ? dr.Cells["GTxtNarration"].Value.ToString()
                                : null,
                            StConvRatio = 0,
                            PhyStkNo = null,
                            PhyStkSno = null
                        };
                        staDetails.Add(details);
                    }

            sta.DetailsList = staDetails;
            var result = actionTag.ToUpper() switch
            {
                "SAVE" => await staRepo?.PushNewAsync(sta)!,
                "NEW" => await staRepo?.PushNewAsync(sta)!,
                "UPDATE" => await staRepo?.PutNewAsync(sta)!,
                //"REVERSE" => await salesChallanRepo?.PutNewAsync(pc),
                //"DELETE" => await salesChallanRepo?.DeleteNewAsync(),
                _ => await staRepo?.PushNewAsync(sta)!
            };
            if (result.Value)
            {
                var query = $@"
                UPDATE AMS.STA_Master SET IsSynced = 1 WHERE StockAdjust_No = '{VmStaMaster.StockAdjust_No}'";
                await SqlExtensions.ExecuteNonTransAsync(query);
            }

            return 1;
        }
        catch (Exception ex)
        {
            return 1;
        }
    }
    
    public int SaveRawMaterialReturn(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag is "UPDATE" or "DELETE" or "REVERSE")
        {
            cmdString.Append(
                $"DELETE AMS.StockDetails WHERE Module='BOM' AND Voucher_No='{VmBomMaster.VoucherNo}'; \n");
            if (actionTag != "REVERSE")
                cmdString.Append($"DELETE INV.BOM_Details WHERE VoucherNo ='{VmBomMaster.VoucherNo}'; \n");
            if (actionTag is "DELETE")
                cmdString.Append($"DELETE INV.BOM_Master WHERE VoucherNo ='{VmBomMaster.VoucherNo}'; \n");
        }

        if (actionTag is "SAVE" or "NEW")
        {
            cmdString.Append(
                "INSERT INTO INV.BOM_Master(VoucherNo, VDate, VMiti, VTime, FinishedGoodsId, FinishedGoodsQty, DepartmentId, CostCenterId, Amount, InWords, Remarks, IsAuthorized, AuthorizeBy, AuthDate, IsReconcile, ReconcileBy, ReconcileDate, IsPosted, PostedBy, PostedDate, OrderNo, OrderDate, EnterBy, EnterDate, BranchId, CompanyUnitId, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)\n");
            cmdString.Append("VALUES \n");
            cmdString.Append(
                $"(N'{VmBomMaster.VoucherNo}', '{VmBomMaster.VDate:yyyy-MM-dd}', N'{VmBomMaster.VMiti}', GETDATE(), {VmBomMaster.FinishedGoodsId}, {VmBomMaster.FinishedGoodsQty},");
            cmdString.Append(VmBomMaster.DepartmentId > 0 ? $"{VmBomMaster.DepartmentId}," : "NULL,");
            cmdString.Append(VmBomMaster.CostCenterId > 0 ? $"{VmBomMaster.CostCenterId}," : "NULL,");
            cmdString.Append(VmBomMaster.Amount > 0 ? $"{VmBomMaster.Amount}," : "0,");
            cmdString.Append(VmBomMaster.InWords.IsValueExits() ? $"N'{VmBomMaster.InWords}'," : "NULL,");
            cmdString.Append(VmBomMaster.Remarks.IsValueExits()
                ? $"N'{VmBomMaster.Remarks.Trim().Replace("'", "''")}',"
                : "NULL,");
            cmdString.Append("0, NULL, NULL, 0, NULL, NULL, 0, NULL, NULL,");
            cmdString.Append(VmBomMaster.OrderNo.IsValueExits()
                ? $"'{VmBomMaster.OrderNo}','{VmBomMaster.OrderDate}',"
                : "NULL,NULL,");
            cmdString.Append($"N'{ObjGlobal.LogInUser}', GETDATE(), {ObjGlobal.SysBranchId},");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"{ObjGlobal.SysCompanyUnitId}," : "NULL,");
            cmdString.Append($"{ObjGlobal.SysFiscalYearId},");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
            cmdString.Append($"{VmBomMaster.SyncRowVersion});\n");
        }
        else if (actionTag is "UPDATE")
        {
            cmdString.Append("UPDATE INV.BOM_Master SET \n");
            cmdString.Append(
                $" VDate= '{VmBomMaster.VDate:yyyy-MM-dd}', VMiti = N'{VmBomMaster.VMiti}',FinishedGoodsId= {VmBomMaster.FinishedGoodsId},FinishedGoodsQty= {VmBomMaster.FinishedGoodsQty},");
            cmdString.Append(VmBomMaster.DepartmentId > 0
                ? $"DepartmentId = {VmBomMaster.DepartmentId},"
                : "DepartmentId = NULL,");
            cmdString.Append(VmBomMaster.CostCenterId > 0
                ? $"CostCenterId = {VmBomMaster.CostCenterId},"
                : "CostCenterId= NULL,");
            cmdString.Append(VmBomMaster.Amount > 0 ? $"Amount = {VmBomMaster.Amount}," : "Amount= 0,");
            cmdString.Append(VmBomMaster.InWords.IsValueExits()
                ? $"InWords = N'{VmBomMaster.InWords}',"
                : "InWords = NULL,");
            cmdString.Append(VmBomMaster.Remarks.IsValueExits()
                ? $"Remarks = N'{VmBomMaster.Remarks.Trim().Replace("'", "''")}',"
                : "Remarks= NULL,");
            cmdString.Append(VmBomMaster.OrderNo.IsValueExits()
                ? $"OrderNo = '{VmBomMaster.OrderNo}',OrderDate = '{VmBomMaster.OrderDate}'"
                : "OrderNo= NULL,OrderDate= NULL");
            cmdString.Append($" WHERE VoucherNo = N'{VmBomMaster.VoucherNo}';\n");
        }

        if (actionTag != "DELETE" && actionTag != "REVERSE")
        {
            var iRow = 0;
            if (VmBomMaster.GetView != null)
            {
                cmdString.Append(
                    " INSERT INTO INV.BOM_Details(VoucherNo, SerialNo, ProductId, GodownId, CostCenterId, OrderNo, OrderSNo, AltQty, AltUnitId, Qty, UnitId, Rate, Amount, Narration, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn,SyncBaseId,SyncRowVersion) \n");
                cmdString.Append("VALUES \n");
                foreach (DataGridViewRow roDetail in VmBomMaster.GetView.Rows)
                {
                    cmdString.Append(
                        $"('{VmBomMaster.VoucherNo}',{roDetail.Cells["GTxtSNo"].Value.GetDecimal()},{roDetail.Cells["GTxtProductId"].Value},");
                    cmdString.Append(roDetail.Cells["GTxtGodownId"].Value.GetInt() > 0
                        ? $"{roDetail.Cells["GTxtGodownId"].Value},"
                        : "NULL,");
                    cmdString.Append(roDetail.Cells["GTxtCostCenterId"].Value.GetInt() > 0
                        ? $"{roDetail.Cells["GTxtCostCenterId"].Value},"
                        : "NULL,");
                    cmdString.Append(roDetail.Cells["GTxtOrderNo"].Value.IsValueExits()
                        ? $"'{roDetail.Cells["GTxtOrderNo"].Value}',"
                        : "NULL,");
                    cmdString.Append(
                        $"{roDetail.Cells["GTxtOrderSNo"].Value.GetInt()},{roDetail.Cells["GTxtAltQty"].Value.GetDecimal()},");
                    cmdString.Append(roDetail.Cells["GTxtAltUOMId"].Value.GetInt() > 0
                        ? $"{roDetail.Cells["GTxtAltUOMId"].Value},"
                        : "NULL,");
                    cmdString.Append($"{roDetail.Cells["GTxtQty"].Value.GetDecimal(true)},");
                    cmdString.Append(roDetail.Cells["GTxtUOMId"].Value.GetInt() > 0
                        ? $"{roDetail.Cells["GTxtUOMId"].Value},"
                        : "NULL,");
                    cmdString.Append(
                        $"{roDetail.Cells["GTxtRate"].Value.GetDecimal()},{roDetail.Cells["GTxtAmount"].Value.GetDecimal()},");
                    cmdString.Append(roDetail.Cells["GTxtNarration"].Value.IsValueExits()
                        ? $"'{roDetail.Cells["GTxtNarration"].Value}',"
                        : "NULL,");
                    cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
                    cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
                    cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
                    cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
                    cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
                    cmdString.Append($"{VmBomMaster.SyncRowVersion} ");
                    cmdString.Append(iRow < VmBomMaster.GetView.Rows.Count - 1 ? "),\n" : ");\n");
                    iRow++;
                }
            }
        }

        var result = SqlExtensions.ExecuteNonTrans(cmdString.ToString());
        if (result <= 0) return result;
        if (actionTag is "SAVE")
        {
            //UpdateDocumentNumbering("BOM", VmBomMaster.Schema);
        }
        return result;
    }

    public int UpdateDocumentNumbering(string module, string schema)
    {
        var cmdString = new StringBuilder();
        cmdString.Append(
            $"UPDATE AMS.DocumentNumbering SET DocCurr = ISNULL(DocCurr,0) +1 WHERE DocModule = N'{module}' AND DocDesc = N'{schema}'; ");
        return SqlExtensions.ExecuteNonQuery(
            cmdString.ToString());
    }

    public int UpdateProductionImage(string tableName, string filterColumn)
    {
        //if (VmStockImage.PAttachment1 == null && VmStockImage.PAttachment2 == null &&
        //    VmStockImage.PAttachment3 == null &&
        //    VmStockImage.PAttachment4 == null && VmStockImage.PAttachment5 == null) return 0;
        //var cmdTxt = new StringBuilder();
        //cmdTxt.Append(
        //    $"UPDATE {tableName} SET PAttachment1 = @PImage1,PAttachment2 = @PImage2,PAttachment3 = @PImage3,PAttachment4 = @PImage4,PAttachment5 = @PImage5 WHERE {filterColumn} = N'{VmStockImage.VoucherNo}';");
        //var cmd2 = new SqlCommand(cmdTxt.ToString(), GetConnection.ReturnConnection());
        //cmd2.Parameters.Add("@PImage1", SqlDbType.Image).Value = VmStockImage.PAttachment1;
        //cmd2.Parameters.Add("@PImage2", SqlDbType.Image).Value = VmStockImage.PAttachment2;
        //cmd2.Parameters.Add("@PImage3", SqlDbType.Image).Value = VmStockImage.PAttachment3;
        //cmd2.Parameters.Add("@PImage4", SqlDbType.Image).Value = VmStockImage.PAttachment4;
        //cmd2.Parameters.Add("@PImage5", SqlDbType.Image).Value = VmStockImage.PAttachment5;
        //var nonQuery = cmd2.ExecuteNonQuery();
        return 0;
    }

    #endregion -------------- AUDIT_LOG --------------


    //RETURN DATA TABLE

    #region --------------- RETURN DATATABLE ---------------

    public DataTable GetProductOpeningVoucher(string branchId, string companyUnitId, string fiscalYearId)
    {
        var cmdString = new StringBuilder();
        cmdString.AppendLine($@"
			Select Voucher_No VoucherNo,CASE WHEN (SELECT sc.EnglishDate FROM AMS.SystemSetting sc) = 0 then  OP_Miti else  CONVERT(VARCHAR(10), OP_Date, 101)  end VoucherDate,PName,Convert(decimal(18,2),Qty) Qty,Convert(decimal(18,2),Rate) Rate,Convert(decimal(18,2), Amount) Amount from AMS.ProductOpening OP left outer Join AMS.Product P on OP.Product_Id=P.PID
			where(OP.CBranch_Id = '{branchId}' or OP.CBranch_Id is NUll) and(OP.CUnit_Id = '{companyUnitId}' or OP.CUnit_Id is NUll) and(OP.FiscalYearId is NUll or OP.FiscalYearId = '{fiscalYearId}')
			order By Voucher_No,Serial_No ");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString()).Tables[0];
    }

    public DataTable CheckVoucherNo(string tableName, string tableVoucherNo, string inputVoucherNo)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($" SELECT * FROM {tableName} WHERE {tableVoucherNo}= '{inputVoucherNo}'");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString()).Tables[0];
    }

    public DataTable GetAutoConsumptionProductInvoiceDetails()
    {
        var cmd = $@"
            SELECT  CAST(0 AS BIT) CheckBox, p.PID ProductId,CONVERT(NVARCHAR(10),sm.Invoice_Date,103) VoucherDate, sm.Invoice_Miti VoucherMiti, sd.SB_Invoice VoucherNo, p.PName ProductDesc,FORMAT(sd.Qty,'{ObjGlobal.SysQtyCommaFormat}') Qty,FORMAT(sd.Rate,'{ObjGlobal.SysAmountCommaFormat}') Rate,FORMAT(sd.B_Amount,'{ObjGlobal.SysAmountCommaFormat}') Amount
            FROM AMS.SB_Details                sd
                 LEFT OUTER JOIN AMS.Product   p ON p.PID=sd.P_Id
                 LEFT OUTER JOIN AMS.SB_Master sm ON sm.SB_Invoice=sd.SB_Invoice
            WHERE sd.MaterialPost='N' AND p.PID IN (SELECT FinishedGoodsId FROM INV.BOM_Master);";
        var result = SqlExtensions.ExecuteDataSet(cmd);
        return result.Tables[0];
    }

    public DataTable GetProductBomDetails(long productId)
    {
        var cmd = $@"
            SELECT VoucherNo FROM INV.BOM_Master WHERE FinishedGoodsId = {productId}  ";
        var result = SqlExtensions.ExecuteDataSet(cmd).Tables[0];
        return result;
    }

    #endregion --------------- RETURN DATATABLE ---------------

    
    //RETURN DATA SET

    #region --------------- RETURN DATASET ---------------

    public DataSet GetStockAdjustmentVoucherDetails(string voucherNo)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($@"
			SELECT sm.StockAdjust_No, sm.VDate, sm.Vtime, sm.VMiti, sm.DepartmentId, d.DName, sm.BarCode, sm.PhyStockNo, sm.Posting, sm.Export, sm.PostedBy, sm.ReconcileBy, sm.AuditBy, sm.AuthorizeBy, sm.AuditDate, sm.PostedDate, sm.AuthorizeDate, sm.Remarks, sm.Status, sm.EnterBy, sm.EnterDate, sm.PrintValue, sm.AuthorizeRemarks, sm.BranchId, sm.CompanyUnitId, sm.FiscalYearId
            FROM AMS.STA_Master                 sm
                 LEFT OUTER JOIN AMS.Department d ON sm.DepartmentId=d.DId
            WHERE sm.StockAdjust_No='{voucherNo}';

			SELECT sd.StockAdjust_No, sd.Sno, p.PShortName, sd.ProductId, p.PName, sd.GodownId, g.GName, sd.AdjType, sd.AltQty, sd.AltUnitId, pu.UnitCode AltUnit, sd.Qty, sd.UnitId, pu1.UnitCode Unit, sd.AltStockQty, sd.StockQty, sd.Rate, sd.NetAmount, sd.AddDesc, sd.DepartmentId, d.DName, sd.StConvRatio, sd.PhyStkNo, sd.PhyStkSno
            FROM AMS.STA_Details                 sd
                 LEFT OUTER JOIN AMS.Product     p ON sd.ProductId=p.PID
                 LEFT OUTER JOIN AMS.Godown      g ON sd.GodownId=g.GID
                 LEFT OUTER JOIN AMS.ProductUnit pu ON sd.AltUnitId=pu.UID
                 LEFT OUTER JOIN AMS.ProductUnit pu1 ON sd.UnitId=pu1.UID
                 LEFT OUTER JOIN AMS.Department  d ON sd.DepartmentId=d.DId
            WHERE sd.StockAdjust_No='{voucherNo}'
            ORDER BY sd.StockAdjust_No, sd.Sno; ");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString());
    }

    public DataSet GetBomVoucherDetails(string voucherNo)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($@"
            SELECT bm.VoucherNo, bm.VDate, bm.VMiti, bm.VTime, bm.FinishedGoodsId, p.PName, bm.FinishedGoodsQty, pu.UnitCode, bm.DepartmentId, d.DName, bm.CostCenterId, cc.CCName, bm.Amount, bm.InWords, bm.Remarks, bm.IsAuthorized, bm.AuthorizeBy, bm.AuthDate, bm.IsReconcile, bm.ReconcileBy, bm.ReconcileDate, bm.IsPosted, bm.PostedBy, bm.PostedDate, bm.OrderNo, bm.OrderDate, bm.EnterBy, bm.EnterDate, bm.BranchId, bm.CompanyUnitId, bm.FiscalYearId
            FROM INV.BOM_Master                  bm
                 LEFT OUTER JOIN AMS.Product     p ON bm.FinishedGoodsId=p.PID
                 LEFT OUTER JOIN AMS.Department  d ON bm.DepartmentId=d.DId
                 LEFT OUTER JOIN AMS.CostCenter  cc ON bm.CostCenterId=cc.CCId
                 LEFT OUTER JOIN AMS.ProductUnit pu ON p.PUnit=pu.UID
            WHERE bm.VoucherNo='{voucherNo}';
            SELECT bd.VoucherNo, bd.SerialNo, bd.ProductId, p.PName, p.PShortName, bd.GodownId, g.GName, bd.CostCenterId, cc.CCName, bd.OrderNo, bd.OrderSNo, bd.AltQty, bd.AltUnitId, pu.UnitCode AltUnitCode, bd.Qty, bd.UnitId, pu1.UnitCode UnitCode, bd.Rate, bd.Amount, bd.Narration
            FROM INV.BOM_Details                 bd
                 LEFT OUTER JOIN AMS.Product     p ON bd.ProductId=p.PID
                 LEFT OUTER JOIN AMS.Godown      g ON bd.GodownId=g.GID
                 LEFT OUTER JOIN AMS.CostCenter  cc ON bd.CostCenterId=cc.CCId
                 LEFT OUTER JOIN AMS.ProductUnit pu ON bd.AltUnitId=pu.UID
                 LEFT OUTER JOIN AMS.ProductUnit pu1 ON bd.UnitId=pu1.UID
            WHERE bd.VoucherNo='{voucherNo}'; ");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString());
    }

    #endregion --------------- RETURN DATASET ---------------

    
    //STOCK TRANSACTION POSTING

    #region --------------- STOCK TRANSACTION POSTING ---------------

    public int ProductOpeningStockPosting()
    {
        var cmdString = @"Delete from AMS.StockDetails Where Module in ('POB','N','OB')";
        cmdString += VmProductOpening.Voucher_No.IsValueExits() ? $@" AND Voucher_No='{VmProductOpening.Voucher_No}'" : " ";
        cmdString += @"
			INSERT INTO AMS.StockDetails(Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_Id, Subledger_Id, Agent_Id, Department_Id1, Department_Id2, Department_Id3, Department_Id4, Currency_Id, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_Id, RoomNo, EnterBy, EnterDate, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, Branch_Id, CmpUnit_Id, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT 'POB' Module, Voucher_No, Serial_No,NULL PurRefVno,po.OP_Date Voucher_Date,po.OP_Miti Voucher_Miti,po.Enter_Date Voucher_Time,NULL Ledger_Id,NULL Subledger_Id,NULL Agent_Id,po.Cls1 Department_Id1,po.Cls2 Department_Id2,po.Cls3 Department_Id3,po.Cls4 Department_Id4, Currency_Id, Currency_Rate, Product_Id, Godown_Id,NULL CostCenter_Id, AltQty,po.AltUnit AltUnit_Id, Qty,po.QtyUnit Unit_Id,po.AltQty AltStockQty,po.Qty StockQty,0 FreeQty,NULL FreeUnit_Id,0 StockFreeQty,0 ConvRatio,0 ExtraFreeQty,NULL ExtraFreeUnit_Id,0 ExtraStockFreeQty, Rate,po.Amount BasicAmt, 0 TermAmt,po.Amount NetAmt,0 BillTermAmt,0 TaxRate,0 TaxableAmt, po.LocalAmount DocVal,0 ReturnVal,po.LocalAmount StockVal,0 AddStockVal,NULL PartyInv,'I' EntryType,NULL AuthBy,NULL AuthDate,NULL RecoBy,NULL RecoDate,NULL Counter_Id,NULL RoomNo,po.Enter_By EnterBy,po.Enter_Date EnterDate,0 Adj_Qty,NULL Adj_VoucherNo,NULL Adj_Module,0 SalesRate,po.CBranch_Id Branch_Id,po.CUnit_Id CmpUnit_Id, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
            FROM AMS.ProductOpening AS po  ";
        cmdString += VmProductOpening.Voucher_No.IsValueExits() ? $@" WHERE Voucher_No ='{VmProductOpening.Voucher_No}'" : " ";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }

    public int ProductionStockPosting()
    {
        var cmdString = string.Empty;
        cmdString += "DELETE AMS.StockDetails where Module='IBOM'";
        cmdString += VmProductionMaster.VoucherNo.IsValueExits()
            ? $" AND Voucher_No = '{VmProductionMaster.VoucherNo}'"
            : "";
        cmdString += @"
            INSERT INTO AMS.StockDetails(Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_Id, Subledger_Id, Agent_Id, Department_Id1, Department_Id2, Department_Id3, Department_Id4, Currency_Id, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_Id, RoomNo, EnterBy, EnterDate, Branch_Id, CmpUnit_Id, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId,SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT Production.Module, Production.Voucher_No, Production.Serial_No, Production.PurRefVno, Production.Voucher_Date, Production.Voucher_Miti, Production.Voucher_Time, Production.Ledger_Id, Production.Subledger_Id, Production.Agent_Id, Production.Department_Id1, Production.Department_Id2, Production.Department_Id3, Production.Department_Id4, Production.Currency_Id, Production.Currency_Rate, Production.Product_Id, Production.Godown_Id, Production.CostCenter_Id, Production.AltQty, Production.AltUnit_Id, Production.Qty, Production.Unit_Id, Production.AltStockQty, Production.StockQty, Production.FreeQty, Production.FreeUnit_Id, Production.StockFreeQty, Production.ConvRatio, Production.ExtraFreeQty, Production.ExtraFreeUnit_Id, Production.ExtraStockFreeQty, Production.Rate, Production.BasicAmt, Production.TermAmt, Production.NetAmt, Production.BillTermAmt, Production.TaxRate, Production.TaxableAmt, Production.DocVal, Production.ReturnVal, Production.StockVal, Production.AddStockVal, Production.PartyInv, Production.EntryType, Production.AuthBy, Production.AuthDate, Production.RecoBy, Production.RecoDate, Production.Counter_Id, Production.RoomNo, Production.EnterBy, Production.EnterDate, Production.Branch_Id, Production.CmpUnit_Id, Production.Adj_Qty, Production.Adj_VoucherNo, Production.Adj_Module, Production.SalesRate, Production.FiscalYearId, Production.SyncBaseId, Production.SyncGlobalId, Production.SyncOriginId, Production.SyncCreatedOn, Production.SyncLastPatchedOn, Production.SyncRowVersion
            FROM (SELECT 'IBOM' Module, pm.VoucherNo Voucher_No, 1 Serial_No, pm.OrderNo PurRefVno, pm.VDate Voucher_Date, pm.VMiti Voucher_Miti, pm.VTime Voucher_Time, NULL Ledger_Id, NULL Subledger_Id, NULL Agent_Id, pm.DepartmentId Department_Id1, NULL Department_Id2, NULL Department_Id3, NULL Department_Id4, 1 Currency_Id, 1 Currency_Rate, pm.FinishedGoodsId Product_Id, NULL Godown_Id, pm.CostCenterId CostCenter_Id, 0 AltQty, NULL AltUnit_Id, pm.FinishedGoodsQty Qty, p.PUnit Unit_Id, 0 AltStockQty, pm.FinishedGoodsQty StockQty, 0 FreeQty, NULL FreeUnit_Id, 0 StockFreeQty, 0 ConvRatio, 0 ExtraFreeQty, NULL ExtraFreeUnit_Id, 0 ExtraStockFreeQty, CAST(pm.Amount/pm.FinishedGoodsQty AS DECIMAL(18, 6)) Rate, pm.Amount BasicAmt, 0 TermAmt, pm.Amount NetAmt, 0 BillTermAmt, 0 TaxRate, 0 TaxableAmt, pm.Amount DocVal, 0 ReturnVal, pm.Amount StockVal, 0 AddStockVal, 0 PartyInv, 'I' EntryType, pm.AuthorizeBy AuthBy, pm.AuthDate AuthDate, pm.ReconcileBy RecoBy, pm.ReconcileDate RecoDate, NULL Counter_Id, NULL RoomNo, pm.EnterBy EnterBy, pm.EnterDate EnterDate, pm.BranchId Branch_Id, pm.CompanyUnitId CmpUnit_Id, 0 Adj_Qty, 0 Adj_VoucherNo, 0 Adj_Module, 0 SalesRate, pm.FiscalYearId FiscalYearId,pm.SyncBaseId, pm.SyncGlobalId, pm.SyncOriginId, pm.SyncCreatedOn, pm.SyncLastPatchedOn, pm.SyncRowVersion
	            FROM INV.Production_Master pm
	            LEFT OUTER JOIN AMS.Product p ON pm.FinishedGoodsId = p.PID";
        cmdString += VmProductionMaster.VoucherNo.IsValueExits()
            ? $" WHERE pm.VoucherNo = '{VmProductionMaster.VoucherNo}'"
            : "";
        cmdString += @"
            UNION ALL
            SELECT 'IBOM' Module, pd.VoucherNo Voucher_No, pd.SerialNo Serial_No, pm.OrderNo PurRefVno, pm.VDate Voucher_Date, pm.VMiti Voucher_Miti, pm.VTime Voucher_Time, NULL Ledger_Id, NULL Subledger_Id, NULL Agent_Id, pm.DepartmentId Department_Id1, NULL Department_Id2, NULL Department_Id3, NULL Department_Id4, 1 Currency_Id, 1 Currency_Rate, pd.ProductId Product_Id, pd.GodownId Godown_Id, pd.CostCenterId CostCenter_Id, 0 AltQty, pd.AltUnitId AltUnit_Id, pd.Qty Qty, pd.UnitId Unit_Id, pd.AltQty AltStockQty, pd.Qty StockQty, 0 FreeQty, NULL FreeUnit_Id, 0 StockFreeQty, 0 ConvRatio, 0 ExtraFreeQty, NULL ExtraFreeUnit_Id, 0 ExtraStockFreeQty, Rate Rate, pd.Amount BasicAmt, 0 TermAmt, pd.Amount NetAmt, 0 BillTermAmt, 0 TaxRate, 0 TaxableAmt, pd.Amount DocVal, 0 ReturnVal, pd.Amount StockVal, 0 AddStockVal, 0 PartyInv, 'O' EntryType, pm.AuthorizeBy AuthBy, pm.AuthDate AuthDate, pm.ReconcileBy RecoBy, pm.ReconcileDate RecoDate, NULL Counter_Id, NULL RoomNo, pm.EnterBy EnterBy, pm.EnterDate EnterDate, pm.BranchId Branch_Id, pm.CompanyUnitId CmpUnit_Id, 0 Adj_Qty, 0 Adj_VoucherNo, 0 Adj_Module, 0 SalesRate, pm.FiscalYearId FiscalYearId,pm.SyncBaseId, pm.SyncGlobalId, pm.SyncOriginId, pm.SyncCreatedOn, pm.SyncLastPatchedOn, pm.SyncRowVersion
	            FROM INV.Production_Details pd
	            LEFT OUTER JOIN INV.Production_Master pm ON pd.VoucherNo = pm.VoucherNo";
        cmdString += VmProductionMaster.VoucherNo.IsValueExits()
            ? $" WHERE pd.VoucherNo = '{VmProductionMaster.VoucherNo}'"
            : "";
        cmdString += " ) Production ";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }

    public int StockAdjustmentStockPosting()
    {
        try
        {
            var cmdString = @"Delete from AMS.StockDetails Where Module='SA'";
            if (VmStaMaster.StockAdjust_No != null && !string.IsNullOrEmpty(VmStaMaster.StockAdjust_No))
                cmdString += $"and Voucher_No ='{VmStaMaster.StockAdjust_No}'; \n";
            cmdString += @"
				INSERT INTO AMS.StockDetails (Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_Id, Subledger_Id,Agent_Id, Department_Id1, Department_Id2, Department_Id3, Department_Id4, Currency_Id, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_Id, RoomNo, EnterBy, EnterDate, Branch_Id, CmpUnit_Id, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId)
				SELECT 'SA' [Module], sd.StockAdjust_No [Voucher_No],sd.Sno,NULL PurRefVno, sm1.VDate [Voucher_Date],sm1.VMiti [Voucher_Miti],Vtime [Voucher_Time],NULL [Ledger_Id],NULL [Subledger_Id],NULL [Agent_Id],sd.DepartmentId [Department_Id1],NULL [Department_Id2],NULL [Department_Id3],NULL [Department_Id4],1 [Currency_Id],1 [Currency_Rate],sd.ProductId [Product_Id],sd.GodownId [Godown_Id],NULL [CostCenter_Id],[AltQty],sd.AltUnitId [AltUnit_Id],[Qty],sd.UnitId [Unit_Id],[AltStockQty],[StockQty],0 [FreeQty],NULL [FreeUnit_Id],0 [StockFreeQty],0 [ConvRatio],0 [ExtraFreeQty],NULL [ExtraFreeUnit_Id],0 [ExtraStockFreeQty],[Rate],sd.NetAmount [BasicAmt],0 [TermAmt],sd.NetAmount  [NetAmt],0 [BillTermAmt],0 [TaxRate],0 [TaxableAmt],sd.NetAmount  [DocVal],0 [ReturnVal],sd.NetAmount  [StockVal],0 AddStockVal,0 [PartyInv],sd.AdjType [EntryType],NULL [AuthBy],NULL [AuthDate],NULL [RecoBy],NULL [RecoDate],NULL [Counter_Id],NULL [RoomNo],[EnterBy],[EnterDate],sm1.BranchId [Branch_Id],sm1.CompanyUnitId [CmpUnit_Id],0 [Adj_Qty],NULL [Adj_VoucherNo],NULL [Adj_Module],0 [SalesRate],[FiscalYearId]
				FROM AMS.STA_Details sd INNER JOIN AMS.STA_Master sm1 ON sd.StockAdjust_No=sm1.StockAdjust_No ";
            if (VmStaMaster.StockAdjust_No != null && !string.IsNullOrEmpty(VmStaMaster.StockAdjust_No))
                cmdString += $" WHERE sd.StockAdjust_No ='{VmStaMaster.StockAdjust_No}';";
            var iResult =
                SqlExtensions.ExecuteNonQuery(cmdString);
            return iResult;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            return 0;
        }
    }

    public int SaveBillOfMaterialsSetup()
    {
        throw new NotImplementedException();
    }

    #endregion --------------- STOCK TRANSACTION POSTING ---------------

    
    // OBJECT FOR THIS CLASS

    #region --------------- OBJECT FOR THIS CLASS ---------------

    private readonly string[] _tagStrings = ["DELETE", "REVERSE"];
    public string NumberSchema { get; set; }
    public string Module { get; set; }

    public ProductOpening VmProductOpening { get; set; } = new();
    public STA_Master VmStaMaster { get; set; } = new();
    public BOM_Master VmBomMaster { get; set; } = new();
    public Production_Master VmProductionMaster { get; set; } = new();
    public StockIssue_Master IssueMaster { get; set; } = new();

    #endregion --------------- OBJECT FOR THIS CLASS ---------------
}