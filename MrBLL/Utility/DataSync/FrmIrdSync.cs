using DevExpress.XtraSplashScreen;
using MrBLL.Utility.SplashScreen;
using MrDAL.Core.Extensions;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Utility.dbMaster;
using System;
using System.Windows.Forms;

namespace MrBLL.Utility.DataSync;

public partial class FrmIrdSync : Form
{
    private readonly ClsIrdApiSync _apiSync = new();

    public FrmIrdSync()
    {
        InitializeComponent();
        RGrid.AutoGenerateColumns = false;
        CmbSource.SelectedIndex = 0;
        CmbSource_SelectedIndexChanged(this, EventArgs.Empty);
    }

    private void FrmIrdSync_Load(object sender, EventArgs e)
    {
    }

    private void FrmIrdSync_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void CmbSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CmbSource.Text is "Sales Invoice")
        {
            RGrid.DataSource = _apiSync.GetOutstandingSalesInvoice();
        }
        else if (CmbSource.Text is "Sales Return")
        {
            RGrid.DataSource = _apiSync.GetOutstandingSalesReturnInvoice();
        }
        else if (CmbSource.Text is "Sales Return Cancel")
        {
            RGrid.DataSource = _apiSync.GetOutstandingSalesReturnCancelInvoice();
        }
        else if (CmbSource.Text is "Sales Cancel")
        {
            RGrid.DataSource = _apiSync.GetOutstandingSalesCancelInvoice();
        }
    }

    private void RGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        SelectGridValue();
    }

    private void RGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        SelectGridValue();
    }

    private void ChkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in RGrid.Rows)
        {
            row.Cells[0].Value = ChkSelectAll.Checked ? 1 : 0;
        }
    }

    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.Question("ARE YOU SURE WANT TO SYNC..??") is DialogResult.Yes)
        {
            var list = new FrmListView();
            SplashScreenManager.ShowForm(typeof(FrmWait));
            CreateDatabaseTable.DropTrigger();
            foreach (DataGridViewRow row in RGrid.Rows)
            {
                var result = string.Empty;
                if (row.Cells[0].Value.GetInt() <= 0) continue;
                var voucherNo = row.Cells["VoucherNo"].Value.GetString();
                if (CmbSource.Text is "Sales Invoice")
                {
                    result = _apiSync.SyncSalesBillApiAsync(voucherNo);
                }
                else if (CmbSource.Text is "Sales Return")
                {
                    result = _apiSync.SyncSalesReturnApi(voucherNo);
                }
                else if (CmbSource.Text is "Sales Return Cancel")
                {
                    result = _apiSync.SyncSalesReturnApi(voucherNo);
                }
                else if (CmbSource.Text is "Sales Cancel")
                {
                    result = _apiSync.SyncSalesCancelApi(voucherNo);
                }
                list.AddToList(result, _apiSync.IsSuccess);
            }
            SplashScreenManager.CloseForm(false);
            list.Show();
            CreateDatabaseTable.CreateTrigger();
            CmbSource_SelectedIndexChanged(sender, EventArgs.Empty);
        }
    }

    private void BtnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void SelectGridValue()
    {
        if (RGrid.CurrentRow != null)
        {
            var value = RGrid.CurrentRow.Cells[0].Value.GetInt();
            RGrid.CurrentRow.Cells[0].Value = value is 1 ? 0 : 1;
        }
    }
}