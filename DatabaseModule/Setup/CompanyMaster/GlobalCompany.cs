using DatabaseModule.CloudSync;

namespace DatabaseModule.Setup.CompanyMaster;

public class GlobalCompany : BaseSyncData
{
    public int GComp_Id { get; set; }
    public string Database_Name { get; set; }
    public string Company_Name { get; set; }
    public string PrintingDesc { get; set; }
    public string Database_Path { get; set; }
    public bool? Status { get; set; }
    public string PanNo { get; set; }
    public string Address { get; set; }
    public string CurrentFiscalYear { get; set; }
    public System.DateTime? LoginDate { get; set; }
    public string DataSyncOriginId { get; set; }
    public string DataSyncApiBaseUrl { get; set; }
    public System.Guid ApiKey { get; set; }
}