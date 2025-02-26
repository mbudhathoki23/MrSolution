using DatabaseModule.Master.LedgerSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.LedgerSetup;

public interface IMainAreaRepository
{
    //INSERT UPDATE DELETE
    string GetMainAreaScript(int mainAreaId = 0);
    int SaveMainArea(string actionTag);

    Task<int> SyncMainAreaAsync(string actionTag);
    Task<bool> SyncMainAreaDetailsAsync();

    int SaveMainAreaAuditLog(string actionTag);

    Task<bool> PullMainAreasServerToClientByRowCount(IDataSyncRepository<MainArea> mainAreaRepo, int callCount);


    //RETURN DATA IN THE DATA TABLE
    DataTable GetMasterMainArea(string actionTag, bool status = false, int selectedId = 0);


    //OBJECT OF THIS FORM
    MainArea ObjMainArea { get; set; }
}