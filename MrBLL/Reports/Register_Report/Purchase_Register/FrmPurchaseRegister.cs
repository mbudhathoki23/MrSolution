using MrBLL.Utility.Common;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Utility.PickList;
using System;
using System.Windows.Forms;

namespace MrBLL.Reports.Register_Report.Purchase_Register;

public partial class FrmPurchaseRegister : MrForm
{
    // PURCHASE REGISTER REPORT

    #region --------------- PURCHASE REGISTER ---------------

    public FrmPurchaseRegister(string reportModule)
    {
        InitializeComponent();
        _reportModule = reportModule;

        FormControl();
    }

    private void PurchaseInvoiceRegister_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
        if (e.KeyChar == (char)Keys.Escape)
        {
            var dialogResult = MessageBox.Show(@"ARE YOU SURE WANT TO CLOSE FORM..!!", ObjGlobal.Caption,
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) Close();
        }
    }

    private void PurchaseInvoiceRegister_Load(object sender, EventArgs e)
    {
        ObjGlobal.BindPeriodicDate(MskFrom, MskToDate);
        CmbDateType.SelectedIndex = 8;
        ChkDateWise.Text = ObjGlobal.SysDateType == "M" ? @"Date" : @"Miti";
        ChkSummary_CheckedChanged(sender, e);
        rChkDate_CheckedChanged(sender, e);
        CmbDateType.Focus();
    }

    private void CmbDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", CmbDateType.SelectedIndex, MskFrom, MskToDate);
    }

    private void MskFrom_Validated(object sender, EventArgs e)
    {
        if (MskFrom.MaskCompleted)
        {
            if (ObjGlobal.SysDateType == "D")
            {
                if (MskFrom.Text.IsValidDate())
                {
                    if (MskFrom.Text.IsValidDateRange("D")) return;
                    MessageBox.Show($@"Date Must be Between {ObjGlobal.CfStartAdDate} and {ObjGlobal.CfEndAdDate} ",
                        ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MskFrom.Focus();
                }
                else
                {
                    this.NotifyValidationError(MskFrom, "PLZ. ENTER VALID DATE..!!");
                }
            }
            else
            {
                if (MskFrom.Text.IsDateExits("M"))
                {
                    if (MskFrom.Text.IsValidDateRange("M")) return;
                    this.NotifyValidationError(MskFrom,
                        $@"DATE MUST BE BETWEEN {ObjGlobal.CfStartBsDate} AND {ObjGlobal.CfEndBsDate} ");
                }
                else
                {
                    this.NotifyValidationError(MskFrom, "PLZ. ENTER VALID DATE..!!");
                }
            }
        }
        else
        {
            this.NotifyValidationError(MskFrom, "PLZ. ENTER VALID DATE..!!");
        }
    }

    private void MskToDate_Validated(object sender, EventArgs e)
    {
        if (MskToDate.MaskCompleted)
        {
            if (ObjGlobal.SysDateType == "D")
            {
                if (MskToDate.Text.IsValidDate())
                {
                    if (!MskToDate.Text.IsValidDateRange("D")) return;
                    this.NotifyValidationError(MskFrom,
                        $@"DATE MUST BE BETWEEN {ObjGlobal.CfStartAdDate} AND {ObjGlobal.CfEndAdDate} ");
                }
                else
                {
                    this.NotifyValidationError(MskFrom, "PLZ. ENTER VALID DATE..!!");
                }
            }
            else
            {
                if (MskToDate.Text.IsDateExits("M"))
                {
                    if (MskToDate.Text.IsValidDateRange("M")) return;
                    this.NotifyValidationError(MskFrom,
                        $@"DATE MUST BE BETWEEN {ObjGlobal.CfStartAdDate} AND {ObjGlobal.CfEndAdDate} ");
                }
                else
                {
                    this.NotifyValidationError(MskFrom, "PLZ. ENTER VALID DATE..!!");
                }
            }
        }
        else
        {
            this.NotifyValidationError(MskFrom, "PLZ. ENTER VALID DATE..!!");
        }
    }

    private void ChkSummary_CheckedChanged(object sender, EventArgs e)
    {
        ChkIncludeOrderNo.Enabled = ChkIncludeChallanNo.Enabled = ChkSummary.Checked;
        ChkFreeQty.Enabled = !ChkSummary.Checked;
        ChkFreeQty.Checked = (!ChkFreeQty.Checked || ChkSummary.Checked) && ChkFreeQty.Checked;
        ChkIncludeBatch.Enabled = !ChkSummary.Checked;
        ChkIncludeBatch.Checked = (!ChkIncludeBatch.Checked || ChkSummary.Checked) && ChkIncludeBatch.Checked;
        ChkHorizontal.Enabled = !ChkSummary.Checked;
        ChkHorizontal.Checked = (!ChkHorizontal.Checked || ChkSummary.Checked) && ChkHorizontal.Checked;
        ChkIncludeOrderNo.Checked = (ChkSummary.Checked || !ChkIncludeOrderNo.Checked) && ChkIncludeOrderNo.Checked;
        ChkIncludeChallanNo.Enabled =
            (ChkSummary.Checked || !ChkIncludeOrderNo.Checked) && ChkIncludeOrderNo.Checked;
    }

    private void rChkDate_CheckedChanged(object sender, EventArgs e)
    {
        if (rChkDate.Checked || rChkInvoice.Checked)
        {
            rChkAllType.Enabled = true;
            rChkCashSales.Enabled = true;
            rChkCreditSales.Enabled = true;
            rChkCardSales.Enabled = true;
            rChkOtherSales.Enabled = true;
        }
        else
        {
            rChkAllType.Enabled = false;
            rChkAllType.Checked = false;

            rChkCashSales.Checked = false;
            rChkCashSales.Enabled = false;

            rChkCreditSales.Enabled = false;
            rChkCreditSales.Checked = false;

            rChkCardSales.Enabled = false;
            rChkCardSales.Checked = false;

            rChkOtherSales.Checked = false;
            rChkOtherSales.Enabled = false;
        }
    }

    private void BtnShow_Click(object sender, EventArgs e)
    {
        _rptType = "NORMAL";
        _rptName = "PURCHASE REGISTER";
        _invoiceCategory = rChkAllType.Checked ? "All" :
            rChkCashSales.Checked ? "Cash" :
            rChkCreditSales.Checked ? "Credit" :
            rChkCardSales.Checked ? "Card" :
            rChkOtherSales.Checked ? "Other" : "Cash";

        _fromAdDate = ObjGlobal.SysDateType == "M" ? ObjGlobal.ReturnEnglishDate(MskFrom.Text) : MskFrom.Text;
        _fromBsDate = ObjGlobal.SysDateType == "M" ? MskFrom.Text : ObjGlobal.ReturnNepaliDate(MskFrom.Text);
        _toAdDate = ObjGlobal.SysDateType == "M" ? ObjGlobal.ReturnEnglishDate(MskToDate.Text) : MskToDate.Text;
        _toBsDate = ObjGlobal.SysDateType == "M" ? MskToDate.Text : ObjGlobal.ReturnNepaliDate(MskToDate.Text);

        _rptMode =
            rChkVendor.Checked ? "Vendor Wise"
            : rChkAgent.Checked ? "Agent Wise"
            : rChkArea.Checked ? "Area Wise"
            : rChkProduct.Checked ? "Product Wise"
            : rChkProductGroup.Checked ? "Product Group"
            : rChkProductSubGroup.Checked ? "Product SubGroup Wise"
            : rChkDepartment.Checked ? "Department Wise"
            : rChkDate.Checked ? "Date Wise"
            : rChkReturnRegister.Checked ? "Return Bill"
            : rChkCancelRegister.Checked ? "Cancel Wise"
            : rChkInvoice.Checked ? "Voucher Wise" : "Date Wise";

        if (chk_SelectAll.Checked)
        {
        }
        else if (rChkVendor.Checked)
        {
            using var frm2 = new FrmTagList
            {
                ReportDesc = @"GENERAL_LEDGER",
                Category = "Vendor",
                BranchId = ObjGlobal.SysBranchId.ToString(),
                CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString(),
                FiscalYearId = ObjGlobal.SysFiscalYearId.ToString()
            };
            frm2.ShowDialog();
            _ledgerId = ClsTagList.PlValue1;
            if (string.IsNullOrEmpty(_ledgerId))
            {
                MessageBox.Show(@"VENDOR LEDGER NOT SELECTED..!!", ObjGlobal.Caption);
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
                MessageBox.Show(@"PURCHASE RETURN AGENT ARE NOT SELECTED..!!", ObjGlobal.Caption);
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
                MessageBox.Show(@"PURCHASE INVOICE AREA ARE NOT SELECTED..!!", ObjGlobal.Caption);
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
            if (_productId.IsBlankOrEmpty())
            {
                MessageBox.Show(@"PURCHASE INVOICE PRODUCT ARE NOT SELECTED..!!", ObjGlobal.Caption);
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
                MessageBox.Show(@"PURCHASE INVOICE PRODUCT GROUP ARE NOT SELECTED..!!", ObjGlobal.Caption);
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
                MessageBox.Show(@"PURCHASE INVOICE PRODUCT SUB GROUP ARE NOT SELECTED..!!", ObjGlobal.Caption);
                return;
            }
        }
        else if (rChkDepartment.Checked)
        {
            using var frm2 = new FrmTagList
            {
                ReportDesc = @"DEPARTMENT",
                BranchId = ObjGlobal.SysBranchId.ToString(),
                CompanyUnitId = ObjGlobal.SysCompanyUnitId.ToString(),
                FiscalYearId = ObjGlobal.SysFiscalYearId.ToString()
            };
            frm2.ShowDialog();

            _departmentId = ClsTagList.PlValue1;
            if (string.IsNullOrEmpty(_departmentId))
            {
                MessageBox.Show(@"PURCHASE INVOICE DEPARTMENT ARE NOT SELECTED..!!", ObjGlobal.Caption);
                return;
            }
        }

        _rptDate = ObjGlobal.SysDateType.Equals("M") && ChkDateWise.Checked ||
                   ObjGlobal.SysDateType.Equals("D") && ChkDateWise.Checked
            ? $"FROM DATE {_fromBsDate} TO {_toBsDate}"
            : $"FROM DATE {Convert.ToDateTime(_fromAdDate).ToShortDateString()} TO {Convert.ToDateTime(_toAdDate).ToShortDateString()}";

        var display = new DisplayRegisterReports
        {
            RptMode = _rptMode,
            RptType = _rptType,
            RptName = _rptName,
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
            CounterId = _counterId,
            AgentId = _agentId,
            AreaId = _areaId,
            DepartmentId = _departmentId,
            FilterValue = TxtVoucherNo.Text,
            Module = _reportModule,
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
        _subLedgerId = string.Empty;
        _invoiceCategory = string.Empty;
        _getEntryUser = string.Empty;
        _areaId = string.Empty;
        _counterId = string.Empty;
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    #endregion --------------- PURCHASE REGISTER ---------------

    // METHOD FOR THIS FORM

    #region ----------------  METHOD ----------------

    private void FormControl()
    {
        Text = _reportModule switch
        {
            "PIN" => "PURCHASE INDENT REGISTER",
            "PO" => "PURCHASE ORDER REGISTER",
            "PC" => "PURCHASE CHALLAN REGISTER",
            "PR" => "PURCHASE RETURN REGISTER",
            _ => "PURCHASE INVOICE REGISTER"
        };
        ChkIncludeOrderNo.Text = _reportModule switch
        {
            "PO" => ChkIncludeOrderNo.Text,
            "PC" => ChkIncludeOrderNo.Text,
            "PR" => ChkIncludeOrderNo.Text,
            _ => ChkIncludeOrderNo.Text
        };
        ChkIncludeChallanNo.Text = _reportModule switch
        {
            "PC" => "Indent No",
            "PR" => ChkIncludeOrderNo.Text,
            _ => ChkIncludeOrderNo.Text
        };
        ChkIncludeOrderNo.Enabled = _reportModule switch
        {
            "PO" => false,
            _ => ChkIncludeOrderNo.Enabled
        };
        ChkIncludeOrderNo.Visible = ChkIncludeOrderNo.Enabled;
    }

    #endregion ----------------  METHOD ----------------

    //OBJECT FOR THIS FORM

    #region --------------- OBJECT ---------------

    private string _rptType;
    private string _rptMode;
    private string _rptName;
    private string _rptDate;
    private string _fromAdDate;
    private string _fromBsDate;
    private string _toAdDate;
    private string _toBsDate;

    private string _getEntryUser = string.Empty;
    private string _ledgerId = string.Empty;
    private string _productId = string.Empty;
    private string _groupId = string.Empty;
    private string _subGroupId = string.Empty;
    private string _departmentId = string.Empty;
    private string _agentId = string.Empty;
    private string _areaId = string.Empty;
    private string _counterId = string.Empty;
    private string _subLedgerId = string.Empty;
    private string _invoiceCategory = string.Empty;
    private readonly string _reportModule = string.Empty;

    #endregion --------------- OBJECT ---------------
}