using DatabaseModule.DataEntry.StockTransaction.ProductScheme;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.ProductSetup;

public interface IProductSchemeRepository
{

    // INSERT UPDATE DELETE
    int SaveProductScheme(string actionTag);
    Task<int> SyncProductSchemeAsync(string actionTag);

    // RETURN VALUE IN DATA TABLE
    DataTable GetProductListWithCheckbox(string productId);

    // OBJECT FOR THIS FORM
    Scheme_Master SchemeMaster { get; set; }
}