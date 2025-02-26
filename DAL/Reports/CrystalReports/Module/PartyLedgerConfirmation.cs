namespace MrDAL.Reports.CrystalReports.Module;

public class PartyLedgerConfirmation
{
    public string LedgerName { get; set; }
    public string LedgerAddress { get; set; }
    public string ShortName { get; set; }
    public string LedgerPanNo { get; set; }
    public string OpeningBalance { get; set; }
    public string OpeningType { get; set; }
    public string NetAmount { get; set; }
    public string TotalNetAmount { get; set; }
    public string TaxAmount { get; set; }
    public string TaxableAmount { get; set; }
    public string TaxExempted { get; set; }
    public string NetReturnAmount { get; set; }
    public string TotalNetReturnAmount { get; set; }
    public string TaxReturnAmount { get; set; }
    public string TaxableReturn { get; set; }
    public string TaxReturnExempted { get; set; }
    public string ClosingBalance { get; set; }
    public string ClosingType { get; set; }
    public int IsGroup { get; set; }
}