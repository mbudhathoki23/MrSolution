using DatabaseModule.Setup.UserSetup;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Setup.Interface;
using MrDAL.Utility.Server;
using System.Text;
namespace MrDAL.Setup.UserSetup;

public class UserAccessControlRepository : IUserAccessControl
{
    public int SaveSecurityRights(string actionTag)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($@"
        DELETE FROM AMS.UserAccessControl where UserRoleId={AccessControl.UserRoleId}");
        cmdString.Append(AccessControl.UserId > 0 ? $@" AND UserId={AccessControl.UserId}" : "");
        cmdString.Append(@" 
        INSERT INTO AMS.UserAccessControl(UserRoleId,FeatureAlias,BranchId,IsValid,CreatedOn,ModifiedOn,UpdatedBy,ConfigXml,UserId,ConfigFormsXml)");
        cmdString.Append("\n VALUES ( ");
        cmdString.Append($"{AccessControl.UserRoleId},");
        cmdString.Append($"{AccessControl.FeatureAlias},");
        cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" N'{ObjGlobal.SysBranchId}'," : "NULL,");
        cmdString.Append(AccessControl.IsValid ? "1," : "0,");
        cmdString.Append("GETDATE(),");
        cmdString.Append(AccessControl.ModifiedOn.IsValueExits() ? $"N'{AccessControl.ModifiedOn}'," : "NULL,");
        cmdString.Append($"{AccessControl.UserId},");
        cmdString.Append(AccessControl.ConfigXml.IsValueExits() ? $" '{AccessControl.ConfigXml}'," : "NUll,");
        cmdString.Append($"{AccessControl.UserId},");
        cmdString.Append(AccessControl.ConfigFormsXml.IsValueExits() ? $" '{AccessControl.ConfigFormsXml}'" : "NUll");
        cmdString.Append(" )");
        var exe = SqlExtensions.ExecuteNonQueryOnMaster(cmdString.ToString());
        return exe;
    }
    public UserAccessControl AccessControl { get; set; } = new();
}