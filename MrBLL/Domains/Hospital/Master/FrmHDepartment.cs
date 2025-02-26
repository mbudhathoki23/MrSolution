using DevExpress.XtraEditors;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MrBLL.Domains.Hospital.Master;

public partial class FrmHDepartment : MrForm
{
    // HOSPITAL DEPARTMENT

    #region -------------- From --------------

    public FrmHDepartment(bool zoom)
    {
        InitializeComponent();
        _isZoom = zoom;
        EnableControl();
        ClearControl();
        CmbLevel.SelectedIndex = 0;
    }

    private void FrmDepartment_Load(object sender, EventArgs e)
    {
        BtnNew.Focus();
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void Global_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void FrmDepartment_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (BtnNew.Enabled)
            {
                if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
                {
                    BtnExit.PerformClick();
                }
            }
            else
            {
                if (CustomMessageBox.ClearVoucherDetails("HOSPITAL DEPARTMENT") == DialogResult.Yes)
                {
                    _actionTag = string.Empty;
                    EnableControl();
                    ClearControl();
                    BtnNew.Focus();
                }
            }
        }
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ClearControl();
        EnableControl(true);
        TxtDescription.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        (TxtDescription.Text, DepartmentId) = GetMasterList.GetHospitalDepartmentList(_actionTag);
        TxtDescription.Focus();
    }

    private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void TxtDescription_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
    }

    private void TxtCharge_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
    }

    private void TxtCharge_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e.KeyChar is (char)Keys.Enter ? new KeyPressEventArgs((char)Keys.Enter) : new KeyPressEventArgs(e.KeyChar));
        e.Handled = e.IsDecimal(sender);
    }

    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
    }

    private void TxtShortName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
    }

    private void CmbLevel_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
    }

    private void TxtDoctor_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnDoctorList_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            (TxtDoctor.Text, DoctorId) = GetMasterList.CreateDoctor(true);
            TxtDoctor.Focus();
        }
        else
        {
        }
    }

    private void TxtDoctor_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (!TxtDescription.IsValueExits() || DoctorId != 0) return;
        CustomMessageBox.Warning("DOCTOR SHOULD BE TAG IN DEPARTMENT..!");
        TxtDoctor.Focus();
    }

    private void BtnDoctorList_Click(object sender, EventArgs e)
    {
        (TxtDoctor.Text, DoctorId) = GetMasterList.GetDoctorList(_actionTag);
        TxtDoctor.Focus();
    }

    #endregion -------------- From --------------

    // METHOD FOR THIS FORM

    #region -------------- Method --------------

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable && !_actionTag.Equals("DELETE");
        TxtDescription.Enabled = isEnable;
        TxtShortName.Enabled = isEnable;
        CmbLevel.Enabled = isEnable;
        TxtDoctor.Enabled = isEnable;
        TxtCharge.Enabled = isEnable;
        ChkActive.Enabled = _actionTag.Equals("UPDATE");
        BtnSave.Enabled = isEnable || _actionTag.Equals("DELETE");
        BtnCancel.Enabled = BtnSave.Enabled;
    }

    private void ClearControl()
    {
        Text = _actionTag.IsValueExits() ? $"HOSPITAL DEPARTMENT SETUP [{_actionTag}]" : "HOSPITAL DEPARTMENT SETUP";
        DepartmentId = 0;
        TxtDescription.Clear();
        TxtShortName.Clear();
        TxtCharge.Clear();
        DoctorId = 0;
        TxtDoctor.Clear();
    }

    private void SaveHospitalDepartment()
    {
    }

    #endregion -------------- Method --------------

    // OBJECT FOR THIS CLASS

    #region -------------- OBJECT --------------

    private string _actionTag = string.Empty;
    public string DepartmentName { get; set; }
    public int DepartmentId { get; set; }
    private int DoctorId { get; set; }
    private bool _isZoom = false;

    #endregion -------------- OBJECT --------------
}