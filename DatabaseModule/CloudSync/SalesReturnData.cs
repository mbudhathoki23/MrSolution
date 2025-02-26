using DatabaseModule.DataEntry.SalesMaster.SalesReturn;
using System.Collections.Generic;

namespace DatabaseModule.CloudSync;

public class SalesReturnData : BaseSyncData
{
    public IList<SR_Master> Masters { get; set; }
    public IList<SR_Details> Details { get; set; }
    public IList<SR_Term> Terms { get; set; }
}