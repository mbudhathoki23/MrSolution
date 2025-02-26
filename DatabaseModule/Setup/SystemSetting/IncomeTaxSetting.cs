namespace DatabaseModule.Setup.SystemSetting;

public class IncomeTaxSetting
{
    public int SerialNo { get; set; }
    public int FiscalYearId { get; set; }
    public string IncomeTaxTitle { get; set; }
    public decimal SingleTaxAmount { get; set; }
    public decimal MarriedTaxAmount { get; set; }
    public decimal TaxRate { get; set; }

}