using MrDAL.Models.Common;
using MrDAL.Models.Custom;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
namespace MrDAL.Master.Interface.SystemSetup;

public interface ISparePartsImportRepository
{
    #region ---------------  PRODUCT INSERT, UPDATE EVENT ---------------
    Task<NonQueryResult> UpdateProductImportAsync(IList<ImportSparePartsEModel> rows, int branchId, string username);
    Task<ListResult<ProductSubGroupEModel>> CreateAndFetchProductSubgroupsAsync(int branchId, IList<ProductSubGroupEModel> subGroups, string username);
    Task<ListResult<IntValueModel>> CreateAndFetchProductGroupsAsync(int branchId, IList<string> groups, string username);
    Task<ListResult<IntValueModel>> CreateAndFetchUnitsAsync(int branchId, IList<string> units, string username);

    #endregion ---------------  PRODUCT INSERT, UPDATE EVENT --------------- 
    DataTable ReadExcelFile(string path, string sheetName);
    // RETURN VALUE IN INT
    void DataGridToExcel(DataTable dt, string folderPath, string fileName);

    // RETURN VALUE IN DATA TABLE
    DataTable GetProductListForImportFormat();
    DataTable DownloadFormat();
}