using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using System;
using System.Collections.Generic;

namespace MrDAL.Domains.Shared.UserAccessControl.Models;

public class UserRolePermissionModel
{
    public int Id { get; set; }
    public int UserRoleId { get; set; }
    public string Role { get; set; }
    public int FeatureAlias { get; set; }
    public UacAccessFeature Feature => (UacAccessFeature)FeatureAlias;
    public string FeatureName => Feature.GetDescription();
    public int? BranchId { get; set; }
    public string Branch { get; set; }
    public DateTime CreatedOn { get; set; }
    public string UpdatedBy { get; set; }
    public string ConfigXml { get; set; }

    public UserRolePermissionConfigModel ConfigModel => string.IsNullOrWhiteSpace(ConfigXml)
        ? null
        : XmlUtils.XmlDeserialize<UserRolePermissionConfigModel>(ConfigXml);

    public IList<UacAction> Actions => ConfigModel == null ? new List<UacAction>() : ConfigModel.Actions;
}