using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Utility.PickList;
using System;
using System.Windows.Forms;

namespace MrBLL.Reports.Finance_Report.ListOfMaster;

public partial class FrmLedgerList : MrForm
{
    // LEDGER LIST FORM

    #region --------- LEDGER LIST FORM ---------

    public FrmLedgerList()
    {
        InitializeComponent();
    }

    private void FrmLedgerList_Load(object sender, EventArgs e)
    {
        ChkLedger_CheckedChanged(sender, e);
    }

    private void FrmLedgerList_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }

        if (e.KeyChar != (char)Keys.Escape)
        {
            return;
        }
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
    }

    private void BtnShow_Click(object sender, EventArgs e)
    {
        if (!ChkSelectAll.Checked)
        {
            var result = GetSelectedIdValue();
            if (!result)
            {
                return;
            }
        }
        var display = new DisplayFinanceReports()
        {
            Text = @"LIST OF MASTER REPORT",
            RptType = "LIST_OF_MASTER",
            RptName = "LIST_OF_MASTER",
            ReportOption = GetReportOption(),
            GroupId = _accountGroupId,
            SubGroupId = _subledgerId,
            AgentId = _agentId,
            AreaId = _areaId,
            SubLedgerId = _subledgerId,
            IncludeLedger = ChkIncludeLedger.Checked,
            IsLedgerContactDetails = ChkLedgerContactDetails.Checked,
            IsLedgerScheme = ChkLedgerScheme.Checked
        };
        display.Show(this);
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        var dialogResult = CustomMessageBox.ExitActiveForm();
        if (dialogResult == DialogResult.Yes)
        {
            Close();
        }
    }

    private void ChkLedger_CheckedChanged(object sender, EventArgs e)
    {
        ChkIncludeLedger.Enabled = !ChkLedger.Checked;
        if (ChkIncludeLedger.Checked)
        {
            if (ChkLedger.Checked)
            {
                ChkIncludeLedger.Checked = false;
            }
        }
    }

    private void ChkDepartment_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkDepartment.Checked)
        {
            ChkIncludeLedger.Checked = false;
            ChkIncludeLedger.Enabled = false;
        }
    }
    #endregion

    // METHOD FOR THIS FORM
    #region --------- OBJECT FOR THIS FORM ---------
    private string GetReportOption()
    {
        var reportOption = string.Empty;
        if (ChkLedger.Checked)
        {
            reportOption = "Ledger";
        }
        else if (ChkAccountGroup.Checked)
        {
            reportOption = ChkIncludeLedger.Checked ? "Account Group/Ledger" : "Account Group";
        }
        else if (ChkAccountSubGroup.Checked)
        {
            reportOption = ChkIncludeLedger.Checked ? "Account Group/Sub Group/Ledger" : "Account Group/Sub Group";
        }
        else if (ChkSubLedger.Checked)
        {
            reportOption = ChkIncludeLedger.Checked ? "SubLedger/Ledger" : "SubLedger";
        }
        else if (ChkDepartment.Checked)
        {
            reportOption = ChkIncludeLedger.Checked ? "Department/Ledger" : "Department";
        }
        else if (ChkAgent.Checked)
        {
            reportOption = ChkIncludeLedger.Checked && ChkIncludeMainAgent.Checked
                ? "Main Agent/Ledger" : ChkIncludeLedger.Checked && !ChkIncludeMainAgent.Checked ? "Agent/Ledger" : "Agent";
        }
        else if (ChkArea.Checked)
        {
            reportOption = ChkIncludeLedger.Checked && ChkIncludeMainAgent.Checked
                ? "Main Area/Ledger" : ChkIncludeLedger.Checked && !ChkIncludeMainAgent.Checked ? "Area/Ledger" : "Area";
        }
        return reportOption;
    }

    private bool GetSelectedIdValue()
    {
        if (ChkAccountGroup.Checked)
        {
            if (!GetAccountGroupId())
            {
                return false;
            }
        }
        else if (ChkAccountSubGroup.Checked)
        {
            if (!GetAccountSubGroupId())
            {
                return false;
            }
        }
        else if (ChkSubLedger.Checked && ChkIncludeLedger.Checked)
        {
            if (!GetSubLedgerId())
            {
                return false;
            }
        }
        else if (ChkAgent.Checked && ChkIncludeLedger.Checked)
        {
            if (!GetAgentId())
            {
                return false;
            }
        }
        else if (ChkArea.Checked && ChkIncludeLedger.Checked)
        {
            if (!GetAreaId())
            {
                return false;
            }
        }
        return true;
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
        if (!string.IsNullOrEmpty(_accountGroupId))
        {
            return true;
        }
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
        _departmentId = ClsTagList.PlValue1;
        if (!string.IsNullOrEmpty(_departmentId))
        {
            return true;
        }
        MessageBox.Show(@"DEPARTMENT NOT SELECTED..!!", ObjGlobal.Caption);
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
        _agentId = ClsTagList.PlValue1;
        if (!string.IsNullOrEmpty(_agentId))
        {
            return true;
        }
        MessageBox.Show(@"AGENT/SALES MAN NOT SELECTED..!!", ObjGlobal.Caption);
        return false;
    }

    private bool GetAreaId()
    {
        var frm2 = new FrmTagList
        {
            ReportDesc = @"AREA",
            BranchId = _branchId,
            CompanyUnitId = _companyUnitId,
            FiscalYearId = _fiscalYearId
        };
        frm2.ShowDialog();
        _areaId = ClsTagList.PlValue1;
        if (!string.IsNullOrEmpty(_areaId))
        {
            return true;
        }
        MessageBox.Show(@"AREA / SALES DIVISION NOT SELECTED..!!", ObjGlobal.Caption);
        return false;
    }
    #endregion


    // OBJECT FOR THIS FORM
    #region --------- OBJECT FOR THIS FORM ---------
    private string _branchId = ObjGlobal.SysBranchId.ToString();
    private string _companyUnitId = ObjGlobal.SysCompanyUnitId.ToString();
    private string _currencyId = ObjGlobal.SysCurrencyId.ToString();
    private string _fiscalYearId = ObjGlobal.SysFiscalYearId.ToString();

    private string _accountGroupId = string.Empty;
    private string _accountSubGroupId = string.Empty;
    private string _agentId = string.Empty;
    private string _areaId = string.Empty;
    private string _ledgerId = string.Empty;
    private string _subledgerId = string.Empty;
    private string _departmentId = string.Empty;
    private string _user = string.Empty;

    #endregion


}