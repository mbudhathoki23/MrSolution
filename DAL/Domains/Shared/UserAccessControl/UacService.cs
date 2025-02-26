using Dapper;
using DatabaseModule.Setup.UserSetup;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.UserAccessControl.Models;
using MrDAL.Lib.Dapper.Contrib;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable All

namespace MrDAL.Domains.Shared.UserAccessControl;

public class UacService
{
    public async Task<ListResult<UserRolePermissionModel>> GetPermissionsForRoleAsync(int roleId, int? branchId)
    {
        var result = new ListResult<UserRolePermissionModel>();

        var parameters = new DynamicParameters();
        var sql = new StringBuilder(@"
                            SELECT
	                            perm.*, rol.Role, br.Branch_Name AS Branch
                            FROM master.AMS.UserAccessControl perm
                            JOIN master.AMS.User_Role rol ON perm.UserRoleId = rol.Role_Id
                            LEFT JOIN AMS.Branch br ON perm.BranchId = br.Branch_ID
                            WHERE perm.UserRoleId = @prRoleId AND perm.IsValid = 1 ");

        parameters.Add("prRoleId", roleId);

        if (branchId.HasValue)
        {
            sql.Append("AND perm.BranchId = @prBranchId ");
            parameters.Add("prBranchId", branchId.Value);
        }

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            var records = await conn.QueryAsync<UserRolePermissionModel>(sql.ToString(), parameters);
            result.List = records.AsList();
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<UserRolePermissionModel>(this);
        }

        return result;
    }

    public async Task<ListResult<UserRoleModel>> GetRolesAsync(UacRoleType? roleType, bool? active)
    {
        var result = new ListResult<UserRoleModel>();

        try
        {
            const string sql =
                @"SELECT Role_Id AS Id, Role AS Name, ISNULL(Status,0) AS Active FROM master.AMS.User_Role ";
            using var conn = GetConnection.ReturnConnection();

            var records = await conn.QueryAsync<UserRoleModel>(sql);

            if (roleType.HasValue)
                records = roleType switch
                {
                    UacRoleType.Administrator => records.Where(x => x.IsAdmin),
                    UacRoleType.Normal => records.Where(x => !x.IsAdmin),
                    _ => throw new ArgumentOutOfRangeException(nameof(roleType), roleType, null)
                };

            if (active.HasValue) records = records.Where(x => active != null && x.Active == active.Value);

            result.List = records.AsList();
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<UserRoleModel>(this);
        }

        return result;
    }

    /// <summary>
    ///     Gets users as per the given parameter
    /// </summary>
    /// <param name="roleType">The user role type to filter</param>
    /// <param name="roleId">The user role Id. Send NULL value for admin roles</param>
    /// <returns></returns>
    public async Task<ListResult<UserInfoModel>> GetUsersAsync(UacRoleType? roleType, int? roleId = null)
    {
        var result = new ListResult<UserInfoModel>();

        var parameters = new DynamicParameters();
        var query = new StringBuilder(
            @"SELECT
	                usr.User_Id AS UserId, usr.Full_Name AS FullName, usr.User_Name AS Username, usr.Address, usr.Mobile_No AS Phone,
	                usr.Email_Id AS EmailId, usr.Branch_Id AS BranchId, rol.Role_Id AS RoleId, rol.Role, usr.Created_Date AS CreatedDate, ISNULL(usr.Status,0) AS Active
                FROM master.AMS.UserInfo usr
                JOIN master.AMS.User_Role rol ON rol.Role_Id = usr.Role_Id WHERE ISNULL(rol.Status,0) = 1 AND ISNULL(usr.IsDeleted,0) = 0 ");

        if (roleType.HasValue)
            switch (roleType)
            {
                case UacRoleType.Administrator:
                    query.AppendLine("AND rol.Role IN ('Admin', 'Administrator') ");
                    break;

                case UacRoleType.Normal:
                    query.AppendLine("AND rol.Role NOT IN ('Admin', 'Administrator') ");
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(roleType), roleType, null);
            }

        if (roleId.HasValue)
        {
            query.AppendLine("AND rol.Role_Id = @prRoleId ");
            parameters.Add("prRoleId", roleId.Value);
        }

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            var records = await conn.QueryAsync<UserInfoModel>(query.ToString(), parameters);
            result.List = records.AsList();
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<UserInfoModel>(this);
        }

        return result;
    }

    public async Task<ListResult<UserPermissionModel>> GetNormalUserPermissionsAsync(string username, int? branchId)
    {
        var result = new ListResult<UserPermissionModel>();

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            var user = conn.QueryFirstOrDefault<UserInfo>(
                "SELECT * FROM master.AMS.UserInfo WHERE User_Name = @prUsername AND ISNULL(IsDeleted,0) = 0 ",
                new { prUsername = username });

            if (user == null)
            {
                result.ResultType = ResultType.EntityNotExists;
                result.ErrorMessage = "The requested user doesn't exist or has been deleted.";
                return result;
            }

            var permResponse = await GetPermissionsForRoleAsync(user.Role_Id ?? 0, branchId);
            if (!permResponse.Success)
            {
                result.ErrorMessage = permResponse.ErrorMessage;
                result.ResultType = permResponse.ResultType;
                return result;
            }

            result.List = permResponse.List.Select(x => new UserPermissionModel
            {
                Id = x.Id,
                FeatureAlias = x.FeatureAlias,
                Branch = x.Branch,
                BranchId = x.BranchId,
                ConfigXml = x.ConfigXml,
                CreatedOn = x.CreatedOn,
                UpdatedBy = x.UpdatedBy
            }).ToList();
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<UserPermissionModel>(this);
        }

        return result;
    }

    public async Task<NonQueryResult> ApplyPermissionsAsync(int roleId, IList<UacEntryModel> models, string updatedBy)
    {
        var result = new NonQueryResult();
        var currentDateTime = DateTime.Now;

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            await conn.OpenAsync();

            using var trans = conn.BeginTransaction();
            try
            {
                var role = await conn.QueryFirstOrDefaultAsync<User_Role>(@"SELECT * FROM master.AMS.User_Role WHERE Role_Id = @prRoleId ", new { prRoleId = roleId }, trans);
                if (role == null)
                {
                    result.ResultType = ResultType.EntityNotExists;
                    result.ErrorMessage = "Selected role is not valid.";
                    return result;
                }

                if (role.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase) || role.Role.Equals("Administrator", StringComparison.OrdinalIgnoreCase))
                {
                    result.ResultType = ResultType.ValidationError;
                    result.ErrorMessage = "Access control feature is not applicable for admin role.";
                    return result;
                }

                await conn.ExecuteAsync(@"UPDATE master.AMS.userAccessControl SET IsValid = 0 , ModifiedOn = @prDateTime, UpdatedBy = @prUser WHERE UserRoleId = @prRoleId AND IsValid = 1 ", new
                {
                    prRoleId = roleId,
                    prDateTime = currentDateTime,
                    prUser = updatedBy
                }, trans);

                foreach (var model in models) await conn.InsertAsync(new DatabaseModule.Setup.UserSetup.UserAccessControl
                {
                    BranchId = model.BranchId,
                    CreatedOn = currentDateTime,
                    UpdatedBy = updatedBy,
                    UserRoleId = roleId,
                    IsValid = true,
                    FeatureAlias = model.FeatureAlias,
                    ConfigXml = model.ActionsXml
                }, trans);

                trans.Commit();
                result.Completed = result.Value = true;
            }
            catch (Exception e)
            {
                result = e.ToNonQueryErrorResult(e.StackTrace);
            }
        }
        catch (Exception e)
        {
            result = e.ToNonQueryErrorResult(e.StackTrace);
        }

        return result;
    }

    public Task<InfoResult<UserInfo>> GetUserAsync(string username)
    {
        var result = new InfoResult<UserInfo>();

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            var user = conn.QueryFirstOrDefault<UserInfo>(
                "SELECT * FROM master.AMS.UserInfo WHERE User_Name = @prUsername AND ISNULL(IsDeleted,0) = 0 ",
                new { prUsername = username });

            result.Model = user;
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<UserInfo>(this);
        }

        return Task.FromResult(result);
    }
}