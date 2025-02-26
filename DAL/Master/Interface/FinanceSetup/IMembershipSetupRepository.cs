using DatabaseModule.Master.FinanceSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Threading.Tasks;


namespace MrDAL.Master.Interface.FinanceSetup;

public interface IMembershipSetupRepository
{
    // INSERT UPDATE DELETE
    int SaveMembershipSetup(string actionTag);
    Task<int> SyncMembershipSetupAsync(string actionTag);
    string GetMembershipSetupScript(int membershipId = 0);
    Task<bool> PullMembershipSetupServerToClientByRowCount(IDataSyncRepository<MemberShipSetup> membershipSetupRepo, int callCount);

    // OBJECT FOR THIS FORM
    MemberShipSetup MemberShipSetup { get; set; }
}