using DevExpress.XtraEditors;
using MrDAL.Control.ControlsEx;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master.Interface.LedgerSetup;
using MrDAL.Master.LedgerSetup;
using MrDAL.Utility.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Master.LedgerSetup;

public partial class FrmClass : MrForm
{
    private void TxtDescription_TextChanged(object sender, EventArgs e)
    {
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        BtnDescription_Click(sender, e);
    }

    #region --------------- Class ---------------

    private int DepartmentId;

    private readonly bool _IsZoom;

    public string DepartmentDesc = string.Empty;
    private string Query = string.Empty;
    private string _ActionTag = string.Empty;
    private string _SearchKey = string.Empty;

    private ObjGlobal Gobj = new();
    private readonly DataTable dt = new();

    private readonly IClassRepository _groupSetup = new ClassRepository();
    private ClsMasterForm clsFormControl;

    #endregion --------------- Class ---------------

    #region --------------- Form ---------------

    public FrmClass(bool IsZoom = false)
    {
        InitializeComponent();
        clsFormControl = new ClsMasterForm(this, BtnExit);
        _IsZoom = IsZoom;
        _groupSetup = new ClassRepository();
    }

    private void FrmClass_Load(object sender, EventArgs e)
    {
        ClearControl();
        EnableControl();
        CmbSection.SelectedIndex = 0;
        BtnNew.Focus();
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void FrmClass_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (BtnNew.Enabled == false || BtnEdit.Enabled == false || BtnDelete.Enabled == false)
            {
                if (MessageBox.Show(@"ARE YOU SURE WANT TO CLEAR FORM..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _ActionTag = string.Empty;
                    Text = "COUNTER SETUP";
                    ClearControl();
                    EnableControl();
                    BtnNew.Focus();
                }
            }
            else
            {
                if (MessageBox.Show(@"ARE YOU SURE WANT TO CLOSE FORM..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo) == DialogResult.Yes) Close();
            }
        }
    }

    #endregion --------------- Form ---------------

    #region --------------- Botton ---------------

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

    private bool IsValidForm()
    {
        if (_ActionTag.IsBlankOrEmpty())
        {
            return false;
        }

        if (TxtDescription.IsBlankOrEmpty())
        {
            TxtDescription.WarningMessage(@"DESCRIPTION IS REQUIRED..!!");
            return false;
        }

        if (CmbSection.IsBlankOrEmpty())
        {
            CmbSection.WarningMessage(@"SECTION IS REQUIRED..!");
            return false;
        }

        if (_ActionTag != "SAVE") ;
        {
            return false;
        }
        return true;
    }
    //if (string.IsNullOrEmpty(TxtDescription.Text.Trim()))
    //{
    //    MessageBox.Show(@"DESCRIPTION IS REQUIRED..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
    //        MessageBoxIcon.Warning);
    //    TxtDescription.Focus();
    //    return false;
    //}

    //if (string.IsNullOrEmpty(CmbSection.Text.Trim()))
    //{
    //    MessageBox.Show(@"SECTION IS REQUIRED..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
    //        MessageBoxIcon.Warning);
    //    CmbSection.Focus();
    //    return false;
    //}



    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (!IsValidForm()) return;
        if (IUDDepartmentDetails() > 0)
        {
            if (_IsZoom)
            {
                DepartmentDesc = TxtDescription.Text;
                Close();
            }

            MessageBox.Show($@"DATA {_ActionTag} SUCCESSFULLY..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            ClearControl();
            TxtDescription.Focus();
        }
        else
        {
            MessageBox.Show($@"DATA {_ActionTag} UNSUCCESSFULLY..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show(@"DO YOU WANT TO CLOSE THE FORM..??", ObjGlobal.Caption, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            Close();
        else
            ClearControl();
    }

    #endregion --------------- Botton ---------------

    #region --------------- Event ---------------

    private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1) BtnDescription_Click(sender, e);
        if (e.KeyCode is Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtDescription.Text.Trim()) && TxtDescription.Focused is true)
            {
                MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtDescription.Focus();
            }
        }
        else
        {
            if (TxtDescription.ReadOnly is true && !string.IsNullOrEmpty(_ActionTag))
                ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                    e.KeyCode.ToString(), TxtDescription, BtnDescription);
        }
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
        if (ActiveControl is null)
            return;
        if (string.IsNullOrEmpty(TxtDescription.Text.Trim()) && TxtDescription.Focused is true &&
            TxtDescription.Enabled is true)
        {
            MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
        }
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        using var dt = _groupSetup.CheckIsValidData(_ActionTag, "DepartmentName", "SectionName", "DepartmentId",
            TxtDescription.Text, DepartmentId.ToString());

        if (dt.Rows.Count <= 0) return;
        MessageBox.Show(@"DESCRIPTION IS ALREADY EXISTS..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
        TxtDescription.Focus();
        TxtDescription.SelectAll();
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MIN", "ACCOUNTGROUP", ObjGlobal.SearchText, _ActionTag,
            string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtDescription.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                DepartmentId = ObjGlobal.ReturnInt(frmPickList.SelectedList[0]["Id"].ToString().Trim());
                if (_ActionTag != "SAVE")
                {
                    TxtDescription.ReadOnly = false;
                    SetGridDataToText(DepartmentId);
                }
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"DEPARTMENT NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtDescription.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtDescription.Focus();
    }

    private void CmbSection_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape) SendKeys.Send("{F4}");
    }

    #endregion --------------- Event ---------------

    #region --------------- Method ---------------

    private void EnableControl(bool Txt = false, bool Btn = true)
    {
        #region "Details"

        TxtDescription.Enabled = BtnSave.Enabled =
            BtnCancel.Enabled = !string.IsNullOrEmpty(_ActionTag) && _ActionTag != "SAVE" ? true : Txt;
        CmbSection.Enabled = Txt;
        ChkActive.Enabled = !string.IsNullOrEmpty(_ActionTag) && _ActionTag != "UPDATE" ? true : false;

        #endregion "Details"

        #region "Buttons"

        BtnNew.Enabled = Btn;
        BtnEdit.Enabled = Btn;
        BtnDelete.Enabled = Btn;

        #endregion "Buttons"
    }

    private void ClearControl()
    {
        Text = $"Account Group Details {_ActionTag} ".Trim();
        IList list = StorePanel.Controls;
        for (var i = 0; i < list.Count; i++)
        {
            var control = (Control)list[i];
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

    private void SetGridDataToText(int DId)
    {
        if (GetConnection.SelectDataTableQuery("Select  * from AMS.ClassSetup	where CID='" + DId + "'").Rows.Count >
            0)
        {
            TxtDescription.Text = dt.Rows[0]["CName"].ToString();
            CmbSection.Text = dt.Rows[0]["CbSection"].ToString();
            if (_ActionTag == "UPDATE")
                TxtDescription.Focus();
            else
                BtnSave.Focus();
        }
    }

    private int IUDDepartmentDetails()
    {
        //_groupSetup._groupSetup.MasterId = DepartmentId;
        //_groupSetup._groupSetup._ActionTag = _ActionTag;
        //_groupSetup._groupSetup.TxtDescription = TxtDescription.Text.Trim();
        //_groupSetup._groupSetup.TxtType = CmbSection.Text.Trim();
        //_groupSetup._groupSetup.TxtBranchId = Convert.ToInt32(ObjGlobal.SysBranchId);
        //int.TryParse(ObjGlobal.SysCompanyUnitId.ToString(), out var _CompanyUnitId);
        //_groupSetup._groupSetup.TxtCompanyUnitId = _CompanyUnitId;
        //_groupSetup._groupSetup.ChkActive = ChkActive.Checked;
        //_groupSetup._groupSetup.TxtEnterBy = ObjGlobal.LogInUser;
        //return _groupSetup.SaveClassSetup(_ActionTag);
        return 0;
    }

    #endregion --------------- Method ---------------
}