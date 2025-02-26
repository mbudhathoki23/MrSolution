using MrBLL.DataEntry.FinanceMaster;
using MrBLL.DataEntry.OpeningMaster;
using MrBLL.DataEntry.PurchaseMaster;
using MrBLL.DataEntry.SalesMaster;
using MrDAL.Control.WinControl;
using MrDAL.Utility.Config;
using MrDAL.Utility.Interface;
using System;
using System.Windows.Forms;

namespace MrBLL.Utility.Database;

public partial class FrmDataReconciliation : MrForm
{
    private readonly bool _isDetails;
    private readonly bool _isZoom;
    private readonly string _module;
    private int _rowIndexId;
    private IUtility GetUtility { get; }

    public FrmDataReconciliation(bool isDetails = false, string module = "", bool isZoom = false)
    {
        InitializeComponent();
        GetUtility = new ClsUtilityTools();
        _isZoom = isZoom;
        _module = module;
        _isDetails = isDetails;
    }

    private void FrmDataReconciliation_Load(object sender, EventArgs e)
    {
        DGrid.Rows.Clear();
        if (_isDetails)
        {
            rDetails.Checked = true;
            rDetails.Focus();
        }
        else
        {
            rSummary.Checked = true;
            rSummary.Focus();
        }

        if (_isZoom) BtnShow.PerformClick();
    }

    private void FrmDataReconciliation_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape) BtnCancel.PerformClick();
    }

    private void RSummary_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void BtnShow_Click(object sender, EventArgs e)
    {
        if (rSummary.Checked)
        {
            GetUtility.ReturnSummaryAccountTransactionDesign(DGrid);
        }
        else
        {
            GetUtility.ReturnDetailsAccountTransactionDesign(DGrid);
        }
        DGrid.DataSource = rSummary.Checked ? GetUtility.CheckSummaryAccountTransactionPosting() : GetUtility.CheckDetailsAccountTransactionPosting(_module);
    }

    private void DGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void DGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        _rowIndexId = e.RowIndex;
    }

    private void DGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            DGrid.Rows[_rowIndexId].Selected = true;
            e.SuppressKeyPress = true;
            var module = DGrid.Rows[_rowIndexId].Cells["GTxtModule"].Value.ToString().Trim();
            if (rSummary.Checked)
            {
                var showDialog = new FrmDataReconciliation(true, module, true);
                showDialog.ShowDialog();
            }
            else if (rDetails.Checked)
            {
                var voucherNo = DGrid.Rows[_rowIndexId].Cells["GTxtVoucherNo"].Value.ToString().Trim();
                using var frm = module switch
                {
                    "LOB" => new FrmLedgerOpeningEntry(true, voucherNo),
                    "OB" => new FrmLedgerOpeningEntry(true, voucherNo),
                    "N" => new FrmLedgerOpeningEntry(true, voucherNo),
                    "CB" => new FrmCashBankEntry(true, voucherNo),
                    "PDC" => new FrmPDCVoucher(true, voucherNo),
                    "JV" => new FrmJournalVoucherEntry(true, voucherNo),
                    "SB" => new FrmSalesInvoiceEntry(true, voucherNo),
                    "SR" => new FrmSalesReturnEntry(true, voucherNo),
                    "PB" => new FrmPurchaseInvoiceEntry(true, voucherNo),
                    "PR" => new FrmPurchaseReturnEntry(true, voucherNo),
                    "DN" => new FrmVoucherNotesEntry(true, voucherNo, module),
                    "CN" => new FrmVoucherNotesEntry(true, voucherNo, module),
                    _ => new Form()
                };
                if (string.IsNullOrEmpty(frm.Text)) return;
                frm.ShowDialog();
                BtnShow_Click(sender, e);
            }
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void DGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        DGrid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
    }
}