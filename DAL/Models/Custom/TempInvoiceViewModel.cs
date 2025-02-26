using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
using System.Collections.Generic;

namespace MrDAL.Models.Custom;

public class TempInvoiceViewModel
{
    public Temp_SB_Master Master { get; set; }
    public IList<Temp_SB_Details> Items { get; set; }
}