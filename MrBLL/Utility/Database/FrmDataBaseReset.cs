using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Utility.Config;
using MrDAL.Utility.Interface;
using System;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.Utility.Database;

public partial class FrmDataBaseReset : MrForm
{
    #region---------------FORM---------------

    public FrmDataBaseReset()
    {
        InitializeComponent();
        _utility = new ClsImport();
    }

    private void FrmDataBaseReset_Load(object sender, EventArgs e)
    {
        ClearControl();
        BindChkTransactionList();
        BindChkMasterListItem();
    }

    private void ClearControl()
    {
        BtnResetDatabase.Enabled = BtnReset.Enabled = BtnCancel.Enabled = true;
        ckbSelectAll.Checked = true;
        checkBox1.Checked = true;
        chkAudit.Checked = true;
        ckbSelectAll.Checked = false;
        checkBox1.Checked = false;
        chkAudit.Checked = false;
    }

    #endregion

    #region---------------METHOD---------------

    private void BindChkTransactionList()
    {
        var list = new DataTable();
        list = list.DataEntryResetModule();
        ChkListEntry.DataSource = list;
        ChkListEntry.DisplayMember = "DESCRIPTION";
        ChkListEntry.ValueMember = "MODULE";
    }

    private void BindChkMasterListItem()
    {
        var list = new DataTable();
        list = list.MasterResetModule();

        ChkListMaster.DataSource = list;
        ChkListMaster.DisplayMember = "DESCRIPTION";
        ChkListMaster.ValueMember = "DESCRIPTION";
    }

    private void TagMaster()
    {
        if (ChkListMaster.Items.Count <= 0)
        {
            return;
        }
        for (var i = 0; i < ChkListMaster.Items.Count; i++)
        {
            ChkListMaster.SetItemChecked(i, ckbSelectAll.Checked);
        }
    }

    private void TagTransaction()
    {
        if (ChkListEntry.Items.Count <= 0)
        {
            return;
        }
        for (var i = 0; i < ChkListEntry.Items.Count; i++)
        {
            ChkListEntry.SetItemChecked(i, checkBox1.Checked);
        }
    }

    public bool ResetDataMaster()
    {
        var data = new ClsImport();
        if (ChkListMaster.CheckedItems.Count <= 0)
        {
            return true;
        }
        for (var i = 0; i < ChkListMaster.Items.Count; i++)
        {
            ChkListMaster.SelectedIndex = i;
            if (ChkListMaster.GetItemChecked(ChkListMaster.SelectedIndex))
            {
                var value = ChkListMaster.SelectedValue.ToString();
                var result = data.ResetMaster(value, ObjGlobal.InitialCatalog, _isAuditRecord);
            }
        }

        return true;
    }

    public bool ResetTransaction()
    {
        _isAuditRecord = chkAudit.Checked;
        if (ChkListEntry.CheckedItems.Count <= 0)
        {
            return true;
        }
        for (var i = 0; i < ChkListEntry.Items.Count; i++)
        {
            ChkListEntry.SelectedIndex = i;
            if (ChkListEntry.GetItemChecked(ChkListEntry.SelectedIndex))
            {
                var result = _utility.ResetTransaction(ChkListEntry.SelectedValue.ToString(), ObjGlobal.InitialCatalog, _isAuditRecord);
            }
        }

        return true;
    }

    #endregion

    #region---------------EVENT---------------

    private void BtnReset_Click(object sender, EventArgs e)
    {
        try
        {
            if (CustomMessageBox.Question("ONCE RESET THE DATA WON'T BE RECOVER, ARE YOU SURE WANT TO RESET..??") is DialogResult.Yes)
            {
                BtnReset.Enabled = BtnResetDatabase.Enabled = BtnCancel.Enabled = false;

                _isAuditRecord = chkAudit.Checked;

                if (ChkListMaster.CheckedItems.Count > 0)
                {
                    if (ResetDataMaster())
                    {
                        CustomMessageBox.Information("DATABASE RESET SUCCESSFULLY..!!");
                    }
                    else
                    {
                        CustomMessageBox.ErrorMessage("ERROR OCCURS WHILE DATABASE RESET..!!");
                    }
                }

                if (ChkListEntry.CheckedItems.Count > 0)
                {
                    if (ResetTransaction())
                    {
                        CustomMessageBox.Information("DATABASE RESET SUCCESSFULLY..!!");
                    }
                    else
                    {
                        CustomMessageBox.ErrorMessage("ERROR OCCURS WHILE DATABASE RESET..!!");
                    }
                }
            }
            else
            {
                BtnReset.Focus();
            }

            ClearControl();
        }
        catch (Exception ex)
        {
            MessageBox.Show($@"TO DELETE MASTER DELETE TRANSACTION FIRST..!!!{ex.Message}");
            ClearControl();
        }
    }

    private void BtnResetDatabase_Click(object sender, EventArgs e)
    {
        try
        {
            //PanelHeader.Visible = false;
            if (CustomMessageBox.Question("ONCE RESET THE DATA WON'T BE RECOVER, ARE YOU SURE WANT TO RESET..??") is DialogResult.Yes)
            {
                BtnResetDatabase.Enabled = BtnReset.Enabled = BtnCancel.Enabled = false;

                for (var i = 0; i < ChkListEntry.Items.Count; i++)
                {
                    ChkListEntry.SetItemChecked(i, true);
                }
                for (var i = 0; i < ChkListMaster.Items.Count; i++)
                {
                    ChkListMaster.SetItemChecked(i, true);
                }
                if (ChkListEntry.CheckedItems.Count > 0 && ChkListMaster.CheckedItems.Count > 0)
                {
                    if (ResetTransaction() && ResetDataMaster())
                    {
                        CustomMessageBox.Information("DATABASE RESET SUCCESSFULLY..!!");
                    }
                    else
                    {
                        CustomMessageBox.ErrorMessage("ERROR OCCURS WHILE DATABASE RESET..!!");
                    }
                }
            }
            else
            {
                BtnResetDatabase.Focus();
            }
            ClearControl();
        }
        catch (Exception ex)
        {
            CustomMessageBox.ErrorMessage($"ERROR OCCURS WHILE DATABASE RESET [{ex.Message}]..!!");
            ClearControl();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
    }

    private void CkbSelectAll_Master_CheckedChanged(object sender, EventArgs e)
    {
        TagMaster();
    }

    private void CkbSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        TagTransaction();
    }

    private void FrmDataBaseReset_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (ChkListMaster.CheckedItems.Count > 0 || ChkListEntry.CheckedItems.Count > 0)
            {
                if (MessageBox.Show(@"ARE YOU SURE WANT TO CLEAR FORM..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    ClearControl();
            }
            else
            {
                if (MessageBox.Show(@"ARE YOU SURE WANT TO CLEAR FORM..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Close();
            }
        }
    }

    #endregion

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

    #endregion

    #region---------------OBJECT GLOBAL---------------
    private bool _isAuditRecord = true;
    private IOnlineSync _utility;

    #endregion
}