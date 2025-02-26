using System.Windows.Forms;

namespace DatabaseModule.DataEntry.StockTransaction.GodownTransfer;

public class GT_MASTER
{
    public string VoucherNo { get; set; }
    public System.DateTime VoucherDate { get; set; }
    public string VoucherMiti { get; set; }
    public int FrmGdn { get; set; }
    public int? Cls1 { get; set; }
    public int? Cls2 { get; set; }
    public int? Cls3 { get; set; }
    public int? Cls4 { get; set; }
    public string Remarks { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public int? CompanyUnit { get; set; }
    public int BranchId { get; set; }
    public string ReconcileBy { get; set; }
    public System.DateTime? ReconcileDate { get; set; }
    public string ModifyAction { get; set; }
    public string ModifyBy { get; set; }
    public System.DateTime? ModifyDate { get; set; }
    public DataGridView GetView { get; set; }
}