using MrDAL.Core.Extensions;
using MrDAL.Domains.VehicleManagement;
using MrDAL.Global.Common;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Utility.Interface;
using MrDAL.Utility.Server;
using System;
using System.Collections;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MrDAL.Utility.PickList;

public class ClsPickList : IPickList
{
   
    // HOSPITAL MASTER LIST

    #region --------------- HOSPITAL MASTER LIST ---------------

    public DataTable GetPatientList()
    {
        var cmdString = $@"
			SELECT pm.PaitentId LedgerId, pm.IPDId, pm.ShortName, LTRIM(RTRIM(Title)) + ' ' + pm.PaitentDesc Description, pm.TAddress, pm.AccountLedger, pm.ContactNo
			FROM HOS.PatientMaster pm
			WHERE pm.BranchId = '{ObjGlobal.SysBranchId}' AND (pm.CompanyUnitSetup = '{ObjGlobal.SysCompanyUnitId}' OR pm.CompanyUnitSetup IS NULL ) AND (pm.Status IS NULL OR pm.Status = 1)
			ORDER BY pm.ShortName DESC,pm.AccountLedger";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    #endregion --------------- HOSPITAL MASTER LIST ---------------

    // HOSPITAL MASTER LIST
    public DataTable GetDoctorTypeList(string actionTag = "", bool status = true)
    {
        var cmdString = @"
			SELECT DtID DoctorTypeId, DrTypeDesc TypeDescription, DrTypeShortName TypeShortName, Status
			FROM HOS.DoctorType
			WHERE 1=1 ";
        cmdString += status ? @" AND IsNUll(Status,0) = 1 " : "";
        cmdString += @"
			ORDER BY TypeDescription";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    // SETUP MASTER LIST

    #region --------------- SETUP MASTER LIST ---------------

    public DataTable GetCompanyList()
    {
        const string cmdString = @"
		select  GComp_Id CompanyId,Company_Name CompanyName FROM AMS.GlobalCompany gc
        ORDER BY gc.Company_Name ASC";
        return SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];
    }

    public DataTable GetCompanyUnitList(string actionTag)
    {
        const string cmdString =
            @"SELECT CmpUnit_ID, cu.CmpUnit_Name,cu.CmpUnit_Code,cu.Reg_Date,cu.ContactPerson,cu.ContactPersonAdd FROM AMS.CompanyUnit cu  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetBackupRestoreLogList(string actionTag)
    {
        var cmdString = actionTag switch
        {
            "RESTORE" =>
                @" SELECT br.DB_NAME DATABASE_NAME, br.COMPANY COMPANY, br.LOCATION PATH, br.USED_BY LOGIN_USER, br.USED_ON DATE , CASE WHEN br.ACTION = 'B' THEN 'BACKUP' WHEN br.ACTION = 'R' THEN 'RESTORE' END AS ACTION_TAKEN , br.SyncRowVersion ACTION_TIMES FROM MASTER.AMS.BR_LOG br WHERE br.ACTION = 'R' ORDER BY DATE DESC",
            "BACKUP" =>
                @"SELECT br.DB_NAME DATABASE_NAME, br.COMPANY COMPANY, br.LOCATION PATH, br.USED_BY LOGIN_USER, br.USED_ON DATE , CASE WHEN br.ACTION = 'B' THEN 'BACKUP' WHEN br.ACTION = 'R' THEN 'RESTORE' END AS ACTION_TAKEN, br.SyncRowVersion ACTION_TIMES  FROM MASTER.AMS.BR_LOG br WHERE br.ACTION = 'B' ORDER BY DATE DESC",
            _ => string.Empty
        };
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetUserList(string actionTag)
    {
        const string cmdString = @"
        SELECT User_Id,Full_Name,User_Name,Mobile_No FROM AMS.UserInfo WHERE USER_NAME NOT IN ('ADMIN','AMSADMIN','MRSOLUTION','MRDEMO')";
        return SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];
    }

    public DataTable GetUserRole(string actionTag)
    {
        const string cmdString = "SELECT Role_Id LedgerId, Role Description, Status status FROM MASTER.AMS.User_Role ";
        return SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];
    }

    #endregion --------------- SETUP MASTER LIST ---------------

    // TRANSACTION MASTER LIST

    #region --------------- TRANSACTION MASTER LIST ---------------

    public DataTable GetAccountGroupList(string actionTag, string category, int status = 0, bool isPrimary = false)
    {
        var cmdString =
            " Select ag.Schedule Schedule,GrpId LedgerId,GrpName Description,GrpCode ShortName,CASE WHEN PrimaryGrp='TA' THEN 'Trading Account' WHEN PrimaryGrp='PL' THEN 'Profit & Loss' WHEN ag.PrimaryGrp='BS' THEN 'Balance Sheet' ELSE ag.PrimaryGrp END ACGroup,CASE WHEN  GrpType='L' THEN 'Liabilities' WHEN ag.GrpType='A' THEN 'Assets' WHEN ag.GrpType = 'E' THEN 'Expenses' WHEN ag.GrpType =  'I' THEN 'Income' ELSE ag.GrpType end  GrpType FROM AMS.AccountGroup ag ";
        cmdString += " WHERE 1=1 ";
        if (category.IsValueExits())
            cmdString += category switch
            {
                "Customer" => " AND GrpType in ('Assets','A') ",
                "Vendor" => " AND GrpType in ( 'Liabilities','L') ",
                "Both" => " AND GrpType in ( 'Assets','Liabilities','A','L') ",
                _ => string.Empty
            };
        if (actionTag.IsValueExits() && actionTag == "DELETE")
            cmdString += " AND GrpId NOT IN (SELECT gl.GrpId FROM AMS.GeneralLedger gl) ";
        cmdString += isPrimary ? " AND PrimaryGroupId IS Null OR PrimaryGroupId = '' " : " ";
        cmdString += status.Equals(1) ? " and ag.Status=1" : " ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetAccountSubgroupList(string actionTag, string category, string listType, int status = 0, bool isPrimary = false)
    {
        var cmdString = "";
        if (listType == "ACCOUNTSUBGROUP")
        {
            cmdString =
               "Select SubGrpId LedgerId, SubGrpName Description,SubGrpCode ShortName, ASG.PrimaryGroupId Secondary_GrpId, A.GrpName SEC_ACGroup, AG.GrpName ACGroup,PrimarySubGroupId Secondary From AMS.AccountSubGroup ASG inner join AMS.AccountGroup AG on  AG.GrpID = ASG.GrpId LEFT OUTER JOIN AMS.AccountGroup A on  A.GrpID = ASG.PrimaryGroupId  Where 1=1 ";
            if (!string.IsNullOrEmpty(category) && !category.Equals("0")) cmdString += $" AND AG.GrpID='{category}' ";
            if (isPrimary) cmdString += " AND (PrimarySubGroupId IS Null OR PrimarySubGroupId = '') ";
            if (status > 0) cmdString += $" AND Status is {status} ";

        }
        else
        {
            cmdString =
                "Select SubGrpId LedgerId, SubGrpName Description,SubGrpCode ShortName, ASG.PrimaryGroupId Secondary_GrpId, A.GrpName SEC_ACGroup, AG.GrpName ACGroup,PrimarySubGroupId Secondary From AMS.AccountSubGroup ASG inner join AMS.AccountGroup AG on  AG.GrpID = ASG.GrpId LEFT OUTER JOIN AMS.AccountGroup A on  A.GrpID = ASG.PrimaryGroupId  Where 1=1 AND ASG.Status=1";
            if (!string.IsNullOrEmpty(category) && !category.Equals("0")) cmdString += $" AND AG.GrpID='{category}' ";
            if (isPrimary) cmdString += " AND (PrimarySubGroupId IS Null OR PrimarySubGroupId = '') ";
            if (status > 0) cmdString += $" AND Status is {status} ";
        }
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetMemberShipList(string actionTag)
    {
        var cmdString = @"
			SELECT MShipId LedgerId, MShipDesc Description, MShipShortName ShortName, MSS.PhoneNo Phone, MSS.EmailAdd Email, GLName Ledger, MemberDesc MemberType, MSS.MExpireDate EndDate, MSS.MValidDate StartDate
			 FROM AMS.MemberShipSetup MSS
				  LEFT OUTER JOIN AMS.GeneralLedger AS GL ON GL.GLID = MSS.LedgerId
				  LEFT OUTER JOIN AMS.MemberType AS MType ON MType.MemberTypeId = MSS.MemberTypeId
			 WHERE 1=1 ";
        if (actionTag.Equals("SAVE"))
            cmdString += @"
				AND(MSS.ActiveStatus IS NULL OR MSS.ActiveStatus = 1)  ";

        if (actionTag.Equals("DELETE"))
            cmdString += @"
				AND MemberId NOT IN  ( SELECT MShipId FROM AMS.AccountDetails WHERE MShipId IS NOT NULL)  ";
        cmdString += " ORDER BY MShipDesc;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetMemberTypeList(string actionTag)
    {
        var cmdString = @"
			SELECT MemberTypeId LedgerId, MemberDesc Description, MemberShortName ShortName, CAST(ISNULL(Discount, 0) AS DECIMAL(18, 2)) Discount
			FROM AMS.MemberType
			WHERE 1=1";
        if (actionTag.Equals("DELETE"))
            cmdString += @"
				AND MemberTypeId NOT IN (SELECT MemberId FROM AMS.MemberShipSetup) ";
        if (actionTag.Equals("SAVE"))
            cmdString += @"
				AND (ActiveStatus =1 OR ActiveStatus IS NULL) ";
        cmdString += @"ORDER BY MemberDesc ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetOpeningGeneralLedgerList(string actionTag, string category, string loginDate)
    {
        if (loginDate.IsBlankOrEmpty() || loginDate == " /  /    ") loginDate = DateTime.Now.GetSystemDate();
        var cmdString = @$"
			SELECT GLID LedgerId, GLName Description, gl.GLCode ShortName, PanNo, Balance Balance,CASE WHEN Balance > 0 THEN 'Dr' WHEN b.Balance < 0 THEN 'Cr' ELSE '' END AmtType, GrpName, ISNULL(AgentName, 'No Agent') AgentName, CAST(ISNULL(gl.CrLimit, 0) AS DECIMAL(18, 2)) CrLimit, ISNULL(gl.PhoneNo, 'No Number') PhoneNo, ISNULL(GLAddress, '') GLAddress
			FROM AMS.GeneralLedger gl
				 LEFT OUTER JOIN AMS.AccountGroup AG ON AG.GrpId=gl.GrpId
				 LEFT OUTER JOIN AMS.JuniorAgent AS JA ON JA.AgentId=gl.AgentId
				 LEFT OUTER JOIN (
						SELECT Ledger_ID, SUM(LocalDebit_Amt -LocalCredit_Amt) Balance FROM AMS.AccountDetails WHERE Voucher_Date <'{loginDate}'
						GROUP BY Ledger_ID
				 ) b ON b.Ledger_ID = gl.GLID
			WHERE AG.PrimaryGrp IN ('Balance Sheet', 'BS')
			ORDER BY gl.GLName ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetGeneralLedgerList(string actionTag, string category, string loginDate, bool status)
    {
        var cmdString = $@"
			SELECT gl.GLID LedgerId, gl.GLName Description, gl.GLCode ShortName, gl.ACCode LedgerCode,CAST(ABS(ob.Balance) AS DECIMAL(18,{ObjGlobal.SysAmountLength})) Balance, ";
        cmdString += $@"
			CASE WHEN ob.Balance>0 THEN 'Dr' WHEN ob.Balance<0 THEN 'Cr' ELSE '' END BType, gl.PanNo, gl.GLType, AG.GrpType, AG.PrimaryGrp, AG.GrpName GroupDesc, ASG.SubGrpName SubGroupDesc, gl.GLAddress, JA.AgentName SalesMan, CU.CCode Currency, gl.CrDays, gl.CrLimit, gl.CrTYpe, gl.PhoneNo,gl.Status
						FROM AMS.GeneralLedger gl
				 LEFT OUTER JOIN AMS.AccountGroup AS AG ON gl.GrpId=AG.GrpId
				 LEFT OUTER JOIN AMS.AccountSubGroup AS ASG ON gl.SubGrpId=ASG.SubGrpId
				 LEFT OUTER JOIN AMS.JuniorAgent AS JA ON gl.AgentId=JA.AgentId
				 LEFT OUTER JOIN AMS.Currency AS CU ON CU.CId=gl.CurrId
				 LEFT OUTER JOIN (
								   SELECT Opening.Ledger_ID, SUM(Balance) Balance
								   FROM (
										  SELECT ad.Ledger_ID, SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt) Balance
										  FROM AMS.AccountDetails ad
											   LEFT OUTER JOIN AMS.GeneralLedger gl ON ad.Ledger_ID=gl.GLID
											   LEFT OUTER JOIN AMS.AccountGroup AS AG ON gl.GrpId=AG.GrpId
										  WHERE AG.PrimaryGrp IN ('Balance Sheet', 'BS') AND ad.FiscalYearId<{ObjGlobal.SysFiscalYearId}
										  GROUP BY ad.Ledger_ID
										  UNION ALL
										  SELECT ad.Ledger_ID, SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt) Balance
										  FROM AMS.AccountDetails ad
										  WHERE ad.FiscalYearId={ObjGlobal.SysFiscalYearId}
										  GROUP BY ad.Ledger_ID
										) Opening
								   GROUP BY Opening.Ledger_ID
								 ) ob ON ob.Ledger_ID=gl.GLID
			WHERE 1=1 ";
        switch (category.ToUpper())
        {
            case "CUSTOMER":
                cmdString += @"AND gl.GLType in ('Customer', 'Both', 'Cash', 'Bank')";
                break;

            case "VENDOR":
                cmdString += @"AND gl.GLType in ('Vendor', 'Both', 'Cash', 'Bank')";
                break;

            case "CASH":
                cmdString += @"AND gl.GLType in ('Cash','Bank')";
                break;

            case "BANK":
                cmdString += @"AND gl.GLType in ('Bank','Cash')";
                break;

            case "OTHER":
                cmdString += @"AND gl.GLType in ('Other')";
                break;

            case "OPENING":
                cmdString += @" AG.PrimaryGrp IN ('Balance Sheet', 'BS') ";
                break;
        }

        if (actionTag.Equals("SAVE")) cmdString += @" AND (gl.Status = 1 OR gl.Status IS NULL) ";
        cmdString += @"
			ORDER BY gl.GLName; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetPartyLedgerList(string actionTag)
    {
        var cmdString = @"
			SELECT pl.LedgerId, pl.Particular Description, pl.ShortName, pl.LedgerCode, pl.PanNo, pl.GLType, pl.GrpType, pl.PrimaryGrp, pl.GroupDesc, pl.SubGroupDesc, pl.GLAddress, pl.SalesMan, pl.Currency, pl.CrDays, pl.CrLimit, pl.CrTYpe, pl.PhoneNo
			FROM (
				   SELECT gl.GLID LedgerId, gl.GLName Particular, gl.GLCode ShortName, gl.ACCode LedgerCode, gl.PanNo, gl.GLType, AG.GrpType, AG.PrimaryGrp, AG.GrpName GroupDesc, ASG.SubGrpName SubGroupDesc, gl.GLAddress, JA.AgentName SalesMan, CU.CCode Currency, gl.CrDays, gl.CrLimit, gl.CrTYpe, gl.PhoneNo
				   FROM AMS.GeneralLedger gl
						LEFT OUTER JOIN AMS.AccountGroup AS AG ON gl.GrpId=AG.GrpId
						LEFT OUTER JOIN AMS.AccountSubGroup AS ASG ON gl.SubGrpId=ASG.SubGrpId
						LEFT OUTER JOIN AMS.JuniorAgent AS JA ON gl.AgentId=JA.AgentId
						LEFT OUTER JOIN AMS.Currency AS CU ON CU.CId=gl.CurrId
				   WHERE gl.GLType IN ('Customer', 'Vendor', 'Both')
				   UNION ALL
				   SELECT ad.PartyLedger_Id LedgerId, ad.PartyName Particular, NULL ShortName, NULL LedgerCode, ad.Party_PanNo, 'PartyLedger' GLType, NULL GrpType, NULL PrimaryGrp, NULL GroupDesc, NULL SubGroupDesc, NULL GLAddress, NULL SalesMan, NULL Currency, NULL CrDays, NULL CrLimit, NULL CrTYpe, NULL PhoneNo
				   FROM AMS.AccountDetails ad
				   WHERE ad.PartyName<>'' AND ad.PartyName IS NOT NULL AND ad.PartyName NOT IN (SELECT gl.GLName FROM AMS.GeneralLedger gl)
				   GROUP BY ad.PartyLedger_Id, ad.PartyName, ad.Party_PanNo
				 )  pl
			ORDER BY pl.Particular;  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetSubLedgerList(string actionTag, string category, string loginDate)
    {
        return _objPick.GetSubLedger(actionTag, category, (ObjGlobal.SysQtyFormat.Length - 2).ToString(),
            ObjGlobal.SysAmountLength.ToString(), loginDate);
    }

    public DataTable GetMainAreaList(string actionTag, bool active)
    {
        var cmdString = @"
			SELECT MAreaId LedgerId, MAreaName Description, MAreaCode ShortName, MCountry
			FROM AMS.MainArea ma
			WHERE 1=1 ";
        if (actionTag.Equals("SAVE")) cmdString += @" AND (ma.Status = 1 OR ma.Status IS NULL) ";
        cmdString += @"
			ORDER BY MAreaName;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetAreaList(string actionTag)
    {
        var cmdString = @"
			SELECT AreaId LedgerId,AreaName Description,AreaCode ShortName,Country FROM AMS.Area a
			WHERE 1=1";
        cmdString += actionTag.Equals("SAVE") ? " AND ISNULL(a.Status,0) = 1" : "";
        cmdString += @"
			ORDER BY AreaName;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetJrAgentList(string actionTag)
    {
        var cmdString = @"
			SELECT AgentId LedgerId, AgentName Description, AgentCode ShortName, Address, PhoneNo, Commission
			FROM AMS.JuniorAgent
			WHERE 1=1 ";
        //    if (actionTag.Equals("DELETE"))
        //        cmdString += @"
        //AND AgentId NOT IN (SELECT AgentId FROM AMS.AccountDetails) ";
        if (actionTag.Equals("SAVE")) cmdString += "  AND (Status=1 OR Status IS NULL) ";
        cmdString += " ORDER BY AgentName; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetSrAgentList(string actionTag)
    {
        var cmdString = @"
			SELECT SAgentId LedgerId, SAgent Description, SAgentCode ShortName, Address, PhoneNo, Comm
			FROM AMS.SeniorAgent
			WHERE 1=1 ";
        //    if (actionTag.Equals("DELETE"))
        //        cmdString += @"
        //AND SAgent NOT IN (SELECT SAgent FROM AMS.JuniorAgent) ";
        if (actionTag.Equals("SAVE")) cmdString += "  AND (Status=1 OR Status IS NULL) ";
        cmdString += " ORDER BY SAgent; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetPurchaseTerm(string actionTag, string category, bool active)
    {
        var cmdString =
            "SELECT PT_ID LedgerId,PT_Name Description ,Order_No OrderNo,Module,PT_Type PType,GLName LedgerName,CAST(PT_Rate as DECIMAL(18,2)) Rate from AMS.PT_Term PT left outer join AMS.GeneralLedger GL on PT.Ledger = GL.GLID ";
        cmdString += category.ToUpper() == "A" ? "WHERE PT_Type = 'A'" : " ";
        cmdString += category.ToUpper() == "P" ? " WHERE PT_Condition = 'P' " : " ";
        cmdString += category.ToUpper() == "B" ? " WHERE PT_Condition = 'B'" : " ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetSalesTerm(string actionTag, string category, bool active)
    {
        var cmdString =
            "SELECT ST_ID LedgerId, Order_No OrderNo, CASE WHEN ST.Module = 'SB'  THEN 'Sales Invoice' WHEN ST.Module = 'SR' THEN 'Sales Return' END AS Module,ST_Name Description,CASE WHEN ST.ST_Type = 'G' THEN 'General' WHEN ST.ST_Type = 'A' THEN 'Additional' WHEN ST.ST_Type = 'R' THEN 'RoundOff' END AS ST_Type,Gl.GLID GLID,Gl.GLName LedgerName,CASE WHEN ST.ST_Basis = 'V' THEN 'Value' WHEN ST.ST_Basis = 'Q' THEN 'Quantity' END AS ST_Basis,ST.ST_Sign,CASE WHEN ST.ST_Condition = 'B' THEN 'Billwise' WHEN ST.ST_Condition = 'P' THEN 'Productwise' END AS  ST_Condition,ST.ST_Rate Rate,ST.ST_Profitability,ST.ST_Supess,ST.ST_Status FROM AMS.ST_Term ST LEFT OUTER JOIN AMS.GeneralLedger AS  Gl ON ST.Ledger = Gl.GLID  ";
        cmdString += category.ToUpper() == "A" ? "WHERE ST_Type = 'A' " : " ";
        cmdString += category.ToUpper() == "B" ? "WHERE ST_Condition = 'B' " : " ";
        cmdString += category.ToUpper() == "P" ? "WHERE ST_Condition = 'P' " : " ";
        cmdString += category.ToUpper() == "PB" ? "WHERE ST_Condition in( 'P', 'B')" : " ";
        cmdString += "ORDER BY ST.Order_No ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetFloor(string actionTag)
    {
        var cmdString = @"
			SELECT FloorId LedgerId,Description,ShortName,Type
			FROM AMS.Floor
			WHERE 1=1 ";
        if (actionTag.Equals("DELETE"))
            cmdString += @"
				AND FloorId NOT IN (SELECT FloorId FROM AMS.TableMaster)";
        if (actionTag.Equals("SAVE")) cmdString += "  AND (Status = 1 OR Status IS NULL) ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetTableTm(string actionTag, bool active, string category = "")
    {
        var cmdString = @"
			SELECT tm.TableId, tm.TableName, tm.TableCode, tm.FloorId,f.Description,CASE when tm.TableStatus = 'A' THEN 'Available' WHEN tm.TableStatus = 'O' THEN 'Occupied' WHEN tm.TableStatus='R' THEN 'Repair' END TableStatus FROM AMS.TableMaster tm
			LEFT OUTER JOIN AMS.Floor f ON tm.FloorId = f.FloorId where 1=1";
        if (actionTag.Equals("DELETE"))
            cmdString += @"
				AND tm.TableId NOT IN (SELECT sm.TableId FROM AMS.SB_Master sm)
				AND tm.TableId NOT IN(SELECT sm1.TableId FROM AMS.SO_Master sm1)";
        if (actionTag.Equals("SAVE") && active) cmdString += "  AND tm.Status = 1";
        if (category.IsValueExits())
            cmdString += category.GetInt() > 0
                ? $" AND tm.TableId <> '{category}'"
                : $"AND tm.TableStatus = '{category}'";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetNarration(string actionTag, bool active)
    {
        const string cmdString =
            @"SELECT n.NRID , n.NRDESC, CASE WHEN n.NRTYPE = 'RE' THEN 'Remarks' WHEN n.NRTYPE = 'NA' THEN 'Narration' WHEN n.NRTYPE = 'BO' THEN 'Both' END NRTYPE , IsActive FROM AMS.NR_Master n";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetDepartment(string actionTag)
    {
        const string cmdString =
            @"SELECT d.DId LedgerId,d.DName Description, d.DCode ShortName  FROM AMS.Department d	";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetHosDepartment(string actionTag)
    {
        const string cmdString = @"
			SELECT d.DId LedgerId,d.DName Description, d.DCode ShortName,d.DoctorId,d1.DrName Doctor,CAST(D.ChargeAmt AS DECIMAL(18,2)) Rate FROM HOS.Department d
			LEFT OUTER JOIN HOS.Doctor d1 ON d1.DrId = d.DoctorId";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetProductGroup(string actionTag)
    {
        var cmdString = @"
			SELECT PGrpID LedgerId,GrpName Description,GrpCode ShortName,Convert(Decimal(18,2),GMargin) Margin
			FROM AMS.ProductGroup
			WHERE 1=1 ";
        if (actionTag.Equals("SAVE")) cmdString += "\n AND [Status] = 1 OR [Status] IS NULL ";
        if (actionTag is "DELETE")
            cmdString +=
                "\n AND AMS.ProductGroup.PGrpID NOT IN (SELECT p.PGrpId FROM AMS.Product p WHERE p.PGrpId IS  NOT NULL OR p.PGrpId <> '') ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetProductSubGroup(string actionTag, string groupId)
    {
        var cmdString = @"
			SELECT PSubGrpId LedgerId, SubGrpName Description, ShortName, PG.GrpName GrpName
			FROM AMS.ProductSubGroup PSG
				LEFT OUTER JOIN AMS.ProductGroup PG ON PSG.GrpId = PG.PGrpId ";
        if (actionTag.Equals("SAVE") && groupId.GetInt() > 0) cmdString += $"\n WHERE PG.PGrpId = '{groupId}'";
        cmdString += "\n ORDER BY PSG.SubGrpName";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetProductSubGroups(string actionTag, string groupId)
    {
        var cmdString = @"
			SELECT PSubGrpId LedgerId, SubGrpName Description, ShortName, PG.GrpName GrpName
			FROM AMS.ProductSubGroup PSG
				LEFT OUTER JOIN AMS.ProductGroup PG ON PSG.GrpId = PG.PGrpId ";
        //if (actionTag.Equals("SAVE") && groupId.GetInt() > 0) cmdString += $"\n WHERE PG.PGrpId = '{groupId}'";
        cmdString += "\n ORDER BY PSG.SubGrpName";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetProductUnit(string actionTag, string category)
    {
        var cmdString = @"
			SELECT UID LedgerId, UnitName Description, UnitCode ShortName
			FROM AMS.ProductUnit
			WHERE 1=1 ";
        if (actionTag.Equals("SAVE")) cmdString += @" AND (Status = 1 OR Status IS NULL) ";
        if (actionTag.Equals("DELETE")) cmdString += @"AND UID NOT IN (SELECT Unit_Id FROM AMS.StockDetails)";
        cmdString += "ORDER BY UnitCode";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetProductBatch(long productId)
    {
        var cmdString = $@"
			SELECT BatchNo Description,MFDate,ExpDate, FORMAT(SUM(CASE WHEN VoucherType='I' THEN Qty ELSE -Qty END),'{ObjGlobal.SysQtyCommaFormat}') StockQty,CAST(Rate AS DECIMAL(18,{ObjGlobal.SysAmountLength})) Rate,DATEDIFF(DAY,GETDATE(),ExpDate) Days,CASE WHEN DATEDIFF(DAY,GETDATE(),ExpDate) <=0 THEN 12 ELSE 0 END IsGroup
			FROM AMS.ProductAddInfo
			WHERE ProductId = {productId}
			GROUP BY BatchNo, Rate,ExpDate,MFDate
			HAVING SUM(CASE WHEN VoucherType='I' THEN Qty ELSE -Qty END)>0
			ORDER BY BatchNo;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetVehicleWithQty(string actionTag, string loginDate)
    {
        return _objPick.GetVehicleListWithQty((ObjGlobal.SysQtyFormat.Length - 2).ToString(),
            ObjGlobal.SysAmountLength.ToString(), loginDate, ObjGlobal.StockAvailableStock);
    }

    public DataTable GetProductWithQty(string actionTag, string loginDate)
    {
        if (string.IsNullOrEmpty(loginDate) || loginDate.Equals("EMPTY"))
            loginDate = DateTime.Now > ObjGlobal.CfEndAdDate.GetDateTime()
                ? ObjGlobal.CfEndAdDate.GetSystemDate()
                : DateTime.Now.ToString("yyyy-MM-dd");

        var getDate = DateTime.Parse(loginDate).ToString("yyyy-MM-dd");
        var cmdString = $@"
		SELECT p.PID LedgerId, p.PAlias, p.PName Description,P.HsCode, p.PShortName ShortName, CAST(ISNULL(sd.AltQty, 0) AS DECIMAL(18, 2)) BalanceAltQty, PAU.UnitCode AltUnitCode, CAST(ISNULL(sd.Qty, 0) AS DECIMAL(18, 2)) BalanceQty, PU.UnitCode, CAST(p.PBuyRate AS DECIMAL(18, 2)) BuyRate, CAST(p.PSalesRate AS DECIMAL(18, 2)) PSalesRate, ISNULL(PG.GrpName, 'No Group') GrpName, ISNULL(PSG.SubGrpName, 'No SubGroup') SubGrpName, p.Barcode, p.Barcode1, p.Barcode2, p.Barcode3
        FROM AMS.Product p
             LEFT OUTER JOIN(SELECT sd.Product_Id, SUM(CASE WHEN sd.EntryType='I' THEN sd.StockQty WHEN sd.EntryType='O' THEN -sd.StockQty ELSE 0 END) Qty, SUM(CASE WHEN sd.EntryType='I' THEN sd.AltStockQty WHEN sd.EntryType='O' THEN -sd.AltStockQty ELSE 0 END) AltQty FROM AMS.StockDetails sd WHERE sd.Voucher_Date<='{getDate}' GROUP BY sd.Product_Id) AS sd ON sd.Product_Id=p.PID
             LEFT OUTER JOIN AMS.ProductUnit AS PU ON p.PUnit=PU.UID
             LEFT OUTER JOIN AMS.ProductUnit AS PAU ON p.PAltUnit=PAU.UID
             LEFT OUTER JOIN AMS.ProductGroup AS PG ON PG.PGrpId=p.PGrpId
             LEFT OUTER JOIN AMS.ProductSubGroup AS PSG ON PSG.PSubGrpId=p.PSubGrpId ";
        cmdString += ObjGlobal.StockAvailableStock ? @" WHERE sd.Qty > 0 " : "  ";
        cmdString += @"
		ORDER BY p.PName, PG.GrpName, PSG.SubGrpName, BuyRate DESC;";
        var result = SqlExtensions.ExecuteDataSet(cmdString);
        return result.Tables[0];
    }

    public DataTable GetFinishedProductWithQty(string actionTag, string loginDate)
    {
        if (string.IsNullOrEmpty(loginDate) || loginDate.Equals("EMPTY"))
            loginDate = DateTime.Now > ObjGlobal.CfEndAdDate.GetDateTime()
                ? ObjGlobal.CfEndAdDate.GetSystemDate()
                : DateTime.Now.ToString("yyyy-MM-dd");
        var getDate = DateTime.Parse(loginDate).ToString("yyyy-MM-dd");
        var cmdString = $@"
			SELECT PID LedgerId, PAlias, p.PName Description, p.PShortName ShortName, CAST(sd.Qty AS DECIMAL(18, 2)) BalanceQty, PU.UnitCode, CAST(p.PBuyRate AS DECIMAL(18, 2)) BuyRate, CAST(p.PSalesRate AS DECIMAL(18, 2)) PSalesRate, ISNULL(PG.GrpName, 'No Group') GrpName, ISNULL(PSG.SubGrpName, 'No SubGroup') SubGrpName
			FROM AMS.Product                              p
				 LEFT OUTER JOIN (SELECT sd.Product_Id, SUM(CASE WHEN sd.EntryType='I' THEN sd.Qty WHEN sd.EntryType='O' THEN -sd.StockQty ELSE 0 END) Qty
								  FROM AMS.StockDetails sd
								  WHERE sd.Voucher_Date<='{getDate}'
								  GROUP BY sd.Product_Id) AS sd ON sd.Product_Id=p.PID
				 LEFT OUTER JOIN AMS.ProductUnit          AS PU ON p.PUnit=PU.UID
				 LEFT OUTER JOIN AMS.ProductGroup         AS PG ON PG.PGrpId=p.PGrpId
				 LEFT OUTER JOIN AMS.ProductSubGroup      AS PSG ON PSG.PSubGrpId=p.PSubGrpId
			WHERE p.PID IN (SELECT FinishedGoodsId FROM INV.BOM_Master) ";
        cmdString += ObjGlobal.StockAvailableStock ? @" AND sd.Qty > 0 " : "";
        cmdString += @"
			ORDER BY p.PName; ";
        var result = SqlExtensions.ExecuteDataSet(cmdString);
        return result.Tables[0];
    }

    public DataTable GetRawProductWithQty(string actionTag, string loginDate)
    {
        if (string.IsNullOrEmpty(loginDate) || loginDate.Equals("EMPTY"))
            loginDate = DateTime.Now > ObjGlobal.CfEndAdDate.GetDateTime()
                ? ObjGlobal.CfEndAdDate.GetSystemDate()
                : DateTime.Now.ToString("yyyy-MM-dd");

        var getDate = DateTime.Parse(loginDate).ToString("yyyy-MM-dd");
        var cmdString = $@"
			SELECT Pid LedgerId, PAlias, P.PName Description, P.PShortName ShortName,CAST ( sd.Qty AS DECIMAL(18,2))  BalanceQty, PU.UnitCode,CAST ( P.PBuyRate AS DECIMAL(18,2))  BuyRate, CAST (  P.PSalesRate AS DECIMAL(18,2)) PSalesRate, ISNULL(PG.GrpName, 'No Group') GrpName,ISNULL(PSG.SubGrpName, 'No SubGroup') SubGrpName
			FROM AMS.Product p
			LEFT OUTER JOIN
			(
				SELECT sd.Product_Id,SUM(CASE WHEN sd.EntryType='I' THEN  sd.Qty WHEN sd.EntryType='O' THEN -sd.StockQty ELSE 0 END ) Qty FROM AMS.StockDetails sd WHERE SD.Voucher_Date <='{getDate}'
				GROUP BY sd.Product_Id
			) AS sd ON sd.Product_Id= p.PID
			left outer join AMS.ProductUnit as PU on P.PUnit = PU.UID
			left outer join AMS.ProductGroup as PG on PG.PGrpID = p.PGrpId
			left outer join AMS.ProductSubGroup as PSG on PSG.PSubGrpID = p.PSubGrpId			";
        if (ObjGlobal.StockAvailableStock)
            cmdString += @"
			WHERE sd.Qty > 0 ";
        cmdString += " ORDER BY p.PName";
        var result = SqlExtensions.ExecuteDataSet(cmdString);
        return result.Tables[0];
    }

    public DataTable GetProductWithQtyFilterByLedger(string actionTag, string loginDate, string category, string moduleType)
    {
        var cmdString = $@"
			SELECT PID LedgerId, PAlias, p.PName Description, P.HsCode,p.PShortName ShortName, CAST(sd.Qty AS DECIMAL(18, 2)) BalanceQty, PU.UnitCode, CAST(p.PBuyRate AS DECIMAL(18, 2)) BuyRate, CAST(p.PSalesRate AS DECIMAL(18, 2)) PSalesRate, ISNULL(PG.GrpName, 'No Group') GrpName, ISNULL(PSG.SubGrpName, 'No SubGroup') SubGrpName
			FROM AMS.Product p
				 LEFT OUTER JOIN (
								   SELECT sd.Product_Id, SUM( CASE WHEN sd.EntryType='I' THEN sd.Qty
																WHEN sd.EntryType='O' THEN -sd.StockQty ELSE 0 END
															) Qty
								   FROM AMS.StockDetails sd
								   WHERE sd.Voucher_Date<='{loginDate.GetSystemDate()}'
								   GROUP BY sd.Product_Id
								 ) AS sd ON sd.Product_Id=p.PID
				 LEFT OUTER JOIN AMS.ProductUnit AS PU ON p.PUnit=PU.UID
				 LEFT OUTER JOIN AMS.ProductGroup AS PG ON PG.PGrpId=p.PGrpId
				 LEFT OUTER JOIN AMS.ProductSubGroup AS PSG ON PSG.PSubGrpId=p.PSubGrpId
			WHERE 1=1 ";
        if (ObjGlobal.StockAvailableStock) cmdString += " and sd.Qty > 0 ";
        if (category.Equals("LEDGER") && moduleType.GetLong() > 0)
            cmdString += $@"
				AND p.PID IN (SELECT Product_Id FROM ams.StockDetails sd1 WHERE sd1.Ledger_Id = {moduleType}) ";
        cmdString += " ORDER BY p.PName";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetMasterRestroProductList(string actionTag)
    {
        var cmdString = @"
			SELECT PID LedgerId, P.PName Description, P.PShortName ShortName, PU.UnitCode, CAST(P.PBuyRate AS DECIMAL(18, 2)) BuyRate, CAST(P.PSalesRate AS DECIMAL(18, 2)) PSalesRate, PG.GrpName GrpName, PSG.SubGrpName
			FROM AMS.Product AS P
				 LEFT OUTER JOIN AMS.ProductUnit AS PU ON P.PUnit=PU.UID
				 LEFT OUTER JOIN AMS.ProductGroup AS PG ON PG.PGrpId=P.PGrpId
				 LEFT OUTER JOIN AMS.ProductSubGroup PSG ON P.PSubGrpId=PSG.PSubGrpId
			WHERE CAST(P.PSalesRate AS DECIMAL(18, 2))>0 AND(p.PType='S' OR P.PType='Service' OR P.PType = 'I' OR P.PType = 'Inventory')AND(p.PCategory='FG' OR p.PCategory='Finished Goods')  ";
        if (actionTag.Equals("SAVE")) cmdString += @" AND (P.Status  = 1 OR P.Status IS NULL) ";
        cmdString += @"
			ORDER BY P.PName; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetPrinter()
    {
        var table = new DataTable("Description");
        if (PrinterSettings.InstalledPrinters.Count <= 0)
        {
            MessageBox.Show(@"PRINTER LIST NOT FOUND..!!", ObjGlobal.Caption);
            return new DataTable();
        }

        table.Columns.Add("Description");
        for (var index = 0; index < PrinterSettings.InstalledPrinters.Count; index++)
        {
            var printer = PrinterSettings.InstalledPrinters[index];
            table.Rows.Add(printer);
        }

        return table;
    }

    public DataTable GetPrintDesign(string source)
    {
        var data = new DataTable();
        data.Columns.Add("Description");
        data.Columns.Add("ShortName");
        switch (source)
        {
            case "SB":
                {
                    data.Rows.Add("DefaultInvoice", "DefaultInvoice");
                    data.Rows.Add("DefaultInvoiceWithPAN", "DefaultInvoiceWithPAN");
                    data.Rows.Add("DefaultInvoiceWithVAT", "DefaultInvoiceWithVAT");
                    data.Rows.Add("BanepaMiniMart", "BanepaMiniMart");
                    data.Rows.Add("MorselDesignWithVAT", "MorselDesignWithVAT");
                    data.Rows.Add("RestaurantDesignWithVAT", "RestaurantDesignWithVAT");
                    data.Rows.Add("RestaurantDesignWithPAN", "RestaurantDesignWithPAN");
                    data.Rows.Add("RestaurantDesignWithVAT5Inch", "RestaurantDesignWithVAT5Inch");
                    data.Rows.Add("RestaurantDesignWithPAN5Inch", "RestaurantDesignWithPAN5Inch");
                    data.Rows.Add("AbbreviatedTaxInvoice3inch", "AbbreviatedTaxInvoice3inch");
                    break;
                }
            case "SO":
                {
                    data.Rows.Add("DefaultOrder", "DefaultOrder");
                    data.Rows.Add("KOT/BOT", "KOT/BOT");
                    break;
                }
            case "BC":
                {
                    data.Rows.Add("DefaultBarcode", "DefaultBarcode");
                    break;
                }
        }

        return data;
    }

    public DataTable GetDocumentNumberingSchema()
    {
        var data = new DataTable();
        data.Columns.Add("Description");
        data.Columns.Add("ShortName");

        data.Rows.Add("Charts of Account Opening", "COA");
        data.Rows.Add("Product Opening", "POP");
        data.Rows.Add("PDC", "PDC");
        data.Rows.Add("Journal Voucher", "JV");
        data.Rows.Add("CashBank Voucher", "CV");
        data.Rows.Add("Debit Note", "DN");
        data.Rows.Add("Credit Note", "CN");
        data.Rows.Add("Purchase Indent", "PI");
        data.Rows.Add("Purchase Quotation", "PQ");
        data.Rows.Add("Purchase Order", "PO");
        data.Rows.Add("Goods In Transit", "GIT");
        data.Rows.Add("Purchase Challan", "PC");
        data.Rows.Add("Purchase Challan Return", "PCR");
        data.Rows.Add("Purchase Inter Branch", "PIB");
        data.Rows.Add("Purchase Quality Control", "PQC");
        data.Rows.Add("Purchase Invoice", "PB");
        data.Rows.Add("Purchase Additional", "PAB");
        data.Rows.Add("Purchase Return", "PR");
        data.Rows.Add("Purchase Travel & Tour", "PBT");
        data.Rows.Add("Purchase Expiry/Breakage Return", "PEB");
        data.Rows.Add("Sales Quotation", "SQ");
        data.Rows.Add("Sales Order", "SO");
        data.Rows.Add("Restro Order", "RSO");
        data.Rows.Add("Sales Order Cancellation", "SOC");
        data.Rows.Add("Sales Dispatch Order", "SDO");
        data.Rows.Add("Sales Dispatch Order Cancellation", "SDOC");
        data.Rows.Add("Sales Challan", "SC");
        data.Rows.Add("Sales Inter Branch", "SIB");
        data.Rows.Add("Sales Invoice", "SB");
        data.Rows.Add("Temp Sales Invoice", "TSB");
        data.Rows.Add("Sales Additional Invoice", "SAB");
        data.Rows.Add("Point Of Sales", "POS");
        data.Rows.Add("Restro Invoice", "RSB");
        data.Rows.Add("Abbreviated Tax Invoice", "ATI");
        data.Rows.Add("Sales Return", "SR");
        data.Rows.Add("Sales Expiry/Breakage Return", "SEB");
        data.Rows.Add("Sales Tours & Travel", "SBT");
        data.Rows.Add("Godown Transfer", "GT");
        data.Rows.Add("Stock Adjustment", "SA");
        data.Rows.Add("Physical Stock", "PSA");
        data.Rows.Add("Transfer Expiry/Breakage", "STEB");
        data.Rows.Add("Refund Entry", "NN");
        data.Rows.Add("Assembly Master", "ASSM");
        data.Rows.Add("Memo", "BOM");
        data.Rows.Add("Inventory Requisition", "SREQ");
        data.Rows.Add("Inventory Issue", "MI");
        data.Rows.Add("Inventory Issue Return", "MIR");
        data.Rows.Add("Inventory Receive", "MR");
        data.Rows.Add("Inventory Receive Return", "MRR");
        data.Rows.Add("Cost Center Expenses", "PCCE");
        data.Rows.Add("Sample Costing", "PSC");
        data.Rows.Add("Production Master Memo", "MBOM");
        data.Rows.Add("Production Memo", "IBOM");
        data.Rows.Add("Production Requisition", "IREQ");
        data.Rows.Add("Production Issue", "RMI");
        data.Rows.Add("Production Issue Return", "RMIR");
        data.Rows.Add("Finished Good Receive", "FGR");
        data.Rows.Add("Finished Good Receive Return", "FGRR");
        data.Rows.Add("Production Order", "IPO");
        data.Rows.Add("Production Planning", "IPP");
        data.Rows.Add("Assets Log", "ASL");
        data.Rows.Add("Bank Reconcillation", "BRN");
        data.Rows.Add("Patient Registration", "PGL");
        data.Rows.Add("OPD Patient Registration", "OPR");
        data.Rows.Add("IPD Patient Registration", "IPR");
        data.Rows.Add("EMR Patient Registration", "EPR");
        data.Rows.Add("Patient Flowup", "FPR");
        data.Rows.Add("Hospital", "HS");
        data.Rows.Add("OPD Billing", "OPDB");
        data.Rows.Add("IPD Billing", "IPDB");
        data.Rows.Add("Patient Discharge", "HPD");
        data.Rows.Add("Patient Drug History", "PDH");
        return data;
    }

    public DataTable GetGodownList(string actionTag)
    {
        const string cmdString =
            "SELECT GID LedgerId, GName Description, GCode ShortName, GLocation Location FROM AMS.Godown;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetRackList(string actionTag)
    {
        return _objPick.GetRackList(actionTag, ObjGlobal.SysBranchId, Convert.ToInt32(ObjGlobal.SysCompanyUnitId));
    }

    public DataTable GetCounterList(string actionTag, bool isActive = false)
    {
        var cmdString = "Select CId LedgerId, CName Description,CCode ShortName,Printer From AMS.Counter where 1=1";
        if (isActive) cmdString += "AND[Status] = 1 OR[Status] IS NULL";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetCostCenter(string actionTag)
    {
        var cmdString = @"
			SELECT CCId LedgerId, CCName Description, CCcode ShortName
			FROM AMS.CostCenter
			WHERE 1=1";
        if (actionTag.Equals("SAVE"))
        {
        }

        if (actionTag.Equals("DELETE"))
        {
        }

        cmdString += @"
			ORDER BY CCName";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetCurrency(string actionTag)
    {
        var cmdString = @"
			SELECT CId LedgerId,CName Description,CCode ShortName,CRate
			FROM AMS.Currency
			WHERE 1=1 ";
        cmdString += @"
			Order By CName";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetDoctor(string tag)
    {
        var cmdString = $@"
			SELECT DrId LedgerId, DRName Description, DrShortName ShortName, DrTypeDesc, ContactNo, Address, DP.DName DName
			FROM HOS.Doctor AS D
				LEFT OUTER JOIN HOS.DoctorType AS DT ON D.Drtype=Dt.DTID
				LEFT OUTER JOIN HOS.Department AS DP ON DP.DoctorId=D.DrId
			WHERE (D.BranchId IS NULL OR D.BranchId='{ObjGlobal.SysBranchId}')AND(D.CompanyUnitSetup IS NULL OR D.CompanyUnitSetup='{ObjGlobal.SysCompanyUnitId}')";
        cmdString += tag.Equals("SAVE") ? @" AND (D.Status IS NULL OR D.Status=1) " : "";
        cmdString += @"
			ORDER BY DrName;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetHDepartment()
    {
        return _objHMaster.MasterHDepartment();
    }

    public DataTable GetProductSchemeList(string tag)
    {
        const string cmdString =
            "SELECT SchemeId,SchemeDesc, ValidFromMiti, ValidToMiti FROM AMS.Scheme_Master ORDER BY SchemeId ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetProductPublicationList(string tag)
    {
        const string cmdString =
            "SELECT DISTINCT Publisher Description,0 ID FROM AMS.BookDetails WHERE Publisher <> '' AND Publisher IS NOT NULL;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetProductAuthorList(string tag)
    {
        const string cmdString =
            "SELECT DISTINCT Author Description,0 ID FROM AMS.BookDetails WHERE Author <> '' AND Author IS NOT NULL;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetGiftVoucherList(string tag)
    {
        const string cmdString =
            "SELECT VoucherId LedgerId,CardNo ShortName,Description,ExpireDate,CAST(IssueAmount AS DECIMAL(18,2)) Amount FROM AMS.GiftVoucherList WHERE IsUsed = 0";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetDocumentNumbering(string actionTag, string category)
    {
        var cmdString = @"
			SELECT DocId LedgerId,DocModule Module, DocDesc Description, DocStartMiti DocStartMiti, DocEndMiti DocEndMiti
			FROM AMS.DocumentNumbering
			WHERE 1=1 ";
        cmdString += actionTag.Equals("VIEW") ? "" : $"AND DocModule = '{category}'";
        if (!ObjGlobal.DomainLoginUser.Contains(ObjGlobal.LogInUser.GetUpper()))
            cmdString += $"AND DocBranch IN ({ObjGlobal.SysBranchId}) ";
        cmdString += $"AND FiscalYearId IN ({ObjGlobal.SysFiscalYearId}) ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetGlobalQuery(string cmdString)
    {
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    #endregion --------------- TRANSACTION MASTER LIST ---------------

    // MASTER LIST

    #region --------------- MASTER LIST  ---------------

    public DataTable GetBranchList()
    {
        const string script =
            "SELECT b.Branch_Id ValueId, b.Branch_Name ValueName,b.Branch_Code ShortName,b.Address FROM AMS.Branch b ";
        return SqlExtensions.ExecuteDataSet(script).Tables[0];
    }

    public DataTable GetCompanyUnitList()
    {
        const string script = @"
        SELECT cu.CmpUnit_ID ValueId, cu.CmpUnit_Name ValueName,cu.CmpUnit_Code ShortName,cu.Address FROM AMS.CompanyUnit  cu ";
        return SqlExtensions.ExecuteDataSet(script).Tables[0];
    }
    public DataTable GetFiscalYearList()
    {
        const string script = @"
        SELECT cu.CmpUnit_ID CmpUnit_ID, cu.CmpUnit_Name ValueName,cu.CmpUnit_Code ShortName,cu.Address FROM AMS.CompanyUnit  cu ";
        return SqlExtensions.ExecuteDataSet(script).Tables[0];
    }

    public DataTable GetVehicleModel(string actionTag)
    {
        return _objVMaster.Get_DataVehicleModel(actionTag);
    }

    public DataTable GetVehicleNumber(string actionTag)
    {
        return _objVMaster.Get_DataVehicleNumber(actionTag);
    }

    public DataTable GetVehicleColor(string actionTag)
    {
        return _objVMaster.Get_DataVehicleColor(actionTag);
    }

    public DataTable MasterSubLedger(string actionTag = "")
    {
        var commandText = @"
			SELECT SLId LedgerId, SLName Description, SLCode ShortName, SLAddress Address, SLPhoneNo PhoneNo, gl.GLName Ledger
			FROM AMS.SubLedger sl
				 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=TRY_CAST(ISNULL(sl.GLID,0) AS BIGINT)
			WHERE 1=1 ";
        if (actionTag.Equals("SAVE")) commandText += " AND (sl.Status = 1 OR sl.Status IS NULL) ";
        if (actionTag.ToUpper() == "DELETE")
            commandText += " AND sl.SLId  NOT IN (SELECT Subleder_ID FROM AMS.AccountDetails  WHERE Subleder_ID IS NOT NULL) ";
        return SqlExtensions.ExecuteDataSet(commandText).Tables[0];
    }

    public DataTable MasterGeneralLedger(string actionTag = "")
    {
        var cmdString = @"
			SELECT gl.GLID LedgerId, gl.GLName Description, gl.GLCode ShortName, gl.ACCode LedgerCode, 0 Balance, '' BType, gl.PanNo, gl.GLType, AG.GrpType, AG.PrimaryGrp, AG.GrpName GroupDesc, ASG.SubGrpName SubGroupDesc, gl.GLAddress, JA.AgentName SalesMan, CU.CCode Currency, gl.CrDays, gl.CrLimit, gl.CrTYpe, gl.PhoneNo
			FROM AMS.GeneralLedger gl
				 LEFT OUTER JOIN AMS.AccountGroup AS AG ON gl.GrpId=AG.GrpId
				 LEFT OUTER JOIN AMS.AccountSubGroup AS ASG ON gl.SubGrpId=ASG.SubGrpId
				 LEFT OUTER JOIN AMS.JuniorAgent AS JA ON gl.AgentId=JA.AgentId
				 LEFT OUTER JOIN AMS.Currency AS CU ON CU.CId=gl.CurrId
			WHERE 1=1";
        if (actionTag.ToUpper() == "DELETE")
            cmdString += " AND gl.GLID NOT IN ( SELECT Ledger_Id FROM AMS.AccountDetails ad) ";
        cmdString += "ORDER BY gl.GLName";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable MasterProduct(string actionTag)
    {
        var commandText = $@"
			SELECT PID LedgerId, P.PName Description, P.PShortName ShortName, UID, PU.UnitCode, CAST(P.PBuyRate AS DECIMAL(18,{ObjGlobal.SysAmountLength})) BuyRate, CAST(P.PSalesRate AS DECIMAL(18,{ObjGlobal.SysAmountLength})) PSalesRate, PG.PGrpId, ISNULL(PG.GrpName,'NO CATEGORY') GrpName,P.PSubGrpId,ISNULL(psg.SubGrpName,'NO SUB CATEGORY') SubGrpName, CAST(PTax AS DECIMAL(18,{ObjGlobal.SysAmountLength})) PTax, P.Barcode, P.Barcode1,CAST(P.PMRP AS DECIMAL(18,{ObjGlobal.SysAmountLength})) PMRP, CAST(P.TradeRate AS DECIMAL(18,{ObjGlobal.SysAmountLength})) TradeRate,CAST(P.BeforeBuyRate AS DECIMAL(18,{ObjGlobal.SysAmountLength})) BeforeBuyRate,CAST(P.BeforeSalesRate AS DECIMAL(18,{ObjGlobal.SysAmountLength})) BeforeSalesRate
			FROM AMS.Product AS P
				 LEFT OUTER JOIN AMS.ProductUnit AS PU ON P.PUnit=PU.UID
				 LEFT OUTER JOIN AMS.ProductGroup AS PG ON PG.PGrpId=P.PGrpId
				 LEFT OUTER JOIN AMS.ProductSubGroup AS psg ON psg.PSubGrpId = P.PSubGrpId
			WHERE 1=1 ";
        if (actionTag.ToUpper() == "DELETE")
        {
            commandText += " AND Pid not in (Select Product_Id from AMS.StockDetails) ";
        }
        if (actionTag.ToUpper() == "SAVE")
        {
            commandText += " AND (P.Status = 1 OR P.Status IS NULL)";
        }
        commandText += " ORDER BY p.PName, PG.GrpName, PSG.SubGrpName, BuyRate DESC; ";
        return SqlExtensions.ExecuteDataSet(commandText).Tables[0];
    }

    #endregion --------------- MASTER LIST  ---------------

    // TRANSACTION REPORT LIST

    #region --------------- TRANSACTION REPORT LIST  ---------------

    public DataTable ReturnLedgerOpeningVoucherList(string module)
    {
        var cmdString = @$"
			SELECT Voucher_No VoucherNo,CASE WHEN (SELECT sc.EnglishDate FROM AMS.SystemSetting sc) = 0 then  vm.OP_Miti else  CONVERT(VARCHAR(10), vm.OP_Date, 101)  end VoucherDate,GLName,CAST(Sum(VM.LocalDebit -VM.LocalCredit) AS DECIMAL(18,{ObjGlobal.SysAmountLength}))  Amount,CASE WHEN Sum(VM.LocalDebit - VM.LocalCredit) > 0 THEN 'Dr' WHEN Sum(VM.LocalDebit - VM.LocalCredit) < 0 THEN 'Cr' ELSE '' END AmtType
			FROM AMS.LedgerOpening AS VM
				LEFT OUTER JOIN AMS.GeneralLedger GL ON VM.Ledger_Id=GL.GLID
			WHERE  VM.Module IN ('{module}','LOB','OB') AND (vm.Branch_id IS NULL OR vm.Branch_id='{ObjGlobal.SysBranchId}') AND (vm.Company_Id IS NULL OR vm.Company_Id='{ObjGlobal.SysCompanyUnitId}') AND (VM.FiscalYearId IS NULL OR VM.FiscalYearId='{ObjGlobal.SysFiscalYearId}')
			GROUP BY Voucher_No, OP_Date, OP_Miti, GLName
			ORDER BY Voucher_No";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable ReturnJournalVoucherList(bool isProvision, DateTime vDate, string vnoMode, bool isActive)
    {
        var cmdString = $@"
			SELECT CASE WHEN (SELECT sc.EnglishDate FROM AMS.SystemSetting sc) = 0 THEN Voucher_Miti ELSE CONVERT(VARCHAR(10), Voucher_Date, 101) END VoucherDate, JD.Voucher_No VoucherNo, JD.Ledger_ID LedgerId, gl.GLName LedgerDesc, CAST(JD.Debit AS DECIMAL(18,2)) DebitAmount, CAST(JD.Credit AS DECIMAL(18,2)) CreditAmount
			FROM AMS.JV_Master JV
				 INNER JOIN AMS.JV_Details JD ON JD.Voucher_No = JV.Voucher_No
				 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID = JD.Ledger_ID
			WHERE JV.FiscalYearId = {ObjGlobal.SysFiscalYearId} AND JV.Action_Type <> 'CANCEL' AND JV.Voucher_Date <='{vDate.GetSystemDate()}'  AND gl.Branch_ID = {ObjGlobal.SysBranchId}  AND JV.FiscalYearId = {ObjGlobal.SysFiscalYearId}
			ORDER BY VoucherNo, VoucherDate,JD.Debit  desc,LedgerDesc;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable ReturnCashBankVoucherList(bool isProvision, DateTime vDate, string vnoMode, bool isActive,
        string category = "CONTRA")
    {
        var cmdString = $@"
			SELECT CASE WHEN (SELECT sc.EnglishDate FROM AMS.SystemSetting sc) = 0 THEN Voucher_Miti ELSE CONVERT(VARCHAR(10), Voucher_Date, 101) END VoucherDate, CD.Voucher_No VoucherNo, CD.Ledger_Id LedgerId, gl.GLName LedgerDesc, CAST(CD.Debit AS DECIMAL(18, 2)) DebitAmount, CAST(CD.Credit AS DECIMAL(18, 2)) CreditAmount
			FROM AMS.CB_Master CB
				 INNER JOIN AMS.CB_Details CD ON CB.Voucher_No = CD.Voucher_No
				 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID = CD.Ledger_Id
			WHERE CB.FiscalYearId = {ObjGlobal.SysFiscalYearId} AND CB.Action_Type <> 'CANCEL' AND CB.Voucher_Date <='{vDate.GetSystemDate()}' ";
        cmdString += isProvision ? " AND CB.VoucherMode ='PROV' " : " ";
        cmdString += category is "REMIT"
            ? $" AND CB.VoucherMode = '{category}'"
            : " AND CB.VoucherMode IN ('Contra', 'Payment', 'Receipt', 'CC', 'P', 'R') ";
        cmdString += @"
			ORDER BY VoucherNo, VoucherDate, LedgerDesc ; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable ReturnDebitNotesVoucherList(bool isProvision, DateTime vDate, string vnoMode, bool isActive)
    {
        var cmdString = @$"
			SELECT CASE WHEN (SELECT sc.EnglishDate FROM AMS.SystemSetting sc) = 0 THEN Voucher_Miti ELSE CONVERT(VARCHAR(10), Voucher_Date, 101) END VoucherDate, ND.Voucher_No VoucherNo, ND.Ledger_Id LedgerId, gl.GLName LedgerDesc, ND.Debit DebitAmount, ND.Credit CreditAmount
			FROM AMS.Notes_Master NM
				 INNER JOIN AMS.Notes_Details ND ON ND.Voucher_No = NM.Voucher_No
				 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID = ND.Ledger_Id
			WHERE NM.FiscalYearId = {ObjGlobal.SysFiscalYearId} AND NM.VoucherMode='DN' AND NM.Action_Type <> 'CANCEL'
			ORDER BY VoucherNo, VoucherDate, LedgerDesc;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable ReturnCreditNotesVoucherList(bool isProvision, DateTime vDate, string vnoMode, bool isActive)
    {
        var cmdString = @$"
			SELECT CASE WHEN (SELECT sc.EnglishDate FROM AMS.SystemSetting sc) = 0 THEN Voucher_Miti ELSE CONVERT(VARCHAR(10), Voucher_Date, 101) END VoucherDate, ND.Voucher_No VoucherNo, ND.Ledger_Id LedgerId, gl.GLName LedgerDesc, ND.Debit DebitAmount, ND.Credit CreditAmount
			FROM AMS.Notes_Master NM
				 INNER JOIN AMS.Notes_Details ND ON ND.Voucher_No = NM.Voucher_No
				 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID = ND.Ledger_Id
			WHERE NM.FiscalYearId = {ObjGlobal.SysFiscalYearId} AND NM.VoucherMode='CN' AND NM.Action_Type <> 'CANCEL'
			ORDER BY VoucherNo, VoucherDate, LedgerDesc; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable ReturnPdcVoucherList(string voucherType)
    {
        var cmdString = @$"
			SELECT PDCId VoucherId,VoucherNo,Case when (SELECT EnglishDate FROM AMS.SystemSetting)= 0 then  VoucherMiti else  CONVERT(VARCHAR(10), VoucherDate, 101)  end VoucherDate,VoucherType, gl.GLName,CONVERT (DECIMAL (18,2), pc.Amount ) Amount
			FROM AMS.PostDateCheque pc
				LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID= pc.LedgerId
			WHERE  (BranchId='{ObjGlobal.SysBranchId}' OR BranchId IS NULL) and (CompanyUnitId='{ObjGlobal.SysCompanyUnitId}' OR CompanyUnitId IS NULL)";
        cmdString += voucherType.IsValueExits() ? $" AND pc.Status = '{voucherType}'" : " AND pc.Status='Due' ";
        cmdString += " ORDER BY pc.voucherNo,pc.VoucherDate ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    // PURCHASE REGISTER
    public DataTable ReturnPurchaseIndentVoucherNoList(string voucherType)
    {
        var cmdTxt = $@"
			SELECT pm.PIN_Invoice VoucherNo,CASE WHEN (SELECT sc.EnglishDate FROM AMS.SystemSetting sc) = 1 THEN CONVERT(VARCHAR, pm.PIN_Date, 103) ELSE pm.PIN_Miti END VoucherDate,Person Ledger,0 TPAN,0 Amount, pm.Remarks
			FROM AMS.PIN_Master  pm
			WHERE pm.BranchId = '{ObjGlobal.SysBranchId}' AND pm.FiscalYearId = '{ObjGlobal.SysFiscalYearId}' ";
        cmdTxt += "ORDER BY pm.PIN_Invoice,pm.PIN_Date ;";
        return SqlExtensions.ExecuteDataSet(cmdTxt).Tables[0];
    }

    public DataTable ReturnPurchaseOrderVoucherNoList(string voucherType)
    {
        var cmdTxt = $@"
			SELECT pm.PO_Invoice VoucherNo, CASE WHEN (SELECT sc.EnglishDate FROM AMS.SystemSetting sc) = 1 THEN CONVERT(VARCHAR, pm.Invoice_Date, 103) ELSE pm.Invoice_Miti END VoucherDate, CASE WHEN pm.Party_Name IS NOT NULL AND LTRIM(pm.Party_Name) <> '' THEN gl.GLName + ' (' + pm.Party_Name + ')' ELSE gl.GLName END Ledger, CASE WHEN pm.Vat_No IS NULL THEN gl.PanNo ELSE pm.Vat_No END TPAN, CAST(pm.LN_Amount AS DECIMAL(18, 2)) Amount, pm.Remarks
			FROM AMS.PO_Master pm, AMS.GeneralLedger gl
			WHERE pm.Vendor_ID = gl.GLID AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id = '{ObjGlobal.SysBranchId}' AND pm.FiscalYearId = '{ObjGlobal.SysFiscalYearId}' ";
        cmdTxt += "ORDER BY pm.PO_Invoice,pm.Invoice_Date ;";
        return SqlExtensions.ExecuteDataSet(cmdTxt).Tables[0];
    }

    public DataTable ReturnPurchaseChallanVoucherNoList(string voucherType)
    {
        var cmdTxt = $@"
			SELECT pm.PC_Invoice VoucherNo, CASE WHEN (SELECT sc.EnglishDate FROM AMS.SystemSetting sc) = 1 THEN CONVERT ( VARCHAR, pm.Invoice_Date, 103 ) ELSE pm.Invoice_Miti END VoucherDate, CASE WHEN pm.Party_Name IS NOT NULL AND LTRIM ( pm.Party_Name ) <> '' THEN gl.GLName + ' (' + pm.Party_Name + ')' ELSE gl.GLName END Ledger, CASE WHEN pm.Vat_No IS NULL THEN gl.PanNo ELSE pm.Vat_No END TPAN, CAST( pm.LN_Amount AS DECIMAL(18,2)) Amount, pm.Remarks
			 FROM AMS.PC_Master pm, AMS.GeneralLedger gl
			 WHERE pm.Vendor_ID = gl.GLID AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id = '{ObjGlobal.SysBranchId}' AND pm.FiscalYearId = '{ObjGlobal.SysFiscalYearId}' ";
        if (voucherType.Equals("OPC"))
            cmdTxt += @"
				 AND pm.PC_Invoice NOT IN (SELECT pb.PC_Invoice FROM AMS.PB_Master pb WHERE pb.PC_Invoice IS NOT NULL AND pb.PC_Invoice <> '')";
        cmdTxt += @"
			 ORDER BY pm.PC_Invoice, pm.Invoice_Date;";
        return SqlExtensions.ExecuteDataSet(cmdTxt).Tables[0];
    }

    public DataTable ReturnPurchaseChallanReturnVoucherNoList(string voucherType)
    {
        var cmdTxt = $@"
			SELECT pm.PCR_Invoice VoucherNo, CASE WHEN (SELECT sc.EnglishDate FROM AMS.SystemSetting sc) = 1 THEN CONVERT ( VARCHAR, pm.Invoice_Date, 103 ) ELSE pm.Invoice_Miti END VoucherDate, CASE WHEN pm.Party_Name IS NOT NULL AND LTRIM ( pm.Party_Name ) <> '' THEN gl.GLName + ' (' + pm.Party_Name + ')' ELSE gl.GLName END Ledger, CASE WHEN pm.Vat_No IS NULL THEN gl.PanNo ELSE pm.Vat_No END TPAN, CAST( pm.LN_Amount AS DECIMAL(18,2)) Amount, pm.Remarks
			 FROM AMS.PCR_Master pm, AMS.GeneralLedger gl
			 WHERE pm.Vendor_ID = gl.GLID AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id = '{ObjGlobal.SysBranchId}' AND pm.FiscalYearId = '{ObjGlobal.SysFiscalYearId}' ";
        if (voucherType.Equals("OPC"))
            cmdTxt += @"
				 AND pm.PCR_Invoice NOT IN (SELECT pb.ChallanReturnNo FROM AMS.PR_Master pb WHERE pb.PCR_Invoice IS NOT NULL AND pb.PCR_Invoice <> '')";
        cmdTxt += @"
			 ORDER BY pm.PCR_Invoice, pm.Invoice_Date;";
        return SqlExtensions.ExecuteDataSet(cmdTxt).Tables[0];
    }

    public DataTable ReturnPurchaseInvoiceVoucherNoList(string voucherType)
    {
        var cmdTxt = $@"
			SELECT pm.PB_Invoice VoucherNo, CASE WHEN (SELECT sc.EnglishDate FROM AMS.SystemSetting sc) = 1 THEN CONVERT(VARCHAR, pm.Invoice_Date, 103) ELSE pm.Invoice_Miti END VoucherDate, CASE WHEN pm.Party_Name IS NOT NULL AND LTRIM(pm.Party_Name) <> '' THEN gl.GLName + ' (' + pm.Party_Name + ')' ELSE gl.GLName END Ledger, CASE WHEN pm.Vat_No IS NULL THEN gl.PanNo ELSE pm.Vat_No END TPAN,CAST( pm.LN_Amount AS DECIMAL(18,2)) Amount, pm.Remarks
			FROM AMS.PB_Master pm, AMS.GeneralLedger gl
			WHERE pm.Vendor_ID = gl.GLID AND pm.R_Invoice = 0 AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id = '{ObjGlobal.SysBranchId}'";
        cmdTxt += !voucherType.Equals("RETURN") ? $" AND pm.FiscalYearId = '{ObjGlobal.SysFiscalYearId}' " : " ";
        cmdTxt += @"
			ORDER BY pm.PB_Invoice, pm.Invoice_Date; ";
        return SqlExtensions.ExecuteDataSet(cmdTxt).Tables[0];
    }

    public DataTable ReturnPurchaseReturnVoucherNoList(string voucherType)
    {
        var cmdTxt = $@"
			SELECT pm.PR_Invoice VoucherNo, CASE WHEN (SELECT sc.EnglishDate FROM AMS.SystemSetting sc) = 1 THEN CONVERT(VARCHAR, pm.Invoice_Date, 103) ELSE pm.Invoice_Miti END VoucherDate, CASE WHEN pm.Party_Name IS NOT NULL AND LTRIM(pm.Party_Name) <> '' THEN gl.GLName + ' (' + pm.Party_Name + ')' ELSE gl.GLName END Ledger, CASE WHEN pm.Vat_No IS NULL THEN gl.PanNo ELSE pm.Vat_No END TPAN, CAST(pm.LN_Amount AS DECIMAL(18, 2)) Amount, pm.Remarks
			FROM AMS.PR_Master pm, AMS.GeneralLedger gl
			 WHERE pm.Vendor_ID = gl.GLID AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id = '{ObjGlobal.SysBranchId}' AND pm.FiscalYearId = '{ObjGlobal.SysFiscalYearId}' ";
        cmdTxt += voucherType.Equals("RETURN") || voucherType.Equals("PCR")
            ? " AND pm.Invoice_Type ='RETURN' "
            : " AND pm.Invoice_Type <> 'RETURN'";
        cmdTxt += "ORDER BY pm.PR_Invoice,pm.Invoice_Date; ";
        return SqlExtensions.ExecuteDataSet(cmdTxt).Tables[0];
    }

    public DataTable ReturnPurchaseAdditionalVoucherNoList(string voucherType)
    {
        var cmdTxt = $@"
			SELECT pm.PB_Invoice VoucherNo, CASE WHEN (SELECT sc.EnglishDate FROM AMS.SystemSetting sc) = 1 THEN CONVERT ( VARCHAR, pm.Invoice_Date, 103 ) ELSE pm.Invoice_Miti END VoucherDate, CASE WHEN pm.Party_Name IS NOT NULL AND LTRIM ( pm.Party_Name ) <> '' THEN gl.GLName + ' (' + pm.Party_Name + ')' ELSE gl.GLName END Ledger, CASE WHEN pm.Vat_No IS NULL THEN gl.PanNo ELSE pm.Vat_No END TPAN, CAST( pm.LN_Amount AS DECIMAL(18,2)) Amount, pm.Remarks
			 FROM AMS.PB_Master pm, AMS.GeneralLedger gl
			 WHERE pm.Vendor_ID = gl.GLID AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id = '{ObjGlobal.SysBranchId}' AND pm.FiscalYearId = '{ObjGlobal.SysFiscalYearId}' ";
        cmdTxt += "ORDER BY pm.PB_Invoice,pm.Invoice_Date; ";
        return SqlExtensions.ExecuteDataSet(cmdTxt).Tables[0];
    }

    // SALES REGISTER
    public DataTable ReturnSalesHoldVoucherNoList(string voucherType)
    {
        var cmdTxt = $@"
			SELECT sm.SB_Invoice VoucherNo,CASE WHEN (SELECT sc.EnglishDate FROM AMS.SystemSetting sc) = 1 THEN CONVERT(VARCHAR, sm.Invoice_Date, 103) ELSE sm.Invoice_Miti END VoucherDate,
			CASE WHEN  sm.Party_Name IS NOT NULL AND LTRIM(sm.Party_Name) <> '' THEN gl.GLName + ' (' + sm.Party_Name + ')'  ELSE gl.GLName  END Ledger,
			CASE WHEN sm.Vat_No  IS NULL THEN gl.PanNo ELSE sm.Vat_No END TPAN,CAST( sm.LN_Amount AS DECIMAL(18,2)) Amount, sm.Remarks
			FROM AMS.temp_SB_Master  sm, AMS.GeneralLedger gl
			WHERE sm.Customer_ID = gl.GLID
			and sm.CBranch_Id = '{ObjGlobal.SysBranchId}' AND sm.FiscalYearId = '{ObjGlobal.SysFiscalYearId}' ";
        cmdTxt += "ORDER BY sm.SB_Invoice,sm.Invoice_Date; ";
        return SqlExtensions.ExecuteDataSet(cmdTxt).Tables[0];
    }

    public DataTable ReturnSalesQuotationVoucherNoList(string voucherType)
    {
        var cmdTxt = $@"
			SELECT sm.SQ_Invoice VoucherNo,CASE WHEN (SELECT sc.EnglishDate FROM AMS.SystemSetting sc) = 1 THEN CONVERT(VARCHAR, sm.Invoice_Date, 103) ELSE sm.Invoice_Miti END VoucherDate,
			CASE WHEN  sm.Party_Name IS NOT NULL AND LTRIM(sm.Party_Name) <> '' THEN gl.GLName + ' (' + sm.Party_Name + ')'  ELSE gl.GLName  END Ledger,
			CASE WHEN sm.Vat_No  IS NULL THEN gl.PanNo ELSE sm.Vat_No END TPAN,CAST( sm.LN_Amount AS DECIMAL(18,2)) Amount, sm.Remarks
			FROM AMS.SQ_Master sm, AMS.GeneralLedger gl
			WHERE sm.Customer_ID = gl.GLID
			and sm.CBranch_Id = '{ObjGlobal.SysBranchId}' AND sm.FiscalYearId = '{ObjGlobal.SysFiscalYearId}' ";
        cmdTxt += "ORDER BY sm.SQ_Invoice,sm.Invoice_Date; ";
        return SqlExtensions.ExecuteDataSet(cmdTxt).Tables[0];
    }

    public DataTable ReturnSalesOrderVoucherNoList(string voucherType)
    {
        var cmdString = $@"
			SELECT sm.SO_Invoice VoucherNo, CASE WHEN(SELECT sc.EnglishDate FROM AMS.SystemSetting sc)=1 THEN CONVERT(VARCHAR, sm.Invoice_Date, 103)ELSE sm.Invoice_Miti END VoucherDate, CASE WHEN sm.Party_Name IS NOT NULL AND LTRIM(sm.Party_Name)<>'' THEN gl.GLName+' ('+sm.Party_Name+')' ELSE gl.GLName END Ledger, CASE WHEN sm.Vat_No IS NULL THEN gl.PanNo ELSE sm.Vat_No END TPAN, CAST(sm.LN_Amount AS DECIMAL(18, 2)) Amount, sm.Remarks
			FROM AMS.SO_Master sm, AMS.GeneralLedger gl
			WHERE sm.Customer_Id=gl.GLID AND sm.CBranch_Id={ObjGlobal.SysBranchId} AND sm.FiscalYearId={ObjGlobal.SysFiscalYearId}";
        cmdString += voucherType.Equals("O") ? @"AND sm.Invoice_Type <> 'POSTED'" : "";
        cmdString += voucherType switch
        {
            "PRINT" => @"
			ORDER BY sm.SO_Invoice DESC, sm.Invoice_Date DESC; ",
            _ => @"
			ORDER BY sm.SO_Invoice, sm.Invoice_Date; "
        };
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable ReturnSalesChallanVoucherNoList(string voucherType)
    {
        var cmdTxt = $@"
			SELECT sm.SC_Invoice VoucherNo, CASE WHEN(SELECT sc.EnglishDate FROM AMS.SystemSetting sc)= 1 THEN CONVERT(VARCHAR, sm.Invoice_Date, 103)ELSE sm.Invoice_Miti END VoucherDate, CASE WHEN sm.Party_Name IS NOT NULL AND LTRIM(sm.Party_Name)<>'' THEN gl.GLName+' ('+sm.Party_Name+')' ELSE gl.GLName END Ledger, CASE WHEN sm.Vat_No IS NULL THEN gl.PanNo ELSE sm.Vat_No END TPAN, CAST( sm.LN_Amount AS DECIMAL(18,2)) Amount, sm.Remarks
			FROM AMS.SC_Master sm, AMS.GeneralLedger gl
			WHERE sm.Customer_Id=gl.GLID AND sm.CBranch_Id = '{ObjGlobal.SysBranchId}' AND sm.FiscalYearId = '{ObjGlobal.SysFiscalYearId}' ";
        cmdTxt += voucherType is "OSC"
            ? " AND sm.SC_Invoice NOT IN (SELECT sb.SC_Invoice FROM AMS.SB_Master sb WHERE sb.SC_Invoice IS NOT NULL OR sb.SC_Invoice <> '')"
            : "";
        cmdTxt += voucherType.Equals("Print")
            ? " ORDER BY sm.Invoice_Date,sm.SC_Invoice ;"
            : " ORDER BY sm.SC_Invoice, sm.Invoice_Date;";
        return SqlExtensions.ExecuteDataSet(cmdTxt).Tables[0];
    }

    public DataTable ReturnSalesInvoiceVoucherNoList(string voucherType)
    {
        var cmdTxt = $@"
			SELECT sm.SB_Invoice VoucherNo, CASE WHEN(SELECT sc.EnglishDate FROM AMS.SystemSetting sc)= 1 THEN CONVERT(VARCHAR, sm.Invoice_Date, 103)ELSE sm.Invoice_Miti END VoucherDate, CASE WHEN sm.Party_Name IS NOT NULL AND LTRIM(sm.Party_Name)<>'' THEN gl.GLName+' ('+sm.Party_Name+')' ELSE gl.GLName END Ledger, CASE WHEN sm.Vat_No IS NULL THEN gl.PanNo ELSE sm.Vat_No END TPAN, CAST(sm.LN_Amount AS DECIMAL(18, 2)) Amount, sm.Remarks
			FROM AMS.SB_Master sm, AMS.GeneralLedger gl
			WHERE sm.Customer_ID = gl.GLID AND sm.R_Invoice = 0 AND sm.CBranch_Id = '{ObjGlobal.SysBranchId}' AND sm.FiscalYearId = '{ObjGlobal.SysFiscalYearId}' ";
        cmdTxt += voucherType.Equals("Print")
            ? "ORDER BY sm.Invoice_Date DESC,sm.SB_Invoice DESC"
            : " ORDER BY sm.SB_Invoice,sm.Invoice_Date ";
        return SqlExtensions.ExecuteDataSet(cmdTxt).Tables[0];
    }

    public DataTable ReturnSalesReturnVoucherNoList(string voucherType)
    {
        var cmdTxt = $@"
			SELECT sm.SR_Invoice VoucherNo, CASE WHEN(SELECT sc.EnglishDate FROM AMS.SystemSetting sc)= 1  THEN CONVERT ( VARCHAR, sm.Invoice_Date, 103 ) ELSE sm.Invoice_Miti END VoucherDate, CASE WHEN sm.Party_Name IS NOT NULL AND LTRIM ( sm.Party_Name ) <> '' THEN gl.GLName + ' (' + sm.Party_Name + ')' ELSE gl.GLName END Ledger, CASE WHEN sm.Vat_No IS NULL THEN gl.PanNo ELSE sm.Vat_No END TPAN, CAST( sm.LN_Amount AS DECIMAL(18,2)) Amount, sm.Remarks
			 FROM AMS.SR_Master sm, AMS.GeneralLedger gl
			 WHERE sm.Customer_ID = gl.GLID AND Action_Type <> 'CANCEL' AND sm.CBranch_Id = '{ObjGlobal.SysBranchId}' AND sm.FiscalYearId = '{ObjGlobal.SysFiscalYearId}'";
        cmdTxt += voucherType.Equals("Print")
            ? "ORDER BY sm.Invoice_Date DESC,sm.SR_Invoice DESC"
            : "ORDER BY sm.SR_Invoice,sm.Invoice_Date ";
        return SqlExtensions.ExecuteDataSet(cmdTxt).Tables[0];
    }

    public DataTable ReturnTodaySalesReports()
    {
        var cmdString = $@"
			SELECT sm.SB_Invoice VoucherNo, CASE WHEN(SELECT sc.EnglishDate FROM AMS.SystemSetting sc)= 1 THEN CONVERT(VARCHAR, sm.Invoice_Date, 103)ELSE sm.Invoice_Miti END VoucherDate, CASE WHEN sm.Party_Name IS NOT NULL AND LTRIM(sm.Party_Name)<>'' THEN gl.GLName+' ('+sm.Party_Name+')' ELSE gl.GLName END Ledger, CASE WHEN sm.Vat_No IS NULL THEN gl.PanNo ELSE sm.Vat_No END TPAN, CAST(sm.LN_Amount AS DECIMAL(18, 2)) Amount, sm.Payment_Mode PaymentMode
			FROM AMS.SB_Master sm, AMS.GeneralLedger gl
			WHERE sm.Customer_Id=gl.GLID AND sm.R_Invoice=0 AND sm.CBranch_Id='{ObjGlobal.SysBranchId}' AND sm.FiscalYearId='{ObjGlobal.SysFiscalYearId}' AND gl.EnterBy='{ObjGlobal.LogInUser}' AND sm.Invoice_Date='{Convert.ToDateTime(DateTime.Now):yyyy-MM-dd}'
			ORDER BY sm.SB_Invoice; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    // STOCK ENTRY VOUCHER LIST

    #region **---------- Stock Entry ----------**

    public DataTable GetProductOpeningVoucherList()
    {
        var cmdString = new StringBuilder();
        cmdString.AppendLine($@"
			SELECT Voucher_No VoucherNo, CASE WHEN (SELECT sc.EnglishDate FROM AMS.SystemSetting sc) = 0 THEN OP_Miti ELSE CONVERT(VARCHAR(10), OP_Date, 101) END VoucherDate, PName, CONVERT(DECIMAL(18, 2), Qty) Qty, CONVERT(DECIMAL(18, 2), Rate) Rate, CONVERT(DECIMAL(18, 2), Amount) Amount
			FROM AMS.ProductOpening OP
				 LEFT OUTER JOIN AMS.Product P ON OP.Product_Id=P.PID
			WHERE (OP.CBranch_Id = '{ObjGlobal.SysBranchId}' OR OP.CBranch_Id IS NULL) AND (OP.CUnit_Id = '{ObjGlobal.SysCompanyUnitId}' OR OP.CUnit_Id IS NULL) AND (OP.FiscalYearId IS NULL OR OP.FiscalYearId = '{ObjGlobal.SysFiscalYearId}')
			ORDER BY Voucher_No, Serial_No ");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString()).Tables[0];
    }

    public DataTable GetStockAdjustmentVoucherList()
    {
        var cmdTxt = $@"
			SELECT sm.StockAdjust_No VoucherNo, CASE WHEN(SELECT sc.EnglishDate FROM AMS.SystemSetting sc)= 1 THEN CONVERT(VARCHAR, sm.VDate, 103)ELSE sm.VMiti END VoucherDate, sm.PhyStockNo, sm.Remarks
			FROM AMS.STA_Master sm
			 WHERE sm.BranchId = '{ObjGlobal.SysBranchId}' AND sm.FiscalYearId = '{ObjGlobal.SysFiscalYearId}'
			ORDER BY sm.StockAdjust_No ; ";
        return SqlExtensions.ExecuteDataSet(cmdTxt).Tables[0];
    }

    public DataTable GetBomVoucherList(string category)
    {
        var cmdString = $@"
			SELECT bm.VoucherNo, Case when (SELECT EnglishDate FROM AMS.SystemSetting)= 0 THEN bm.VMiti ELSE CONVERT(VARCHAR(10), bm.VDate, 101) END VoucherDate, p.PName, CAST(bm.FinishedGoodsQty AS DECIMAL(18, 2)) FinishedGoodsQty, pu.UnitCode, CAST(bm.Amount AS DECIMAL(18, 2)) Amount
			FROM INV.BOM_Master bm
				 LEFT OUTER JOIN AMS.Product p ON bm.FinishedGoodsId = p.PID
				 LEFT OUTER JOIN AMS.ProductUnit pu ON p.PUnit = pu.UID
			WHERE bm.BranchId ='{ObjGlobal.SysBranchId}' ";
        if (category.GetLong() > 0) cmdString += $" AND bm.FinishedGoodsId = '{category}' ";
        //AND bm.FiscalYearId='{ObjGlobal.SysFiscalYearId}';
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetIbomVoucherList()
    {
        var cmdString = $@"
			SELECT bm.VoucherNo, Case when (SELECT EnglishDate FROM AMS.SystemSetting)= 0  THEN bm.VMiti ELSE CONVERT ( VARCHAR(10), bm.VDate, 101 ) END VoucherDate, p.PName, bm.FinishedGoodsQty, pu.UnitCode, CAST(bm.Amount AS DECIMAL(18, 2)) Amount
			 FROM INV.Production_Master bm
				  LEFT OUTER JOIN AMS.Product p ON bm.FinishedGoodsId = p.PID
				  LEFT OUTER JOIN AMS.ProductUnit pu ON p.PUnit = pu.UID
			WHERE bm.BranchId ='{ObjGlobal.SysBranchId}' AND bm.FiscalYearId='{ObjGlobal.SysFiscalYearId}'; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    #endregion **---------- Stock Entry ----------**

    #endregion --------------- TRANSACTION REPORT LIST  ---------------

    // CRM LIST OF MASTER

    #region ------------------- CRM MASTER -------------------

    public DataTable GetClientCollection(string actionTag = "", bool status = true)
    {
        var cmdString = @"
			SELECT ClientId LedgerId,ClientDescription Description,PanNo,ClientAddress,EmailAddress FROM CRM.ClientCollection
			WHERE 1=1 ";
        cmdString += status ? @" AND Status = 1 " : "";
        cmdString += @"
			ORDER BY ClientDescription";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetClientSource(string actionTag = "", bool status = true)
    {
        var cmdString = @"
			SELECT SDescription Description FROM CRM.ClientSource
			WHERE 1=1 ";
        cmdString += status ? @" AND IsActive = 1 " : "";
        cmdString += @"
			ORDER BY SDescription";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    #endregion ------------------- CRM MASTER -------------------

    //OBJECT

    #region ------------------- GLOBAL VIARABLE -------------------

    public string GetToDate()
    {
        return ToDate;
    }

    public string GetFrmName()
    {
        return FrmName1;
    }

    public string SetFrmName(string value)
    {
        return FrmName1 = value;
    }

    public void SetToDate(string value)
    {
        ToDate = value;
    }

    public string FrmType;
    public string SelectQuery;
    public string FromDate;
    public DataTable Data;
    public string SearchText;
    public string FrmName;
    public string FrmName1;
    public string FrmName2;
    public string ToDate;
    public string Size;

    public static string PlValue1;
    public static string PlValue2;
    public static string PlValue3;
    public static string PlValue4;
    public static string PlValue5;
    public static string PlValue6;
    public static string PlValue7;
    public static string PlValue8;
    public static string PlValue9;
    public static string PlValue10;
    public static string Text = string.Empty;

    public string ConType;
    public static int Id = 0;
    public ArrayList HeaderCap;
    public ArrayList ColumnWidths;

    public static string ControlDesc = string.Empty;
    private readonly IVMaster _objVMaster = new ClsVMaster();
    private readonly IHMaster _objHMaster = new ClsHMaster();
    private readonly IMasterSetup _objPick = new ClsMasterSetup();

    #endregion ------------------- GLOBAL VIARABLE -------------------
}