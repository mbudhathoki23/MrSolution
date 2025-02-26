using System.Windows.Forms;

namespace DatabaseModule.DataEntry.ProductionSystem.FinishedGoodReceived;

public class FGR_MASTER
{
    public string FGR_No { get; set; }
    public System.DateTime FGR_Date { get; set; }
    public System.DateTime FGR_Time { get; set; }
    public string FGR_Miti { get; set; }
    public int? Gdn_Id { get; set; }
    public int? Agent_ID { get; set; }
    public int? Cls1 { get; set; }
    public int? Cls2 { get; set; }
    public int? Cls3 { get; set; }
    public int? Cls4 { get; set; }
    public long? GLID { get; set; }
    public int? CBranch_Id { get; set; }
    public int? CUnit_Id { get; set; }
    public int? CC_Id { get; set; }
    public string SB_No { get; set; }
    public int? SB_Sno { get; set; }
    public string SO_No { get; set; }
    public string Auth_By { get; set; }
    public System.DateTime? Auth_Date { get; set; }
    public string Source { get; set; }
    public string Export { get; set; }
    public string Remarks { get; set; }
    public int? FiscalYearId { get; set; }
    public string Enter_By { get; set; }
    public System.DateTime Enter_Date { get; set; }
    public string ModifyAction { get; set; }
    public string ModifyBy { get; set; }
    public System.DateTime? ModifyDate { get; set; }
    public DataGridView GetView { get; set; }
}