using DatabaseModule.CloudSync;

namespace DatabaseModule.Setup.CompanyMaster;

public class CompanyRights : BaseSyncData
{
    public int CompanyRights_Id { get; set; }
    public int User_Id { get; set; }
    public int Company_Id { get; set; }
    public string Company_Name { get; set; }
    public System.DateTime? Start_AdDate { get; set; }
    public System.DateTime? End_AdDate { get; set; }
    public System.DateTime? Modify_Start_AdDate { get; set; }
    public System.DateTime? Modify_End_AdDate { get; set; }
    public bool? Back_Days_Restriction { get; set; }
}