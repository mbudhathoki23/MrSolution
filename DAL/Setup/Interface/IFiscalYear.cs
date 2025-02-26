using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;
using DatabaseModule.Setup.CompanyMaster;

namespace MrDAL.Setup.Interface;

public interface IFiscalYear
{
    int SaveFiscalYear(string actionTag);
    // RETURN VALUE IN DATA TABLE
    DataTable GetMasterFiscalYear();
    string GetFiscalYearScript(int yearId = 0);
    Task<bool> PullFiscalYearServerToClientByRowCount(IDataSyncRepository<FiscalYear> fiscalYearRepo, int callCount);
    // OBJECT FOR THIS FORM
    FiscalYear Fiscal { get; set; }
}