using DatabaseModule.DataEntry.ProductionSystem.Production;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface.ProductionSystem.Production;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MrDAL.DataEntry.ProductionSystem.Production;

public class ProductionRepository : IProductionRepository
{
    public ProductionRepository()
    {
        VmProductionMaster = new Production_Master();
        DetailsList = new List<Production_Details>();
    }

    // INSERT UPDATE DELETE
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
            // if (VmProductionMaster.GetView != null)
            // {
            cmdTxt.Append(
                " INSERT INTO INV.Production_Details (VoucherNo, SerialNo, ProductId, GodownId, CostCenterId, BOMNo, BOMSNo, BOMQty, IssueNo, IssueSNo, OrderNo, OrderSNo, AltQty, AltUnitId, Qty, UnitId, Rate, Amount, Narration, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
            cmdTxt.Append(" VALUES \n");
            foreach (var roDetail in DetailsList)
            {
                cmdTxt.Append($"('{roDetail.VoucherNo}',{roDetail.SerialNo},{roDetail.ProductId},");
                cmdTxt.Append(roDetail.GodownId > 0 ? $"{roDetail.GodownId}," : "NULL,");
                cmdTxt.Append(roDetail.CostCenterId > 0 ? $"{roDetail.CostCenterId}," : "NULL,");
                cmdTxt.Append(roDetail.BOMNo.IsValueExits() ? $"'{roDetail.BOMNo}', {roDetail.BOMSNo}," : "NULL,0,");
                cmdTxt.Append($"{roDetail.BOMQty},");
                cmdTxt.Append(roDetail.IssueNo.IsValueExits() ? $"'{roDetail.IssueNo}', {roDetail.IssueSNo}," : "NULL,0,");
                cmdTxt.Append(roDetail.OrderNo.IsValueExits() ? $"'{roDetail.OrderNo}', {roDetail.OrderSNo}," : "NULL,0,");
                cmdTxt.Append($"{roDetail.AltQty},");
                cmdTxt.Append(roDetail.AltUnitId > 0 ? $"{roDetail.AltUnitId}," : "NULL,");
                cmdTxt.Append($"{roDetail.Qty.GetDecimal(true)},");
                cmdTxt.Append(roDetail.UnitId > 0 ? $"{roDetail.UnitId}," : "NULL,");
                cmdTxt.Append($"{roDetail.Rate},");
                cmdTxt.Append($"{roDetail.Amount},");
                cmdTxt.Append(roDetail.Narration.IsValueExits() ? $"'{roDetail.Narration}'," : "NULL,");
                cmdTxt.Append("NULL,NULL,NULL,NULL,GETDATE(),1");
                cmdTxt.Append(irow == DetailsList.Count ? "),\n" : "),\n");
                irow++;
            }

            int index = cmdTxt.ToString().LastIndexOf(',');
            if (index >= 0)
            {
                cmdTxt.Remove(index, 1);
            }
            // }
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


    // RETURN VALUE IN DATA TABLE
    public DataSet GetProductionVoucherDetails(string voucherNo)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($@"
			SELECT bm.VoucherNo, bm.BOMVNo, bm.BOMDate, bm.VDate, bm.VMiti, bm.VTime, bm.FinishedGoodsId, p.PName, bm.FinishedGoodsQty, pu.UnitCode, bm.Costing, bm.Machine, bm.DepartmentId, d.DName, bm.CostCenterId, cc.CCName, bm.Amount, bm.InWords, bm.Remarks, bm.IsAuthorized, bm.AuthorizeBy, bm.AuthDate, bm.IsCancel, bm.IsReturn, bm.IsReconcile, bm.ReconcileBy, bm.ReconcileDate, bm.IsPosted, bm.PostedBy, bm.PostedDate, bm.IssueNo, bm.IssueDate, bm.OrderNo, bm.OrderDate, bm.EnterBy, bm.EnterDate, bm.BranchId, bm.CompanyUnitId, bm.FiscalYearId, bm.Source, [PAttachment1], [PAttachment2], [PAttachment3], [PAttachment4], [PAttachment5]
            FROM INV.Production_Master           bm
                 LEFT OUTER JOIN AMS.Product     p ON bm.FinishedGoodsId=p.PID
                 LEFT OUTER JOIN AMS.Department  d ON bm.DepartmentId=d.DId
                 LEFT OUTER JOIN AMS.CostCenter  cc ON bm.CostCenterId=cc.CCId
                 LEFT OUTER JOIN AMS.ProductUnit pu ON p.PUnit=pu.UID
            WHERE bm.VoucherNo='{voucherNo}';
            SELECT bd.VoucherNo, bd.SerialNo, bd.ProductId, p.PName, p.PShortName, bd.GodownId, g.GName, bd.CostCenterId, cc.CCName, bd.BOMNo, bd.BOMSNo, bd.IssueNo, bd.IssueSNo, bd.OrderNo, bd.OrderSNo, bd.AltQty, bd.AltUnitId, pu.UnitCode AltUnitCode, bd.BOMQty, bd.Qty, bd.UnitId, pu1.UnitCode UnitCode, bd.Rate, bd.Amount, bd.Narration
            FROM INV.Production_Details          bd
                 LEFT OUTER JOIN AMS.Product     p ON bd.ProductId=p.PID
                 LEFT OUTER JOIN AMS.Godown      g ON bd.GodownId=g.GID
                 LEFT OUTER JOIN AMS.CostCenter  cc ON bd.CostCenterId=cc.CCId
                 LEFT OUTER JOIN AMS.ProductUnit pu ON bd.AltUnitId=pu.UID
                 LEFT OUTER JOIN AMS.ProductUnit pu1 ON bd.UnitId=pu1.UID
            WHERE bd.VoucherNo='{voucherNo}';");
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

    // OBJECT FOR THIS FORM
    public Production_Master VmProductionMaster { get; set; }
    private readonly string[] _tagStrings = { "DELETE", "REVERSE" };
    public List<Production_Details> DetailsList { get; set; }


}