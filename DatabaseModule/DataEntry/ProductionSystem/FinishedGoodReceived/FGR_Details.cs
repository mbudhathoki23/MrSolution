namespace DatabaseModule.DataEntry.ProductionSystem.FinishedGoodReceived;

public class FGR_Details
{
    public string FGR_No { get; set; }
    public int SNo { get; set; }
    public long P_Id { get; set; }
    public int? Gdn_Id { get; set; }
    public int? CC_Id { get; set; }
    public int? Agent_ID { get; set; }
    public decimal Alt_Qty { get; set; }
    public int? Alt_UnitId { get; set; }
    public decimal Qty { get; set; }
    public int? Unit_Id { get; set; }
    public decimal AltStock_Qty { get; set; }
    public decimal Stock_Qty { get; set; }
    public decimal Rate { get; set; }
    public decimal N_Amount { get; set; }
    public decimal Cnv_Ratio { get; set; }
    public string ModifyAction { get; set; }
    public string ModifyBy { get; set; }
    public System.DateTime? ModifyDate { get; set; }
}