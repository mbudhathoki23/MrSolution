using DatabaseModule.Master.LedgerSetup;
using System.Collections.Generic;

namespace DatabaseModule.CloudSync;

public class AccountLedgerDataSync : BaseSyncData
{
    public IList<AccountGroup> AccountGroups { get; set; }
    public IList<AccountSubGroup> AccountSubGroups { get; set; }
    public IList<GeneralLedger> GeneralLedgers { get; set; }
    public IList<JuniorAgent> Agents { get; set; }
    public IList<Currency> Currencies { get; set; }
}