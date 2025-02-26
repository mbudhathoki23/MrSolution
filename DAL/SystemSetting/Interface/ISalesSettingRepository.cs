using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Threading.Tasks;

namespace MrDAL.SystemSetting.Interface;

public interface ISalesSettingRepository
{
    int SaveSalesSetting(string actionTag);
    string GetSalesSettingScript(int settingId = 0);
    Task<bool> PullSalesSettingServerToClientByRowCounts(
        IDataSyncRepository<DatabaseModule.Setup.SystemSetting.SalesSetting> salesSettingRepo, int callCount);
    // OBJECT 
    DatabaseModule.Setup.SystemSetting.SalesSetting VmSales { get; set; }
}