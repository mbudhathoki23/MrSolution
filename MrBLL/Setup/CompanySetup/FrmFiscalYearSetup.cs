using MrDAL.Core.Extensions;
using MrDAL.Global.Control;
using System;
using System.Windows.Forms;
using MrBLL.Utility.Common.Class;

namespace MrBLL.Setup.CompanySetup;

public partial class FrmFiscalYearSetup : Form
{
    #region || --------------- FISCAL YEAR SETUP FUNCTION --------------- ||
    public FrmFiscalYearSetup()
    {
        InitializeComponent();
        ClearControl();
        EnableControl();
    }

    private void FrmFiscalYearSetup_Load(object sender, EventArgs e)
    {
        BtnNew.Focus();
    }

    private void FrmFiscalYearSetup_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is not (char)Keys.Escape)
        {
            return;
        }

        if (BtnNew.Enabled)
        {
            var question = CustomMessageBox.ExitActiveForm();
            if (question != DialogResult.Yes)
            {
                return;
            }

            Close();
            return;
        }
        _actionTag = string.Empty;
        ClearControl();
        EnableControl();
        BtnNew.Focus();
    }

    private void EnterKeyFunction(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is not Keys.Enter)
        {
            return;
        }

        e.Handled = true;
        e.SuppressKeyPress = true;
        SendKeys.Send("{TAB}");
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ClearControl();
        EnableControl(true);
        MskBsYear.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {

    }

    private void BtnExit_Click(object sender, EventArgs e)
    {

    }

    private void MskBsYear_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnDescription_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (!MskBsYear.MaskCompleted)
            {
                MskBsYear.WarningMessage("PLEASE ENTER THE DESCRIPTION...!!");
                return;
            }
            EnterKeyFunction(sender, e);
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", MskBsYear, BtnDescription);
        }
    }

    private void MskBsYear_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (_actionTag.IsBlankOrEmpty())
        {
            return;
        }

        if (MskBsYear.IsBlankOrEmpty() && MskBsYear.Enabled)
        {
            if (MskBsYear.ValidControl(ActiveControl))
            {
                MskBsYear.WarningMessage("DESCRIPTION IS REQUIRED..!!");
                return;
            }
        }

        if (MskBsYear.IsValueExits())
        {
            var dtCheck = MskBsYear.IsDuplicate("BS_FY", _fiscalYearId, _actionTag, "FISCALYEAR");
            if (dtCheck)
            {
                MskBsYear.WarningMessage("DESCRIPTION IS ALREADY EXITS..!!");
                return;
            }
        }
    }

    private void MskAdYear_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnDescription_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (!MskBsYear.MaskCompleted)
            {
                MskBsYear.WarningMessage("PLEASE ENTER THE DESCRIPTION...!!");
                return;
            }
            EnterKeyFunction(sender, e);
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", MskBsYear, BtnDescription);
        }
    }

    private void MskAdYear_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (_actionTag.IsBlankOrEmpty())
        {
            return;
        }

        if (MskBsYear.IsBlankOrEmpty() && MskBsYear.Enabled)
        {
            if (MskBsYear.ValidControl(ActiveControl))
            {
                MskBsYear.WarningMessage("DESCRIPTION IS REQUIRED..!!");
                return;
            }
        }

        if (MskBsYear.IsValueExits())
        {
            var dtCheck = MskBsYear.IsDuplicate("BS_FY", _fiscalYearId, _actionTag, "FISCALYEAR");
            if (dtCheck)
            {
                MskBsYear.WarningMessage("DESCRIPTION IS ALREADY EXITS..!!");
                return;
            }
        }
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var (_, id) = GetMasterList.GetBranchList(_actionTag);
        if (id > 0)
        {
            _fiscalYearId = id;
            if (!_actionTag.Equals("SAVE"))
            {
                //SetGridDataToText(_branchId);
            }
        }
        MskBsYear.Focus();
    }

    private void MskStartDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {

    }

    private void MskEndDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {

    }

    private void MskStartMiti_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {

    }

    private void MskEndMiti_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {

    }

    private void BtnSync_Click(object sender, EventArgs e)
    {

    }

    private void BtnSave_Click(object sender, EventArgs e)
    {

    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {

    }
    #endregion



    #region || --------------- METHOD FOR THIS FORM --------------- ||

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = !isEnable && !_actionTag.Equals("DELETE");
        BtnEdit.Enabled = BtnNew.Enabled;

    }

    private void ClearControl()
    {
        MskBsYear.Clear();
        MskAdYear.Clear();
        MskStartDate.Clear();
        MskEndDate.Clear();
        MskStartMiti.Clear();
        MskEndDate.Clear();
    }
    #endregion



    #region || --------------- OBJECT FOR THIS FORM --------------- ||

    private string _actionTag = string.Empty;
    private int _fiscalYearId = 0;

    #endregion
}