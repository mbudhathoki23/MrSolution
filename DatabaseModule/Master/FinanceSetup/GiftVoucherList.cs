using DatabaseModule.CloudSync;

namespace DatabaseModule.Master.FinanceSetup;

public class GiftVoucherList : BaseSyncData
{
    public int VoucherId { get; set; }
    public long CardNo { get; set; }
    public System.DateTime ExpireDate { get; set; }
    public string Description { get; set; }
    public string VoucherType { get; set; }
    public decimal IssueAmount { get; set; }
    public bool IsUsed { get; set; }
    public decimal BalanceAmount { get; set; }
    public decimal BillAmount { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
    public bool Status { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public System.Guid SyncBaseId { get; set; }
    public System.Guid SyncGlobalId { get; set; }
    public System.Guid SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public byte SyncRowVersion { get; set; }
}