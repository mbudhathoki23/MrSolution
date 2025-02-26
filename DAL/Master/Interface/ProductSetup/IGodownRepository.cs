using DatabaseModule.Master.ProductSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.ProductSetup;

public interface IGodownRepository
{
    // INSERT UPDATE DELETE
    int SaveGodown(string actionTag);
    Task<int> SyncGodownAsync(string actionTag);
    Task<int> SyncUpdateGodown(int gId = 0);

    Task<bool> GetAndSaveUnSynchronizedGodown();

    Task<bool> PullGoDownsServerToClientByRowCounts(IDataSyncRepository<Godown> godownRepo, int callCount);




    // RETURN VALUE IN DATA TABLE
    public string GetGodownScript(int gId = 0);
    DataTable GetMasterGoDown(string actionTag, int status = 0, int selectedId = 0);

    // OBJECT FOR THIS FORM
    Godown ObjGodown { get; set; }
}