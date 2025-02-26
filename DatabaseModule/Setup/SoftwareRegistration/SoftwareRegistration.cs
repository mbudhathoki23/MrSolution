using DatabaseModule.CloudSync;

namespace DatabaseModule.Setup.SoftwareRegistration;

public class SoftwareRegistration : BaseSyncData
{
    public System.Guid RegistrationId { get; set; }
    public string CustomerId { get; set; }
    public string Server_MacAdd { get; set; }
    public string Server_Desc { get; set; }
    public string ClientDescription { get; set; }
    public string ClientAddress { get; set; }
    public string ClientSerialNo { get; set; }
    public string RequestBy { get; set; }
    public string RegisterBy { get; set; }
    public string RegistrationDate { get; set; }
    public string RegistrationDays { get; set; }
    public string ExpiredDate { get; set; }
    public string ProductDescription { get; set; }
    public string NoOfNodes { get; set; }
    public string Module { get; set; }
    public string System_Id { get; set; }
    public string ActivationCode { get; set; }
    public bool? IsOnline { get; set; }
    public bool IrdRegistration { get; set; }
}