using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Threading.Tasks;

namespace MrDAL.SystemSetting.Interface;

public interface IFinanceSettingRepository
{
    int SaveFinanceSetting(string actionTag);
    string GetFinanceScript(int settingId = 0);
    Task<bool> PullFinanceSettingServerToClientByRowCounts(
        IDataSyncRepository<DatabaseModule.Setup.SystemSetting.FinanceSetting> financeRepo, int callCount);
    // OBJECT 
    DatabaseModule.Setup.SystemSetting.FinanceSetting VmFinance { get; set; }
}