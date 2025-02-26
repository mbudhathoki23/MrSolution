using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Utility.Server;
using System;
using System.Data;
using DatabaseModule.Setup.CompanyMaster;
using MrDAL.Setup.CompanySetup;
using MrDAL.Setup.Interface;

namespace MrDAL.Utility.dbMaster;

public class ClsAttachDeAttach
{
    public int AttachSelectedDatabase(string folderLocation, string fileName)
    {
        string cmdString;
        var iResult = 0;
        try
        {
            cmdString = @$"
            EXEC sys.sp_attach_db @dbname = '{fileName.Replace(".mdf", "")}',@filename1 = N'{folderLocation}' ,@filename2 = N'{folderLocation.Replace(".mdf", ".ldf")}'";
            iResult = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
            if (iResult == 0)
            {
                CustomMessageBox.ErrorMessage("ERROR OCCURS WHILE DATA BASE ATTACH..!!");
            }
        }
        catch (Exception e)
        {
            e.DialogResult();
        }

        if (iResult != 0)
        {
            cmdString = $"SELECT * FROM [{fileName.Replace(".mdf", "")}].[AMS].[CompanyInfo] ci ";
            var dt = SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];
            if (dt.Rows.Count <= 0)
            {
                return iResult;
            }

            var row = dt.Rows[0];
            var companySetup = new GlobalCompany
            {
                GComp_Id = 0,
                Database_Name = row["Database_Name"].GetString(),
                Company_Name = row["Company_Name"].GetTrimApostrophe(),
                PrintingDesc = row["PrintDesc"].GetTrimApostrophe(),
                Database_Path = folderLocation,
                Status = true,
                PanNo = row["Pan_No"].ToString(),
                Address = row["Address"].ToString(),
                CurrentFiscalYear = "",
                LoginDate = DateTime.Now,
                DataSyncOriginId = "",
                DataSyncApiBaseUrl = "",
                ApiKey = row["ApiKey"].GetGuid()
            };
            _companySetup.Setup = companySetup;
            _companySetup.RightsList.Clear();
            var details = new CompanyRights
            {
                User_Id = ObjGlobal.LogInUserId,
                Company_Id = _companySetup.Info.Company_Id,
                Company_Name = _companySetup.Info.Company_Name,
                Start_AdDate = DateTime.Now,
                End_AdDate = DateTime.Now,
                Modify_Start_AdDate = null,
                Modify_End_AdDate = null,
                Back_Days_Restriction = false
            };
            _companySetup.RightsList.Add(details);
            var saveGlobal = _companySetup.SaveGlobalCompany("SAVE");
        }

        return iResult;
    }
    public int DeAttachSelectedDatabase(string fileName)
    {
        var result = 0;
        try
        {
            var cmdString = $@"
            EXEC sys.sp_kill_oldest_transaction_on_secondary @database_name = {fileName},@kill_all = 1 ,@killed_xdests =1 
            EXEC sys.sp_detach_db @dbname = '{fileName}',@skipchecks = N'True' ,@keepfulltextindexfile = N'True' ";
            result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        }
        catch (Exception)
        {
            // ignore
        }

        if (result != 0)
        {
            var cmd = $"SELECT gc.GComp_Id FROM MASTER.AMS.GlobalCompany gc WHERE gc.Database_Name='{fileName}'";

            var companyId = cmd.GetQueryData().GetInt();

            _companySetup.Info.Company_Id = companyId;
            _companySetup.Setup.GComp_Id = companyId;
            _companySetup.Info.Database_Name = fileName;
            _companySetup.Setup.Database_Name = fileName;
            
            var details = new CompanyRights
            {
                Company_Id = companyId,
                User_Id = ObjGlobal.LogInUserId
            }; 
            _companySetup.RightsList.Clear();
            _companySetup.RightsList.Add(details);
            var saveCompanyRights = _companySetup.SaveGlobalCompany("DELETE");
        }

        return result;
    }
    public DataTable GetCompanyList()
    {
        var cmdString = ObjGlobal.LogInUser.GetUpper() switch
        {
            "ADMIN" or "AMSADMIN" => $@"
            SELECT gc.Database_Name Initial,gc.Company_Name CompanyInfo 
            FROM AMS.GlobalCompany gc 
                WHERE gc.Database_Name <> '{ObjGlobal.InitialCatalog}'",
            _ => $@" 
            SELECT gc.Database_Name Initial,gc.Company_Name CompanyInfo 
            FROM AMS.GlobalCompany gc
                WHERE gc.Database_Name <> '{ObjGlobal.InitialCatalog}' AND gc.GComp_Id IN (SELECT cr.Company_Id FROM AMS.CompanyRights cr WHERE cr.User_Id = '{ObjGlobal.LogInUserId}')"
        };
        var result = SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];

        return result;
    }

    private readonly ICompanySetup _companySetup = new CompanySetupRepository();
}