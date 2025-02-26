using DatabaseModule.CloudSync;

namespace DatabaseModule.Setup.CompanyMaster;

public class CompanyInfo : BaseSyncData
{
    public int Company_Id { get; set; }
    public string Company_Name { get; set; }
    public string PrintDesc { get; set; }
    public byte[] Company_Logo { get; set; }
    public System.DateTime? CReg_Date { get; set; }
    public string Address { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string PhoneNo { get; set; }
    public string Fax { get; set; }
    public string Pan_No { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public string Database_Name { get; set; }
    public string Database_Path { get; set; }
    public string Version_No { get; set; }
    public bool Status { get; set; }
    public System.DateTime? CreateDate { get; set; }
    public string SoftModule { get; set; }
    public System.DateTime? LoginDate { get; set; }
    public bool? IsSyncOnline { get; set; }
    public System.Guid ApiKey { get; set; }
    public bool IsTaxRegister { get; set; }

}