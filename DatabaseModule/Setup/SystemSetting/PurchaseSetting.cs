namespace DatabaseModule.Setup.SystemSetting;

public class PurchaseSetting
{
    public byte PurId { get; set; }
    public long? PBLedgerId { get; set; }
    public long? PRLedgerId { get; set; }
    public int? PBVatTerm { get; set; }
    public int? PBDiscountTerm { get; set; }
    public int? PBProductDiscountTerm { get; set; }
    public int? PBAdditionalTerm { get; set; }
    public bool? PBDateChange { get; set; }
    public string PBCreditDays { get; set; }
    public string PBCreditLimit { get; set; }
    public bool? PBCarryRate { get; set; }
    public bool? PBChangeRate { get; set; }
    public bool? PBLastRate { get; set; }
    public bool? POEnable { get; set; }
    public bool? POMandetory { get; set; }
    public bool? PCEnable { get; set; }
    public bool? PCMandetory { get; set; }
    public bool? PBSublegerEnable { get; set; }
    public bool? PBSubledgerMandetory { get; set; }
    public bool? PBAgentEnable { get; set; }
    public bool? PBAgentMandetory { get; set; }
    public bool? PBDepartmentEnable { get; set; }
    public bool? PBDepartmentMandetory { get; set; }
    public bool? PBCurrencyEnable { get; set; }
    public bool? PBCurrencyMandetory { get; set; }
    public bool? PBCurrencyRateChange { get; set; }
    public bool? PBGodownEnable { get; set; }
    public bool? PBGodownMandetory { get; set; }
    public bool? PBAlternetUnitEnable { get; set; }
    public bool? PBIndent { get; set; }
    public bool? PBNarration { get; set; }
    public bool? PBBasicAmount { get; set; }
    public bool? PBRemarksEnable { get; set; }
    public bool? PBRemarksMandatory { get; set; }
}