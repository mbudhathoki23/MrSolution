using DatabaseModule.CloudSync;

namespace DatabaseModule.Setup.SoftwareRegistration;

public class LicenseInfo : BaseSyncData
{
    public System.Guid LicenseId { get; set; }
    public string InstalDate { get; set; }
    public System.Guid RegistrationId { get; set; }
    public string RegistrationDate { get; set; }
    public System.Guid SerialNo { get; set; }
    public System.Guid ActivationId { get; set; }
    public string LicenseExpireDate { get; set; }
    public System.Guid ExpireGuid { get; set; }
    public string LicenseTo { get; set; }
    public string RegAddress { get; set; }
    public string ParentCompany { get; set; }
    public string ParentAddress { get; set; }
    public string CustomerId { get; set; }
    public string ServerMacAddress { get; set; }
    public string ServerName { get; set; }
    public string RequestBy { get; set; }
    public string RegisterBy { get; set; }
    public string LicenseDays { get; set; }
    public string LicenseModule { get; set; }
    public string NodesNo { get; set; }
    public string SystemId { get; set; }
}