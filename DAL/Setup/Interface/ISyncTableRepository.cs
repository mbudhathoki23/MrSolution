using System.Threading.Tasks;

namespace MrDAL.Setup.Interface;

public interface ISyncTableRepository
{
    int SaveSyncTable(string actionTag);
    Task<int> SyncTableTask(string actionTag);
}