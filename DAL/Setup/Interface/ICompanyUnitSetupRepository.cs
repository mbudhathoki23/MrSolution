using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;
using DatabaseModule.Setup.CompanyMaster;

namespace MrDAL.Setup.Interface;

public interface ICompanyUnitSetupRepository
{
    // RETURN VALUE IN DATA TABLE

    DataTable CheckMasterValidData(string actionTag, string tableName, string whereValue, string validId, string inputTxt, string selectedId);

    // INSERT UPDATE DELETE

    int SaveCompanyUnit(string actionTag, bool IsSync = true);
    Task<bool> SyncCompanyUnitDetailsAsync();

    Task<bool> PullCompanyUnitServerToClientByRowCounts(IDataSyncRepository<CompanyUnit> companyUnitRepository,
        int callCount);
    string GetCompanyUnitScript(int cmpunit_ID = 0);

    // OBJECT FOR THIS FORM

    CompanyUnit CompanyUnitSetup { get; set; }


}