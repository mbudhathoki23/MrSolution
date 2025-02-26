using MrDAL.Master.Interface;
using MrDAL.Utility.Server;
using System.Data;

namespace MrDAL.Master;

public class ClsListofMaster : IListOfFinanceMaster
{
    public DataTable AccountGroup()
    {
        var cmdString = @"
			SELECT AG.GrpId,AG.GrpName[DESCRIPTION],AG.GrpCode[SHORTNAME],AG.Schedule[SCHEDULE],AG.PrimaryGrp[PRIMARY GROUP],AG.GrpType[GROUP TYPE],B.Branch_Name[BRANCH],IsNull(CU.CmpUnit_Code,'No CompanyUnitSetup') [BRANCH UNIT],Case when AG.Status=1 then 'ACTIVE' else 'DISABLE' end [STATUS], AG.EnterBy [CREATE BY],CONVERT(VARCHAR(10), AG.EnterDate, 103)[CREATED DATE]   from AMS.AccountGroup AG
			LEFT OUTER JOIN AMS.Branch as B on AG.Branch_ID = B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit CU on AG.Company_Id = CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable AccountSubGroup()
    {
        var cmdString = @"
			SELECT ASG.SubGrpId,ASG.SubGrpName [DESCRIPTION],ASG.SubGrpCode[SHORTNAME],AG.GrpName [GROUP DESCRIPTION],B.Branch_Name [BRANCH],CU.CmpUnit_Name [BRANCH UNIT],CASE WHEN AG.Status=1 THEN 'ACTIVE' ELSE 'DISABLE' END [STATUS],AG.EnterBy[CREATED BY], CONVERT(VARCHAR(10), ASG.EnterDate, 103)[CREATED DATE] FROM AMS.AccountSubGroup ASG
			LEFT OUTER JOIN AMS.AccountGroup as AG on ASG.GrpID=AG.GrpId
			LEFT OUTER JOIN AMS.Branch as B on ASG.Branch_ID=B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit as CU on ASG.Company_ID=CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GeneralLedger(string ReportType)
    {
        var cmdString = @"
			SELECT GL.GLID,GL.GLName[DESCRIPTION],GL.GLCode[SHORTNAME],GL.ACCode[ACCOUNTING CODE],GL.GLType[CATEGORY],AG.GrpName[ACCOUNT GROUP],ASG.SubGrpName[ACCOUNT SUBGROUP],GL.PanNo[PAN NO],A.AreaName [AREA],JA.AgentName [SALES MAN],C.Ccode [CURRENCY],GL.CrLimit [CREDIT LIMIT],GL.CrDays[CREDIT DAYS],GL.IntRate [INTEREST RATE],GL.GLAddress [ADDRESS],GL.PhoneNo [PHONE NO],GL.LandLineNo [LANDLINE NO],B.Email [EMAIL ADRESS],B.Branch_Name [BRANCH],CU.CmpUnit_Name [BRANCH UNIT],Case when GL.Status=1 then 'ACTIVE' else 'DISABLE' end [STATUS],GL.EnterBy [CREATED BY],CONVERT(VARCHAR(10), GL.EnterDate, 103)[CREATED DATE] FROM AMS.GeneralLedger GL
			LEFT OUTER JOIN AMS.AccountGroup AS AG ON GL.GrpId=AG.GrpId
			LEFT OUTER JOIN AMS.AccountSubGroup AS ASG ON GL.SubGrpId=ASG.SubGrpId
			LEFT OUTER JOIN AMS.JuniorAgent AS JA ON GL.AgentId=JA.AgentId
			LEFT OUTER JOIN AMS.Area AS A ON GL.AreaId=A.AreaId
			LEFT OUTER JOIN AMS.Currency AS C ON GL.CurrId=C.CId
			LEFT OUTER JOIN AMS.Branch AS B ON GL.Branch_id=B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit AS CU ON GL.Company_Id=CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        var table = SqlExtensions.ExecuteDataSet(cmdString)
            .Tables[0];
        var dtReport = new DataTable();

        return table;
    }

    public DataTable Subledger()
    {
        var cmdString = @"
			SELECT SL.SLId,SL.SLName [DESCRIPTION],SL.SLCode [SHORTNAME],SL.SLAddress [ADDRESS],GL.GLName [LEDGER DESCRIPTION],B.Branch_Name [BRANCH],CU.CmpUnit_Name [BRANCH UNIT],SL.EnterBy[CREATED BY], CONVERT(VARCHAR(10), SL.EnterDate, 103)[CREATED DATE] FROM AMS.SubLedger SL
			LEFT OUTER JOIN AMS.GeneralLedger AS GL ON SL.GLID=GL.GLID
			LEFT OUTER JOIN AMS.Branch AS B ON SL.Branch_id=B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit AS CU ON SL.Company_ID=CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable MainAgent()
    {
        var cmdString = @"
			SELECT SA.SAgentId,SA.SAgent [DESCRIPTION],SA.SAgentCode [SHORTNAME],GL.GLName [LEDGER],SA.PhoneNo [PHONE NO],SA.Address[ADDRESS],B.Branch_Name [BRANCH],CU.CmpUnit_Name[BRANCH UNIT],CASE WHEN SA.Status=1 THEN 'ACTIVE' ELSE 'DISABLE' END [STATUS],SA.EnterBy[CREATED BY], CONVERT(VARCHAR(10), SA.EnterDate, 103)[CREATED DATE] FROM AMS.SeniorAgent SA
			LEFT OUTER JOIN AMS.GeneralLedger AS GL ON SA.GLID=GL.GLID
			LEFT OUTER JOIN AMS.Branch AS B ON SA.Branch_id=B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit AS CU ON SA.Company_ID=CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable Agent()
    {
        var cmdString = @"
			SELECT JA.AgentId,JA.AgentName [DESCRIPTION],JA.AgentCode [SHORTNAME],GL.GLName [LEDGER],JA.PhoneNo [PHONE NO],JA.Address[ADDRESS],JA.Commission [INSENTIVE],SA.SAgent [MAIN SALESMAN],B.Branch_Name [BRANCH],CU.CmpUnit_Name[BRANCH UNIT],CASE WHEN JA.Status=1 THEN 'ACTIVE' ELSE 'DISABLE' END [STATUS],JA.EnterBy[CREATED BY], CONVERT(VARCHAR(10), JA.EnterDate, 103)[CREATED DATE] FROM AMS.JuniorAgent JA
			LEFT OUTER JOIN AMS.SeniorAgent AS SA ON JA.SAgent=SA.SAgentId
			LEFT OUTER JOIN AMS.GeneralLedger AS GL ON JA.GLCode=GL.GLID
			LEFT OUTER JOIN AMS.Branch AS B ON JA.Branch_id=B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit AS CU ON JA.Company_ID=CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable MainArea()
    {
        var cmdString = @"
			SELECT MA.MAreaId,MA.MAreaName[DESCRIPTION],MA.MAreaCode [SHORTNAME],MA.MCountry [COUNTER],CASE WHEN MA.Status=1 THEN 'ACTIVE' ELSE 'DISABLE' END [STATUS],MA.EnterBy[CREATED BY], CONVERT(VARCHAR(10), MA.EnterDate, 103)[CREATED DATE] FROM AMS.MainArea MA
			LEFT OUTER JOIN AMS.Branch AS B ON MA.Branch_id=B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit AS CU ON MA.Company_ID=CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable Area()
    {
        var cmdString = @"
			SELECT A.AreaId,A.AreaName [DESCRIPTION],A.AreaCode [SHORTNAME],A.Country [COUNTRY],B.Branch_Name [BRANCH],CU.CmpUnit_Name[BRANCH UNIT],CASE WHEN A.Status=1 THEN 'ACTIVE' ELSE 'DISABLE' END [STATUS],A.EnterBy[CREATED BY], CONVERT(VARCHAR(10), A.EnterDate, 103)[CREATED DATE] FROM AMS.Area A
			LEFT OUTER JOIN AMS.MainArea AS MA ON A.Main_Area=MA.MAreaId
			LEFT OUTER JOIN AMS.Branch AS B ON A.Branch_id=B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit AS CU ON A.Company_ID=CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable Department()
    {
        var cmdString = @"
			SELECT D.DId,D.DName [DESCRIPTION],D.DCode [SHORTNAME],D.Dlevel [LEVEL],B.Branch_Name [BRANCH],CU.CmpUnit_Name[BRANCH UNIT],CASE WHEN D.Status=1 THEN 'ACTIVE' ELSE 'DISABLE' END [STATUS],D.EnterBy[CREATED BY], CONVERT(VARCHAR(10), D.EnterDate, 103)[CREATED DATE] FROM AMS.Department D
			LEFT OUTER JOIN AMS.Branch AS B ON D.Branch_id=B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit AS CU ON D.Company_ID=CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable Counter()
    {
        var cmdString = @"
			SELECT C.CId,C.CName [DESCRIPTION],C.CCode [SHORTNAME],B.Branch_Name [BRANCH],CU.CmpUnit_Name[BRANCH UNIT],CASE WHEN C.Status=1 THEN 'ACTIVE' ELSE 'DISABLE' END [STATUS],C.EnterBy[CREATED BY], CONVERT(VARCHAR(10), C.EnterDate, 103)[CREATED DATE] FROM AMS.Counter C
			LEFT OUTER JOIN AMS.Branch AS B ON C.Branch_id=B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit AS CU ON C.Company_ID=CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable Floor()
    {
        var cmdString = @"
			SELECT F.FloorId,F.Description [DESCRIPTION],F.ShortName [SHORTNAME],F.Type [DIVISION],B.Branch_Name [BRANCH],CU.CmpUnit_Name[BRANCH UNIT],CASE WHEN F.Status=1 THEN 'ACTIVE' ELSE 'DISABLE' END [STATUS],F.EnterBy[CREATED BY], CONVERT(VARCHAR(10), F.EnterDate, 103)[CREATED DATE] FROM AMS.Floor F
			LEFT OUTER JOIN AMS.Branch AS B ON F.Branch_id=B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit AS CU ON F.Company_ID=CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable Table()
    {
        var cmdString = @"
			SELECT T.TableId,T.TableName [DESCRIPTION],T.TableCode [SHORTNAME],F.FloorId [FLOOR],B.Branch_Name [BRANCH],CU.CmpUnit_Name[BRANCH UNIT],CASE WHEN T.Status=1 THEN 'ACTIVE' ELSE 'DISABLE' END [STATUS],T.EnterBy[CREATED BY], CONVERT(VARCHAR(10), T.EnterDate, 103)[CREATED DATE] FROM AMS.TableMaster T
			LEFT OUTER JOIN AMS.Branch AS B ON T.Branch_id=B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit AS CU ON T.Company_ID=CU.CmpUnit_Id
			LEFT OUTER JOIN AMS.Floor AS F ON T.FloorId=F.FloorId
			ORDER BY DESCRIPTION   ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable Currency()
    {
        var cmdString = @"
			SELECT C.CId,C.CName [DESCRIPTION],C.Ccode [SHORTNAME],C.CRate [RATE],B.Branch_Name [BRANCH],CU.CmpUnit_Name[BRANCH UNIT],CASE WHEN C.Status=1 THEN 'ACTIVE' ELSE 'DISABLE' END [STATUS],C.EnterBy[CREATED BY], CONVERT(VARCHAR(10), C.EnterDate, 103)[CREATED DATE] FROM AMS.Currency C
			LEFT OUTER JOIN AMS.Branch AS B ON C.Branch_id=B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit AS CU ON C.Company_ID=CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable Narration()
    {
        var cmdString = @"
			Select AG.GrpId,AG.GrpName[DESCRIPTION],AG.GrpCode[SHORTNAME],AG.Schedule[SCHEDULE],AG.PrimaryGrp[PRIMARY GROUP],AG.GrpType[GROUP TYPE],B.Branch_Name[BRANCH],IsNull(CU.CmpUnit_Code,'No CompanyUnitSetup') [BRANCH UNIT],Case when AG.Status=1 then 'Active' else 'Disable' end [STATUS], AG.EnterBy [CREATE BY],AG.EnterDate [CREATE DATE]   from AMS.AccountGroup AG
			LEFT OUTER JOIN ams.Branch as B on AG.Branch_ID = B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit CU on AG.Company_Id = CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable DocNumbering()
    {
        var cmdString = @"
			Select AG.GrpId,AG.GrpName[DESCRIPTION],AG.GrpCode[SHORTNAME],AG.Schedule[SCHEDULE],AG.PrimaryGrp[PRIMARY GROUP],AG.GrpType[GROUP TYPE],B.Branch_Name[BRANCH],IsNull(CU.CmpUnit_Code,'No CompanyUnitSetup') [BRANCH UNIT],Case when AG.Status=1 then 'Active' else 'Disable' end [STATUS], AG.EnterBy [CREATE BY],AG.EnterDate [CREATE DATE]   from AMS.AccountGroup AG
			LEFT OUTER JOIN ams.Branch as B on AG.Branch_ID = B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit CU on AG.Company_Id = CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable DocReNumbering()
    {
        var cmdString = @"
			Select AG.GrpId,AG.GrpName[DESCRIPTION],AG.GrpCode[SHORTNAME],AG.Schedule[SCHEDULE],AG.PrimaryGrp[PRIMARY GROUP],AG.GrpType[GROUP TYPE],B.Branch_Name[BRANCH],IsNull(CU.CmpUnit_Code,'No CompanyUnitSetup') [BRANCH UNIT],Case when AG.Status=1 then 'Active' else 'Disable' end [STATUS], AG.EnterBy [CREATE BY],AG.EnterDate [CREATE DATE]   from AMS.AccountGroup AG
			LEFT OUTER JOIN ams.Branch as B on AG.Branch_ID = B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit CU on AG.Company_Id = CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable PurchaseTerm()
    {
        var cmdString = @"
			Select AG.GrpId,AG.GrpName[DESCRIPTION],AG.GrpCode[SHORTNAME],AG.Schedule[SCHEDULE],AG.PrimaryGrp[PRIMARY GROUP],AG.GrpType[GROUP TYPE],B.Branch_Name[BRANCH],IsNull(CU.CmpUnit_Code,'No CompanyUnitSetup') [BRANCH UNIT],Case when AG.Status=1 then 'Active' else 'Disable' end [STATUS], AG.EnterBy [CREATE BY],AG.EnterDate [CREATE DATE]   from AMS.AccountGroup AG
			LEFT OUTER JOIN ams.Branch as B on AG.Branch_ID = B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit CU on AG.Company_Id = CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable SalesTerm()
    {
        var cmdString = @"
			Select AG.GrpId,AG.GrpName[DESCRIPTION],AG.GrpCode[SHORTNAME],AG.Schedule[SCHEDULE],AG.PrimaryGrp[PRIMARY GROUP],AG.GrpType[GROUP TYPE],B.Branch_Name[BRANCH],IsNull(CU.CmpUnit_Code,'No CompanyUnitSetup') [BRANCH UNIT],Case when AG.Status=1 then 'Active' else 'Disable' end [STATUS], AG.EnterBy [CREATE BY],AG.EnterDate [CREATE DATE]   from AMS.AccountGroup AG
			LEFT OUTER JOIN ams.Branch as B on AG.Branch_ID = B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit CU on AG.Company_Id = CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable Branch()
    {
        var cmdString = @"
			Select AG.GrpId,AG.GrpName[DESCRIPTION],AG.GrpCode[SHORTNAME],AG.Schedule[SCHEDULE],AG.PrimaryGrp[PRIMARY GROUP],AG.GrpType[GROUP TYPE],B.Branch_Name[BRANCH],IsNull(CU.CmpUnit_Code,'No CompanyUnitSetup') [BRANCH UNIT],Case when AG.Status=1 then 'Active' else 'Disable' end [STATUS], AG.EnterBy [CREATE BY],AG.EnterDate [CREATE DATE]   from AMS.AccountGroup AG
			LEFT OUTER JOIN ams.Branch as B on AG.Branch_ID = B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit CU on AG.Company_Id = CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable Unit()
    {
        var cmdString = @"
			Select AG.GrpId,AG.GrpName[DESCRIPTION],AG.GrpCode[SHORTNAME],AG.Schedule[SCHEDULE],AG.PrimaryGrp[PRIMARY GROUP],AG.GrpType[GROUP TYPE],B.Branch_Name[BRANCH],IsNull(CU.CmpUnit_Code,'No CompanyUnitSetup') [BRANCH UNIT],Case when AG.Status=1 then 'Active' else 'Disable' end [STATUS], AG.EnterBy [CREATE BY],AG.EnterDate [CREATE DATE]   from AMS.AccountGroup AG
			LEFT OUTER JOIN ams.Branch as B on AG.Branch_ID = B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit CU on AG.Company_Id = CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable ProductGroup()
    {
        var cmdString = @"
			Select AG.GrpId,AG.GrpName[DESCRIPTION],AG.GrpCode[SHORTNAME],AG.Schedule[SCHEDULE],AG.PrimaryGrp[PRIMARY GROUP],AG.GrpType[GROUP TYPE],B.Branch_Name[BRANCH],IsNull(CU.CmpUnit_Code,'No CompanyUnitSetup') [BRANCH UNIT],Case when AG.Status=1 then 'Active' else 'Disable' end [STATUS], AG.EnterBy [CREATE BY],AG.EnterDate [CREATE DATE]   from AMS.AccountGroup AG
			LEFT OUTER JOIN ams.Branch as B on AG.Branch_ID = B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit CU on AG.Company_Id = CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable ProductSubGroup()
    {
        var cmdString = @"
			Select AG.GrpId,AG.GrpName[DESCRIPTION],AG.GrpCode[SHORTNAME],AG.Schedule[SCHEDULE],AG.PrimaryGrp[PRIMARY GROUP],AG.GrpType[GROUP TYPE],B.Branch_Name[BRANCH],IsNull(CU.CmpUnit_Code,'No CompanyUnitSetup') [BRANCH UNIT],Case when AG.Status=1 then 'Active' else 'Disable' end [STATUS], AG.EnterBy [CREATE BY],AG.EnterDate [CREATE DATE]   from AMS.AccountGroup AG
			LEFT OUTER JOIN ams.Branch as B on AG.Branch_ID = B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit CU on AG.Company_Id = CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable ProductUnit()
    {
        var cmdString = @"
			Select AG.GrpId,AG.GrpName[DESCRIPTION],AG.GrpCode[SHORTNAME],AG.Schedule[SCHEDULE],AG.PrimaryGrp[PRIMARY GROUP],AG.GrpType[GROUP TYPE],B.Branch_Name[BRANCH],IsNull(CU.CmpUnit_Code,'No CompanyUnitSetup') [BRANCH UNIT],Case when AG.Status=1 then 'Active' else 'Disable' end [STATUS], AG.EnterBy [CREATE BY],AG.EnterDate [CREATE DATE]   from AMS.AccountGroup AG
			LEFT OUTER JOIN ams.Branch as B on AG.Branch_ID = B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit CU on AG.Company_Id = CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable Godown()
    {
        var cmdString = @"
			Select AG.GrpId,AG.GrpName[DESCRIPTION],AG.GrpCode[SHORTNAME],AG.Schedule[SCHEDULE],AG.PrimaryGrp[PRIMARY GROUP],AG.GrpType[GROUP TYPE],B.Branch_Name[BRANCH],IsNull(CU.CmpUnit_Code,'No CompanyUnitSetup') [BRANCH UNIT],Case when AG.Status=1 then 'Active' else 'Disable' end [STATUS], AG.EnterBy [CREATE BY],AG.EnterDate [CREATE DATE]   from AMS.AccountGroup AG
			LEFT OUTER JOIN ams.Branch as B on AG.Branch_ID = B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit CU on AG.Company_Id = CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable CostCentert()
    {
        var cmdString = @"
			Select AG.GrpId,AG.GrpName[DESCRIPTION],AG.GrpCode[SHORTNAME],AG.Schedule[SCHEDULE],AG.PrimaryGrp[PRIMARY GROUP],AG.GrpType[GROUP TYPE],B.Branch_Name[BRANCH],IsNull(CU.CmpUnit_Code,'No CompanyUnitSetup') [BRANCH UNIT],Case when AG.Status=1 then 'Active' else 'Disable' end [STATUS], AG.EnterBy [CREATE BY],AG.EnterDate [CREATE DATE]   from AMS.AccountGroup AG
			LEFT OUTER JOIN ams.Branch as B on AG.Branch_ID = B.Branch_Id
			LEFT OUTER JOIN AMS.CompanyUnit CU on AG.Company_Id = CU.CmpUnit_Id
			ORDER BY DESCRIPTION  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }
}