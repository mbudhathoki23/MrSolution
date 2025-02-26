namespace DatabaseModule.DataEntry.FinanceTransaction.DayClosing;

public class CashClosing
{
    public int CC_Id { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public string EnterMiti { get; set; }
    public System.DateTime EnterTime { get; set; }
    public string CB_Balance { get; set; }
    public decimal Cash_Sales { get; set; }
    public decimal Cash_Purchase { get; set; }
    public decimal TotalCash { get; set; }
    public decimal ThauQty { get; set; }
    public decimal ThouVal { get; set; }
    public decimal FivHunQty { get; set; }
    public decimal FivHunVal { get; set; }
    public decimal HunQty { get; set; }
    public decimal HunVal { get; set; }
    public decimal FiFtyQty { get; set; }
    public decimal FiftyVal { get; set; }
    public decimal TwenteyFiveQty { get; set; }
    public decimal TwentyFiveVal { get; set; }
    public decimal TwentyQty { get; set; }
    public decimal TwentyVal { get; set; }
    public decimal TenQty { get; set; }
    public decimal TenVal { get; set; }
    public decimal FiveQty { get; set; }
    public decimal FiveVal { get; set; }
    public decimal TwoQty { get; set; }
    public decimal TwoVal { get; set; }
    public decimal OneQty { get; set; }
    public decimal OneVal { get; set; }
    public decimal Cash_Diff { get; set; }
    public string Module { get; set; }
    public string HandOverUser { get; set; }
    public string Remarks { get; set; }
}