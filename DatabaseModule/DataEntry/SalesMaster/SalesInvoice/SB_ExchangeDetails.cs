namespace DatabaseModule.DataEntry.SalesMaster.SalesInvoice;

public class SB_ExchangeDetails
{
    public string SB_Invoice { get; set; }
    public decimal Invoice_SNo { get; set; }
    public long P_Id { get; set; }
    public int? Gdn_Id { get; set; }
    public long? ExchangeGLD { get; set; }
    public decimal Alt_Qty { get; set; }
    public int? Alt_UnitId { get; set; }
    public decimal Qty { get; set; }
    public int? Unit_Id { get; set; }
    public decimal Rate { get; set; }
    public decimal B_Amount { get; set; }
    public decimal N_Amount { get; set; }
}