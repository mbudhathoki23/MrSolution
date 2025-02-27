using DatabaseModule.Setup.SecurityRights;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Utility.Interface;
using MrDAL.Utility.Server;
using System.Data;

namespace MrDAL.Utility.PickList;

public class ClsTagList : ITagList
{
    #region --------------- General ledger ---------------

    public DataTable GetAccountGroupList()
    {
        var cmdString = $@"
			SELECT  CONVERT(bit, 0) IsCheck,ag.GrpId ParticularId,ag.GrpName Particular,ag.GrpCode ShortName FROM ams.AccountGroup ag
            WHERE ag.GrpId IN
            (
	            SELECT gl.GrpId FROM AMS.GeneralLedger gl
	            WHERE gl.GLID IN (SELECT gl.GLID FROM ams.AccountDetails ad WHERE ad.Branch_ID IN ({ObjGlobal.SysBranchId}) AND (ad.CmpUnit_ID IN ('{ObjGlobal.SysCompanyUnitId}') OR ad.CmpUnit_ID IS NULL) AND ad.FiscalYearId IN ({ObjGlobal.SysFiscalYearId}))
	            OR GL.GLID IN (SELECT sd.Ledger_ID FROM AMS.StockDetails sd where sd.Branch_ID IN ({ObjGlobal.SysBranchId}) AND (sd.CmpUnit_ID IN ('{ObjGlobal.SysCompanyUnitId}') OR sd.CmpUnit_ID IS NULL) AND sd.FiscalYearId IN ({ObjGlobal.SysFiscalYearId}))
	            OR gl.GLID IN (SELECT pm.Vendor_ID FROM ams.PO_Master pm)
	            OR gl.GLID IN (SELECT sm.Customer_Id FROM ams.SO_Master sm)
	            --OR gl.GLID IN (SELECT sm.Customer_ID FROM ams.SQ_Master sm)
            ) ORDER BY ag.GrpName";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetAccountSubGroupList(string groupId)
    {
        var cmdString = $@"
			SELECT CONVERT(bit, 0) [CHECK],gl.SubGrpId ParticularId,ISNULL(asg.SubGrpName,'NO SUBGROUP') PARTICULAR,asg.SubGrpCode ShortName  FROM AMS.AccountDetails ad
            INNER JOIN AMS.GeneralLedger gl ON ad.Ledger_ID = gl.GLID
            LEFT OUTER JOIN AMS.AccountSubGroup asg ON gl.SubGrpId = asg.SubGrpId
			WHERE ad.Branch_ID ='{ObjGlobal.SysBranchId}' AND ad.FiscalYearId ='{ObjGlobal.SysFiscalYearId}'";
        cmdString += groupId.IsValueExits() ? $" and ag.GrpId IN ({groupId})" : string.Empty;
        cmdString += @"
            GROUP BY gl.SubGrpId,asg.SubGrpName,asg.SubGrpCode
			ORDER BY SubGrpName ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetAllGeneralLedgerList(string category, string groupId, string subGroupId, string module)
    {
        var cmdString = @"
			SELECT CONVERT(bit, 0) IsCheck,gl.GLID ParticularId,gl.GLName Particular,GL.GLCODE ShortName,gl.PanNo,gl.GLType,AG.GrpName GROUPDESC,ASG.SubGrpName SUBGROUPDESC,gl.GLAddress,JA.AgentName SALESMAN
            FROM AMS.GeneralLedger gl
            INNER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
            LEFT OUTER JOIN AMS.AccountSubGroup asg ON gl.SubGrpId = asg.SubGrpId
            LEFT OUTER JOIN AMS.JUNIORAGENT AS JA ON GL.AGENTID = JA.AGENTID
            LEFT OUTER JOIN AMS.CURRENCY AS CU ON CU.CID=GL.CurrId
            WHERE 1=1 ";
        cmdString += category.ToUpper() switch
        {
            "CUSTOMER" => " AND GLTYPE IN ('Customer','Both') ",
            "VENDOR" => " AND GLTYPE IN ('Vendor','Both') ",
            "BOTH" => " AND GLTYPE IN ('Both') ",
            "CASH" => " AND GLTYPE IN ('Cash','Bank') ",
            "BANK" => " AND GLTYPE IN ('Bank','Cash') ",
            "OTHER" => " AND GLTYPE IN ('Other') ",
            _ => string.Empty
        };
        if (!string.IsNullOrEmpty(groupId)) cmdString += $" AND  gl.GrpId in ({groupId}) ";
        if (!string.IsNullOrEmpty(subGroupId)) cmdString += $" AND  gl.SubGrpId in ({subGroupId}) ";
        cmdString += module switch
        {
            "SQ" =>
                $" AND gl.GLID IN (SELECT sm.Customer_ID FROM AMS.SQ_Master sm WHERE sm.CBranch_Id IN ({ObjGlobal.SysBranchId}) AND sm.FiscalYearId IN ({ObjGlobal.SysFiscalYearId}) AND (sm.CUnit_Id ='{ObjGlobal.SysCompanyUnitId}' OR sm.CUnit_Id IS NULL)) ",
            "SO" =>
                $" AND gl.GLID IN (SELECT sm.Customer_Id FROM AMS.SO_Master sm) WHERE sm.CBranch_Id IN ({ObjGlobal.SysBranchId}) AND sm.FiscalYearId IN ({ObjGlobal.SysFiscalYearId}) AND (sm.CUnit_Id ='{ObjGlobal.SysCompanyUnitId}' OR sm.CUnit_Id IS NULL))",
            "PO" =>
                $" AND gl.GLID IN (SELECT pm.Vendor_ID FROM AMS.PO_Master pm WHERE pm.CBranch_Id IN ({ObjGlobal.SysBranchId}) AND pm.FiscalYearId IN ({ObjGlobal.SysFiscalYearId}) AND (pm.CUnit_Id ='{ObjGlobal.SysCompanyUnitId}' OR pm.CUnit_Id IS NULL)) ",
            _ => @" AND gl.GLID IN
                                    (
                                      SELECT Ledger.Ledger_Id FROM ( SELECT Ledger_Id FROM AMS.StockDetails sd
                                      UNION ALL
                                      SELECT Ledger_ID FROM AMS.AccountDetails ad
                                    ) Ledger
                               WHERE Ledger.Ledger_Id IS NOT NULL
                               GROUP BY Ledger.Ledger_Id ) "
        };
        cmdString += " ORDER BY gl.GLName ASC ; ";
        //WHERE sd.Branch_Id IN ({ObjGlobal.SysBranchId}) AND ( sd.CmpUnit_Id IN ('{ObjGlobal.SysCompanyUnitId}') OR sd.CmpUnit_Id IS NULL ) AND sd.FiscalYearId IN ({ObjGlobal.SysFiscalYearId})
        //WHERE ad.Branch_ID IN ({ObjGlobal.SysBranchId}) AND ( ad.CmpUnit_ID IN ('{ObjGlobal.SysCompanyUnitId}') OR ad.CmpUnit_ID IS NULL ) AND ad.FiscalYearId IN ({ObjGlobal.SysFiscalYearId})
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetSubLedgerList(string ledgerId, string module)
    {
        var cmdString = @"
            SELECT CONVERT(bit, 0) IsCheck,s.SLId ParticularId, s.SLName Particular, s.SLCode ShortName FROM ams.SubLedger s
            WHERE 1=1 ";
        cmdString += module switch
        {
            "SQ" => " AND s.SLId IN ( SELECT sm.Subledger_Id FROM AMS.SQ_Master SM)",
            "SO" => " AND s.SLId IN (SELECT sm.Subledger_Id FROM AMS.SO_Master sm) ",
            "PIN" => " AND s.SLId IN ( SELECT SM.Sub_Ledger FROM AMS.PIN_Master SM)",
            "PO" => "  AND s.SLId IN ( SELECT pm.Subledger_Id FROM AMS.PO_Master pm) ",
            _ =>
                @$"  AND s.SLId IN (SELECT ad.Subleder_ID FROM AMS.AccountDetails ad WHERE ad.Branch_ID IN ({ObjGlobal.SysBranchId}) AND (ad.CmpUnit_ID IN ('{ObjGlobal.SysCompanyUnitId}') OR ad.CmpUnit_ID IS NULL) AND ad.FiscalYearId IN ({ObjGlobal.SysFiscalYearId}) ) OR s.SLId IN (SELECT sd.Subledger_Id FROM ams.StockDetails sd where sd.Branch_ID IN ({ObjGlobal.SysBranchId}) AND (sd.CmpUnit_ID IN ('{ObjGlobal.SysCompanyUnitId}') OR sd.CmpUnit_ID IS NULL) AND sd.FiscalYearId IN ({ObjGlobal.SysFiscalYearId})) "
        };
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetEntryBranchList()
    {
        const string cmdString =
            " SELECT  CONVERT(bit, 0) IsCheck,b.Branch_ID ParticularId, b.Branch_Name Particular, b.Branch_Code ShortName FROM ams.Branch b \n";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetEntryFiscalYearList()
    {
        const string cmdString = @"
            SELECT CONVERT(bit, 0) IsCheck, FY_Id ParticularId, fy.BS_FY + ' - ' + fy.AD_FY ShortName, fy.Start_BSDate + ' / ' + CAST( CONVERT(VARCHAR, fy.Start_ADDate, 103) AS VARCHAR(10)) + ' - ' + fy.End_BSDate + ' / ' + CAST(  CONVERT(VARCHAR,fy.End_ADDate,103) AS VARCHAR) Particular FROM ams.FiscalYear fy
            WHERE fy.FY_Id IN(SELECT ad.FiscalYearId FROM ams.AccountDetails ad) OR fy.FY_Id IN(SELECT sd.FiscalYearId FROM ams.StockDetails sd)
            ORDER BY fy.BS_FY  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetEntryCompanyUnitList()
    {
        var cmdString = $@"
            SELECT CONVERT(bit, 0) IsCheck,cu.CmpUnit_ID ParticularId,cu.CmpUnit_Name Particular, cu.CmpUnit_Code ShortName
            FROM AMS.CompanyUnit cu
            WHERE cu.Branch_ID IN ({ObjGlobal.SysBranchId})";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    #endregion --------------- General ledger ---------------

    // PRODUCT DETAILS

    #region --------------- Product Details ---------------

    public DataTable GetProductGroupList()
    {
        const string cmdString = @"
            SELECT CONVERT(bit, 0) IsCheck, pg.PGrpId ParticularId,pg.GrpName Particular,pg.GrpCode ShortName FROM ams.ProductGroup pg
            WHERE pg.PGrpId IN
            (
                SELECT p.PGrpId FROM ams.Product p
                WHERE p.PID IN ( SELECT sd.Product_Id FROM AMS.StockDetails sd  )
                OR p.PID IN (SELECT sd.P_Id FROM AMS.SO_Details sd )
                OR p.PID IN (SELECT pd.P_Id FROM AMS.PO_Details pd)
                OR P.PID IN (SELECT pd.P_Id FROM AMS.PIN_Details pd)
            )";
        //where sd.Branch_ID IN ({ObjGlobal.SysBranchId}) AND (sd.CmpUnit_ID IN ('{ObjGlobal.SysCompanyUnitId}') OR sd.CmpUnit_ID IS NULL) AND sd.FiscalYearId IN ({ObjGlobal.SysFiscalYearId})
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetProductSubGroupList(string groupId)
    {
        var cmdString = $@"
			SELECT  CONVERT(bit, 0) IsCheck,psg.PSubGrpId ParticularId,psg.SubGrpName Particular,psg.ShortName ShortName FROM ams.ProductSubGroup psg
            WHERE psg.GrpId IN ({groupId}) ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetProductList(string groupId, string subGroupId, string module)
    {
        var cmdString = @"
			Select CONVERT(bit, 0) IsCheck,p.PID ParticularId,p.PName PARTICULAR,pu.UnitCode,ISNULL(pg.GrpName,'NO GROUP')  GrpName, ISNULL(psg.SubGrpName,'NO SUBGROUP') SubGrpName FROM AMS.Product p
            LEFT OUTER HASH JOIN AMS.ProductGroup pg ON pg.PGrpID=p.PGrpId
            LEFT OUTER HASH JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId=p.PSubGrpId
            LEFT OUTER JOIN AMS.ProductUnit pu ON pu.UID=p.PUnit
            WHERE 1=1 ";
        if (!string.IsNullOrEmpty(groupId)) cmdString += $" AND p.PGrpId in ({groupId}) ";
        if (!string.IsNullOrEmpty(subGroupId)) cmdString += $" AND p.PSubGrpId in ({subGroupId}) ";
        cmdString += module switch
        {
            "SQ" => " AND p.PID IN (SELECT sd.P_Id FROM AMS.SQ_Details sd) ",
            "SO" => " AND p.PID IN (SELECT sd.P_Id FROM AMS.SO_Details sd) ",
            "PO" => " AND p.PID IN(SELECT pd.P_Id FROM AMS.PO_Details pd) ",
            "PIN" => " AND P.PID IN(SELECT pd.P_Id FROM AMS.PIN_Details pd) ",
            "SB" => " AND P.PID IN(SELECT pd.P_Id FROM AMS.SB_Details pd) ",
            "PB" => " AND P.PID IN(SELECT pd.P_Id FROM AMS.PB_Details pd) ",
            _ => " AND p.PID IN(SELECT sd.Product_Id FROM AMS.StockDetails sd  )"
        };
        cmdString += " ORDER BY p.PName";
        //where sd.Branch_ID IN ({ObjGlobal.SysBranchId}) AND (sd.CmpUnit_ID IN ('{ObjGlobal.SysCompanyUnitId}') OR sd.CmpUnit_ID IS NULL) AND sd.FiscalYearId IN ({ObjGlobal.SysFiscalYearId})
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetEntryUserInfo(string module)
    {
        var cmdString = @"
			SELECT CONVERT(bit, 0) IsCheck, ui.User_Id,ua.EnterBy ShortName,ui.Full_Name Particular FROM
            (
	            SELECT EnterBy FROM AMS.AccountDetails
	            UNION ALL
	            SELECT EnterBy FROM AMS.StockDetails
            ) AS ua
            LEFT OUTER JOIN master.AMS.UserInfo ui ON ua.EnterBy = ui.User_Name
            GROUP BY ua.EnterBy,User_Id,ui.Full_Name ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    #endregion --------------- Product Details ---------------

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT ---------------

    public TagList TagList { get; set; } = new();
    public static string PlValue1 { get; set; } = string.Empty;
    public static string PlValue2 { get; set; } = string.Empty;
    public static string PlValue3 { get; set; } = string.Empty;
    public static string PlValue4 { get; set; } = string.Empty;
    public static string PlValue5 { get; set; } = string.Empty;

    #endregion --------------- OBJECT ---------------
}