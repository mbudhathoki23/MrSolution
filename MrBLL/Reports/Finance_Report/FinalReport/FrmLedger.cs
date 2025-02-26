using MrBLL.Utility.Common;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Utility.PickList;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MrBLL.Reports.Finance_Report.FinalReport;

public partial class FrmLedger : MrForm
{
    // LEDGER REPORT FORM

    #region --------------- LEDGER ---------------

    public FrmLedger(string ledgerType)
    {
        InitializeComponent();
        _ledgerType = ledgerType;
        ObjGlobal.BindPeriodicDate(MskFrom, MskToDate);
        ObjGlobal.BindDateType(CmbDateType);
        ChkDate.Text = ObjGlobal.SysDateType is "M" ? "Miti" : "Date";
        CmbDateType.SelectedIndex = 8;
    }

    private void FrmLedger_Load(object sender, EventArgs e)
    {
        switch (_ledgerType.ToUpper())
        {
            case "CUSTOMER":
            {
                RbtnCustomer.Checked = true;
                GrpLedgerType.Enabled = false;
                break;
            }
            case "VENDOR":
            {
                RbtnVendor.Checked = true;
                GrpLedgerType.Enabled = false;
                break;
            }
        }
        rChkLedgerWise_CheckedChanged(sender, e);
        rChkSummary_CheckedChanged(sender, e);
        rChkSubledgerWise_CheckedChanged(sender, e);
        rChkSummary.Focus();
    }

    private void FrmLedger_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
        else if (e.KeyChar is (char)Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() is DialogResult.Yes)
            {
                Close();
            }
        }
    }

    private void FrmLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyData is Keys.F4)
        {
            var result = GetMasterList.GetTagMasterList("BRANCH");
            if (result.IsValueExits())
            {
                _branchId = result;
            }
        }
        else if (e.KeyData is Keys.F6)
        {
            var result = GetMasterList.GetTagMasterList("COMPANYUNIT");
            if (result.IsValueExits())
            {
                _companyUnitId = result;
            }
        }
        else if (e.KeyData is Keys.F7)
        {
            var result = GetMasterList.GetTagMasterList("FISCALYEAR");
            if (result.IsValueExits())
            {
                _fiscalYearId = result;
            }
        }
    }

    private void BtnShow_Click(object sender, EventArgs e)
    {
        if (!IsValidForm())
        {
            return;
        }
        _rptType = "Normal";
        _fromAdDate = ObjGlobal.SysDateType is "M" ? ObjGlobal.ReturnEnglishDate(MskFrom.Text) : MskFrom.Text;
        _fromBsDate = ObjGlobal.SysDateType is "M" ? MskFrom.Text : ObjGlobal.ReturnNepaliDate(MskFrom.Text);
        _toAdDate = ObjGlobal.SysDateType is "M" ? ObjGlobal.ReturnEnglishDate(MskToDate.Text) : MskToDate.Text;
        _toBsDate = ObjGlobal.SysDateType is "M" ? MskToDate.Text : ObjGlobal.ReturnNepaliDate(MskToDate.Text);

        _rptDate = ChkDate.Checked && ChkDate.Text is @"Miti"
            ? $"From Date {_fromBsDate} To {_toBsDate}"
            : $"From Date {_fromAdDate.GetDateString()} To {_toAdDate.GetDateString()}";
        if (!ChkSelectAll.Checked)
        {
            if (!GetSelectedIdValue())
            {
                return;
            }
        }

        var reportOption = GetReportOption();
        var getDisplay = new DisplayFinanceReports
        {
            Text = _rptName + @" REPORT",
            RptType = _rptType,
            RptName = "LEDGER",
            RptDate = _rptDate,

            FromAdDate = _fromAdDate,
            ToAdDate = _toAdDate,

            IsDetails = rChkDetails.Checked,
            CurrencyId = _currencyId,

            IsSubLedger = ChkIncludeSubledger.Checked,
            IsPostingDetails = ChkPostingDetails.Checked,
            IsProductDetails = ChkProductDetails.Checked,
            IncludeUdf = ChkIncludeUDF.Checked,

            IsDnCnDetails = ChkDNCNDetails.Checked,
            IncludeNarration = ChkIncludeNarration.Checked,
            IncludeRemarks = ChkIncludeRemarks.Checked,

            IsDate = ChkDate.Checked,
            GroupId = _accountGroupId,
            SubGroupId = _accountSubGroupId,

            LedgerId = _ledgerId,
            SubLedgerId = _subledgerId,
            BranchId = _branchId,
            AgentId = _agentId,

            ReportOption = reportOption,
            IncludeRefVno = ChkRefVno.Checked,
            IncludePdc = ChkIncludePDC.Checked,
            IsZeroBalance = ChkIncludeAdjustment.Checked && rChkSummary.Checked,
            IncludeAdjustment = ChkIncludeAdjustment.Checked && rChkDetails.Checked,
            CurrencyType = RbtnLocal.Checked ? "Local" :
                RbtnCurrencyBoth.Checked ? "Both" :
                RbtnForeign.Checked ? "Foreign" : "Local",

            AccountType = RbtnCustomer.Checked ? "Customer" :
                RbtnVendor.Checked ? "Vendor" :
                RbtnCash.Checked ? "Cash" :
                RbtnBank.Checked ? "Bank" :
                RbtnAll.Checked ? "All" : "Other"
        };
        getDisplay.Show();

        _accountGroupId = string.Empty;
        _accountSubGroupId = string.Empty;
        _agentId = string.Empty;
        _ledgerId = string.Empty;
        _ledgerType = string.Empty;
        _subledgerId = string.Empty;
        _departmentId = string.Empty;
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() is DialogResult.Yes)
        {
            Close();
        }
    }

    private void CmbDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", CmbDateType.SelectedIndex, MskFrom, MskToDate);
    }

    private void MskFrom_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskFrom, 'E');
    }

    private void MskFrom_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskFrom, 'L');
    }

    private void MskToDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskToDate, 'E');
    }

    private void MskToDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskToDate, 'L');
    }

    private void MskFrom_Validating(object sender, CancelEventArgs e)
    {
        if (MskFrom.MaskCompleted)
        {
            if (MskFrom.Text.IsDateExits(ObjGlobal.SysDateType))
            {
                if (!MskFrom.Text.IsValidDateRange(ObjGlobal.SysDateType))
                    this.NotifyValidationError(MskFrom, "INVALID DATE..!!");
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

    private void MskToDate_Validating(object sender, CancelEventArgs e)
    {
        if (MskToDate.MaskCompleted)
        {
            if (MskToDate.Text.IsDateExits(ObjGlobal.SysDateType))
            {
                if (!MskToDate.Text.IsValidDateRange(ObjGlobal.SysDateType))
                    this.NotifyValidationError(MskToDate, "INVALID DATE..!!");
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

    private void ChkRefVno_CheckStateChanged(object sender, EventArgs e)
    {
        if (ChkRefVno.Checked) _rptName = "Ledger - Details With RefVno";
    }

    private void ChkIncludeSubledger_CheckedChanged(object sender, EventArgs e)
    {
        ChkAllSubledger.Enabled = ChkIncludeSubledger.Checked;
        ChkAllSubledger.Checked = ChkAllSubledger.Enabled && ChkAllSubledger.Checked;
    }

    private void rChkSummary_CheckedChanged(object sender, EventArgs e)
    {
        ChkPostingDetails.Enabled = rChkDetails.Checked;

        ChkProductDetails.Enabled = rChkDetails.Checked;
        ChkProductDetails.Checked = ChkProductDetails.Enabled && ChkProductDetails.Checked;

        ChkIncludeUDF.Enabled = rChkDetails.Checked;
        ChkIncludeUDF.Checked = ChkIncludeUDF.Enabled && ChkIncludeUDF.Checked;

        ChkDNCNDetails.Enabled = rChkDetails.Checked;
        ChkDNCNDetails.Checked = ChkDNCNDetails.Enabled && ChkDNCNDetails.Checked;

        ChkLedgerPanVat.Enabled = !rChkDetails.Checked;
        ChkLedgerPanVat.Checked = ChkLedgerPanVat.Enabled && ChkLedgerPanVat.Checked;

        ChkIncludeNarration.Enabled = rChkDetails.Checked;
        ChkIncludeNarration.Checked = ChkIncludeNarration.Enabled && ChkIncludeNarration.Checked;

        ChkIncludeAdjustment.Text = rChkDetails.Checked ? @"Include Adjustment" : @"Zero Balance";
    }

    private void rChkLedgerWise_CheckedChanged(object sender, EventArgs e)
    {
        if (rChkLedgerWise.Checked)
        {
            ChkIncludeLedger.Enabled = false;
            ChkIncludeLedger.Checked = false;
        }
        else
        {
            ChkIncludeLedger.Enabled = true;
            ChkIncludeLedger.Checked = true;
        }
    }

    private void rChkSubledgerWise_CheckedChanged(object sender, EventArgs e)
    {
        ChkIncludeSubledger.Enabled = !rChkSubledgerWise.Checked;
        ChkIncludeSubledger.Checked = ChkIncludeSubledger.Enabled && ChkIncludeSubledger.Checked;
    }

    #endregion --------------- LEDGER ---------------

    //METHOD FOR THIS FORM

    #region --------------- METHOD ---------------

    private bool IsValidForm()
    {
        if (MskFrom.Text.Trim() == "/  /" || MskToDate.Text.Trim() == "/  /")
        {
            MessageBox.Show(@"DATE CAN'T BE BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            MskFrom.Focus();
            return false;
        }

        if (!MskToDate.MaskCompleted || !MskFrom.MaskCompleted)
        {
            MessageBox.Show(@"DATE CAN'T BE BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            MskToDate.Focus();
            return false;
        }

        return true;
    }

    private bool GetLedgerId()
    {
        var frm2 = new FrmTagList
        {
            ReportDesc = @"GENERALLEDGER",
            Category = RbtnCustomer.Checked ? "Customer" :
                RbtnVendor.Checked ? "Vendor" :
                RbtnCash.Checked ? "Cash" :
                RbtnBank.Checked ? "Bank" :
                RbtnAll.Checked ? "All" : "Other",
            BranchId = _branchId,
            CompanyUnitId = _companyUnitId,
            FiscalYearId = _fiscalYearId,
            GroupId = _accountGroupId,
            SubGroupId = _accountSubGroupId
        };
        frm2.ShowDialog();

        _ledgerId = ClsTagList.PlValue1;
        if (!string.IsNullOrEmpty(_ledgerId)) return true;
        MessageBox.Show(@"LEDGER NOT SELECTED..!!", ObjGlobal.Caption);
        return false;
    }

    private bool GetAccountGroupId()
    {
        using (var frm2 = new FrmTagList
               {
                   ReportDesc = "ACCOUNT_GROUP",
                   BranchId = _branchId,
                   CompanyUnitId = _companyUnitId,
                   FiscalYearId = _fiscalYearId
               })
        {
            frm2.ShowDialog();
        }

        _accountGroupId = ClsTagList.PlValue1;
        if (!string.IsNullOrEmpty(_accountGroupId)) return true;
        MessageBox.Show(@"ACCOUNT GROUP NOT SELECTED..!!", ObjGlobal.Caption);
        return false;
    }

    private bool GetAccountSubGroupId()
    {
        var frm2 = new FrmTagList
        {
            ReportDesc = "ACCOUNTSUBGROUP",
            BranchId = _branchId,
            CompanyUnitId = _companyUnitId,
            FiscalYearId = _fiscalYearId,
            GroupId = _accountGroupId
        };
        frm2.ShowDialog();

        _accountSubGroupId = ClsTagList.PlValue1;
        if (!string.IsNullOrEmpty(_accountSubGroupId)) return true;
        MessageBox.Show(@"ACCOUNT SUBGROUP NOT SELECTED..!!", ObjGlobal.Caption);
        return false;
    }

    private bool GetSubLedgerId()
    {
        var frm2 = new FrmTagList
        {
            ReportDesc = @"SUBLEDGER",
            BranchId = _branchId,
            CompanyUnitId = _companyUnitId,
            FiscalYearId = _fiscalYearId,
            GroupId = _accountGroupId,
            SubGroupId = _accountSubGroupId,
            LedgerId = _ledgerId
        };
        frm2.ShowDialog();
        _subledgerId = ClsTagList.PlValue1;
        if (!string.IsNullOrEmpty(_subledgerId)) return true;
        MessageBox.Show(@"SUB-LEDGER NOT SELECTED..!!", ObjGlobal.Caption);
        return false;
    }

    private bool GetDepartmentId()
    {
        var frm2 = new FrmTagList
        {
            ReportDesc = @"DEPARTMENT",
            BranchId = _branchId,
            CompanyUnitId = _companyUnitId,
            FiscalYearId = _fiscalYearId
        };
        frm2.ShowDialog();
        _subledgerId = ClsTagList.PlValue1;
        if (!string.IsNullOrEmpty(_subledgerId)) return true;
        MessageBox.Show(@"SUB-LEDGER NOT SELECTED..!!", ObjGlobal.Caption);
        return false;
    }

    private bool GetAgentId()
    {
        var frm2 = new FrmTagList
        {
            ReportDesc = @"AGENT",
            BranchId = _branchId,
            CompanyUnitId = _companyUnitId,
            FiscalYearId = _fiscalYearId
        };
        frm2.ShowDialog();
        _subledgerId = ClsTagList.PlValue1;
        if (!string.IsNullOrEmpty(_subledgerId)) return true;
        MessageBox.Show(@"SUB-LEDGER NOT SELECTED..!!", ObjGlobal.Caption);
        return false;
    }

    private bool GetDocAgentId()
    {
        var frm2 = new FrmTagList
        {
            ReportDesc = @"DOCAGENT",
            BranchId = _branchId,
            CompanyUnitId = _companyUnitId,
            FiscalYearId = _fiscalYearId
        };
        frm2.ShowDialog();
        _subledgerId = ClsTagList.PlValue1;
        if (!string.IsNullOrEmpty(_subledgerId)) return true;
        MessageBox.Show(@"SUB-LEDGER NOT SELECTED..!!", ObjGlobal.Caption);
        return false;
    }

    private bool GetSelectedIdValue()
    {
        if (rChkAccountGroupWise.Checked)
        {
            if (!GetAccountGroupId()) return false;
            if (!GetLedgerId()) return false;
        }
        else if (rChkAccountSubGroupWise.Checked)
        {
            if (!GetAccountSubGroupId()) return false;
            if (!GetLedgerId()) return false;
        }
        else if (rChkLedgerWise.Checked)
        {
            if (!GetLedgerId()) return false;
            if (!ChkAllSubledger.Checked && ChkIncludeSubledger.Checked && !GetSubLedgerId()) return false;
        }
        else if (rChkSubledgerWise.Checked)
        {
            if (!GetSubLedgerId()) return false;
            if (!GetLedgerId()) return false;
        }
        else if (rChkDepartmentWise.Checked)
        {
            if (!GetDepartmentId()) return false;
            if (!GetLedgerId()) return false;
        }
        else if (rChkAgentWise.Checked)
        {
            if (!ChkDocAgents.Checked)
            {
                if (!GetAgentId()) return false;
            }
            else
            {
                if (!GetDocAgentId()) return false;
            }

            if (!GetLedgerId()) return false;
        }

        return true;
    }

    private string GetReportOption()
    {
        var reportOption = string.Empty;
        if (rChkLedgerWise.Checked)
            reportOption = ChkIncludeSubledger.Checked ? "Ledger/SubLedger" : "Ledger";
        else if (rChkAccountGroupWise.Checked)
            reportOption = ChkIncludeLedger.Checked ? "Account Group/Ledger" : "Account Group";
        else if (rChkAccountSubGroupWise.Checked)
            reportOption = ChkIncludeLedger.Checked ? "Account Group/Sub Group/Ledger" : "Account Group/Sub Group";
        else if (rChkSubledgerWise.Checked)
            reportOption = ChkIncludeLedger.Checked ? "SubLedger/Ledger" : "SubLedger";
        else if (rChkDepartmentWise.Checked)
            reportOption = ChkIncludeLedger.Checked ? "Department/Ledger" : "Department";
        else if (rChkAgentWise.Checked)
            reportOption = ChkIncludeLedger.Checked && ChkDocAgents.Checked ? "Doc Agent/Ledger"
                : ChkIncludeLedger.Checked && !ChkDocAgents.Checked ? "Agent/Ledger"
                : !ChkIncludeLedger.Checked && ChkDocAgents.Checked ? "Doc Agent" : "Agent";
        return reportOption;
    }

    #endregion --------------- METHOD ---------------

    // OBJECT FOR THIS CLASS

    #region ---------- OBJECT ----------

    private string _branchId = ObjGlobal.SysBranchId.ToString();
    private string _companyUnitId = ObjGlobal.SysCompanyUnitId.ToString();
    private string _currencyId = string.Empty;
    private string _fiscalYearId = ObjGlobal.SysFiscalYearId.ToString();

    private string _accountGroupId = string.Empty;
    private string _accountSubGroupId = string.Empty;
    private string _agentId = string.Empty;
    private string _areaId = string.Empty;
    private string _ledgerId = string.Empty;
    private string _subledgerId = string.Empty;
    private string _departmentId = string.Empty;
    private string _user = string.Empty;

    private string _fromAdDate = string.Empty;
    private string _fromBsDate = string.Empty;
    private string _rptDate = string.Empty;
    private string _rptName = string.Empty;
    private string _rptType = string.Empty;
    private string _ledgerType = string.Empty;
    private string _toAdDate = string.Empty;
    private string _toBsDate = string.Empty;

    #endregion ---------- OBJECT ----------
}