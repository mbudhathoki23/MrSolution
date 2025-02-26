using DevExpress.XtraEditors;
using MrDAL.Control.ControlsEx;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.ProductSetup;
using MrDAL.Master.ProductSetup;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Master.ProductSetup;

public partial class FrmWareHouse : MrForm
{
    protected void SetGridDataToText(int WareHouseId)
    {
        using var dt = _wareHouseRepository.GetMasterGoDown(_ActionTag, 1, WareHouseId);
        if (dt != null && dt.Rows.Count > 0)
        {
            int.TryParse(dt.Rows[0]["GID"].ToString(), out var _WarehouseId);
            WareHouseId = _WarehouseId;
            TxtDescription.Text = dt.Rows[0]["GName"].ToString();
            TxtShortName.Text = dt.Rows[0]["GCode"].ToString();
            TxtLocation.Text = dt.Rows[0]["GLocation"].ToString();

            if (_ActionTag is "UPDATE")
                TxtDescription.Focus();
            else
                BtnSave.Focus();
        }
    }

    private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnDescription_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtDescription.Text.Trim()) && TxtDescription.Enabled is true &&
                TxtDescription.Focused is true)
            {
                MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtDescription.Focus();
            }
        }
        else if (_ActionTag.ToUpper() != "SAVE" && TxtDescription.ReadOnly is true)
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtDescription, BtnDescription);
        }
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        using var frmPickList =
            new FrmAutoPopList("MIN", "GODOWN", ObjGlobal.SearchText, _ActionTag, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtDescription.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                WareHouseId = ObjGlobal.ReturnInt(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
                if (_ActionTag != "SAVE")
                {
                    TxtDescription.ReadOnly = false;
                    SetGridDataToText(WareHouseId);
                }
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"GODOWN NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtDescription.Focus();
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && string.IsNullOrEmpty(TxtDescription.Text.Trim()) &&
            TxtDescription.Focused is true && !string.IsNullOrEmpty(_ActionTag))
        {
            MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
        }
    }

    #region --------------- GLOBAL VARIABLE ---------------

    public string WareHouse = string.Empty;
    public int WareHouseId;
    private string _SearchKey;
    private string Query = string.Empty;
    private string _ActionTag = string.Empty;
    private string Searchtext = string.Empty;
    private bool _IsZoom;
    private readonly IWareHouseRepository _wareHouseRepository = new WareHouseRepository();
    private IMasterSetup _setup;
    private ClsMasterForm clsFormControl;

    #endregion --------------- GLOBAL VARIABLE ---------------

    #region --------------- FrmWareHouse ---------------

    public FrmWareHouse(bool IsZoom)
    {
        InitializeComponent();
        _ActionTag = string.Empty;
        _IsZoom = IsZoom;
        clsFormControl = new ClsMasterForm(this, BtnExit);
    }

    private void FrmWareHouse_Load(object sender, EventArgs e)
    {
        ClearControl();
        EnableControl();
        BtnNew.Focus();
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmWareHouse_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (BtnNew.Enabled is false || BtnEdit.Enabled is false || BtnDelete.Enabled is false)
            {
                if (MessageBox.Show(@"ARE YOU SURE WANT TO CLEAR FORM..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _ActionTag = string.Empty;
                    ClearControl();
                    EnableControl();
                    Text = "WAREHOUSE SETUP";
                    BtnNew.Focus();
                }
            }
            else
            {
                BtnExit.PerformClick();
            }
        }
    }

    #endregion --------------- FrmWareHouse ---------------

    #region --------------- BUTTON ---------------

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _ActionTag = "SAVE";
        ClearControl();
        EnableControl(true, false);
        TxtDescription.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _ActionTag = "UPDATE";
        ClearControl();
        EnableControl(true, false);
        TxtDescription.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _ActionTag = "DELETE";
        ClearControl();
        EnableControl(false, false);
        TxtDescription.Focus();
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (!FormValid())
        {
            TxtDescription.Focus();
            return;
        }

        if (I_U_D_WareHouse() > 0)
        {
            MessageBox.Show($@"DATA {_ActionTag.ToUpper()}  SUCCESSFULLY..!!", ObjGlobal.Caption,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearControl();
            TxtDescription.Focus();
        }
        else
        {
            MessageBox.Show($@"DATA {_ActionTag.ToUpper()}  UNSUCCESSFULLY..!!", ObjGlobal.Caption,
                MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtDescription.Text.Trim())) BtnExit.PerformClick();
        else ClearControl();
    }

    #endregion --------------- BUTTON ---------------

    #region --------------- METHOD ---------------

    private void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_ActionTag)
            ? $"WAREHOUSE DETAILS SETUP {_ActionTag}"
            : "WAREHOUSE DETAILS SETUP";
        IList list = StorePanel.Controls;
        for (var i = 0; i < list.Count; i++)
        {
            using var control = (Control)list[i];
            if (control is TextBox)
            {
                control.Text = string.Empty;
                control.BackColor = SystemColors.Window;
            }
        }

        ChkActive.Checked = true;
        TxtDescription.ReadOnly =
            !string.IsNullOrEmpty(_ActionTag) && _ActionTag.ToUpper() != "SAVE" ? true : false;
    }

    private void EnableControl(bool Txt = false, bool Btn = true)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = Btn;
        TxtDescription.Enabled = BtnDescription.Enabled =
            !string.IsNullOrEmpty(_ActionTag) && _ActionTag != "SAVE" ? true : Txt;
        TxtShortName.Enabled = TxtLocation.Enabled = Txt;
        ChkActive.Enabled = !string.IsNullOrEmpty(_ActionTag) && _ActionTag == "UPDATE" ? true : false;
        BtnSave.Enabled =
            BtnCancel.Enabled = !string.IsNullOrEmpty(_ActionTag) && _ActionTag != "SAVE" ? true : Txt;
    }

    private bool FormValid()
    {
        return true;
    }

    private int I_U_D_WareHouse()
    {
        return 1;
    }

    #endregion --------------- METHOD ---------------
}