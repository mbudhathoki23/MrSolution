using MrBLL.Utility.Common.Class;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.Domains.Hospital.Master;

public partial class FrmDoctorType : MrForm
{
    // DOCTOR TYPE SETUP

    #region --------------- Bed Type ---------------

    public FrmDoctorType(bool isZoom)
    {
        InitializeComponent();
        ControlEnable();
        ClearControl();
        _master = new ClsHMaster();
        _isZoom = isZoom;
    }

    private void FrmDoctorType_Load(object sender, EventArgs e)
    {
        BtnNew.Focus();
    }

    private void FrmDoctorType_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (BtnNew.Enabled)
            {
                BtnExit.PerformClick();
            }
        }
        else if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{Tab}");
        }
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ControlEnable(true);
        ClearControl();
        TxtDescription.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        ControlEnable(true);
        ClearControl();
        TxtDescription.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ControlEnable();
        ClearControl();
        TxtDescription.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (BtnNew.Enabled)
        {
            if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
            {
                Close();
                return;
            }
        }
        else
        {
            if (CustomMessageBox.ClearVoucherDetails("DOCTOR TYPE") == DialogResult.Yes)
            {
                ClearControl();
                ControlEnable();
                BtnNew.Focus();
            }
        }
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        GetHospitalMaster.GetDoctorTypeList("VIEW");
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (IsFormValid())
        {
            if (SaveDoctorType() != 0)
            {
                if (_isZoom)
                {
                    Close();
                    return;
                }
                CustomMessageBox.ActionSuccess(TxtDescription.GetUpper(), "DOCTOR TYPE", _actionTag);
                ClearControl();
                TxtDescription.Focus();
            }
            else
            {
                TxtDescription.WarningMessage("ERROR WHILE INFORMATION PLEASE ENTER VALID INPUT..!!");
                return;
            }
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        BtnExit.PerformClick();
    }

    private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnDescription_Click(sender, e);
        }
        else if (TxtDescription.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtDescription, BtnDescription);
        }
    }

    private void TxtDescription_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (TxtDescription.IsBlankOrEmpty())
        {
            if (_actionTag.IsValueExits() && TxtDescription.ValidControl(ActiveControl))
            {
                TxtDescription.WarningMessage($"DESCRIPTION IS REQUIRED FOR {_actionTag}");
                return;
            }
        }
        else
        {
            if (_actionTag.Equals("SAVE"))
            {
                TxtShortName.Text = TxtDescription.GenerateShortName("DOCTORTYPE");
            }

            var exits = TxtDescription.CheckValueExits(_actionTag, "DOCTORTYPE", "DrTypeDesc", DoctorTypeId);
            if (exits.RowsCount() > 0)
            {
                TxtDescription.WarningMessage("DESCRIPTION IS ALREADY EXITS..!!");
                return;
            }
        }
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var result = GetHospitalMaster.GetDoctorTypeList(_actionTag);
        if (result.id > 0)
        {
            DoctorTypeId = result.id;
            SetGridDataToText();
        }
        TxtDescription.Focus();
    }

    private void TxtShortName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (TxtShortName.IsBlankOrEmpty())
        {
            if (_actionTag.IsValueExits() && TxtShortName.ValidControl(ActiveControl))
            {
                TxtShortName.WarningMessage($"DESCRIPTION SHORT NAME IS REQUIRED FOR {_actionTag}");
                return;
            }
        }
        else
        {
            var exits = TxtShortName.CheckValueExits(_actionTag, "DOCTORTYPE", "DrTypeShortName", DoctorTypeId);
            if (exits.RowsCount() > 0)
            {
                TxtShortName.WarningMessage("DESCRIPTION SHORT NAME IS ALREADY EXITS..!!");
                return;
            }
        }
    }

    #endregion --------------- Bed Type ---------------

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------

    private void ControlEnable(bool isEnable = false)
    {
        BtnNew.Enabled = !isEnable;
        BtnEdit.Enabled = BtnDelete.Enabled = BtnNew.Enabled;
        BtnDelete.Visible = !ObjGlobal.IsOnlineSync;

        TxtDescription.Enabled = isEnable || _actionTag.Equals("DELETE");
        BtnDescription.Enabled = TxtDescription.Enabled;

        TxtShortName.Enabled = isEnable;
        ChkActive.Enabled = _actionTag.Equals("UPDATE");

        BtnSave.Enabled = TxtDescription.Enabled;
        BtnCancel.Enabled = BtnSave.Enabled;
    }

    private void ClearControl()
    {
        Text = _actionTag.IsBlankOrEmpty()
            ? "DOCTOR TYPE SETUP"
            : $"DOCTOR TYPE SETUP [{_actionTag}]";
        TxtDescription.Clear();
        TxtShortName.Clear();
        ChkActive.Checked = true;
        TxtDescription.ReadOnly = !_actionTag.Equals("SAVE");
    }

    private int SaveDoctorType()
    {
        var syncRow = 0;
        DoctorTypeId = _actionTag.Equals("SAVE") ? DoctorTypeId.ReturnMaxIntId("DOCTORTYPE") : DoctorTypeId;
        _master.VmDoctorType.ActionTag = _actionTag;
        _master.VmDoctorType.DtID = DoctorTypeId;
        _master.VmDoctorType.NepaliDesc = TxtDescription.Text;
        _master.VmDoctorType.DrTypeDesc = TxtDescription.Text;
        _master.VmDoctorType.DrTypeShortName = TxtShortName.Text;
        _master.VmDoctorType.Status = ChkActive.Checked;
        syncRow = syncRow.ReturnSyncRowNo("DOCTORTYPE", DoctorTypeId);
        _master.VmDoctorType.SyncRowVersion = (short)syncRow;
        var result = _master.SaveDoctorType();
        return result;
    }

    private void SetGridDataToText()
    {
        var result = _master.GetDoctorTypeInformation(DoctorTypeId);
        if (result.HasErrors || result.RowsCount() == 0)
        {
            return;
        }

        foreach (DataRow row in result.Rows)
        {
            TxtDescription.Text = row["DrTypeDesc"].ToString();
            TxtShortName.Text = row["DrTypeShortName"].ToString();
            ChkActive.Checked = row["Status"].GetBool();
        }
    }

    private bool IsFormValid()
    {
        if (_actionTag.Equals("DELETE"))
        {
            if (DoctorTypeId is 0 || TxtDescription.IsBlankOrEmpty())
            {
                TxtDescription.WarningMessage("SELECTED DOCTOR TYPE IS INVALID..!!");
                return false;
            }
        }
        else
        {
            if (_actionTag.Equals("UPDATE"))
            {
                if (DoctorTypeId is 0 || TxtDescription.IsBlankOrEmpty())
                {
                    TxtDescription.WarningMessage("SELECTED DOCTOR TYPE IS INVALID..!!");
                    return false;
                }
            }

            if (TxtDescription.IsBlankOrEmpty())
            {
                TxtDescription.WarningMessage("DESCRIPTION IS REQUIRED..!!");
                return false;
            }

            if (TxtShortName.IsBlankOrEmpty())
            {
                TxtShortName.WarningMessage("SHORT NAME IS REQUIRED..!!");
                return false;
            }
        }

        return true;
    }

    #endregion --------------- Method ---------------

    // OBJECT FOR THIS FORM
    private string _actionTag = "";

    private bool _isZoom = false;
    public string DoctorTypeDesc { get; set; }
    public int DoctorTypeId { get; set; }
    private IHMaster _master;
}