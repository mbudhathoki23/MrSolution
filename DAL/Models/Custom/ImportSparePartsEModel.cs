namespace MrDAL.Models.Custom;

public class ImportSparePartsEModel
{
    public string ShortName { get; set; }
    public string Description { get; set; }
    public int GroupId { get; set; }
    public string Group { get; set; }
    public int SubGroupId { get; set; }
    public string SubGroup { get; set; }
    public int UId { get; set; }
    public string Unit { get; set; }
    public int AltUnit { get; set; }
    public int QtyConv { get; set; }
    public int AltConv { get; set; }
    public decimal BuyRate { get; set; }
    public decimal SalesRate { get; set; }
    public decimal TaxRate { get; set; }
    public decimal MinStock { get; set; }
    public decimal MaxStock { get; set; }
    public decimal MRPRate { get; set; }
    public int BranchId { get; set; }
    public int CompanyId { get; set; }
}