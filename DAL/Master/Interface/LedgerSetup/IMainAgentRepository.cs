using DatabaseModule.Master.LedgerSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.LedgerSetup;

public interface IMainAgentRepository
{
    // INSERT UPDATE DELETE

    int SaveSeniorAgent(string actionTag);
    Task<int> SyncSeniorAgentAsync(string actionTag);
    Task<bool> SyncSeniorAgentDetailsAsync();
    Task<int> SyncUpdateSeniorAgent(int sAgentId = 0);

    Task<bool> PullSeniorAgentsFromServerToClientDBByCallCount(IDataSyncRepository<MainAgent> seniorAgentRepo,
        int callCount);

    public int SaveMainAgentAuditLog(string actionTag);

    // RETURN DATA IN DATA TABLE

    public string GetSeniorAgentScript(int sAgentId = 0);
    DataTable GetMasterSrAgent(string actionTag, long selectedId = 0);
    int ReturnIntValueFromTable(string tableName, string tableId, string tableColumn, string filterTxt);
    DataTable CheckIsValidData(string actionTag, string tableName, string whereValue, string validId, string inputTxt, string selectedId);


    // OBJECT FOR THIS FROM 

    MainAgent ObjSeniorAgent { get; set; }
}