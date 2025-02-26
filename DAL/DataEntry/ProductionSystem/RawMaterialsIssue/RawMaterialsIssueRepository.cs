using DatabaseModule.DataEntry.ProductionSystem.RawMaterialsIssue;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface.ProductionSystem.RawMaterialsIssue;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MrDAL.DataEntry.ProductionSystem.RawMaterialsIssue;

public class RawMaterialsIssueRepository : IRawMaterialsIssueRepository
{
    public RawMaterialsIssueRepository()
    {
        IssueMaster = new StockIssue_Master();
        DetailsList = new List<StockIssue_Details>();
    }

    // RETURN VALUE IN DATA TABLE
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
    public int SaveRawMaterialIssue(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag is "UPDATE" or "DELETE" or "REVERSE")
        {
            cmdString.Append($"DELETE AMS.StockDetails WHERE Module='MI' AND Voucher_No='{IssueMaster.VoucherNo}'; \n");

            if (actionTag != "REVERSE")
                cmdString.Append($"DELETE INV.StockIssue_Details WHERE VoucherNo ='{IssueMaster.VoucherNo}'; \n");
            if (actionTag is "DELETE")
                cmdString.Append($"DELETE INV.StockIssue_Master WHERE VoucherNo ='{IssueMaster.VoucherNo}'; \n");
        }

        if (actionTag is "SAVE" or "NEW")
        {
            cmdString.Append(
                "INSERT INTO INV.StockIssue_Master(VoucherNo, VoucherDate, VoucherMiti, VoucherTime, BOM_Vno, BOM_Date, BOM_Miti, DepartmentId, CostCenterId, FinishedGoodsId, AltQty, AltUnitId, Qty, UnitId, Factor, AdditionalAmount, Remarks, EnterBy, EnterDate, AuthorizeBy, AuthorizeDate, ReconcileBy, ReconcileDate, IsReverse, CancelBy, CancelDate, CancelReason, BranchId, CompanyUnitId, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
            cmdString.Append("VALUES \n");
            cmdString.Append(
                $"(N'{IssueMaster.VoucherNo}', '{IssueMaster.VoucherDate.GetSystemDate()}', N'{IssueMaster.VoucherMiti}', GETDATE(),");
            cmdString.Append(IssueMaster.BOM_Vno.IsValueExits()
                ? $"'{IssueMaster.BOM_Vno}','{IssueMaster.BOM_Date.GetSystemDate()}','{IssueMaster.BOM_Miti}',"
                : "NULL,NULL,NULL,");
            cmdString.Append(IssueMaster.DepartmentId > 0 ? $"{IssueMaster.DepartmentId}," : "NULL,");
            cmdString.Append(IssueMaster.CostCenterId > 0 ? $"{IssueMaster.CostCenterId}," : "NULL,");
            cmdString.Append(IssueMaster.FinishedGoodsId > 0 ? $"{IssueMaster.FinishedGoodsId}," : "NULL,");
            cmdString.Append($"{IssueMaster.AltQty.GetDecimal()},");
            cmdString.Append(IssueMaster.AltUnitId > 0 ? $"N'{IssueMaster.AltUnitId}'," : "NULL,");
            cmdString.Append($"{IssueMaster.Qty.GetDecimal()},");
            cmdString.Append(IssueMaster.UnitId > 0 ? $"N'{IssueMaster.AltUnitId}'," : "NULL,");
            cmdString.Append($"{IssueMaster.Factor.GetDecimal()},{IssueMaster.AdditionalAmount.GetDecimal()},");
            cmdString.Append(IssueMaster.Remarks.IsValueExits()
                ? $"N'{IssueMaster.Remarks.Trim().Replace("'", "''")}',"
                : "NULL,");
            cmdString.Append($"N'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append($"NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL,{ObjGlobal.SysBranchId},");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"{ObjGlobal.SysCompanyUnitId}," : "NULL,");
            cmdString.Append($"{ObjGlobal.SysFiscalYearId},");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
            cmdString.Append($"{IssueMaster.SyncRowVersion}); \n");
        }
        else if (actionTag is "UPDATE")
        {
            cmdString.Append("UPDATE INV.StockIssue_Master SET \n");
            cmdString.Append(
                $" VoucherDate= '{IssueMaster.VoucherDate.GetSystemDate()}', VoucherMiti = N'{IssueMaster.VoucherMiti}',");
            cmdString.Append(IssueMaster.BOM_Vno.IsValueExits()
                ? $"BOM_Vno = '{IssueMaster.BOM_Vno}',BOM_Date = '{IssueMaster.BOM_Date.GetSystemDate()}',BOM_Miti= '{IssueMaster.BOM_Miti}',"
                : "BOM_Vno = NULL,BOM_Date = NULL,BOM_Miti = NULL,");
            cmdString.Append(IssueMaster.DepartmentId > 0
                ? $"DepartmentId = {IssueMaster.DepartmentId},"
                : "DepartmentId = NULL,");
            cmdString.Append(IssueMaster.CostCenterId > 0
                ? $"CostCenterId = {IssueMaster.CostCenterId},"
                : "CostCenterId= NULL,");
            //cmdString.Append(IssueMaster.Amount > 0 ? $"Amount = {IssueMaster.Amount}," : "Amount= 0,");
            //cmdString.Append(IssueMaster.InWords.IsValueExits() ? $"InWords = N'{IssueMaster.InWords}'," : "InWords = NULL,");
            cmdString.Append(IssueMaster.Remarks.IsValueExits()
                ? $"Remarks = N'{IssueMaster.Remarks.Trim().Replace("'", "''")}',"
                : "Remarks= NULL,");
            //cmdString.Append(IssueMaster.OrderNo.IsValueExits() ? $"OrderNo = '{IssueMaster.OrderNo}',OrderDate = '{IssueMaster.OrderDate}'" : "OrderNo= NULL,OrderDate= NULL");
            cmdString.Append($" WHERE VoucherNo = N'{IssueMaster.VoucherNo}';\n");
        }

        if (actionTag != "DELETE" && actionTag != "REVERSE")
        {
            var iRow = 0;
            // if (IssueMaster.GetView != null)
            // {
            cmdString.Append(
                " INSERT INTO INV.StockIssue_Details(VoucherNo, SerialNo, ProductId, CostCenterId, GodownId, AltQty, AltUnitId, Qty, UnitId, Rate, Amount, ConvFactor, BomQty, Source) \n");
            cmdString.Append("VALUES \n");
            foreach (var roDetail in DetailsList)
            {
                cmdString.Append($"('{roDetail.VoucherNo}',{roDetail.SerialNo},{roDetail.ProductId},");
                cmdString.Append(roDetail.GodownId > 0 ? $"{roDetail.GodownId}," : "NULL,");
                cmdString.Append(roDetail.CostCenterId > 0 ? $"{roDetail.CostCenterId}," : "NULL,");
                cmdString.Append($"{roDetail.AltQty},");
                cmdString.Append(roDetail.AltUnitId > 0 ? $"{roDetail.AltUnitId}," : "NULL,");
                cmdString.Append($"{roDetail.Qty.GetDecimal(true)},");
                cmdString.Append(roDetail.UnitId > 0 ? $"{roDetail.UnitId}," : "NULL,");
                cmdString.Append($"{roDetail.Rate},{roDetail.Amount},");
                cmdString.Append($"{roDetail.ConvFactor},{roDetail.BomQty},{roDetail.Source}");
                // cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
                // cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
                // cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
                // cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
                // cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
                // cmdString.Append($"{IssueMaster.SyncRowVersion} ");
                cmdString.Append(iRow == DetailsList.Count ? "),\n" : ");\n");
                iRow++;
            }
            // }
        }

        var result = SqlExtensions.ExecuteNonTrans(cmdString.ToString());
        if (result <= 0) return result;
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
    public string NumberSchema { get; set; }
    public string Module { get; set; }
    public StockIssue_Master IssueMaster { get; set; }
    public List<StockIssue_Details> DetailsList { get; set; }

}