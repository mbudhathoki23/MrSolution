using MrDAL.Core.Extensions;
using MrDAL.Domains.Hospital.ViewModule;
using MrDAL.Global.Common;
using MrDAL.Master.Interface;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace MrDAL.Master;

public class ClsHMaster : IHMaster
{
    // HOSPITAL MODULE

    #region --------------- CLASS FOR HOSPITAL MODULE ---------------

    public ClsHMaster()
    {
        ObjBedMaster = new VmBedMaster();
        ObjBedType = new VmBedType();
        ObjDoctor = new VmDoctor();
        VmPatient = new VmPatient();
        VmDoctorType = new VmDoctorType();
    }

    #endregion --------------- CLASS FOR HOSPITAL MODULE ---------------

    // RETURN VALUE IN DATA TABLE

    #region --------------- RETURN VALUE IN DATA TABLE ---------------

    public DataTable CheckPatientIdExitsOrNot(string patientId)
    {
        var cmdString = $"SELECT * FROM HOS.PatientMaster pm WHERE pm.ShortName='{patientId}'";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable ReturnPatientInformation(long patientId)
    {
        var cmdString = $@"
            SELECT dr.DrName, d.DName, pm.*
            FROM HOS.PatientMaster pm
                LEFT OUTER JOIN HOS.Department d ON d.DId = pm.DepartmentId
                LEFT OUTER JOIN HOS.Doctor dr ON dr.DrId = d.DoctorId
            WHERE pm.PaitentId= {patientId}";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable MasterDoctor(string actionTag, int selectedId = 0)
    {
        var cmdString = selectedId == 0
            ? "SELECT DrId Id,DRName Description,DrShortName ShortName,DrTypeDesc,ContactNo,Address,DP.DName  DName FROM HOS.Doctor As D left Outer Join HOS.DoctorType as DT on D.Drtype=Dt.DTID  left outer Join HOS.Department as DP on DP.DoctorId=D.DrId where (D.Status is NUll	or D.Status=1) and (D.BranchId is null or D.BranchId='1') and (D.CompanyUnitSetup is NUll or D.CompanyUnitSetup='') ORDER BY DrName"
            : "Select * from FROM HOS.Doctor  where DrId='" + selectedId + "' ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetDoctorTypeInformation(int doctorTypeId)
    {
        if (doctorTypeId > 0)
        {
            var cmdString = $"SELECT * FROM HOS.DoctorType WHERE DtID = {doctorTypeId}";
            return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        }

        return new DataTable();
    }

    public DataTable MasterHDepartment(int selectedId = 0)
    {
        var cmdString = selectedId == 0
            ? "SELECT d.DId Id, d.DName Description,d.DCode ShortName,d1.DrName Doctor,d.ChargeAmt Rate FROM HOS.Department d LEFT OUTER JOIN HOS.Doctor d1 ON D.DoctorId=d1.DrId WHERE (d.Status IS NULL OR d.Status=1)"
            : "Select * from FROM HOS.Department  where DId='" + selectedId + "' ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetPatientListOnDashboard()
    {
        var cmdString = @"
            SELECT pm.AccountLedger PATIENT_DESC,PM.ContactNo CONTACT_NO,pm.TAddress TEMP_ADDRESS,pm.Age AGE,pm.RegType REG_TYPE,pm.Nationality NATIONALITY,D.DName DEPARTMENT,D1.DrName DOCTOR_DESC FROM HOS.PatientMaster AS pm
            LEFT OUTER JOIN HOS.Department d ON pm.DepartmentId = d.DId
            LEFT OUTER JOIN HOS.Doctor d1 ON pm.DrId = d1.DrId
            ORDER BY pm.EnterDate DESC,pm.PaitentDesc";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public ComboBox ReturnTitleOfName(ComboBox box)
    {
        var list = new List<ValueModel<string, string>>
        {
            new("Mr.", "Mr."),
            new("Mrs.", "Mrs."),
            new("Dr.", "Dr."),
            new("Master", "Mtr."),
            new("Prof.", "Prf."),
            new("Er.", "Er."),
            new("Miss", "Miss")
        };
        box.DataSource = list;
        box.DisplayMember = "Item1";
        box.ValueMember = "Item2";
        return box;
    }

    public ComboBox ReturnAgeType(ComboBox box)
    {
        var list = new List<ValueModel<string, string>>
        {
            new("Year", "Y"),
            new("Months", "M"),
            new("Days", "D")
        };
        box.DataSource = list;
        box.DisplayMember = "Item1";
        box.ValueMember = "Item2";
        return box;
    }

    public ComboBox ReturnGender(ComboBox box)
    {
        var list = new List<ValueModel<string, string>>
        {
            new("Male", "M"),
            new("Female", "F"),
            new("Other", "O")
        };
        box.DataSource = list;
        box.DisplayMember = "Item1";
        box.ValueMember = "Item2";
        return box;
    }

    public ComboBox ReturnMartial(ComboBox box)
    {
        var list = new List<ValueModel<string, string>>
        {
            new("Married", "M"),
            new("Single", "S"),
            new("Divorced", "D"),
            new("Widowed", "W")
        };
        box.DataSource = list;
        box.DisplayMember = "Item1";
        box.ValueMember = "Item2";
        return box;
    }

    public ComboBox ReturnRegistrationType(ComboBox box)
    {
        var list = new List<ValueModel<string, string>>
        {
            new("OPD", "OPD"),
            new("IPD", "IPD"),
            new("Emergency", "E")
        };
        box.DataSource = list;
        box.DisplayMember = "Item1";
        box.ValueMember = "Item2";
        return box;
    }

    public ComboBox ReturnReligion(ComboBox box)
    {
        var list = new List<ValueModel<string, string>>
        {
            new("Buddhism", "Buddhism"),
            new("Hinduism", "Hinduism"),
            new("Muslims", "Muslims"),
            new("Christianity", "Christianity"),
            new("Roman Catholicism", "Roman Catholicism"),
            new("Jainism", "Jainism"),
            new("Bahia Faith", "Bahia Faith"),
            new("Sikhism", "Sikhism"),
            new("Judaism", "Judaism"),
            new("Islam", "Islam")
        };
        box.DataSource = list;
        box.DisplayMember = "Item1";
        box.ValueMember = "Item2";
        return box;
    }

    public ComboBox ReturnNationality(ComboBox box)
    {
        var list = new List<ValueModel<string, string>>
        {
            new("Nepalese", "Nepalese"),
            new("Chinese", "Chinese"),
            new("Indian", "Indian")
        };
        box.DataSource = list;
        box.DisplayMember = "Item1";
        box.ValueMember = "Item2";
        box.SelectedIndex = box.FindString("Nepalese");
        return box;
    }

    public ComboBox ReturnBloodGroup(ComboBox box)
    {
        var list = new List<ValueModel<string, string>>
        {
            new("O+", "O+"),
            new("O-", "O-"),
            new("A+", "A+"),
            new("A-", "A-"),
            new("B+", "B+"),
            new("B-", "B-"),
            new("AB+", "AB+"),
            new("AB-", "AB-")
        };
        box.DataSource = list;
        box.DisplayMember = "Item1";
        box.ValueMember = "Item2";
        box.SelectedIndex = 0;
        return box;
    }

    #endregion --------------- RETURN VALUE IN DATA TABLE ---------------

    #region --------------- AUDIT LOG ---------------

    private int AuditLogBedMaster()
    {
        var cmdString = $@"
            INSERT INTO AUD.AUDIT_BEDMASTER	 (BID, BedDesc, BedShortName, Bedtype, WId, ChargeAmt, Branch_ID, Company_ID, Status, EnterBy, EnterDate, ModifyAction, ModifyBy, ModifyDate)
			SELECT BID, BedDesc, BedShortName, Bedtype, WId, ChargeAmt, Branch_ID, Company_ID, Status, EnterBy, EnterDate , '{ObjBedMaster._ActionTag}', '{ObjGlobal.LogInUser}', GETDATE()
            FROM HOS.BedMaster
            WHERE BID = {ObjBedMaster.BID} ; ";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }

    private int AuditLogBedType()
    {
        var cmdString = $@"
            INSERT INTO AUD.AUDIT_BEDTYPE (BID, BDesc, BShortName, BranchId, Company_Unit, EnterBy, EnterDate, Status, ModifyAction, ModifyBy, ModifyDate)
			SELECT BID, BDesc, BShortName, BranchId, Company_Unit, EnterBy, EnterDate, Status, '{ObjBedMaster._ActionTag}', '{ObjGlobal.LogInUser}', GETDATE()
            FROM HOS.BedMaster
            WHERE BID = {ObjBedMaster.BID} ; ";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }

    private int SaveAuditLogDoctorType()
    {
        var cmdString = $@"
            INSERT INTO AUD.AUDIT_DOCTORTYPE(DtID, DrTypeDesc, DrTypeShortName, BranchId, Company_Unit, EnterBy, EnterDate, Status, ModifyAction, ModifyBy, ModifyDate)
            SELECT DtID, DrTypeDesc, DrTypeShortName, BranchId, Company_Unit, EnterBy, EnterDate, Status, '{ObjBedMaster._ActionTag}' ModifyAction, '{ObjGlobal.LogInUser}' ModifyBy,GETDATE()  ModifyDate
            FROM HOS.DoctorType
            WHERE DtID = {VmDoctorType.DtID} ; ";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }

    private int AuditLogDoctor()
    {
        return 1;
    }

    #endregion --------------- AUDIT LOG ---------------

    #region ---------- IUD ----------

    public int SaveBedNo()
    {
        var cmdString = new StringBuilder();
        if (ObjBedMaster._ActionTag.ToUpper() is "SAVE")
        {
            cmdString.Append(
                "INSERT INTO HOS.BedMaster (BID, BedDesc, BedShortName, Bedtype, WId, ChargeAmt, Branch_ID, Company_ID, Status, EnterBy, EnterDate, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) \n");
            cmdString.Append($"Values({ObjBedMaster.BID}, ");
            cmdString.Append(!string.IsNullOrEmpty(ObjBedMaster.BedDesc) ? $"N'{ObjBedMaster.BedDesc}'," : "NULL,");
            cmdString.Append(!string.IsNullOrEmpty(ObjBedMaster.BedShortName)
                ? $"N'{ObjBedMaster.BedShortName}',"
                : "NULL,");
            cmdString.Append(ObjGlobal.ReturnInt(ObjBedMaster.Bedtype.ToString()) > 0
                ? $"N'{ObjBedMaster.Bedtype}',"
                : "NULL,");
            cmdString.Append(ObjGlobal.ReturnInt(ObjBedMaster.WId.ToString()) > 0
                ? $"N'{ObjBedMaster.WId}',"
                : "NULL,");
            cmdString.Append(ObjGlobal.ReturnDecimal(ObjBedMaster.ChargeAmt.ToString()) > 0
                ? $"N'{ObjBedMaster.ChargeAmt}',"
                : "NULL,");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" N'{ObjGlobal.SysBranchId}'," : "NULL,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"N'{ObjGlobal.SysCompanyUnitId}'," : "NULL,");
            cmdString.Append(ObjBedMaster.Status is true ? "1" : "0,");
            cmdString.Append($"'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append(ObjGlobal.IsOnlineSync is true ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync
                ? ObjGlobal.LocalOriginId.HasValue ? " '" + ObjGlobal.LocalOriginId + "'," : "NULL,"
                : "NULL,");
            cmdString.Append($"GetDate(),GetDate(),{ObjBedMaster.SyncRowVersion} , ");
            cmdString.Append(ObjGlobal.IsOnlineSync is true ? "NEWID()," : "NULL)");
        }
        else if (ObjBedMaster._ActionTag.ToUpper() is "UPDATE")

        {
            cmdString.Append("UPDATE HOS.BedMaster SET) \n");
            cmdString.Append(!string.IsNullOrEmpty(ObjBedMaster.BedDesc)
                ? $"BedDesc = N'{ObjBedMaster.BedDesc}',"
                : "BedDesc = NULL,");
            cmdString.Append(!string.IsNullOrEmpty(ObjBedMaster.BedShortName)
                ? $"BedDesc = N'{ObjBedMaster.BedShortName}',"
                : " BedDesc = NULL,");
            cmdString.Append(ObjGlobal.ReturnInt(ObjBedMaster.Bedtype.ToString()) > 0
                ? $" Bedtype = N'{ObjBedMaster.Bedtype}',"
                : "Bedtype = NULL,");
            cmdString.Append(ObjGlobal.ReturnInt(ObjBedMaster.WId.ToString()) > 0
                ? $" WId = N'{ObjBedMaster.WId}',"
                : " WId = NULL,");
            cmdString.Append(ObjGlobal.ReturnDecimal(ObjBedMaster.ChargeAmt.ToString()) > 0
                ? $" ChargeAmt = N'{ObjBedMaster.ChargeAmt}',"
                : "ChargeAmt = NULL,");
            cmdString.Append(ObjBedMaster.Status is true ? "Status = 1," : "Status = 0,");
            cmdString.Append("SyncLastPatchedOn = GETDATE(),");
            cmdString.Append($"SyncRowVersion = {ObjBedMaster.SyncRowVersion}");
            cmdString.Append($" WHERE BID = {ObjBedMaster.BID}; ");
        }
        else if (ObjBedMaster._ActionTag.ToUpper() is "DELETE")
        {
            AuditLogBedMaster();
            cmdString.Append($"DELETE FROM hos.BedMaster WHERE BID = {ObjBedMaster.BID}; ");
        }

        var Exe = new SqlCommand(cmdString.ToString(), GetConnection.ReturnConnection()).ExecuteNonQuery();
        if (Exe > 0)
            if (ObjBedMaster._ActionTag != "DELETE")
                AuditLogBedMaster();
        return Exe;
    }

    public int SaveDoctor()
    {
        var cmdString = new StringBuilder();
        if (ObjDoctor._ActionTag.ToUpper() is "SAVE")
        {
            cmdString.Append(
                "INSERT INTO hos.Doctor	 (DrId, DrName, DrShortName, DrType, ContactNo, Address, BranchId, CompanyUnitSetup, Status, EnterBy, EnterDate, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)");
            cmdString.Append($"Values({ObjDoctor.DrId},");
            cmdString.Append(!string.IsNullOrEmpty(ObjDoctor.DrName) ? $"N'{ObjDoctor.DrName}' , " : "NUll,");
            cmdString.Append(!string.IsNullOrEmpty(ObjDoctor.DrShortName)
                ? $"N'{ObjDoctor.DrShortName}' , "
                : "NUll,");
            cmdString.Append(ObjGlobal.ReturnInt(ObjDoctor.DrType.ToString()) > 0
                ? $"N'{ObjDoctor.DrType}',"
                : "NULL,");
            cmdString.Append(ObjGlobal.ReturnDecimal(ObjDoctor.ContactNo.ToString()) > 0
                ? $"N'{ObjDoctor.ContactNo}',"
                : "NULL,");
            cmdString.Append(!string.IsNullOrEmpty(ObjDoctor.Address) ? $"N'{ObjDoctor.Address}' , " : "NUll,");
            cmdString.Append($"N'{ObjGlobal.SysBranchId}',");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"N'{ObjGlobal.SysCompanyUnitId}'," : "NULL,");
            cmdString.Append(ObjBedMaster.Status is true ? "1" : "0");
            cmdString.Append(ObjGlobal.IsOnlineSync is true ? "NEWID()," : "NULL");
            cmdString.Append(ObjGlobal.IsOnlineSync
                ? ObjGlobal.LocalOriginId.HasValue ? " '" + ObjGlobal.LocalOriginId + "'," : "NULL,"
                : "NULL,");
            cmdString.Append($"GetDate(),GetDate(),{ObjDoctor.SyncRowVersion} , ");
            cmdString.Append(ObjGlobal.IsOnlineSync is true ? "NEWID()," : "NULL)");
        }
        else if (ObjDoctor._ActionTag.ToUpper() is "UPDATE")
        {
            cmdString.Append(" UPDATE HOS.Doctor SET  ");
            cmdString.Append(!string.IsNullOrEmpty(ObjDoctor.DrName)
                ? $"DrName = N'{ObjDoctor.DrName}' , "
                : "DrName = NUll,");
            cmdString.Append(!string.IsNullOrEmpty(ObjDoctor.DrShortName)
                ? $"DrShortName = N'{ObjDoctor.DrShortName}' , "
                : "DrShortName =  NUll,");
            cmdString.Append(ObjGlobal.ReturnInt(ObjDoctor.DrType.ToString()) > 0
                ? $"DrType = N'{ObjDoctor.DrType}',"
                : "DrType = NULL,");
            cmdString.Append(ObjGlobal.ReturnDecimal(ObjDoctor.ContactNo.ToString()) > 0
                ? $"ContactNo = N'{ObjDoctor.ContactNo}',"
                : "ContactNo = NULL,");
            cmdString.Append(!string.IsNullOrEmpty(ObjDoctor.Address)
                ? $"Address = N'{ObjDoctor.Address}' , "
                : "Address = NUll,");
            cmdString.Append("SyncLastPatchedOn = GETDATE(),");
            cmdString.Append($"SyncRowVersion =  {ObjDoctor.SyncRowVersion} ");
            cmdString.Append($" WHERE DrId = {ObjDoctor.DrId}; ");
        }
        else if (ObjDoctor._ActionTag.ToUpper() is "DELETE")
        {
            AuditLogDoctor();
            cmdString.Append($"DELETE FROM HOS.DoctorType WHERE DtID   = {ObjDoctor.DrId}");
        }

        var Exe = new SqlCommand(cmdString.ToString(), GetConnection.ReturnConnection()).ExecuteNonQuery();
        if (Exe > 0)
            if (ObjDoctor._ActionTag != "DELETE")
                AuditLogDoctor();
        return Exe;
    }

    public int SaveBedType()
    {
        var cmdString = new StringBuilder();
        if (ObjBedType.ActionTag.ToUpper() is "SAVE")
        {
            cmdString.Append(
                "INSERT INTO HOS.BedType (BID, BDesc, BShortName, BranchId, Company_Unit, EnterBy, EnterDate, Status, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) \n");
            cmdString.Append($"Values({ObjBedType.BID}, ");
            cmdString.Append(!string.IsNullOrEmpty(ObjBedType.BDesc) ? $"N'{ObjBedType.BDesc}'," : "NULL,");
            cmdString.Append(
                !string.IsNullOrEmpty(ObjBedType.BShortName) ? $"N'{ObjBedType.BShortName}'," : "NULL,");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" N'{ObjGlobal.SysBranchId}'," : "NULL,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"N'{ObjGlobal.SysCompanyUnitId}'," : "NULL,");
            cmdString.Append($"'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append(ObjBedType.Status is true ? "1," : "0,");
            cmdString.Append(ObjGlobal.IsOnlineSync is true ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync
                ? ObjGlobal.LocalOriginId.HasValue ? " '" + ObjGlobal.LocalOriginId + "'," : "NULL,"
                : "NULL,");
            cmdString.Append($"GetDate(),GetDate(),{ObjBedType.SyncRowVersion} , ");
            cmdString.Append(ObjGlobal.IsOnlineSync is true ? "NEWID()," : "NULL)");
        }
        else if (ObjBedType.ActionTag.ToUpper() is "UPDATE")
        {
            cmdString.Append(" UPDATE HOS.BedType SET ");
            cmdString.Append(!string.IsNullOrEmpty(ObjBedType.BDesc) ? $"N'{ObjBedType.BDesc}'," : "NULL,");
            cmdString.Append(
                !string.IsNullOrEmpty(ObjBedType.BShortName) ? $"N'{ObjBedType.BShortName}'," : "NULL,");
            cmdString.Append(ObjBedType.Status is true ? "Status = 1," : "Status = 0,");
            cmdString.Append("SyncLastPatchedOn = GETDATE(),");
            cmdString.Append($"SyncRowVersion = {ObjBedType.SyncRowVersion}");
            cmdString.Append($" WHERE BID = {ObjBedType.BID}; ");
        }
        else if (ObjBedType.ActionTag.ToUpper() is "DELETE")
        {
            AuditLogBedType();
            cmdString.Append($"Delete from hos.BedType WHERE BID = {ObjBedType.BID}; ");
        }

        var Exe = new SqlCommand(cmdString.ToString(), GetConnection.ReturnConnection()).ExecuteNonQuery();
        if (Exe > 0)
            if (ObjBedType.ActionTag != "DELETE")
                AuditLogBedType();
        return Exe;
    }

    public int SaveDoctorType()
    {
        var cmdString = string.Empty;
        if (VmDoctorType.ActionTag.ToUpper() is "SAVE")
        {
            cmdString = $@"
                INSERT INTO HOS.DoctorType(DtID, NepaliDesc, DrTypeDesc, DrTypeShortName, BranchId, Company_Unit, EnterBy, EnterDate, Status, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                VALUES({VmDoctorType.DtID},N'{VmDoctorType.NepaliDesc}',N'{VmDoctorType.DrTypeDesc}',N'{VmDoctorType.DrTypeShortName}',";
            cmdString += @$"{VmDoctorType.BranchId},";
            cmdString += VmDoctorType.Company_Unit > 0 ? @$"{VmDoctorType.Company_Unit}," : "NULL,";
            cmdString += @$"'{VmDoctorType.EnterBy}','{VmDoctorType.EnterDate}'";
            cmdString += VmDoctorType.Status ? @"1," : "0,";
            cmdString += @$"'{VmDoctorType.SyncBaseId}','{VmDoctorType.SyncGlobalId}','{VmDoctorType.SyncOriginId}',";
            cmdString += @"GETDATE(),GETDATE(),";
            cmdString += @$"{VmDoctorType.SyncRowVersion}";
            cmdString += @" ); ";
        }
        else if (VmDoctorType.ActionTag.ToUpper() is "UPDATE")
        {
            cmdString = @"
                UPDATE HOS.DoctorType SET ";
            cmdString += @$" NepaliDesc = N'{VmDoctorType.NepaliDesc}'";
            cmdString += @$" DrTypeDesc = N'{VmDoctorType.DrTypeDesc}'";
            cmdString += @$" DrTypeShortName = N'{VmDoctorType.DrTypeShortName}'";
            cmdString += VmDoctorType.Status ? @"Status = 1," : "Status = 0,";
            cmdString += @$"SyncRowVersion = {VmDoctorType.SyncRowVersion}";
            cmdString += $@" WHERE DtID = {VmDoctorType.DtID}; ";
        }
        else if (VmDoctorType.ActionTag.ToUpper() is "DELETE")
        {
            SaveAuditLogDoctorType();
            cmdString = $@"DELETE HOS.DoctorType WHERE DtID = {VmDoctorType.DtID}";
        }

        var result = SqlExtensions.ExecuteNonQuery(cmdString);
        if (result != 0 && VmDoctorType.ActionTag != "DELETE")
        {
        }

        return result;
    }

    public int SavePatient()
    {
        var cmdString = new StringBuilder();

        if (VmPatient.ActionTag.ToUpper() is "SAVE")
        {
            cmdString.Append(
                "INSERT INTO HOS.PatientMaster(PaitentId, RefDate, IPDId, Title, NepaliDesc, PaitentDesc, ShortName, TAddress, PAddress, AccountLedger, ContactNo, Age, AgeType, DateOfBirth, Gender, MaritalStatus, RegType, Nationality, Religion, BloodGrp, DepartmentId, DrId, RefDrDesc, EmailAdd, PastHistory, ContactPer, PhoneNo, Status, EnterBy, EnterDate, BranchId, CompanyUnitSetup, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)");
            cmdString.Append($"VALUES ({VmPatient.PaitentId}, '{VmPatient.RefDate.GetSystemDate()}',");
            cmdString.Append(VmPatient.IpdId > 0 ? $"{VmPatient.IpdId}," : "NULL,");
            cmdString.Append(
                $"'{VmPatient.Title}','{VmPatient.NepaliDesc}','{VmPatient.PaitentDesc}','{VmPatient.ShortName}',");
            cmdString.Append($"'{VmPatient.TAddress}',");
            cmdString.Append(VmPatient.PAddress.IsValueExits() ? $"'{VmPatient.PAddress}'," : "NULL,");
            cmdString.Append($"'{VmPatient.AccountLedger}',");
            cmdString.Append(VmPatient.ContactNo.IsValueExits() ? $"'{VmPatient.ContactNo}'," : "NULL,");
            cmdString.Append(
                $"{VmPatient.Age.GetInt()},'{VmPatient.AgeType}','{VmPatient.DateOfBirth.GetSystemDate()}','{VmPatient.Gender}',");
            cmdString.Append(
                $"'{VmPatient.MaritalStatus}','{VmPatient.RegType}','{VmPatient.Nationality}','{VmPatient.Religion}',");
            cmdString.Append($"'{VmPatient.BloodGrp}',");
            cmdString.Append(VmPatient.DepartmentId > 0 ? $"{VmPatient.DepartmentId}," : "NULL,");
            cmdString.Append(VmPatient.DrId > 0 ? $"{VmPatient.DrId}," : "NULL,");
            cmdString.Append(VmPatient.RefDrDesc.IsValueExits() ? $"'{VmPatient.RefDrDesc}'," : "NULL,");
            cmdString.Append(VmPatient.EmailAdd.IsValueExits() ? $"'{VmPatient.EmailAdd}'," : "NULL,");
            cmdString.Append(VmPatient.PastHistory.IsValueExits() ? $"'{VmPatient.PastHistory}'," : "NULL,");
            cmdString.Append(VmPatient.ContactPer.IsValueExits() ? $"'{VmPatient.ContactPer}'," : "NULL,");
            cmdString.Append(VmPatient.PhoneNo.IsValueExits() ? $"'{VmPatient.PhoneNo}'," : "NULL,");
            cmdString.Append(
                $"CAST('{VmPatient.Status}' AS BIT),'{ObjGlobal.LogInUser}',GETDATE(),{ObjGlobal.SysBranchId},");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"{ObjGlobal.SysCompanyUnitId}," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
            cmdString.Append($"GETDATE(),{VmPatient.SyncRowVersion.GetDecimal(true)}); \n");
        }
        else if (VmPatient.ActionTag.ToUpper() is "UPDATE")
        {
            cmdString.Append("UPDATE HOS.PatientMaster SET ");
            cmdString.Append(VmPatient.IpdId > 0 ? $"IPDId = {VmPatient.IpdId}," : "IPDId = NULL,");
            cmdString.Append(
                $"Title = '{VmPatient.Title}',NepaliDesc = '{VmPatient.NepaliDesc}',PaitentDesc = '{VmPatient.PaitentDesc}',ShortName = '{VmPatient.ShortName}',");
            cmdString.Append($"TAddress = '{VmPatient.TAddress}',");
            cmdString.Append(VmPatient.PAddress.IsValueExits()
                ? $"PAddress = '{VmPatient.PAddress}',"
                : "PAddress = NULL,");
            cmdString.Append($"AccountLedger = '{VmPatient.AccountLedger}',");
            cmdString.Append(VmPatient.ContactNo.IsValueExits()
                ? $"ContactNo = '{VmPatient.ContactNo}',"
                : "ContactNo = NULL,");
            cmdString.Append(
                $"Age = {VmPatient.Age.GetInt()},AgeType = '{VmPatient.AgeType}',DateOfBirth = '{VmPatient.DateOfBirth.GetSystemDate()}',Gender = '{VmPatient.Gender}',");
            cmdString.Append(
                $"MaritalStatus = '{VmPatient.MaritalStatus}',RegType = '{VmPatient.RegType}',Nationality = '{VmPatient.Nationality}',Religion = '{VmPatient.Religion}',");
            cmdString.Append($"BloodGrp = '{VmPatient.BloodGrp}',");
            cmdString.Append(VmPatient.DepartmentId > 0
                ? $" DepartmentId= {VmPatient.DepartmentId},"
                : " DepartmentId=NULL,");
            cmdString.Append(VmPatient.DrId > 0 ? $"DrId = {VmPatient.DrId}," : "DrId = NULL,");
            cmdString.Append(VmPatient.RefDrDesc.IsValueExits()
                ? $"RefDrDesc = '{VmPatient.RefDrDesc}',"
                : "RefDrDesc = NULL,");
            cmdString.Append(VmPatient.EmailAdd.IsValueExits()
                ? $"EmailAdd = '{VmPatient.EmailAdd}',"
                : "EmailAdd = NULL,");
            cmdString.Append(VmPatient.PastHistory.IsValueExits()
                ? $"PastHistory ='{VmPatient.PastHistory}',"
                : "PastHistory =NULL,");
            cmdString.Append(VmPatient.ContactPer.IsValueExits()
                ? $"ContactPer ='{VmPatient.ContactPer}',"
                : "ContactPer = NULL,");
            cmdString.Append(VmPatient.PhoneNo.IsValueExits()
                ? $"PhoneNo = '{VmPatient.PhoneNo}',"
                : "PhoneNo = NULL,");
            cmdString.Append($"Status = CAST('{VmPatient.Status}' AS BIT),");
            cmdString.Append(
                $"SyncLastPatchedOn = GETDATE(),SyncRowVersion = {VmPatient.SyncRowVersion.GetDecimal(true)} where PaitentId= {VmPatient.PaitentId}; \n");
        }

        if (VmPatient.ActionTag.ToUpper() is "DELETE")
            cmdString.Append($" DELETE HOS.PatientMaster WHERE PaitentId = {VmPatient.PaitentId}");
        var nonTrans = SqlExtensions.ExecuteNonTrans(cmdString.ToString());
        return nonTrans;
    }

    #endregion ---------- IUD ----------

    // OBJECT FOR THIS FORM

    #region ---------------VM_HOSPITAL ---------------

    public VmBedMaster ObjBedMaster { get; set; }
    public VmBedType ObjBedType { get; set; }
    public VmDoctor ObjDoctor { get; set; }
    public VmPatient VmPatient { get; set; }
    public VmDoctorType VmDoctorType { get; set; }

    #endregion ---------------VM_HOSPITAL ---------------
}