namespace DatabaseModule.Setup.SystemSetting;

public class InventorySetting
{
    public byte InvId { get; set; }
    public long? OPLedgerId { get; set; }
    public long? CSPLLedgerId { get; set; }
    public long? CSBSLedgerId { get; set; }
    public string NegativeStock { get; set; }
    public bool? AlternetUnit { get; set; }
    public bool? CostCenterEnable { get; set; }
    public bool? CostCenterMandetory { get; set; }
    public bool? CostCenterItemEnable { get; set; }
    public bool? CostCenterItemMandetory { get; set; }
    public bool? ChangeUnit { get; set; }
    public bool? GodownEnable { get; set; }
    public bool? GodownMandetory { get; set; }
    public bool? RemarksEnable { get; set; }
    public bool? GodownItemEnable { get; set; }
    public bool? GodownItemMandetory { get; set; }
    public bool? NarrationEnable { get; set; }
    public bool? ShortNameWise { get; set; }
    public bool? BatchWiseQtyEnable { get; set; }
    public bool? ExpiryDate { get; set; }
    public bool? FreeQty { get; set; }
    public bool? GroupWiseFilter { get; set; }
    public bool? GodownWiseStock { get; set; }
}