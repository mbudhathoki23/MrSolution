using MrDAL.Core.Utils;
using System.Collections.Generic;

namespace MrDAL.Domains.Shared.UserAccessControl.Models;

public class UacEntryModel
{
    public UacEntryModel(int? branchId, List<UacAction> actions, int featureAlias)
    {
        BranchId = branchId;
        Actions = actions;
        FeatureAlias = featureAlias;
        ActionsXml = Actions == null
            ? null
            : XmlUtils.SerializeToXml(new UserRolePermissionConfigModel
            {
                Actions = actions
            });
    }

    public int? BranchId { get; set; }
    public int FeatureAlias { get; set; }
    public List<UacAction> Actions { get; set; }
    public string ActionsXml { get; }
}