using DatabaseModule.Setup.UserSetup;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Setup.Interface;
using MrDAL.Utility.Server;
using System;
using System.Data;
namespace MrDAL.Setup.UserSetup;

public class UserSetupRepository : IUserSetupRepository
{
    public UserSetupRepository()
    {
        UserSetup = new UserInfo();
    }
    public int SaveUserInfo(string actionTag)
    {
        var cmdString = string.Empty;
        if (actionTag is "SAVE")
        {
            cmdString = $@"
                INSERT INTO AMS.UserInfo(Full_Name, User_Name, Password, Address, Mobile_No, Phone_No, Email_Id, Role_Id, Branch_Id, Allow_Posting, Posting_StartDate, Posting_EndDate, Modify_StartDate, Modify_EndDate, Auditors_Lock, Valid_Days, Created_By, Created_Date, Status, Ledger_Id, Category, Authorized, IsQtyChange, IsDefault, IsModify, IsDeleted, IsRateChange, IsPDCDashBoard)
                VALUES('{UserSetup.Full_Name}','{UserSetup.User_Name}', '{UserSetup.Password}',";
            cmdString += UserSetup.Address.IsValueExits() ? $@" '{UserSetup.Address}'," : "NULL,";
            cmdString += UserSetup.Mobile_No.IsValueExits() ? $@" '{UserSetup.Mobile_No}'," : "NULL,";
            cmdString += UserSetup.Phone_No.IsValueExits() ? $@" '{UserSetup.Phone_No}'," : "NULL,";
            cmdString += UserSetup.Email_Id.IsValueExits() ? $@" '{UserSetup.Email_Id}'," : "NULL,";
            cmdString += UserSetup.Role_Id > 0 ? $@" {UserSetup.Role_Id}," : "NULL,";
            cmdString += UserSetup.Branch_Id > 0 ? $@" {UserSetup.Branch_Id}," : "NULL,";
            cmdString += $@" CAST('{UserSetup.Allow_Posting}' AS BIT) , ";
            cmdString += $@" '{UserSetup.Posting_StartDate.GetSystemDate()}', '{UserSetup.Posting_EndDate.GetSystemDate()}', '{UserSetup.Modify_StartDate.GetSystemDate()}', '{UserSetup.Modify_EndDate.GetSystemDate()}',";
            cmdString += $@" CAST('{UserSetup.Auditors_Lock}' AS BIT), {UserSetup.Valid_Days},'{UserSetup.Created_By}' , '{UserSetup.Created_Date.GetSystemDate()}',CAST('{UserSetup.Status}' AS BIT) ,";
            cmdString += UserSetup.Ledger_Id > 0 ? $@" {UserSetup.Ledger_Id}," : "NULL,";
            cmdString += UserSetup.Category.IsValueExits() ? $@" '{UserSetup.Category}'," : "'ADMINISTRATOR',";
            cmdString += $@" CAST('{UserSetup.Authorized}' AS BIT), CAST('{UserSetup.IsQtyChange}' AS BIT) ,0,CAST('{UserSetup.IsModify}' AS BIT) ,CAST('{UserSetup.IsDeleted}' AS BIT) ,CAST('{UserSetup.IsRateChange}' AS BIT) , CAST('{UserSetup.IsPDCDashBoard}' AS BIT) );";
        }
        else if (actionTag is "UPDATE")
        {
            cmdString = $@"
                UPDATE AMS.UserInfo SET Full_Name = '{UserSetup.Full_Name}',Password='{UserSetup.Password}',";
            cmdString += UserSetup.Address.IsValueExits() ? $@" Address = '{UserSetup.Address}'," : "Address = NULL,";
            cmdString += UserSetup.Mobile_No.IsValueExits() ? $@" Mobile_No = '{UserSetup.Mobile_No}'," : "Mobile_No = NULL,";
            cmdString += UserSetup.Phone_No.IsValueExits() ? $@" Phone_No = '{UserSetup.Phone_No}'," : "Phone_No = NULL,";
            cmdString += UserSetup.Email_Id.IsValueExits() ? $@" Email_Id = '{UserSetup.Email_Id}'," : "Email_Id = NULL,";
            cmdString += UserSetup.Role_Id > 0 ? $@" Role_Id = {UserSetup.Role_Id}," : "Role_Id = NULL,";
            cmdString += UserSetup.Branch_Id > 0 ? $@"Branch_Id =  {UserSetup.Branch_Id}," : "Branch_Id = NULL,";
            cmdString += $@" Allow_Posting= CAST('{UserSetup.Allow_Posting}' AS BIT) , ";
            cmdString += $@" Posting_StartDate = '{UserSetup.Posting_StartDate.GetSystemDate()}', Posting_EndDate = '{UserSetup.Posting_EndDate.GetSystemDate()}', Modify_StartDate = '{UserSetup.Modify_StartDate.GetSystemDate()}', Modify_EndDate = '{UserSetup.Modify_EndDate.GetSystemDate()}',";
            cmdString += $@" Auditors_Lock= CAST('{UserSetup.Auditors_Lock}' AS BIT),Valid_Days= {UserSetup.Valid_Days},Created_By='{UserSetup.Created_By}' , Created_Date='{UserSetup.Created_Date.GetSystemDate()}',Status=CAST('{UserSetup.Status}' AS BIT) ,";
            cmdString += UserSetup.Ledger_Id > 0 ? $@" Ledger_Id = {UserSetup.Ledger_Id}," : "Ledger_Id = NULL,";
            cmdString += UserSetup.Category.IsValueExits() ? $@" Category = '{UserSetup.Category}'," : "'ADMINISTRATOR',";
            cmdString += $@" Authorized = CAST('{UserSetup.Authorized}' AS BIT), IsQtyChange = CAST('{UserSetup.IsQtyChange}' AS BIT) ,IsModify = CAST('{UserSetup.IsModify}' AS BIT) ,IsDeleted = CAST('{UserSetup.IsDeleted}' AS BIT) ,IsRateChange = CAST('{UserSetup.IsRateChange}' AS BIT) , IsPDCDashBoard = CAST('{UserSetup.IsPDCDashBoard}' AS BIT)";
            cmdString += $@" WHERE USER_ID = {UserSetup.User_Id}; ";
        }
        else if (actionTag is "DELETE")
        {
            cmdString = @$"
                DELETE AMS.UserInfo WHERE USER_ID = '{UserSetup.User_Id}' AND USER_ID NOT IN (SELECT User_Id FROM AMS.CompanyRights) ";
        }
        return SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
    }
    public int SaveUserConfig()
    {
        var cmdString = $@"
            UPDATE AMS.UserInfo SET Full_Name = '{UserSetup.Full_Name}',Password='{UserSetup.Password}',";
        cmdString += $@" Posting_StartDate = '{UserSetup.Posting_StartDate.GetSystemDate()}', Posting_EndDate = '{UserSetup.Posting_EndDate.GetSystemDate()}', Modify_StartDate = '{UserSetup.Modify_StartDate.GetSystemDate()}', Modify_EndDate = '{UserSetup.Modify_EndDate.GetSystemDate()}',";
        cmdString += $@" {UserSetup.Valid_Days},";
        cmdString += UserSetup.Ledger_Id > 0 ? $@" Ledger_Id = {UserSetup.Ledger_Id}," : "Ledger_Id = NULL,";
        cmdString += UserSetup.Category.IsValueExits() ? $@" Category = '{UserSetup.Category}'," : "'ADMINISTRATOR',";
        cmdString += $@" Authorized = CAST('{UserSetup.Authorized}' AS BIT), IsQtyChange = CAST('{UserSetup.IsQtyChange}' AS BIT) ,IsModify = CAST('{UserSetup.IsModify}' AS BIT) ,IsDeleted = CAST('{UserSetup.IsDeleted}' AS BIT) ,IsRateChange = CAST('{UserSetup.IsRateChange}' AS BIT) , IsPDCDashBoard = CAST('{UserSetup.IsPDCDashBoard}' AS BIT)";
        cmdString += $@" WHERE USER_ID = {UserSetup.User_Id}; ";
        return SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
    }
    public int ChangePassword(string userName, string userPassword)
    {
        var sql = $@"
            Update Master.AMS.UserInfo set Password='{ObjGlobal.Encrypt(userPassword)}' where User_Name='{userName}' ";
        var cmd = SqlExtensions.ExecuteNonQuery(sql);
        return cmd;
    }
    public int ResetLoginLog(string userName)
    {
        var cmdString = $@"
            UPDATE AMS.LOGIN_LOG SET  LOG_STATUS = 0 WHERE LOGIN_DATE = '{DateTime.Now.GetSystemDate()}' AND LOGIN_USER = '{userName}'";
        return SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
    }
    public bool CheckUsernameExits(string userName)
    {
        var sql = $@"Select * from AMS.UserInfo where User_Name='{userName}'";
        var dtCheck = SqlExtensions.ExecuteDataSetOnMaster(sql).Tables[0];
        return dtCheck.Rows.Count > 0;
    }
    public (bool auditLog, string serverName) CheckLoginAuditDetails(string userName)
    {
        var deviceName = ObjGlobal.GetServerName();
        var query = $"SELECT * FROM MASTER.AMS.LOGIN_LOG WHERE LOGIN_USER = '{userName}' AND DEVICE <> '{deviceName}'  AND LOG_STATUS = 1 AND SYSTEM_ID <> '{ObjGlobal.SystemSerialNo}'";
        var dtLogin = SqlExtensions.ExecuteDataSetOnMaster(query).Tables[0];
        if (dtLogin.Rows.Count <= 0)
        {
            return (true, string.Empty);
        }
        return (false, dtLogin.Rows[0]["DEVICE"].GetString());
    }
    public bool CheckUsernamePassword(string userName, string userPassword)
    {
        var sql = $@"Select * from AMS.UserInfo where User_Name='{userName}' and Password='{userPassword}'";
        var dtCheck = SqlExtensions.ExecuteDataSetOnMaster(sql).Tables[0];
        return dtCheck.Rows.Count > 0;
    }
    public bool CheckUsernamePasswordUserType(int userId, string password, string userType)
    {
        var sql = $@"
            SELECT * from AMS.UserInfo 
            WHERE User_Id= '{userId}' AND Password='{ObjGlobal.Encrypt(password)}' and User_Type='{userType}'";
        var result = SqlExtensions.ExecuteDataSetOnMaster(sql).Tables[0];
        return result.Rows.Count > 0;
    }
    public DataTable GetUserInformationUsingId(int userId)
    {
        var sql = $@"
            SELECT * from AMS.UserInfo 
            WHERE User_Id= '{userId}'; ";
        var result = SqlExtensions.ExecuteDataSetOnMaster(sql).Tables[0];
        return result;
    }
    public DataTable GetMasterUser(int userId)
    {
        throw new System.NotImplementedException();
    }
    public DataTable GetUserType(string userName, string password)
    {
        var sql = $@"
            Select User_Id,User_Name,Password,Posting_StartDate,Posting_EndDate,Allow_Posting,Role_Id,UPPER(Full_Name) Name
            FROM AMS.UserInfo
            WHERE User_Name = '{userName}' and Password ='{ObjGlobal.Encrypt(password)}'; ";
        var result = SqlExtensions.ExecuteDataSetOnMaster(sql).Tables[0];
        return result;
    }
    public UserInfo UserSetup { get; set; }
}