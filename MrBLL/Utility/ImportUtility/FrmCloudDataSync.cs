using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Control;
using MrDAL.Utility.Config;
using System;
using System.Data;

namespace MrBLL.Utility.ImportUtility;

public partial class FrmCloudDataSync : MrForm
{
    #region -------------- OBJECT --------------

    private ClsCloudSync _sync;
    #endregion -------------- OBJECT --------------

    #region --------------- IMPORT DATA ---------------

    public FrmCloudDataSync()
    {
        InitializeComponent();
        _sync = new ClsCloudSync();
    }

    private void FrmCloudDataSync_Load(object sender, EventArgs e)
    {
        ClearControl();
        BindTransactionList();
        BindMasterList();
        ControlEnable();
        var result = _sync.GetServerConnection();
        if (!result)
        {
            this.Dispose(true);
        }
    }

    #endregion --------------- IMPORT DATA ---------------


    // METHOD FOR THIS FORM
    #region ---------------  METHOD ---------------
    private void ClearControl()
    {
        BtnUpdate.Enabled = BtnUpdate.Visible = false;
        ChkListMaster.Enabled = ChkCopyMaster.Checked;
        ChkListEntry.Enabled = ChkTransaction.Checked;
    }
    private bool ImportDataFromCloud()
    {
        try
        {
            if (ChkCopyMaster.Checked && ChkListMaster.Items.Count > 0)
            {
                for (var i = 0; i < ChkListMaster.Items.Count; i++)
                {
                    ChkListMaster.SelectedIndex = i;
                    if (ChkListMaster.GetItemChecked(ChkListMaster.SelectedIndex))
                    {
                        _sync.ImportFunction(ChkListEntry.SelectedValue.ToString());
                    }
                }
            }

            if (ChkTransaction.Checked && ChkListEntry.Items.Count > 0)
            {
                for (var i = 0; i < ChkListEntry.Items.Count; i++)
                {
                    ChkListEntry.SelectedIndex = i;
                    if (ChkListEntry.GetItemChecked(ChkListEntry.SelectedIndex))
                    {
                        _sync.ImportFunction(ChkListEntry.SelectedValue.ToString());
                    }
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return false;
        }
    }
    private bool ExportDataToCloud()
    {
        try
        {
            var data = new ClsImport();
            if (ChkCopyMaster.Checked && ChkListMaster.Items.Count > 0)
            {
                for (var i = 0; i < ChkListMaster.Items.Count; i++)
                {
                    ChkListMaster.SelectedIndex = i;
                    if (ChkListMaster.GetItemChecked(ChkListMaster.SelectedIndex))
                    {
                    }
                }
            }

            if (ChkTransaction.Checked && ChkListEntry.Items.Count > 0)
            {
                for (var i = 0; i < ChkListEntry.Items.Count; i++)
                {
                    ChkListEntry.SelectedIndex = i;
                    if (ChkListEntry.GetItemChecked(ChkListEntry.SelectedIndex))
                    {
                        var result = data.ImportTransactionFromLocal(ChkListEntry.SelectedValue.ToString());
                    }
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return false;
        }
    }
    private void BindTransactionList()
    {
        var list = new DataTable();
        list = list.DataEntryResetModule();

        ChkListMaster.DataSource = list;
        ChkListMaster.DisplayMember = "Name";
        ChkListMaster.ValueMember = "Value";
    }
    private void BindMasterList()
    {
        var list = new DataTable();
        list = list.MasterResetModule();

        ChkListMaster.DataSource = list;
        ChkListMaster.DisplayMember = "Name";
        ChkListMaster.ValueMember = "Value";
    }
    private void TagMaster()
    {
        if (ChkListMaster.Items.Count <= 0) return;
        for (var i = 0; i < ChkListMaster.Items.Count; i++)
            ChkListMaster.SetItemChecked(i, ChkCopyMaster.Checked && ckbSelectAll.Checked);
    }
    private void TagTransaction()
    {
        if (ChkListEntry.Items.Count <= 0) return;
        for (var i = 0; i < ChkListEntry.Items.Count; i++)
            ChkListEntry.SetItemChecked(i, ChkTransaction.Checked && ckbSelectAll.Checked);
    }
    private void ControlEnable()
    {
        ChkListMaster.Enabled = ChkCopyMaster.Checked;
        ChkListEntry.Enabled = ChkTransaction.Checked;
    }
    #endregion ---------------  METHOD ---------------

    #region --------------- Event ---------------
    private void ChkCopyMaster_CheckedChanged(object sender, EventArgs e)
    {
        ChkListMaster.Enabled = ChkCopyMaster.Checked;
        TagMaster();
    }
    private void ChkTransaction_CheckedChanged(object sender, EventArgs e)
    {
        ChkListEntry.Enabled = ChkTransaction.Checked;
        TagTransaction();
    }
    private void BtnUpdate_Click(object sender, EventArgs e)
    {

    }
    private void BtnImport_Click(object sender, EventArgs e)
    {
        if (ImportDataFromCloud())
        {
            CustomMessageBox.Information("IMPORT DATA SUCCESSFULLY..!!");
        }
    }
    private void btn_Cancel_Click(object sender, EventArgs e)
    {
    }
    private void ckbSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        TagMaster();
        TagTransaction();
        ckbSelectAll.Checked = ChkCopyMaster.Checked || ChkTransaction.Checked;
    }

    #endregion --------------- Event ---------------

    #region --------------- Class ---------------

    public class ChkList
    {
        public ChkList(int s, string n, string v)
        {
            SNo = s;
            Name = n;
            Value = v;
        }

        public int SNo { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    #endregion --------------- Class ---------------
}