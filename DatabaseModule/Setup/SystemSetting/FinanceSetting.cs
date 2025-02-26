namespace DatabaseModule.Setup.SystemSetting;

public class FinanceSetting
{
    public byte FinId { get; set; }
    public long? ProfiLossId { get; set; }
    public long? CashId { get; set; }
    public long? VATLedgerId { get; set; }
    public long? PDCBankLedgerId { get; set; }
    public bool? ShortNameWisTransaction { get; set; }
    public bool? WarngNegativeTransaction { get; set; }
    public string NegativeTransaction { get; set; }
    public bool? VoucherDate { get; set; }
    public bool? AgentEnable { get; set; }
    public bool? AgentMandetory { get; set; }
    public bool? DepartmentEnable { get; set; }
    public bool? DepartmentMandetory { get; set; }
    public bool? RemarksEnable { get; set; }
    public bool? RemarksMandetory { get; set; }
    public bool? NarrationMandetory { get; set; }
    public bool? CurrencyEnable { get; set; }
    public bool? CurrencyMandetory { get; set; }
    public bool? SubledgerEnable { get; set; }
    public bool? SubledgerMandetory { get; set; }
    public bool? DetailsClassEnable { get; set; }
    public bool? DetailsClassMandetory { get; set; }
}