using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;
using DatabaseModule.Setup.CompanyMaster;

namespace MrDAL.Setup.Interface;

public interface IBranchSetupRepository
{
    //INSERT UPDATE DELETE
    int SaveBranch(string actionTag, bool IsSync = true);
    Task<bool> SyncBranchDetails();
    Task<int> SyncBranchAsync(string actionTag);
    Task<int> SyncUpdateBranch(int branchId = 0);
    //Task<bool> PullBranchByCallCount(IDataSyncRepository<Branch> repository, int callCount);
    Task<bool> PullBranchServerToClientByRowCounts(IDataSyncRepository<Branch> repository, int callCount);
    // RETURN VALUE IN STRING
    string GetBranchScript(int branchId = 0);
    //RETURN VALUE IN THE DATA TABLE
    DataTable GetMasterBranch(int branchId = 0);
    //OBJECT FOR THIS FORM
    Branch BranchSetup { get; set; }
}