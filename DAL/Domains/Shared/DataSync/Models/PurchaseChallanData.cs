using System.Collections.Generic;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Entities;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class PurchaseChallanData : BaseSyncData
{
    public IList<PC_Master> Masters { get; set; }
    public IList<PC_Details> Details { get; set; }
    public IList<PC_Term> Terms { get; set; }
}