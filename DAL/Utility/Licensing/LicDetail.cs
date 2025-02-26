using System;
using System.Collections.Generic;

namespace MrDAL.Utility.Licensing;

public class LicDetail
{
    public string LicenseTo { get; set; }
    public DateTime DateGenerated { get; set; }
    public Guid OriginGroupId { get; set; }
    public IList<string> HwIds { get; set; }
    public bool MultiBranch { get; set; }
    public Guid SubscriptionId { get; set; }
    public IList<LicBranch> Branches { get; set; }
    public LicEdition Edition { get; set; }
    public uint Version { get; set; }
    public LicClient ClientType { get; set; }
}