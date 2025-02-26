using System;

namespace MrDAL.Domains.Hospital.ViewModule;

public class VmPatient
{
    public string ActionTag { get; set; }
    public long PaitentId { get; set; }
    public DateTime RefDate { get; set; }
    public long? IpdId { get; set; }
    public string Title { get; set; }
    public string NepaliDesc { get; set; }
    public string PaitentDesc { get; set; }
    public string ShortName { get; set; }
    public string TAddress { get; set; }
    public string PAddress { get; set; }
    public string AccountLedger { get; set; }
    public string ContactNo { get; set; }
    public decimal Age { get; set; }
    public string AgeType { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string MaritalStatus { get; set; }
    public string RegType { get; set; }
    public string Nationality { get; set; }
    public string Religion { get; set; }
    public string BloodGrp { get; set; }
    public int? DepartmentId { get; set; }
    public int? DrId { get; set; }
    public string RefDrDesc { get; set; }
    public string EmailAdd { get; set; }
    public string PastHistory { get; set; }
    public string ContactPer { get; set; }
    public string PhoneNo { get; set; }
    public bool Status { get; set; }
    public string EnterBy { get; set; }
    public DateTime EnterDate { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnit { get; set; }
    public Guid SyncBaseId { get; set; }
    public Guid SyncGlobalId { get; set; }
    public Guid SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}