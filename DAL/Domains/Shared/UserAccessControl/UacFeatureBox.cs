using MrDAL.Core.Extensions;
using MrDAL.Domains.Shared.UserAccessControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MrDAL.Domains.Shared.UserAccessControl;

public class UacFeatureBox
{
    public UacFeatureBox(UacModule module, UacFeatureGroup? featureGroup, UacAccessFeature feature,
        IEnumerable<UacAction> actions = null)
    {
        Feature = feature;
        FeatureGroup = featureGroup;
        Module = module;
        Actions = actions is IList<UacAction> list ? list : (actions ?? Array.Empty<UacAction>()).ToList();
        FeatureName = feature.GetDescription();
        FeatureGroupName = featureGroup?.GetDescription();
        ModuleName = module.GetDescription();
    }

    public UacAccessFeature Feature { get; set; }
    public int FeatureAlias { get; set; }
    public string FeatureName { get; }
    public UacFeatureGroup? FeatureGroup { get; set; }
    public int? FeatureGroupAlias { get; set; }
    public string FeatureGroupName { get; }
    public UacModule Module { get; set; }
    public string ModuleName { get; }
    public IList<UacAction> Actions { get; set; }
}