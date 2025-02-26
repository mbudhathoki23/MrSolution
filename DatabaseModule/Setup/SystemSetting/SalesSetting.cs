namespace DatabaseModule.Setup.SystemSetting;

public class SalesSetting
{
    public byte SalesId { get; set; }
    public long? SBLedgerId { get; set; }
    public long? SRLedgerId { get; set; }
    public int? SBVatTerm { get; set; }
    public int? SBDiscountTerm { get; set; }
    public int? SBProductDiscountTerm { get; set; }
    public int? SBAdditionalTerm { get; set; }
    public int? SBServiceCharge { get; set; }
    public bool? SBDateChange { get; set; }
    public string SBCreditDays { get; set; }
    public string SBCreditLimit { get; set; }
    public bool? SBCarryRate { get; set; }
    public bool? SBChangeRate { get; set; }
    public bool? SBLastRate { get; set; }
    public bool? SBQuotationEnable { get; set; }
    public bool? SBQuotationMandetory { get; set; }
    public bool? SBDispatchOrderEnable { get; set; }
    public bool? SBDispatchMandetory { get; set; }
    public bool? SOEnable { get; set; }
    public bool? SOMandetory { get; set; }
    public bool? SCEnable { get; set; }
    public bool? SCMandetory { get; set; }
    public bool? SBSublegerEnable { get; set; }
    public bool? SBSubledgerMandetory { get; set; }
    public bool? SBAgentEnable { get; set; }
    public bool? SBAgentMandetory { get; set; }
    public bool? SBDepartmentEnable { get; set; }
    public bool? SBDepartmentMandetory { get; set; }
    public bool? SBCurrencyEnable { get; set; }
    public bool? SBCurrencyMandetory { get; set; }
    public bool? SBCurrencyRateChange { get; set; }
    public bool? SBGodownEnable { get; set; }
    public bool? SBGodownMandetory { get; set; }
    public bool? SBAlternetUnitEnable { get; set; }
    public bool? SBIndent { get; set; }
    public bool? SBNarration { get; set; }
    public bool? SBBasicAmount { get; set; }
    public bool? SBAviableStock { get; set; }
    public bool? SBReturnValue { get; set; }
    public bool? PartyInfo { get; set; }
    public bool? SBRemarksEnable { get; set; }
    public bool? SBRemarksMandatory { get; set; }
}