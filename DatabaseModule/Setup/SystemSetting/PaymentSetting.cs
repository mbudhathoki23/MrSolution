namespace DatabaseModule.Setup.SystemSetting;

public class PaymentSetting
{
    public int PaymentId { get; set; }
    public long? CashLedgerId { get; set; }
    public long? CardLedgerId { get; set; }
    public long? BankLedgerId { get; set; }
    public long? PhonePayLedgerId { get; set; }
    public long? EsewaLedgerId { get; set; }
    public long? KhaltiLedgerId { get; set; }
    public long? RemitLedgerId { get; set; }
    public long? ConnectIpsLedgerId { get; set; }
    public long? PartialLedgerId { get; set; }
}