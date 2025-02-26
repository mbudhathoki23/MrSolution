using System.Collections.Generic;
using System.Windows.Forms;

namespace DatabaseModule.DataEntry.OpeningMaster;

public class LedgerOpening
{
    public int Opening_Id { get; set; }
    public string Module { get; set; }
    public int Serial_No { get; set; }
    public string Voucher_No { get; set; }
    public System.DateTime OP_Date { get; set; }
    public string OP_Miti { get; set; }
    public long Ledger_Id { get; set; }
    public int? Subledger_Id { get; set; }
    public int? Agent_Id { get; set; }
    public int? Cls1 { get; set; }
    public int? Cls2 { get; set; }
    public int? Cls3 { get; set; }
    public int? Cls4 { get; set; }
    public int Currency_Id { get; set; }
    public decimal Currency_Rate { get; set; }
    public decimal Debit { get; set; }
    public decimal LocalDebit { get; set; }
    public decimal Credit { get; set; }

    public decimal LocalCredit { get; set; }

    public string? Narration { get; set; }
    public string? Remarks { get; set; }
    public string Enter_By { get; set; }
    public System.DateTime Enter_Date { get; set; }
    public string? Reconcile_By { get; set; }
    public System.DateTime? Reconcile_Date { get; set; }
    public int Branch_Id { get; set; }
    public int? Company_Id { get; set; }
    public int FiscalYearId { get; set; }
    public bool IsReverse { get; set; }
    public string CancelRemarks { get; set; }
    public string CancelBy { get; set; }
    public System.DateTime? CancelDate { get; set; }
    public System.Guid? SyncBaseId { get; set; }
    public System.Guid? SyncGlobalId { get; set; }
    public System.Guid? SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
    public List<LedgerOpening> Details { get; set; }
    public DataGridView GetView { get; set; }
}