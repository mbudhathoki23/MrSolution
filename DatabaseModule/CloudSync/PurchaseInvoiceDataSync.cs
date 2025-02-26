using DatabaseModule.DataEntry.PurchaseMaster.PurchaseInvoice;
using System.Collections.Generic;

namespace DatabaseModule.CloudSync;

public class PurchaseInvoiceDataSync : BaseSyncData
{
    public IList<PB_Master> Masters { get; set; }
    public IList<PB_Details> Details { get; set; }
    public IList<PB_Term> Terms { get; set; }
}