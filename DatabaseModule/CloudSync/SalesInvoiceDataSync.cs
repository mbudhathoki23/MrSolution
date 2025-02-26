using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
using System.Collections.Generic;

namespace DatabaseModule.CloudSync;

public class SalesInvoiceDataSync : BaseSyncData
{
    public IList<SB_Master> Masters { get; set; }
    public IList<SB_Details> Details { get; set; }
    public IList<SB_Term> Terms { get; set; }
}