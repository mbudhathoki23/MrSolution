using DatabaseModule.DataEntry.PurchaseMaster.PurchaseReturn;
using System.Collections.Generic;

namespace DatabaseModule.CloudSync;

public class PurchaseReturnDataSync : BaseSyncData
{
    public IList<PR_Master> Masters { get; set; }
    public IList<PR_Details> Details { get; set; }
    public IList<PR_Term> Terms { get; set; }
}