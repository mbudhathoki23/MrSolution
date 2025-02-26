using MrDAL.Core.Extensions;
using MrDAL.Domains.CRM.Master.Model;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System.Data;

namespace MrDAL.Domains.CRM.Master;

public class CrmMaster : ICrmMaster
{
    public CrmMaster()
    {
        ClientInfo = new ClientCollection();
        ClientSource = new ClientSource();
    }

    // METHOD FOR THIS CLASS
    public long GetIdFromValue(string val, string module)
    {
        var value = module switch
        {
            "ClientCollection" => "ClientId",
            _ => ""
        };
        var column = module switch
        {
            "ClientCollection" => "ClientDescription",
            _ => ""
        };
        var cmdString = $"SELECT {value} FROM CRM.{module} WHERE {column} = '{val}'";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dt.Rows.Count > 0 ? dt.Rows[0].GetLong() : 1;
    }

    public long ReturnMaxIdFromTable(string module)
    {
        var column = module switch
        {
            "ClientCollection" => "ClientId",
            "ClientSource" => "SourceId",
            _ => ""
        };
        if (column.IsBlankOrEmpty()) return 0;
        var cmdString = $"SELECT TOP (1) ISNULL(MAX({column}),0) + 1 MaxId FROM CRM.{module}";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dt.Rows.Count > 0 ? dt.Rows[0]["MaxId"].GetLong() : 1;
    }

    public string GetValueFromId(long id, string module)
    {
        var column = module switch
        {
            "ClientCollection" => "ClientId",
            _ => ""
        };
        var value = module switch
        {
            "ClientCollection" => "ClientDescription",
            _ => ""
        };
        var cmdString = $"SELECT {value} FROM CRM.{module} WHERE {column} = '{id}'";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dt.Rows.Count > 0 ? dt.Rows[0].GetString() : string.Empty;
    }

    // RETURN DATA TABLE VALUE
    public DataTable GetClientCollectionInformation(long id)
    {
        if (id is 0) return new DataTable();
        var cmdString = $@"SELECT * FROM CRM.ClientCollection WHERE ClientId = {id}";
        var dtResult = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dtResult;
    }

    public DataTable GetClientSource(string description)
    {
        if (description.IsBlankOrEmpty()) return new DataTable();
        var cmdString = $@"SELECT * FROM CRM.ClientSource WHERE SDescription = '{description}';";
        var dtResult = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dtResult;
    }

    // RETURN BOOL VALUE
    public bool AlreadyExits(string value, string module, string column)
    {
        if (module.IsBlankOrEmpty()) return true;
        var cmdString = $"SELECT TOP (1) *  FROM CRM.{module} WHERE {column} = '{value}'";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dt.Rows.Count > 0;
    }

    // OBJECT
    public ClientCollection ClientInfo { get; set; }

    public ClientSource ClientSource { get; set; }

    // SAVE FUNCTION

    #region ------------- SAVE/EDIT/DELETE FUNCTION -------------

    public int SaveClientCollection()
    {
        var cmdString = string.Empty;
        if (ClientInfo.ActionTag is "SAVE")
        {
            cmdString =
                @"INSERT INTO CRM.ClientCollection(ClientId, ClientDescription, PanNo, ClientAddress, EmailAddress, ContactNo, PhoneNo, CollectionSource, Status, EnterBy, EnterDate)";
            cmdString += $"VALUES({ClientInfo.ClientId},N'{ClientInfo.ClientDescription}', ";
            cmdString += ClientInfo.PanNo.IsValueExits() ? $"{ClientInfo.PanNo.GetDouble()}," : "NULL,";
            cmdString += ClientInfo.ClientAddress.IsValueExits() ? $"'{ClientInfo.ClientAddress}'," : "NULL,";
            cmdString += ClientInfo.EmailAddress.IsValueExits() ? $"'{ClientInfo.EmailAddress}'," : "NULL,";
            cmdString += ClientInfo.ContactNo.IsValueExits() ? $"'{ClientInfo.ContactNo}'," : "NULL,";
            cmdString += ClientInfo.PhoneNo.IsValueExits() ? $"'{ClientInfo.PhoneNo}'," : "NULL,";
            cmdString += ClientInfo.CollectionSource.IsValueExits() ? $"'{ClientInfo.CollectionSource}'," : "NULL,";
            cmdString += ClientInfo.Status ? "1," : "0,";
            cmdString += $"'{ObjGlobal.LogInUser}',GETDATE());";
        }

        if (ClientInfo.ActionTag is "UPDATE")
        {
            cmdString = @"UPDATE CRM.ClientCollection SET ";
            cmdString += $"ClientDescription = N'{ClientInfo.ClientDescription}', ";
            cmdString += ClientInfo.PanNo.IsValueExits() ? $"PanNo = {ClientInfo.PanNo.GetDouble()}," : "PanNo = NULL,";
            cmdString += ClientInfo.ClientAddress.IsValueExits()
                ? $"ClientAddress = '{ClientInfo.ClientAddress}',"
                : "ClientAddress = NULL,";
            cmdString += ClientInfo.EmailAddress.IsValueExits()
                ? $"EmailAddress = '{ClientInfo.EmailAddress}',"
                : "EmailAddress = NULL,";
            cmdString += ClientInfo.ContactNo.IsValueExits()
                ? $"ContactNo = '{ClientInfo.ContactNo}',"
                : "ContactNo = NULL,";
            cmdString += ClientInfo.PhoneNo.IsValueExits() ? $"PhoneNo = '{ClientInfo.PhoneNo}'," : "PhoneNo = NULL,";
            cmdString += ClientInfo.CollectionSource.IsValueExits()
                ? $"CollectionSource = '{ClientInfo.CollectionSource}',"
                : "CollectionSource = NULL,";
            cmdString += ClientInfo.Status ? "Status = 1" : "Status = 0";
            cmdString += $" WHERE ClientId = {ClientInfo.ClientId};";
        }

        if (ClientInfo.ActionTag is "DELETE")
            cmdString = $"DELETE CRM.ClientCollection WHERE ClientId ={ClientInfo.ClientId} ";
        var result = SqlExtensions.ExecuteNonQuery(cmdString);
        return result;
    }

    public int SaveClientSource()
    {
        var cmdString = string.Empty;
        if (ClientSource.ActionTag is "SAVE")
        {
            cmdString = @"INSERT INTO CRM.ClientSource(SourceId, SDescription, IsActive, EnterBy, EnterDate)";
            cmdString += $"VALUES({ClientSource.SourceId},N'{ClientSource.SDescription}', ";
            cmdString += ClientSource.IsActive ? "1," : "0,";
            cmdString += $"'{ObjGlobal.LogInUser}',GETDATE());";
        }

        if (ClientSource.ActionTag is "UPDATE")
        {
            cmdString = @"UPDATE CRM.ClientSource SET ";
            cmdString += $"SDescription = N'{ClientSource.SDescription}', ";
            cmdString += ClientSource.IsActive ? "IsActive = 1" : "IsActive = 0";
            cmdString += $" WHERE SourceId = '{ClientSource.SourceId}';";
        }

        if (ClientSource.ActionTag is "DELETE")
            cmdString = $"DELETE CRM.ClientSource WHERE SourceId ='{ClientSource.SourceId}' ";
        var result = SqlExtensions.ExecuteNonQuery(cmdString);
        return result;
    }

    #endregion ------------- SAVE/EDIT/DELETE FUNCTION -------------
}