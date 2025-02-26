using DatabaseModule.Master.ProductSetup;
using System.Data;

namespace MrDAL.Master.Interface.ProductSetup;

public interface IBarcodeListRepository
{
    // RETURN VALUE IN DATA TABLE

    DataTable GetProductBarcodeList(long productId);

    // OBJECT FOR THIS FORM

    BarcodeList BarcodeList { get; set; }
}