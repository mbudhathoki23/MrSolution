using DatabaseModule.DataEntry.ProductionSystem.BillOfMaterials;
using DatabaseModule.DataEntry.ProductionSystem.RawMaterialsIssue;
using MrDAL.Utility.Server;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MrDAL.DataEntry.TransactionClass;

public class ClsProduction
{
    #region --------------- ClsProduction --------------

    public ClsProduction()
    {
        BomMaster = new BOM_Master();
        BomDetails = new List<BOM_Details>();
        InvMaster = new StockIssue_Master();
        InvDetails = new List<StockIssue_Details>();
    }

    public int SaveBillOfMaterials()
    {

        return 0;
    }

    public int SaveFinishedGoods()
    {

        return 0;
    }

    public int SaveInventoryIssue()
    {
        return 0;
    }

    public DataSet GetFinishedGoodsReceived(string MemoNo = "", int BranchId = 0, int CUnitId = 0)
    {
        var cmdString = new StringBuilder();
        cmdString.Append(@" 
        SELECT BMM.*,GName,CCName from [AMS].[FGR_Master] BMM left join[AMS].[Godown] GN on BMM.Gdn_Id= GN.GID left join[AMS].[CostCenter] CC on BMM.CC_Id=CC.CCId where 1=1 ");
        if (!string.IsNullOrEmpty(MemoNo)) cmdString.Append($" and FGR_No='{MemoNo}'\n");
        if (BranchId != 0) cmdString.Append($" and CBranch_Id='{BranchId}'\n");
        if (CUnitId != 0) cmdString.Append($" and CUnit_Id='{CUnitId}'\n");

        cmdString.Append(
            " select BMD.*,GName,PName,CCName,UnitCode from [AMS].[FGR_Details] BMD left join[AMS].[Product] P on BMD.P_Id=P.PID left join[AMS].[ProductUnit] PU on BMD.Unit_Id=PU.UID left join[AMS].[Godown] GN on BMD.Gdn_Id= GN.GID left join[AMS].[CostCenter] CC on BMD.CC_Id=CC.CCId where 1=1  \n");
        if (!string.IsNullOrEmpty(MemoNo)) cmdString.Append($" and FGR_No='{MemoNo}'\n");
        return SqlExtensions.ExecuteDataSet(
            cmdString.ToString());
    }

    public DataSet GetInventoryIssue(string MemoNo = "", int BranchId = 0, int CUnitId = 0)
    {
        var cmdString = new StringBuilder();
        cmdString.Append("select BMM.*,GName,PName,CCName,UnitCode from [AMS].[Inv_Master] BMM \n");
        cmdString.Append(" left join[AMS].[Product] P on BMM.FGProduct_Id=P.PID \n");
        cmdString.Append(" left join[AMS].[ProductUnit] PU on BMM.Unit_Id=PU.UID \n");
        cmdString.Append(" left join[AMS].[Godown] GN on BMM.Gdn_Id= GN.GID \n");
        cmdString.Append(" left join[AMS].[CostCenter] CC on BMM.CC_Id=CC.CCId where 1=1 \n");
        if (!string.IsNullOrEmpty(MemoNo)) cmdString.Append($" and Inv_No='{MemoNo}'\n");
        if (BranchId != 0) cmdString.Append($" and CBranch_Id='{BranchId}'\n");
        if (CUnitId != 0) cmdString.Append($" and CUnit_Id='{CUnitId}'\n");

        cmdString.Append(" select BMD.*,GName,PName,CCName,UnitCode from [AMS].[Inv_Details] BMD \n");
        cmdString.Append(" left join[AMS].[Product] P on BMD.P_Id=P.PID \n");
        cmdString.Append(" left join[AMS].[ProductUnit] PU on BMD.Unit_Id=PU.UID \n");
        cmdString.Append(" left join[AMS].[Godown] GN on BMD.Gdn_Id= GN.GID \n");
        cmdString.Append(" left join[AMS].[CostCenter] CC on BMD.CC_Id=CC.CCId where 1=1 \n");
        if (!string.IsNullOrEmpty(MemoNo)) cmdString.Append($" and Inv_No='{MemoNo}'\n");

        return SqlExtensions.ExecuteDataSet(
            cmdString.ToString());
    }

    public DataSet GetBillOfMaterial(string MemoNo = "", int? BranchId = 0, int CUnitId = 0)
    {
        var cmdString = new StringBuilder();
        cmdString.Append("select BMM.*,GName,PName,CCName,UnitCode from [AMS].[BillOfMaterial_Master] BMM \n");
        cmdString.Append(" left join[AMS].[Product] P on BMM.FGProductId=P.PID \n");
        cmdString.Append(" left join[AMS].[ProductUnit] PU on BMM.UnitId=PU.UID \n");
        cmdString.Append(" left join[AMS].[Godown] GN on BMM.GdnId= GN.GID \n");
        cmdString.Append(" left join[AMS].[CostCenter] CC on BMM.CostCenterId=CC.CCId where 1=1 \n");
        if (!string.IsNullOrEmpty(MemoNo)) cmdString.Append($" and MemoNo='{MemoNo}'\n");
        if (BranchId != 0) cmdString.Append($" and BranchId='{BranchId}'\n");
        if (CUnitId != 0) cmdString.Append($" and CompanyUnitId='{CUnitId}'\n");

        cmdString.Append(" select BMD.*,GName,PName,CCName,UnitCode from [AMS].[BillOfMaterial_Details] BMD \n");
        cmdString.Append(" left join[AMS].[Product] P on BMD.productId=P.PID \n");
        cmdString.Append(" left join[AMS].[ProductUnit] PU on BMD.UnitId=PU.UID \n");
        cmdString.Append(" left join[AMS].[Godown] GN on BMD.GdnId= GN.GID \n");
        cmdString.Append(" left join[AMS].[CostCenter] CC on BMD.CostCenterId=CC.CCId where 1=1 \n");
        if (!string.IsNullOrEmpty(MemoNo)) cmdString.Append($" and MemoNo='{MemoNo}'\n");

        return SqlExtensions.ExecuteDataSet(
            cmdString.ToString());
    }

    public DataTable GetFinishedGoodsForSales(string salesBillNo, int salesSNo)
    {
        var cmdString = $"Select * from AMS.FGR_Master where SB_No='{salesBillNo}' and SB_SNo='{salesSNo}'";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetBomSalesProduct(int productId)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($"Select * from Ams.BillOfMaterial_Master where FGProductId='{productId}' \n");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString())
            .Tables[0];
    }

    public int SaveAutoConsumption()
    {
        return 0;
    }

    #endregion --------------- ClsProduction --------------

    // OBJECT FOR THIS FORM

    #region --------------- Global ---------------

    public BOM_Master BomMaster { get; set; }
    public StockIssue_Master InvMaster { get; set; }
    public List<BOM_Details> BomDetails { get; set; }
    public List<StockIssue_Details> InvDetails { get; set; }

    #endregion --------------- Global ---------------
}