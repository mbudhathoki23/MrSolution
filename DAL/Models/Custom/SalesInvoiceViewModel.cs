using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
using System.Collections.Generic;

namespace MrDAL.Models.Custom;

public class SalesInvoiceViewModel
{
    public SB_Master Master { get; set; }
    public IList<SB_Details> Items { get; set; }
    public IList<SB_Term> Terms { get; set; }
}