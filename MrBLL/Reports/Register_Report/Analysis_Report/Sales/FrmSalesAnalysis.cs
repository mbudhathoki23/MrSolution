using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Utility.PickList;
using System;
using System.Windows.Forms;

namespace MrBLL.Reports.Register_Report.Analysis_Report.Sales;

public partial class FrmSalesAnalysis : MrForm
{
    // SALES ANALYSIS

    #region --------------- SALES ANALYSIS ---------------

    public FrmSalesAnalysis()
    {
        InitializeComponent();
        ObjGlobal.BindDateType(CmbDateType);
        ObjGlobal.BindPeriodicDate(MskFrom, MskToDate);
        CmbDateType.SelectedIndex = 8;
        ChkDate.Text = ObjGlobal.SysDateType.Equals("M") ? "Date" : "Miti";
    }

    private void FrmSalesAnalysis_Load(object sender, EventArgs e)
    {
        RChkProduct_CheckedChanged(this, EventArgs.Empty);
        CmbDateType.Focus();
    }

    private void FrmSalesAnalysis_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
        if (e.KeyChar == (char)Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() is DialogResult.Yes)
            {
                Close();
            }
        }
    }

    private void BtnShow_Click(object sender, EventArgs e)
    {
        _rptType = "NORMAL";
        _rptName = "SALES ANALYSIS";
        _fromAdDate = ObjGlobal.SysDateType.Equals("M") ? ObjGlobal.ReturnEnglishDate(MskFrom.Text) : MskFrom.Text;
        _fromBsDate = ObjGlobal.SysDateType.Equals("M") ? MskFrom.Text : ObjGlobal.ReturnNepaliDate(MskFrom.Text);
        _toAdDate = ObjGlobal.SysDateType.Equals("M") ? ObjGlobal.ReturnEnglishDate(MskToDate.Text) : MskToDate.Text;
        _toBsDate = ObjGlobal.SysDateType.Equals("M") ? MskToDate.Text : ObjGlobal.ReturnNepaliDate(MskToDate.Text);

        if (chk_SelectAll.Checked)
        {
        }
        else if (rChkCustomer.Checked)
        {
            var frm2 = new FrmTagList
            {
                ReportDesc = @"GENERAL_LEDGER",
                Category = "Customer",
                BranchId = ObjGlobal.SysBranchId.ToString(),
                CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString(),
                FiscalYearId = ObjGlobal.SysFiscalYearId.ToString()
            };
            frm2.ShowDialog();
            _ledgerId = ClsTagList.PlValue1;
            if (string.IsNullOrEmpty(_ledgerId))
            {
                MessageBox.Show(@"LEDGER NOT SELECTED..!!", ObjGlobal.Caption);
                return;
            }
        }
        else if (rChkAgent.Checked)
        {
            using var frm2 = new FrmTagList
            {
                ReportDesc = @"AGENT",
                BranchId = ObjGlobal.SysBranchId.ToString(),
                CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString(),
                FiscalYearId = ObjGlobal.SysFiscalYearId.ToString()
            };
            frm2.ShowDialog();
            _agentId = ClsTagList.PlValue1;
            if (string.IsNullOrEmpty(_agentId))
            {
                MessageBox.Show(@"SALES AGENT ARE NOT SELECTED..!!", ObjGlobal.Caption);
                return;
            }
        }
        else if (rChkArea.Checked)
        {
            using var frm2 = new FrmTagList
            {
                ReportDesc = @"AREA",
                BranchId = ObjGlobal.SysBranchId.ToString(),
                CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString(),
                FiscalYearId = ObjGlobal.SysFiscalYearId.ToString()
            };
            frm2.ShowDialog();

            _areaId = ClsTagList.PlValue1;
            if (string.IsNullOrEmpty(_areaId))
            {
                MessageBox.Show(@"SALES AREA ARE NOT SELECTED..!!", ObjGlobal.Caption);
                return;
            }
        }
        else if (rChkProduct.Checked)
        {
            using var frm2 = new FrmTagList
            {
                ReportDesc = @"PRODUCT",
                BranchId = ObjGlobal.SysBranchId.ToString(),
                CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString(),
                FiscalYearId = ObjGlobal.SysFiscalYearId.ToString()
            };
            frm2.ShowDialog();

            _productId = ClsTagList.PlValue1;
            if (!string.IsNullOrEmpty(_productId))
            {
                MessageBox.Show(@"SALES PRODUCT ARE NOT SELECTED..!!", ObjGlobal.Caption);
                return;
            }
        }
        else if (rChkProductGroup.Checked)
        {
            using var frm2 = new FrmTagList
            {
                ReportDesc = @"PRODUCT_GROUP",
                BranchId = ObjGlobal.SysBranchId.ToString(),
                CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString(),
                FiscalYearId = ObjGlobal.SysFiscalYearId.ToString()
            };
            frm2.ShowDialog();

            _groupId = ClsTagList.PlValue1;
            if (string.IsNullOrEmpty(_groupId))
            {
                MessageBox.Show(@"SALES PRODUCT GROUP ARE NOT SELECTED..!!", ObjGlobal.Caption);
                return;
            }
        }
        else if (rChkProductSubGroup.Checked)
        {
            using var frm2 = new FrmTagList
            {
                ReportDesc = @"PRODUCT_SUBGROUP",
                BranchId = ObjGlobal.SysBranchId.ToString(),
                CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString(),
                FiscalYearId = ObjGlobal.SysFiscalYearId.ToString()
            };
            frm2.ShowDialog();

            _subGroupId = ClsTagList.PlValue1;
            if (string.IsNullOrEmpty(_subGroupId))
            {
                MessageBox.Show(@"SALES PRODUCT SUB GROUP ARE NOT SELECTED..!!", ObjGlobal.Caption);
                return;
            }
        }

        _rptDate = ObjGlobal.SysDateType == "M" && ChkDate.Checked || ObjGlobal.SysDateType == "D" && ChkDate.Checked ? $"FROM DATE {_fromBsDate} TO {_toBsDate}" : $"FROM DATE {Convert.ToDateTime(_fromAdDate).ToShortDateString()} TO {Convert.ToDateTime(_toAdDate).ToShortDateString()}";
        _reportMode =
            rChkCustomer.Checked ? "CUSTOMER WISE"
            : rChkAgent.Checked ? "AGENT WISE"
            : rChkArea.Checked ? "AREA WISE"
            : rChkProduct.Checked ? "PRODUCT WISE"
            : rChkProductGroup.Checked ? "PRODUCT GROUP"
            : rChkProductSubGroup.Checked ? "PRODUCT SUBGROUP WISE"
            : rChkAgent.Checked ? "AGENT WISE"
            : rChkArea.Checked ? "AREA WISE" : string.Empty;
        var display = new DisplayRegisterReports
        {
            RptMode = _reportMode,
            RptType = _rptType,
            RptName = _rptName,
            RptDate = _rptDate,
            FromAdDate = _fromAdDate,
            ToAdDate = _toAdDate,
            IsAdditionalTerm = ChkAdditionalTerm.Checked,
            LedgerId = _ledgerId,
            ProductId = _productId,
            PGroupId = _groupId,
            PSubGroupId = _subGroupId,
            CounterId = _counterId,
            AgentId = _agentId,
            AreaId = _areaId,
            DepartmentId = _departmentId,
            Module = "SB",
            EntryUser = _getEntryUser,
            InvoiceType = _invoiceCategory,
            BranchId = ObjGlobal.SysBranchId.ToString(),
            FiscalYearId = ObjGlobal.SysFiscalYearId.ToString(),
            CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString()
        };
        display.Show();

        _ledgerId = string.Empty;
        _productId = string.Empty;
        _groupId = string.Empty;
        _subGroupId = string.Empty;
        _departmentId = string.Empty;
        _agentId = string.Empty;
        _invoiceCategory = string.Empty;
        _getEntryUser = string.Empty;
        _areaId = string.Empty;
        _counterId = string.Empty;
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
    }

    private void CmbDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", CmbDateType.SelectedIndex, MskFrom, MskToDate);
    }

    private void RChkProduct_CheckedChanged(object sender, EventArgs e)
    {
        ChkIncudeProduct.Enabled = !rChkProduct.Checked;
        ChkIncludeCustomer.Enabled = rChkProduct.Checked;
    }

    private void RChkProductGroup_CheckedChanged(object sender, EventArgs e)
    {
        ChkIncudeProduct.Enabled = rChkProductGroup.Checked;
        ChkIncludeCustomer.Enabled = rChkProductGroup.Checked;
    }

    private void RChkProductSubGroup_CheckedChanged(object sender, EventArgs e)
    {
        ChkIncudeProduct.Enabled = rChkProductSubGroup.Checked;
    }

    private void RChkCustomer_CheckedChanged(object sender, EventArgs e)
    {
        ChkIncludeCustomer.Enabled = !rChkCustomer.Checked;
        ChkIncudeProduct.Enabled = rChkCustomer.Checked;
    }

    private void rChkArea_CheckedChanged(object sender, EventArgs e)
    {
        ChkIncludeCustomer.Enabled = rChkArea.Checked;
        ChkIncudeProduct.Enabled = rChkArea.Checked;
        ChkMainArea.Enabled = rChkArea.Checked;
    }

    private void rChkAgent_CheckedChanged(object sender, EventArgs e)
    {
        ChkIncludeCustomer.Enabled = rChkAgent.Checked;
        ChkIncudeProduct.Enabled = rChkAgent.Checked;
        ChkMainAgent.Enabled = rChkAgent.Checked;
        ChkDocumentAgent.Enabled = rChkAgent.Checked;
    }

    private void MskFrom_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (ActiveControl != null && MskFrom.MaskFull)
        {
            if (ObjGlobal.SysDateType.Equals("D"))
            {
                var result = MskFrom.Text.IsValidDateRange("D");
                var exits = MskFrom.Text.IsDateExits("D");
                if (exits)
                {
                    if (!result)
                    {
                        CustomMessageBox.Warning($"DATE MUST BE BETWEEN [{ObjGlobal.CfStartBsDate} AND {ObjGlobal.CfEndBsDate}]");
                        MskFrom.Focus();
                        return;
                    }
                    return;
                }
                CustomMessageBox.Warning($"ENTER DATE IS NOT A VALID DATE");
                MskFrom.Focus();
                return;
            }
            if (ObjGlobal.SysDateType.Equals("M"))
            {
                var result = MskFrom.Text.IsValidDateRange("M");
                var exits = MskFrom.Text.IsDateExits("M");
                if (exits)
                {
                    if (!result)
                    {
                        CustomMessageBox.Warning($"MITI MUST BE BETWEEN [{ObjGlobal.CfStartBsDate} AND {ObjGlobal.CfEndBsDate}]");
                        MskFrom.Focus();
                        return;
                    }
                    return;
                }
                CustomMessageBox.Warning($"ENTER MITI IS NOT A VALID MITI");
                MskFrom.Focus();
                return;
            }
        }
        else
        {
            CustomMessageBox.Warning($"DATE IS REQUIRED FOR REPORT GENERATE..!!");
            MskFrom.Focus();
            return;
        }
    }

    private void MskToDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (MskToDate.MaskFull)
        {
            if (ObjGlobal.SysDateType.Equals("D"))
            {
                var result = MskToDate.Text.IsValidDateRange("D");
                var exits = MskToDate.Text.IsDateExits("D");
                if (exits)
                {
                    if (!result)
                    {
                        CustomMessageBox.Warning($"DATE MUST BE BETWEEN [{ObjGlobal.CfStartBsDate} AND {ObjGlobal.CfEndBsDate}]");
                        MskFrom.Focus();
                        return;
                    }
                    return;
                }
                CustomMessageBox.Warning($"ENTER DATE IS NOT A VALID DATE");
                MskToDate.Focus();
                return;
            }
            if (ObjGlobal.SysDateType.Equals("M"))
            {
                var result = MskToDate.Text.IsValidDateRange("M");
                var exits = MskToDate.Text.IsDateExits("M");
                if (exits)
                {
                    if (!result)
                    {
                        CustomMessageBox.Warning($"MITI MUST BE BETWEEN [{ObjGlobal.CfStartBsDate} AND {ObjGlobal.CfEndBsDate}]");
                        MskToDate.Focus();
                        return;
                    }
                    return;
                }
                CustomMessageBox.Warning($"ENTER MITI IS NOT A VALID MITI");
                MskToDate.Focus();
                return;
            }
        }
        else
        {
            CustomMessageBox.Warning($"DATE IS REQUIRED FOR REPORT GENERATE..!!");
            MskToDate.Focus();
            return;
        }
    }

    #endregion --------------- SALES ANALYSIS ---------------

    // OBJECT FOR THIS FORM

    #region --------------- Class ---------------

    private string _rptType = string.Empty;
    private string _reportMode = string.Empty;
    private string _rptName = string.Empty;
    private string _rptDate = string.Empty;
    private string _fromAdDate = string.Empty;
    private string _fromBsDate = string.Empty;
    private string _toAdDate = string.Empty;
    private string _toBsDate = string.Empty;

    private string _getEntryUser = string.Empty;
    private string _ledgerId = string.Empty;
    private string _productId = string.Empty;
    private string _groupId = string.Empty;
    private string _subGroupId = string.Empty;
    private string _departmentId = string.Empty;
    private string _agentId = string.Empty;
    private string _areaId = string.Empty;
    private string _counterId = string.Empty;
    private string _invoiceCategory = string.Empty;

    #endregion --------------- Class ---------------
}