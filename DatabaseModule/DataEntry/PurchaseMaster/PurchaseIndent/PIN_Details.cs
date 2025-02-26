namespace DatabaseModule.DataEntry.PurchaseMaster.PurchaseIndent;

public class PIN_Details
{
    public string PIN_Invoice { get; set; }
    public int SNo { get; set; }
    public long P_Id { get; set; }
    public int? Gdn_Id { get; set; }
    public decimal Alt_Qty { get; set; }
    public int? Alt_Unit { get; set; }
    public decimal Qty { get; set; }
    public int? Unit { get; set; }
    public decimal AltStock_Qty { get; set; }
    public decimal StockQty { get; set; }
    public decimal Issue_Qty { get; set; }
    public string Narration { get; set; }
}