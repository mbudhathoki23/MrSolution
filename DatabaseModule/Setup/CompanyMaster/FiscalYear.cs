using DatabaseModule.CloudSync;

namespace DatabaseModule.Setup.CompanyMaster;

public class FiscalYear : BaseSyncData
{
    public int FY_Id { get; set; }
    public string AD_FY { get; set; }
    public string BS_FY { get; set; }
    public bool Current_FY { get; set; }
    public System.DateTime Start_ADDate { get; set; }
    public System.DateTime End_ADDate { get; set; }
    public string Start_BSDate { get; set; }
    public string End_BSDate { get; set; }
}