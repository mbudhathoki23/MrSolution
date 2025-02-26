namespace MrDAL.Models.Custom;

public class SalesGeneralLedgerModel
{
    public long LedgerId { get; set; }
    public string Particular { get; set; }
    public string ShortName { get; set; }
    public string LedgerCode { get; set; }
    public string Address { get; set; }
    public string PhoneNo { get; set; }
    public decimal? Balance { get; set; }
    public string BType { get; set; }
    public string PanNo { get; set; }
    public string GLType { get; set; }
    public string GrpType { get; set; }
    public string PrimaryGrp { get; set; }
    public string GroupDesc { get; set; }
    public string SubGroupDesc { get; set; }
    public string SalesMan { get; set; }
    public string Currency { get; set; }
    public decimal CrDays { get; set; }
    public decimal CrLimit { get; set; }
    public string CrTYpe { get; set; }
}