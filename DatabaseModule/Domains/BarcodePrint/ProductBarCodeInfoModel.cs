namespace DatabaseModule.Domains.BarcodePrint;

public class ProductBarCodeInfoModel
{
    public ProductBarCodeInfoModel()
    {
    }

    public ProductBarCodeInfoModel(int productId, string productName, string productCategory, string productBarCode, string printedBarCode)
    {
        ProductId = productId;
        ProductName = productName;
        ProductCategory = productCategory;
        ProductBarCode = productBarCode;
        PrintedBarCode = printedBarCode;
    }

    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductCategory { get; set; }
    public string ProductBarCode { get; set; }
    public string PrintedBarCode { get; set; }
    public decimal SalesRate { get; set; }
    public decimal BuyRate { get; set; }
}