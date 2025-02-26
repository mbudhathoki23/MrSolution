namespace DatabaseModule.Domains.SmsConfig;

public class SMS_CONFIG
{
    public int SMSCONFIG_ID { get; set; }
    public string AlternetNumber { get; set; }
    public string TOKEN { get; set; }
    public bool IsCashBank { get; set; }
    public bool IsJournalVoucher { get; set; }
    public bool IsSalesReturn { get; set; }
    public bool IsSalesInvoice { get; set; }
    public bool IsPurchaseInvoice { get; set; }
    public bool IsPurchaseReturn { get; set; }
}