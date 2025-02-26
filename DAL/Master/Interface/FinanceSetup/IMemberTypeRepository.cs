using DatabaseModule.Master.FinanceSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.FinanceSetup;

public interface IMemberTypeRepository
{
    // INSERT UPDATE DELETE
    int SaveMemberType(string actionTag);

    Task<bool> PullMainAreasServerToClientByRowCount(IDataSyncRepository<MemberType> memberTypeRepo, int callCount);

    string GetMemberTypeScript(int memberTypeId = 0);
    //Task SaveSyncLogDetails(string v);
    Task<int> SyncMemberTypeAsync(string actionTag);

    // OBJECT FOR THIS FORM
    MemberType ObjMemberType { get; set; }
}