using System;

namespace MrDAL.Models.Custom;

public class SalesMembershipModel
{
    public int MShipId { get; set; }
    public string MShipDesc { get; set; }
    public string MShipShortName { get; set; }
    public string PhoneNo { get; set; }
    public long LedgerId { get; set; }
    public string EmailAdd { get; set; }
    public int MemberTypeId { get; set; }
    public string MemberId { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
    public DateTime MValidDate { get; set; }
    public DateTime MExpireDate { get; set; }
    public string EnterBy { get; set; }
    public DateTime EnterDate { get; set; }
    public bool ActiveStatus { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short? SyncRowVersion { get; set; }
    public Guid? SyncBaseId { get; set; }
    public string PriceTag { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal Balance { get; set; }
    public string MemberType { get; set; }
}