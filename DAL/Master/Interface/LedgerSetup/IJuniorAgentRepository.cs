using DatabaseModule.Master.LedgerSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.LedgerSetup;

public interface IJuniorAgentRepository
{
    // INSERT UPDATE DELETE

    int SaveJuniorAgent(string actionTag);
    Task<int> SyncJuniorAgentAsync(string actionTag);
    Task<int> SyncUpdateJuniorAgent(int juniorAgentId = 0);
    // int SaveSeniorAgent(string actionTag);
    Task<bool> SyncJuniorAgentDetailsAsync();

    Task<bool> PullJuniorAgentsFromServerToClientDBByCallCount(IDataSyncRepository<JuniorAgent> juniorAgentRepo,
        int callCount);

    int SaveJuniorAgentAuditLog(string actionTag);
    public string GetJuniorAgentScript(int juniorAgentId = 0);

    // RETURN DATA IN DATA TABLE

    DataTable GetMasterJrAgent(string actionTag, long selectedId = 0);
    DataTable CheckIsValidData(string actionTag, string tableName, string whereValue, string validId, string inputTxt, string selectedId);
    int ReturnIntValueFromTable(string tableName, string tableId, string tableColumn, string filterTxt);

    // OBJECT FOR THIS FROM 

    JuniorAgent ObjJuniorAgent { get; set; }
    MainAgent ObjSeniorAgent { get; set; }

}