using MrDAL.Domains.Shared.UserAccessControl.Models;
using MrDAL.Models.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.UserAccessControl;

public static class UacManager
{
    public static UserRoleModel Role { get; private set; }

    public static IList<UserPermissionModel> Permissions => null;

    public static async Task<NonQueryResult> SetPermissionValues(string username)
    {
        var result = new NonQueryResult();

        var service = new UacService();
        var roleResponse = await service.GetRolesAsync(null, true);
        if (!roleResponse.Success)
        {
            result.ErrorMessage = roleResponse.ErrorMessage;
            result.ResultType = roleResponse.ResultType;
            return result;
        }

        var userResponse = await service.GetUserAsync(username);
        if (userResponse.Model == null)
        {
            result.ErrorMessage = "Error fetching current user detail.";
            result.ResultType = userResponse.ResultType;
            return result;
        }

        Role = roleResponse.List.FirstOrDefault(x => x.Id == userResponse.Model.Role_Id);
        if (Role == null)
        {
            result.ErrorMessage = "Unable to update the user role values.";
            result.ResultType = ResultType.EntityNotExists;
            return result;
        }

        var permResponse = await service.GetNormalUserPermissionsAsync(username, null);
        if (!permResponse.Success)
        {
            result.ErrorMessage = permResponse.ErrorMessage;
            result.ResultType = permResponse.ResultType;
            return result;
        }

        result.Value = result.Completed = true;
        return result;
    }

    public static bool IsPermissionAllowed(UacAccessFeature feature, UacAction action, int? branchId)
    {
        if (Role == null) return false;
        if (Role.IsAdmin) return true;

        var perm = Permissions?.FirstOrDefault(x => x.Feature == feature && x.BranchId == branchId);

        if (perm == null) return false;
        return perm.Actions.Contains(UacAction.All) || perm.Actions.Contains(action);
    }
}