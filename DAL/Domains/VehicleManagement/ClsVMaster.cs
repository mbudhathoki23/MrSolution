using MrDAL.Global.Common;
using MrDAL.Master.Interface;
using MrDAL.Utility.Server;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MrDAL.Domains.VehicleManagement;

public class ClsVMaster : IVMaster
{
    #region ---------------  ClsVMaster ---------------

    public ClsVMaster()
    {
        ObjVMaster = new ObjVMasterSetup();
    }

    #endregion ---------------  ClsVMaster ---------------

    #region --------------- Global Value ---------------

    public class ObjVMasterSetup
    {
        public int TxtGroupId { get; set; }
        public int TxtSubGroupId { get; set; }
        public int TxtSchedule { get; set; }
        public int TxtPrimaryGroupId { get; set; }
        public int TxtSecondaryGroupId { get; set; }
        public int TxtBranchId { get; set; }
        public int TxtCompanyUnitId { get; set; }
        public int TxtMemberTypeId { get; set; }
        public int TxtMemberId { get; set; }

        public long MasterId { get; set; }
        public long TxtLedgerId { get; set; }

        public bool TxtStatus { get; set; }
        public bool TxtIsDefault { get; set; } = false;

        public string _ActionTag { get; set; }
        public string TxtDescription { get; set; }
        public string TxtNepaliName { get; set; }
        public string TxtShortName { get; set; }
        public string TxtPhoneNo { get; set; }
        public string TxtEmailAdd { get; set; }
        public string TxtResPhoneNo { get; set; }
        public string TxtLandLineNo { get; set; }
        public string TxtPrimaryGrp { get; set; }
        public string TxtSecondaryGroup { get; set; }
        public string TxtEnterBy { get; set; }
        public string TxtType { get; set; }
        public string TxtState { get; set; }
        public string TxtGrpType { get; set; }
        public string TxtStartDate { get; set; }
        public string TxtStartMiti { get; set; }
        public string TxtExpireDate { get; set; }
        public string TxtExpireMiti { get; set; }

        public double TxtQty { get; set; }
        public double TxtRate { get; set; }
        public double TxtAmount { get; set; }
    }

    #endregion --------------- Global Value ---------------

    #region --------------- GLobal ---------------

    public ObjVMasterSetup ObjVMaster { get; set; }
    private readonly StringBuilder Query = new();
    private readonly StringBuilder GetString = new();
    private string SQLQuery = string.Empty;
    private readonly GetConnection Connection = new();

    #endregion --------------- GLobal ---------------

    #region --------------- IUD Value ---------------

    private int IUDGlobalValue(StringBuilder _Query)
    {
        GetString.Clear();
        GetString.Append("BEGIN TRY \n");
        GetString.Append("BEGIN TRANSACTION \n");
        GetString.Append(_Query);
        GetString.Append("COMMIT TRANSACTION \n");
        GetString.Append("END TRY \n");
        GetString.Append("BEGIN CATCH \n");
        GetString.Append("PRINT \n");
        GetString.Append("'Error ' + CONVERT(varchar(50), ERROR_NUMBER()) + \n");
        GetString.Append("', Severity ' + CONVERT(varchar(5), ERROR_SEVERITY()) + \n");
        GetString.Append("', State ' + CONVERT(varchar(5), ERROR_STATE()) + \n");
        GetString.Append("', Line ' + CONVERT(varchar(5), ERROR_LINE()) \n");
        GetString.Append("PRINT ERROR_MESSAGE(); \n");
        GetString.Append("IF XACT_STATE() <> 0 BEGIN \n");
        GetString.Append(" ROLLBACK TRANSACTION \n");
        GetString.Append(" END \n");
        GetString.Append(" END CATCH; \n");
        return new SqlCommand(GetString.ToString(), GetConnection.ReturnConnection()).ExecuteNonQuery();
    }

    public int IUDVechileNumber()
    {
        Query.Clear();
        switch (ObjVMaster._ActionTag.ToUpper())
        {
            case "SAVE":
                Query.Append(
                    "INSERT INTO AMS.VehicleNumber (VNDesc, VNShortName, VNState, VNStatus, VNBranchId, VNCompanyUnitId, EnterBy, EnterDate) ");
                Query.Append("VALUES (N'" + ObjVMaster.TxtDescription.Trim().Replace("'", "''") + "', N'" +
                             ObjVMaster.TxtShortName.Trim().Replace("'", "''") + "', ");
                Query.Append(" N'" + ObjVMaster.TxtState + "',");
                Query.Append(ObjVMaster.TxtStatus ? "1," : "0,");
                Query.Append(" " + ObjVMaster.TxtBranchId + ",");
                Query.Append(ObjVMaster.TxtCompanyUnitId != 0 ? " " + ObjVMaster.TxtCompanyUnitId + ", " : "Null,");
                Query.Append(" '" + ObjVMaster.TxtEnterBy + "',");
                Query.Append(" GETDATE() )");
                break;

            case "UPDATE":
                Query.Append(" UPDATE AMS.VehicleNumber SET ");
                Query.Append(" VNDesc = '" + ObjVMaster.TxtDescription.Trim().Replace("'", "''") + "', ");
                Query.Append(" VNShortName =  '" + ObjVMaster.TxtShortName.Trim().Replace("'", "''") + "', ");
                Query.Append(" VNState = '" + ObjVMaster.TxtState + "', ");
                Query.Append(ObjVMaster.TxtStatus ? " VNStatus = 1" : " VNStatus = 0");
                Query.Append(" where VHNoId = " + ObjVMaster.MasterId + " ");
                break;

            case "DELETE":
                Query.Append("Delete from AMS.VehicleNumber  where VHNoId = " + ObjVMaster.MasterId + " ");
                break;
        }

        return IUDGlobalValue(Query);
    }

    public int IUDVechileColor()
    {
        Query.Clear();
        switch (ObjVMaster._ActionTag.ToUpper())
        {
            case "SAVE":
                Query.Append(
                    "INSERT INTO AMS.VehicleColors (VHColorsDesc, VHColorsShortName, VHColorsBranchId, VHColorsCompanyUnitId, VHColorsActive, VHColorsEntryBy, VHColorsEnterDate) ");
                Query.Append("VALUES (N'" + ObjVMaster.TxtDescription.Trim().Replace("'", "''") + "', N'" +
                             ObjVMaster.TxtShortName.Trim().Replace("'", "''") + "', ");
                Query.Append(" " + ObjVMaster.TxtBranchId + ",");
                Query.Append(ObjVMaster.TxtCompanyUnitId != 0 ? " " + ObjVMaster.TxtCompanyUnitId + ", " : "Null,");
                Query.Append(ObjVMaster.TxtStatus ? "1," : "0,");
                Query.Append(" '" + ObjVMaster.TxtEnterBy + "',");
                Query.Append(" GETDATE() )");
                break;

            case "UPDATE":
                Query.Append(" UPDATE AMS.VehicleColors SET ");
                Query.Append(" VHColorsDesc = '" + ObjVMaster.TxtDescription.Trim().Replace("'", "''") + "', ");
                Query.Append(" VHColorsShortName =  '" + ObjVMaster.TxtShortName.Trim().Replace("'", "''") + "', ");
                Query.Append(ObjVMaster.TxtStatus ? " VHColorsActive = 1" : " VHColorsActive = 0");
                Query.Append(" where VHColorsId = " + ObjVMaster.MasterId + " ");
                break;

            case "DELETE":
                Query.Append("Delete from AMS.VehicleColors  where VHColorsId = " + ObjVMaster.MasterId + " ");
                break;
        }

        return IUDGlobalValue(Query);
    }

    public int IUDVechileModel()
    {
        Query.Clear();
        switch (ObjVMaster._ActionTag.ToUpper())
        {
            case "SAVE":
                Query.Append(
                    "INSERT INTO AMS.VehileModel (VHModelDesc, VHModelShortName, VHModelBranchId, VHModelCompanyUnitId, VHModelActive, VHModelEntryBy, VHModelEnterDate)");
                Query.Append("VALUES (N'" + ObjVMaster.TxtDescription.Trim().Replace("'", "''") + "', N'" +
                             ObjVMaster.TxtShortName.Trim().Replace("'", "''") + "', ");
                Query.Append(" " + ObjVMaster.TxtBranchId + ",");
                Query.Append(ObjVMaster.TxtCompanyUnitId != 0 ? " " + ObjVMaster.TxtCompanyUnitId + ", " : "Null,");
                Query.Append(ObjVMaster.TxtStatus ? "1," : "0,");
                Query.Append(" '" + ObjVMaster.TxtEnterBy + "',");
                Query.Append(" GETDATE() )");
                break;

            case "UPDATE":
                Query.Append(" UPDATE AMS.VehileModel SET ");
                Query.Append(" VHModelDesc = '" + ObjVMaster.TxtDescription.Trim().Replace("'", "''") + "', ");
                Query.Append(" VHModelShortName =  '" + ObjVMaster.TxtShortName.Trim().Replace("'", "''") + "', ");
                Query.Append(ObjVMaster.TxtStatus ? " VHModelActive = 1" : " VHModelActive = 0");
                Query.Append(" where VHModelId = " + ObjVMaster.MasterId + " ");
                break;

            case "DELETE":
                Query.Append("Delete from AMS.VehileModel  where VHModelId = " + ObjVMaster.MasterId + " ");
                break;
        }

        return IUDGlobalValue(Query);
    }

    #endregion --------------- IUD Value ---------------

    #region --------------- Data Table ---------------

    public DataTable Get_DataVehicleNumber(string _Tag, int SelectedId = 0)
    {
        if (SelectedId == 0)
        {
            SQLQuery =
                $"SELECT VHNoId LedgerId, VNDesc Description, VNShortName ShortName, VNState State FROM AMS.VehicleNumber vn WHERE vn.VNBranchId ='{ObjGlobal.SysBranchId}'  AND (vn.VNCompanyUnitId='{ObjGlobal.CompanyId}' OR vn.VNCompanyUnitId IS NULL)  ";
            if (!string.IsNullOrEmpty(_Tag) && _Tag == "SAVE")
                SQLQuery += " AND  (vn.VNStatus=1 OR vn.VNStatus IS NULL) ";

            if (!string.IsNullOrEmpty(_Tag) && _Tag == "DELETE")
                SQLQuery += " AND VechileNoId NOT IN(SELECT P.VechileNoId FROM AMS.Product P) ";
            SQLQuery += " Order By VNDesc ";
        }
        else
        {
            SQLQuery = "SELECT * FROM AMS.VehicleNumber vn Where vn.VHNoId ='" + SelectedId + "'";
        }

        return SqlExtensions.ExecuteDataSet(SQLQuery).Tables[0];
    }

    public DataTable Get_DataVehicleColor(string _Tag, int SelectedId = 0)
    {
        if (SelectedId == 0)
        {
            SQLQuery =
                $"SELECT vc.VHColorsId LedgerId, vc.VHColorsDesc Description, vc.VHColorsShortName ShortName FROM AMS.VehicleColors vc  WHERE vc.VHColorsBranchId='{ObjGlobal.SysBranchId}'  AND (vc.VHColorsCompanyUnitId='{ObjGlobal.CompanyId}' OR vc.VHColorsCompanyUnitId IS NULL)  ";
            if (!string.IsNullOrEmpty(_Tag) && _Tag == "SAVE")
                SQLQuery += " AND  (vc.VHColorsActive=1OR vc.VHColorsActive IS NULL) ";
            if (!string.IsNullOrEmpty(_Tag) && _Tag == "DELETE")
                SQLQuery += " AND VHColorsId NOT IN(SELECT P.VHColorsId FROM AMS.Product P) ";
            SQLQuery += " Order By VHColorsDesc ";
        }
        else
        {
            SQLQuery = $"SELECT * FROM AMS.VehicleColors vn Where vn.VHColorsId ='{SelectedId}'";
        }

        return SqlExtensions.ExecuteDataSet(SQLQuery).Tables[0];
    }

    public DataTable Get_DataVehicleModel(string _ActionTag, int SelectedId = 0)
    {
        if (SelectedId == 0)
        {
            SQLQuery =
                $"SELECT vm.VHModelId LedgerId, vm.VHModelDesc Description, vm.VHModelShortName ShortName FROM AMS.VehileModel vm  WHERE vm.VHModelBranchId='{ObjGlobal.SysBranchId}'  AND (vm.VHModelCompanyUnitId='{ObjGlobal.SysBranchId}' OR vm.VHModelCompanyUnitId IS NULL)";
            if (!string.IsNullOrEmpty(_ActionTag) && _ActionTag == "SAVE")
                SQLQuery += " AND   (vm.VHModelActive=1 OR vm.VHModelActive IS NULL) ";
            if (!string.IsNullOrEmpty(_ActionTag) && _ActionTag == "DELETE")
                SQLQuery += " AND VHModelId NOT IN(SELECT P.VHModelId FROM AMS.Product P) ";
            SQLQuery += " Order By VHModelDesc ";
        }
        else
        {
            SQLQuery = "SELECT * FROM AMS.VehicleModel vn Where vn.VHModelId ='" + SelectedId + "'";
        }

        return SqlExtensions.ExecuteDataSet(SQLQuery).Tables[0];
    }

    public DataTable GetCombineDataValue()
    {
        SQLQuery = @"SELECT * FROM (
			SELECT vn5.VechileNoId ID,vn5.VNDesc DESCRIPTION,vn5.VNShortName SHORTNAME,vn5.VNState STATE,vn5.VNStatus STATUS, vn5.VNBranchId BRANCHID, b.Branch_Name BRANCH,vn5.VNCompanyUnitId COMPANYUNITD,CU.CmpUnit_Name COMPANYUNIT ,vn5.EnterBy ENTERBY,vn5.EnterDate ENTERDATE FROM AMS.VechileNumber vn5 INNER JOIN AMS.Branch b ON vn5.VNBranchId=b.Branch_Id LEFT OUTER JOIN AMS.CompanyUnit cu ON vn5.VNCompanyUnitId=CU.CmpUnit_Id
			UNION ALL
			SELECT vc.VHColorsId ID,vc.VHColorsDesc DESCRIPTION,vc.VHColorsShortName SHORTNAME,'' STATE, vc.VHColorsActive STATUS , vc.VHColorsBranchId BRANCHID,b.Branch_Name BRANCH,vc.VHColorsCompanyUnitId COMPANYUNITID,CU.CmpUnit_Name COMPANYUNIT ,vc.VHColorsEntryBy ENTERBY,vc.VHColorsEnterDate ENTERDATE FROM AMS.VechileColors vc INNER JOIN AMS.Branch b ON vc.VHColorsBranchId=b.Branch_Id LEFT OUTER JOIN AMS.CompanyUnit cu ON VC.VHColorsCompanyUnitId=CU.CmpUnit_Id
			UNION ALL
			SELECT vm.VHModelId ID,vm.VHMDesc DESCRIPTION,vm.VHMShortName SHORTNAME,'' STATE,vm.VHMStatus STATUS,vm.VHMBranchId BRANCHID,b.Branch_Name BRANCH,vm.VHMCompanyUnitId COMPANYUNITID,CU.CmpUnit_Name COMPANYUNIT ,vm.VHMEnterBy ENTERBY,vm.VHMEnterDate ENTERDATE FROM AMS.VechileModel vm INNER JOIN AMS.Branch b ON vm.VHMBranchId=b.Branch_Id LEFT OUTER JOIN AMS.CompanyUnit cu ON VM.VHMCompanyUnitId=CU.CmpUnit_Id ";
        SQLQuery +=
            $") AS VEHICLE WHERE VEHICLE.BranchId='{ObjGlobal.SysBranchId}'AND (VEHICLE.COMPANYUNITD='{ObjGlobal.CompanyId}' OR VEHICLE.COMPANYUNITD IS NULL) ";
        return SqlExtensions.ExecuteDataSet(SQLQuery).Tables[0];
    }

    #endregion --------------- Data Table ---------------
}