using DatabaseModule.DataEntry.OpeningMaster;
using MrDAL.Models.Common;
using MrDAL.Models.Custom;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
namespace MrDAL.DataEntry.Interface.OpeningMaster;

public interface IProductOpeningRepository
{
    // INSERT UPDATE DELETE
    Task<NonQueryResult> UpdateProductOpeningImportAsync(IList<ImportProductOpeningEModel> rows, int branchId, string username);
    int SaveProductOpeningSetup(string actionTag);
    Task<int> SyncProductOpeningSetupAsync(string actionTag);
    Task<ListResult<ProductSubGroupEModel>> CreateAndFetchProductSubgroupsAsync(int branchId, IList<ProductSubGroupEModel> subGroups, string username);
    Task<ListResult<IntValueModel>> CreateAndFetchProductGroupsAsync(int branchId, IList<string> groups, string username);
    Task<ListResult<IntValueModel>> CreateAndFetchUnitsAsync(int branchId, IList<string> units, string username);
    Task<ListResult<IntLongValueModel>> CreateAndFetchProductAsync(int branchId, IList<string> units, string username);
    Task<ListResult<IntValueModel>> CreateAndFetchGoDownAsync(int branchId, IList<string> units, string username);
    //PULL PRODUCT OPENING
    // RETURN VALUE IN DATA TABLE
    public string GetProductOpening(int openingId = 0);
    DataSet GetProductOpeningVoucherDetails(string voucherNo);
    // OBJECT FOR THIS FORM
    ProductOpening VmProductOpening { get; set; }
    List<ProductOpening> Details { get; set; }
    DataTable ReadExcelFile(string path, string sheetName);
    // RETURN VALUE IN INT
    void DataGridToExcel(DataTable dt, string folderPath, string fileName);

    // RETURN VALUE IN DATA TABLE
    DataTable GetProductListForImportFormat();
    DataTable DownloadFormat();
}