using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Utility.PickList;
using System;
using System.Windows.Forms;

namespace MrBLL.Reports.Register_Report.Sales_Register;

public partial class FrmSalesInvoiceEntryRegister : MrForm
{
    // SALES INVOICE ENTRY REGISTER

    #region --------------- SALES INVOICE ENTRY REGISTER ---------------

    public FrmSalesInvoiceEntryRegister(string module)
    {
        InitializeComponent();
        ObjGlobal.BindDateType(CmbDateType);
        ObjGlobal.BindPeriodicDate(MskFrom, MskToDate);
        CmbDateType.SelectedIndex = 8;
        ChkDateWise.Text = ObjGlobal.SysDateType == "M" ? @"Date" : @"Miti";
        _module = module;
    }
    private void SalesInvoiceRegister_Load(object sender, EventArgs e)
    {
        Text = _module switch
        {
            "SQ" => "SALES QUOTATION INVOICE REGISTER REPORTS",
            "SO" => "SALES ORDER INVOICE REGISTER REPORTS",
            "SC" => "SALES CHALLAN INVOICE REGISTER REPORTS",
            "SB" => "SALES INVOICE REGISTER REPORTS",
            "SR" => "SALES RETURN INVOICE REGISTER REPORTS",
            _ => "SALES INVOICE REGISTER REPORTS"
        };
        ChkSummary_CheckedChanged(null, EventArgs.Empty);
        rChkDate_CheckedChanged(null, EventArgs.Empty);
        ChkQtyWise_CheckedChanged(null, EventArgs.Empty);
        CmbDateType.Focus();
    }
    private void SalesInvoiceRegister_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
        else if (e.KeyChar == (char)Keys.Escape)
        {
            BtnCancel.PerformClick();
        }
    }
    private void BtnShow_Click(object sender, EventArgs e)
    {
        _rptType = "NORMAL";
        _rptName = "SALES REGISTER";
        _invoiceCategory =
            rChkAllType.Checked ? "All" :
            rChkCashSales.Checked ? "Cash" :
            rChkCreditSales.Checked ? "Credit" :
            rChkCardSales.Checked ? "Card" :
            rChkPartialSales.Checked ? "Partial" :
            rChkOtherSales.Checked ? "Other" : "Normal";

        _fromAdDate = ObjGlobal.SysDateType == "M" ? _fromAdDate.GetEnglishDate(MskFrom.Text) : MskFrom.Text;
        _fromBsDate = ObjGlobal.SysDateType == "M" ? MskFrom.Text : MskFrom.GetNepaliDate();
        _toAdDate = ObjGlobal.SysDateType == "M" ? _toAdDate.GetEnglishDate(MskToDate.Text) : MskToDate.Text;
        _toBsDate = ObjGlobal.SysDateType == "M" ? MskToDate.Text : MskToDate.GetNepaliDate();

        _reportMode = rChkCustomer.Checked ? "Customer Wise"
            : rChkAgent.Checked ? "Agent Wise"
            : rChkArea.Checked ? "Area Wise"
            : rChkProduct.Checked ? "Product Wise"
            : rChkProductGroup.Checked ? "Product Group Wise"
            : rChkProductSubGroup.Checked ? "Product SubGroup Wise"
            : rChkCounter.Checked ? "Counter Wise"
            : rChkUserWise.Checked ? "User Wise"
            : rChkDepartment.Checked ? "Department Wise"
            : rChkDate.Checked ? "Date Wise"
            : rChkInvoice.Checked ? "Voucher Wise" : "Date Wise";

        _filterMode = rChkReturnRegister.Checked ? "Return" : rChkCancelRegister.Checked ? "Cancel" : "Normal";
        if (!ChkSelectAll.Checked)
        {
            if (rChkCustomer.Checked)
            {
                using var generalLedger = new FrmTagList
                {
                    ReportDesc = @"GENERAL_LEDGER",
                    Category = "CUSTOMER",
                    Module = "SB",
                    BranchId = ObjGlobal.SysBranchId.ToString(),
                    CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString(),
                    FiscalYearId = ObjGlobal.SysFiscalYearId.ToString()
                };
                generalLedger.ShowDialog();
                _ledgerId = ClsTagList.PlValue1;
                if (string.IsNullOrEmpty(_ledgerId))
                {
                    MessageBox.Show(@"LEDGER NOT SELECTED..!!", ObjGlobal.Caption);
                    return;
                }
            }
            else if (rChkAgent.Checked)
            {
                using var agent = new FrmTagList
                {
                    ReportDesc = @"AGENT",
                    BranchId = ObjGlobal.SysBranchId.ToString(),
                    CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString(),
                    FiscalYearId = ObjGlobal.SysFiscalYearId.ToString()
                };
                agent.ShowDialog();
                _agentId = ClsTagList.PlValue1;
                if (string.IsNullOrEmpty(_agentId))
                {
                    MessageBox.Show(@"SALES AGENT ARE NOT SELECTED..!!", ObjGlobal.Caption);
                    return;
                }
            }
            else if (rChkArea.Checked)
            {
                using var area = new FrmTagList
                {
                    ReportDesc = @"AREA",
                    BranchId = ObjGlobal.SysBranchId.ToString(),
                    CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString(),
                    FiscalYearId = ObjGlobal.SysFiscalYearId.ToString()
                };
                area.ShowDialog();

                _areaId = ClsTagList.PlValue1;
                if (string.IsNullOrEmpty(_areaId))
                {
                    MessageBox.Show(@"SALES AREA ARE NOT SELECTED..!!", ObjGlobal.Caption);
                    return;
                }
            }
            else if (rChkProduct.Checked)
            {
                using var product = new FrmTagList
                {
                    ReportDesc = @"PRODUCT",
                    BranchId = ObjGlobal.SysBranchId.ToString(),
                    CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString(),
                    FiscalYearId = ObjGlobal.SysFiscalYearId.ToString(),
                    Module = "SB"
                };
                product.ShowDialog();

                _productId = ClsTagList.PlValue1;
                if (string.IsNullOrEmpty(_productId))
                {
                    MessageBox.Show(@"SALES PRODUCT ARE NOT SELECTED..!!", ObjGlobal.Caption);
                    return;
                }
            }
            else if (rChkProductGroup.Checked)
            {
                using var productGroup = new FrmTagList
                {
                    ReportDesc = @"PRODUCT_GROUP",
                    BranchId = ObjGlobal.SysBranchId.ToString(),
                    CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString(),
                    FiscalYearId = ObjGlobal.SysFiscalYearId.ToString()
                };
                productGroup.ShowDialog();

                _groupId = ClsTagList.PlValue1;
                if (string.IsNullOrEmpty(_groupId))
                {
                    MessageBox.Show(@"SALES PRODUCT GROUP ARE NOT SELECTED..!!", ObjGlobal.Caption);
                    return;
                }
            }
            else if (rChkProductSubGroup.Checked)
            {
                using var productSubGroup = new FrmTagList
                {
                    ReportDesc = @"PRODUCT_SUBGROUP",
                    BranchId = ObjGlobal.SysBranchId.ToString(),
                    CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString(),
                    FiscalYearId = ObjGlobal.SysFiscalYearId.ToString()
                };
                productSubGroup.ShowDialog();

                _subGroupId = ClsTagList.PlValue1;
                if (string.IsNullOrEmpty(_subGroupId))
                {
                    MessageBox.Show(@"SALES PRODUCT SUB GROUP ARE NOT SELECTED..!!", ObjGlobal.Caption);
                    return;
                }
            }
            else if (rChkCounter.Checked)
            {
                using var counter = new FrmTagList
                {
                    ReportDesc = @"COUNTER",
                    BranchId = ObjGlobal.SysBranchId.ToString(),
                    CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString(),
                    FiscalYearId = ObjGlobal.SysFiscalYearId.ToString()
                };
                counter.ShowDialog();
                _counterId = ClsTagList.PlValue1;
                if (string.IsNullOrEmpty(_counterId))
                {
                    MessageBox.Show(@"SALES COUNTER ARE NOT SELECTED..!!", ObjGlobal.Caption);
                    return;
                }
            }
            else if (rChkUserWise.Checked)
            {
                using var user = new FrmTagList
                {
                    ReportDesc = @"USER",
                    BranchId = ObjGlobal.SysBranchId.ToString(),
                    CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString(),
                    FiscalYearId = ObjGlobal.SysFiscalYearId.ToString()
                };
                user.ShowDialog();
                _getEntryUser = ClsTagList.PlValue1;
                if (_getEntryUser.IsBlankOrEmpty())
                {
                    MessageBox.Show(@"SALES USER ARE NOT SELECTED..!!", ObjGlobal.Caption);
                    return;
                }
            }
            else if (rChkDepartment.Checked)
            {
                using var department = new FrmTagList
                {
                    ReportDesc = @"DEPARTMENT",
                    BranchId = ObjGlobal.SysBranchId.ToString(),
                    CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString(),
                    FiscalYearId = ObjGlobal.SysFiscalYearId.ToString()
                };
                department.ShowDialog();

                _departmentId = ClsTagList.PlValue1;
                if (string.IsNullOrEmpty(_departmentId))
                {
                    MessageBox.Show(@"SALES DEPARTMENT ARE NOT SELECTED..!!", ObjGlobal.Caption);
                    return;
                }
            }
        }

        _rptDate = ObjGlobal.SysDateType switch
        {
            "M" when !ChkDateWise.Checked => $"FROM DATE {_fromBsDate} TO {_toBsDate}",
            "D" when ChkDateWise.Checked => $"FROM DATE {_fromBsDate} TO {_toBsDate}",
            "M" when ChkDateWise.Checked => $"FROM DATE {_fromAdDate} TO {_toAdDate}",
            "D" when !ChkDateWise.Checked => $"FROM DATE {_fromAdDate} TO {_toAdDate}",
            _ => $"FROM DATE {_fromBsDate} TO {_toBsDate}"
        };
        var display = new DisplayRegisterReports
        {
            Module = _module,
            RptMode = _reportMode.ToUpper(),
            RptType = _rptType.ToUpper(),
            RptName = _rptName.ToUpper(),
            RptDate = _rptDate,
            FromAdDate = _fromAdDate,
            ToAdDate = _toAdDate,
            IsSummary = ChkSummary.Checked,
            IsAdditionalTerm = ChkAdditionalTerm.Checked,
            IsHorizon = ChkHorizontal.Checked,
            IncludeSalesOrder = ChkIncludeOrderNo.Checked,
            IncludeSalesChallan = ChkIncludeChallanNo.Checked,
            IncludeGodown = ChkIncludeGodown.Checked,
            IncludeAltQty = ChkIncludeAltQty.Checked,
            IncludeFreeQty = ChkFreeQty.Checked,
            IncludeRemarks = ChkIncludeRemarks.Checked,
            IsDate = ChkDateWise.Checked,
            LedgerId = _ledgerId,
            ProductId = _productId,
            PGroupId = _groupId,
            PSubGroupId = _subGroupId,
            SubLedgerId = _subLedgerId,
            CounterId = _counterId,
            AgentId = _agentId,
            AreaId = _areaId,
            DepartmentId = _departmentId,
            FilterValue = TxtVoucherNo.Text,
            InvoiceType = _filterMode.GetUpper(),
            InvoiceCategory = _invoiceCategory.ToUpper(),
            EntryUser = _getEntryUser,
            BranchId = ObjGlobal.SysBranchId.ToString(),
            FiscalYearId = ObjGlobal.SysFiscalYearId.ToString(),
            CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString()
        };
        display.Show();
        display.BringToFront();
        _ledgerId = string.Empty;
        _productId = string.Empty;
        _groupId = string.Empty;
        _subGroupId = string.Empty;
        _departmentId = string.Empty;
        _agentId = string.Empty;
        _subLedgerId = string.Empty;
        _invoiceCategory = string.Empty;
        _getEntryUser = string.Empty;
        _areaId = string.Empty;
        _counterId = string.Empty;
        _filterMode = string.Empty;
    }
    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
    private void CmbDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", CmbDateType.SelectedIndex, MskFrom, MskToDate);
    }
    private void MskFrom_Validated(object sender, EventArgs e)
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
    private void MskToDate_Validated(object sender, EventArgs e)
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
    private void ChkSummary_CheckedChanged(object sender, EventArgs e)
    {
        ChkIncludeOrderNo.Enabled = ChkSummary.Checked;
        ChkIncludeChallanNo.Enabled = ChkSummary.Checked;
        ChkIncludeBatch.Enabled = ChkSummary.Checked;
        ChkIncludeGodown.Enabled = ChkSummary.Checked;
        if (ChkSummary.Checked)
        {
            if (rChkProduct.Checked || rChkProductGroup.Checked || rChkProductSubGroup.Checked)
            {
                ChkIncludeAltQty.Enabled = true;
            }
            else
            {
                ChkIncludeAltQty.Enabled = false;
            }
        }
        else
        {
            ChkIncludeAltQty.Enabled = true;
        }

        ChkFreeQty.Enabled = ChkSummary.Checked;
    }
    private void rChkDate_CheckedChanged(object sender, EventArgs e)
    {
        if (rChkDate.Checked || rChkInvoice.Checked || rChkUserWise.Checked)
        {
            mrGroup2.Enabled = true;
            mrGroup4.Enabled = true;
            if (rChkUserWise.Checked)
            {
                mrGroup4.Enabled = false;
            }
        }
        else
        {
            rChkNormal.Checked = true;
            rChkNormalRegister.Checked = true;
            mrGroup2.Enabled = false;
            mrGroup4.Enabled = false;
        }

        if (rChkProduct.Checked || rChkProductGroup.Checked || rChkProductSubGroup.Checked)
        {
            mrGroup6.Enabled = true;
        }
        else
        {
            mrGroup6.Enabled = false;
            ChkQtyWise.Checked = true;
        }
    }
    private void ChkQtyWise_CheckedChanged(object sender, EventArgs e)
    {
        ChkIncludeAltQty.Enabled = ChkQtyWise.Checked;
        if (ChkAltQtyWise.Checked)
        {
            ChkIncludeAltQty.Checked = false;
        }
    }

    #endregion --------------- SALES INVOICE ENTRY REGISTER ---------------

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT ---------------

    private string _rptType;
    private string _reportMode;
    private string _filterMode;
    private string _rptName;
    private string _rptDate;
    private string _fromAdDate;
    private string _fromBsDate;
    private string _toAdDate;
    private string _toBsDate;
    private string _module;

    private string _getEntryUser = string.Empty;
    private string _ledgerId = string.Empty;
    private string _productId = string.Empty;
    private string _groupId = string.Empty;
    private string _subGroupId = string.Empty;
    private string _departmentId = string.Empty;
    private string _agentId = string.Empty;
    private string _subLedgerId = string.Empty;
    private string _areaId = string.Empty;
    private string _counterId = string.Empty;
    private string _invoiceCategory = string.Empty;

    #endregion --------------- OBJECT ---------------
}