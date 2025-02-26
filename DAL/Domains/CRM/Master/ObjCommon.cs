using MrDAL.Core.Extensions;
using MrDAL.Utility.Server;
using System.Windows.Forms;

namespace MrDAL.Domains.CRM.Master;

public static class ObjCommon
{
    public static bool AlreadyExits(this TextBox value, string module, string column, string tag, string id)
    {
        if (module.IsBlankOrEmpty()) return true;
        var checkId = module switch
        {
            "ClientCollection" => "ClientId",
            "ClientSource" => "SDescription",
            "TaskStatus" => "TaskStatusId",
            "TaskType" => "TaskTypeId",
            "RoleAssign" or "RoleUser" => "UserRoleId",
            _ => string.Empty
        };

        var cmdString = $@"
            SELECT TOP (1) *  FROM CRM.{module} WHERE {column} = '{value.Text}' ";
        if (tag.Equals("UPDATE"))
            cmdString += $@"
            AND {checkId} <> '{id}'";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dt.Rows.Count > 0;
    }
}