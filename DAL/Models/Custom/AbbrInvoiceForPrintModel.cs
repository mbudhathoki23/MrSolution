using System;
using System.Collections.Generic;

namespace MrDAL.Models.Custom;

public class AbbrInvoiceForPrintModel
{
    public string BillNo { get; set; }
    public DateTime BillDateTime { get; set; }
    public string PartyName { get; set; }
    public string PartyAddress { get; set; }
    public string PartyPan { get; set; }
    public string PartyContact { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Discount { get; set; }
    public decimal GrandTotal { get; set; }
    public string AmountInWords { get; set; }
    public string PaymentMode { get; set; }
    public decimal TenderAmount { get; set; }
    public decimal ChangeAmount { get; set; }
    public string Cashier { get; set; }
    public string PrintNotes { get; set; }
    public IList<SalesInvoiceItemModel> Items { get; set; }
}