namespace MrDAL.Models.Custom;

public class ImportProductOpeningEModel
{
    public int ProductId { get; set; }
    public string ShortName { get; set; }
    public string Description { get; set; }
    public int GodownId { get; set; }
    public int UId { get; set; }
    public string Unit { get; set; }
    public decimal Quantity { get; set; }
    public decimal Rate { get; set; }
    public decimal Amount { get; set; }
    public int BranchId { get; set; }
    public int CompanyId { get; set; }
}