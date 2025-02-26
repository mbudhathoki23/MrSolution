using System;

namespace DatabaseModule.CloudSync;

public class DbSyncRepoInjectData
{
    public string LocalConnectionString { get; set; }
    public string ExternalConnectionString { get; set; }
    public string LocalOriginId { get; set; }
    public int LocalBranchId { get; set; }
    public string Username { get; set; }
    public int? LocalCompanyUnitId { get; set; }
    public DateTime DateTime { get; set; }
    public SyncApiConfig ApiConfig { get; set; }
    public int SqlTimeOutSeconds => 200;
}