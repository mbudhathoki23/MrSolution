using DatabaseModule.DataEntry.ProductionSystem.BillOfMaterials;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface.ProductionSystem.BillOfMaterials;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MrDAL.DataEntry.ProductionSystem.BillOfMaterials;

public class BillOfMaterialsRepository : IBillOfMaterialsRepository
{
    public BillOfMaterialsRepository()
    {
        VmBomMaster = new BOM_Master();
        DetailsList = new List<BOM_Details>();
    }

    // RETURN VALUE IN DATA TABLE
    public DataTable CheckVoucherNo(string tableName, string tableVoucherNo, string inputVoucherNo)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($" SELECT * FROM {tableName} WHERE {tableVoucherNo}= '{inputVoucherNo}'");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString()).Tables[0];
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

    // INSERT UPDATE DELETE
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
            // if (VmBomMaster.GetView is {RowCount: > 0})
            // {
            foreach (var roDetail in DetailsList)
            {
                // var ProductId = roDetail.ProductId.Value.GetLong();
                // if (ProductId is 0)
                // {
                //     continue;
                // }

                cmdString.Append(" INSERT INTO INV.BOM_Details(VoucherNo, SerialNo, ProductId, GodownId, CostCenterId, OrderNo, OrderSNo, AltQty, AltUnitId, Qty, UnitId, Rate, Amount, Narration, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn,SyncBaseId,SyncRowVersion) \n");
                cmdString.Append("VALUES \n");
                cmdString.Append($"('{roDetail.VoucherNo}',{roDetail.SerialNo},{roDetail.ProductId},");
                cmdString.Append(roDetail.GodownId > 0 ? $"{roDetail.GodownId}," : "NULL,");
                cmdString.Append(roDetail.CostCenterId > 0 ? $"{roDetail.CostCenterId}," : "NULL,");
                cmdString.Append(roDetail.OrderNo.IsValueExits() ? $"'{roDetail.OrderNo}'," : "NULL,");
                cmdString.Append($"{roDetail.OrderSNo},{roDetail.AltQty},");
                cmdString.Append(roDetail.AltUnitId > 0 ? $"{roDetail.AltUnitId}," : "NULL,");
                cmdString.Append($"{roDetail.Qty.GetDecimal(true)},");
                cmdString.Append(roDetail.UnitId > 0 ? $"{roDetail.UnitId}," : "NULL,");
                cmdString.Append($"{roDetail.Rate},{roDetail.Amount},");
                cmdString.Append(roDetail.Narration.IsValueExits() ? $"'{roDetail.Narration}'," : "NULL,");
                cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
                cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
                cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
                cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
                cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");

                cmdString.Append($"{VmBomMaster.SyncRowVersion} ");
                cmdString.Append(iRow == DetailsList.Count ? "),\n" : ");\n");
                iRow++;
            }
            // }
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
    public int UpdateDocumentNumbering(string module, string schema)
    {
        var cmdString = new StringBuilder();
        cmdString.Append(
            $"UPDATE AMS.DocumentNumbering SET DocCurr = ISNULL(DocCurr,0) +1 WHERE DocModule = N'{module}' AND DocDesc = N'{schema}'; ");
        return SqlExtensions.ExecuteNonQuery(
            cmdString.ToString());
    }

    // OBJECT FOR THIS FORM
    public BOM_Master VmBomMaster { get; set; }
    public string NumberSchema { get; set; }
    public string Module { get; set; }
    public List<BOM_Details> DetailsList { get; set; }

}