using DatabaseModule.Setup.SystemSetting;
using System.Collections.Generic;
using System.Data;

namespace MrDAL.SystemSetting.SystemInterface;

public interface IIncomeTaxSettingRepository
{
    int SaveIncomeTaxSetting();

    DataTable ReturnIncomeTaxList();

    List<IncomeTaxSetting> TaxSetting { get; set; }
}