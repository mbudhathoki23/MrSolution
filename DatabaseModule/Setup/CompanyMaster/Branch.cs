using DatabaseModule.CloudSync;

namespace DatabaseModule.Setup.CompanyMaster;

public class Branch : BaseSyncData
{
    public int Branch_ID { get; set; }
    public string NepaliDesc { get; set; }
    public string Branch_Name { get; set; }
    public string Branch_Code { get; set; }
    public System.DateTime? Reg_Date { get; set; }
    public string Address { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string PhoneNo { get; set; }
    public string Fax { get; set; }
    public string Email { get; set; }
    public string ContactPerson { get; set; }
    public string ContactPersonAdd { get; set; }
    public string ContactPersonPhone { get; set; }
    public string Created_By { get; set; }
    public System.DateTime? Created_Date { get; set; }
    public string Modify_By { get; set; }
    public System.DateTime? Modify_Date { get; set; }
    public System.Guid? SyncBaseId { get; set; }
    public System.Guid? SyncGlobalId { get; set; }
    public System.Guid? SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; }
}