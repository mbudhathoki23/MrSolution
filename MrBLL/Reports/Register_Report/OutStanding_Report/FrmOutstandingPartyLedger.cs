using MrBLL.Utility.Common;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Utility.PickList;
using System;
using System.Windows.Forms;

namespace MrBLL.Reports.Register_Report.OutStanding_Report;

public partial class FrmOutstandingPartyLedger : MrForm
{
    //OUTSTANDING PARTY LEDGER

    #region -------------- OUTSTANDING PARTY LEDGER --------------

    public FrmOutstandingPartyLedger(string getFormMode)
    {
        InitializeComponent();
        _getFormMode = getFormMode;
        ObjGlobal.BindDateType(CmbDateType, 9);
        ObjGlobal.BindPeriodicDate(MskFrom, MskToDate);
        CmbDateType.SelectedIndex = 9;
        CmbDateType.Enabled = false;
    }

    private void OutstandingPartyLedger_Load(object sender, EventArgs e)
    {
        ChkDate.Text = ObjGlobal.SysDateType == "M" ? "Miti" : "Date";
        if (CmbDateType.SelectedIndex == 9)
        {
            var dateString = ObjGlobal.SysDateType.Equals("M")
                ? DateTime.Now.GetNepaliDate()
                : DateTime.Now.GetDateString();
            MskToDate.Text = dateString;
        }
        if (_getFormMode == "CUSTOMER")
        {
            Text = @"OUTSTANDING CUSTOMER REPORT";
            rChkCustomer.Visible = rChkCustomer.Checked = true;
            rChkVendor.Visible = rChkVendor.Checked = false;
        }
        else if (_getFormMode == "VENDOR")
        {
            Text = @"OUTSTANDING VENDOR REPORT";
            rChkCustomer.Visible = rChkCustomer.Checked = false;
            rChkVendor.Visible = rChkVendor.Checked = true;
            rChkVendor.Location = rChkCustomer.Location;
        }
        CmbDateType.Focus();
    }

    private void OutstandingPartyLedger_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
        if (e.KeyChar == (char)Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
            {
                Close();
            }
        }
    }

    private void MskFrom_Validated(object sender, EventArgs e)
    {
        if (MskFrom.MaskCompleted)
        {
            if (MskFrom.Text.IsDateExits(ObjGlobal.SysDateType))
            {
                if (!MskFrom.Text.IsValidDateRange(ObjGlobal.SysDateType))
                {
                    this.NotifyValidationError(MskFrom, "INVALID DATE..!!");
                }
            }
            else
            {
                this.NotifyValidationError(MskFrom, "INVALID DATE..!!");
            }
        }
        else if (MskFrom.Focused && MskFrom.Enabled)
        {
            this.NotifyValidationError(MskFrom, "INVALID DATE..!!");
        }
    }

    private void MskToDate_Validated(object sender, EventArgs e)
    {
        if (MskToDate.MaskCompleted)
        {
            if (MskToDate.Text.IsDateExits(ObjGlobal.SysDateType))
            {
                if (!MskToDate.Text.IsValidDateRange(ObjGlobal.SysDateType))
                {
                    this.NotifyValidationError(MskToDate, "INVALID DATE..!!");
                }
            }
            else
            {
                this.NotifyValidationError(MskToDate, "INVALID DATE..!!");
            }
        }
        else if (MskToDate.Focused && MskToDate.Enabled)
        {
            this.NotifyValidationError(MskToDate, "INVALID DATE..!!");
        }
    }

    private void BtnShow_Click(object sender, EventArgs e)
    {
        if (!IsValidForm())
        {
            return;
        }
        if (!ChkSelectAll.Checked)
        {
            if (rChkLedger.Checked)
            {
                if (!GetLedgerId())
                {
                    return;
                }
            }
            else if (rChkAgent.Checked)
            {
                if (ChkBillWiseAgent.Checked)
                {
                    if (!GetDocAgentId())
                    {
                        return;
                    }
                }
                else
                {
                    if (!GetAgentId())
                    {
                        return;
                    }
                }
            }
            else if (rChkArea.Checked)
            {
                if (!GetAreaId())
                {
                    return;
                }
            }
        }
        _accountType = rChkCustomer.Checked ? "Customer,Both" : "Vendor,Both";
        var fromAdDate = ObjGlobal.SysDateType == "M" ? ObjGlobal.ReturnEnglishDate(MskFrom.Text) : MskFrom.Text;
        var fromBsDate = ObjGlobal.SysDateType == "M" ? MskFrom.Text : ObjGlobal.ReturnNepaliDate(MskFrom.Text);
        var toAdDate = ObjGlobal.SysDateType == "M" ? ObjGlobal.ReturnEnglishDate(MskToDate.Text) : MskToDate.Text;
        var toBsDate = ObjGlobal.SysDateType == "M" ? MskToDate.Text : ObjGlobal.ReturnNepaliDate(MskToDate.Text);
        var rptStartDate = ObjGlobal.SysDateType == "M" ? fromBsDate : fromAdDate;
        var rptEndDate = ObjGlobal.SysDateType == "M" ? toBsDate : toAdDate;
        var rptDate = ObjGlobal.SysDateType == "M" && ChkDate.Checked == false || ObjGlobal.SysDateType == "D" && ChkDate.Checked
            ? $"As On Date {toBsDate}"
            : $"As On Date {toAdDate.GetDateString()}";
        var display = new DisplayRegisterReports
        {
            Text = Text + @" REPORT",
            RptType = "NORMAL",
            RptName = "OUTSTANDING",
            RptMode = rChkCustomer.Checked ? "CUSTOMER" : "VENDOR",
            RptDate = rptDate,
            FromAdDate = fromAdDate,
            FromBsDate = fromBsDate,
            ToAdDate = toAdDate,
            ToBsDate = toBsDate,
            BranchId = _getBranchId,
            CompanyUnitId = _getCompanyUnitId,
            FiscalYearId = _getFiscalYearId,
            LedgerId = _getLedgerId,
            AgentId = _getAgentId,
            AreaId = _getAreaId,
            IncludePdc = ChkIncludePDC.Checked,
            IncludeAdjustment = ChkIncludeAdjustment.Checked,
            IsCustomer = rChkCustomer.Checked,
            GroupBy = rChkAgent.Checked
                ? "Agent"
                : rChkArea.Checked
                    ? "Area"
                    : "Ledger",
            AccountType = rChkCustomer.Checked
                ? "Customer,Both"
                : "Vendor,Both"
        };
        display.Show();
        _getLedgerId = string.Empty;
        _getAgentId = string.Empty;
        _getAreaId = string.Empty;
    }

    private void CmbDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", CmbDateType.SelectedIndex, MskFrom, MskToDate);
    }

    private void rChkAgent_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkSelectAll.Checked)
        {
            ChkBillWiseAgent.Enabled = true;
        }
        else
        {
            ChkBillWiseAgent.Enabled = false;
            ChkBillWiseAgent.Checked = false;
        }
    }

    #endregion -------------- OUTSTANDING PARTY LEDGER --------------

    // METHOD FOR THIS FORM

    #region -------------- METHOD --------------

    private bool IsValidForm()
    {
        if (!MskFrom.MaskCompleted)
        {
            this.NotifyValidationError(MskFrom, "INVALID DATE..!!");
            return false;
        }

        if (!MskToDate.MaskCompleted)
        {
            this.NotifyValidationError(MskToDate, "INVALID DATE..!!");
            return false;
        }

        return true;
    }

    private bool GetLedgerId()
    {
        using var frm2 = new FrmTagList
        {
            ReportDesc = @"GENERALLEDGER",
            Category = rChkCustomer.Checked ? "Customer" : "Vendor",
            BranchId = _getBranchId,
            CompanyUnitId = _getCompanyUnitId,
            FiscalYearId = _getFiscalYearId
        };
        frm2.ShowDialog(this);
        _getLedgerId = ClsTagList.PlValue1;
        if (_getLedgerId.IsValueExits())
        {
            return true;
        }
        MessageBox.Show(@"LEDGER ARE NOT SELECTED..!!", ObjGlobal.Caption);
        return false;
    }

    private bool GetAgentId()
    {
        var frm2 = new FrmTagList
        {
            ReportDesc = @"AGENT",
            BranchId = _getBranchId,
            CompanyUnitId = _getCompanyUnitId,
            FiscalYearId = _getFiscalYearId
        };
        frm2.ShowDialog();
        _getAgentId = ClsTagList.PlValue1;
        if (_getAgentId.IsValueExits())
        {
            return true;
        }
        MessageBox.Show(@"AGENT ARE NOT SELECTED..!!", ObjGlobal.Caption);
        return false;
    }

    private bool GetAreaId()
    {
        var frm2 = new FrmTagList
        {
            ReportDesc = @"AREA",
            BranchId = _getBranchId,
            CompanyUnitId = _getCompanyUnitId,
            FiscalYearId = _getFiscalYearId
        };
        frm2.ShowDialog();
        _getAreaId = ClsTagList.PlValue1;
        if (_getAreaId.IsValueExits())
        {
            return true;
        }
        MessageBox.Show(@"AREA ARE NOT SELECTED..!!", ObjGlobal.Caption);
        return false;
    }

    private bool GetDocAgentId()
    {
        var frm2 = new FrmTagList
        {
            ReportDesc = @"DOCAGENT",
            BranchId = _getBranchId,
            CompanyUnitId = _getCompanyUnitId,
            FiscalYearId = _getFiscalYearId
        };
        frm2.ShowDialog();
        _getAgentId = ClsTagList.PlValue1;
        if (_getAgentId.IsValueExits())
        {
            return true;
        }
        MessageBox.Show(@"SUB-LEDGER NOT SELECTED..!!", ObjGlobal.Caption);
        return false;
    }

    #endregion -------------- METHOD --------------

    // OBJECT FOR THIS FORM

    #region -------------- OBJECT FOR THIS FORM --------------

    private readonly string _getFormMode;
    private readonly string _getBranchId = ObjGlobal.SysBranchId.ToString();
    private readonly string _getCompanyUnitId = ObjGlobal.CompanyId.ToString();
    private readonly string _getFiscalYearId = ObjGlobal.SysFiscalYearId.ToString();
    private string _getLedgerId = string.Empty;
    private string _getAgentId = string.Empty;
    private string _getAreaId = string.Empty;
    private string _accountType = string.Empty;

    #endregion -------------- OBJECT FOR THIS FORM --------------
}