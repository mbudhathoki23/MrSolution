using MrBLL.Reports.Register_Report.Vat_Report.ReportTemp;
using MrBLL.Utility.Common;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Utility.PickList;
using System;
using System.Windows.Forms;

namespace MrBLL.Reports.Register_Report.Vat_Report;

public partial class FrmSalesVatRegister : MrForm
{
    // SALES VAT REGISTER
    public FrmSalesVatRegister(string module)
    {
        InitializeComponent();
        ObjGlobal.BindDateType(CmbDateType);
        ObjGlobal.BindPeriodicDate(MskFrom, MskToDate);
        _setup = new ClsMasterSetup();
        _branchId = ObjGlobal.SysBranchId.ToString();
        _companyUnitId = ObjGlobal.SysCompanyUnitId.ToString();
        _currencyId = string.Empty;
        _fiscalYearId = ObjGlobal.SysFiscalYearId.ToString();
        _module = module;
    }

    private void SalesVatRegister_Load(object sender, EventArgs e)
    {
        Text = _module.Equals("SB") ? "SALES VAT REGISTER" : "SALES RETURN VAT REGISTER";
        TxtSalesAbove.Enabled = false;
        ChkIncludeReturn.Enabled = ChkIncludeReturn.Visible = _module.Equals("SB");
        ChkDate.Text = ObjGlobal.SysDateType switch
        {
            "M" => "Date",
            _ => "Miti"
        };
        var dtTerm = _setup.GetMasterSystemTagVatTerm("SB");
        if (dtTerm.Rows.Count > 0)
        {
            _vatTermId = Convert.ToInt32(dtTerm.Rows[0]["ST_Id"].ToString());
            TxtVatTerm.Text = dtTerm.Rows[0]["ST_Name"].ToString();
        }

        CmbDateType.SelectedIndex = 8;
        CmbDateType.Focus();
    }

    private void SalesVatRegister_KeyPress(object sender, KeyPressEventArgs e)
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

    private void MskFromDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskFrom, 'E');
    }

    private void MskFromDate_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void MskFromDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskFrom, 'L');
    }

    private void MskFromDate_Validated(object sender, EventArgs e)
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

    private void MskToDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskToDate, 'E');
    }

    private void MskToDate_KeyDown(object sender, KeyEventArgs e)
    {
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

    private void TxtVatTerm_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtVatTerm, 'E');
    }

    private void TxtVatTerm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnTerm_Click(sender, e);
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtVatTerm, btn_Term);
        }
    }

    private void TxtVatTerm_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void TxtVatTerm_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && TxtVatTerm.Focused && string.IsNullOrWhiteSpace(TxtVatTerm.Text))
        {
            MessageBox.Show(@"SALES VAT TERM IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            TxtVatTerm.Focus();
        }
    }

    private void TxtVatTerm_Validated(object sender, EventArgs e)
    {
    }

    private void TxtSalesAbove_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtSalesAbove, 'E');
    }

    private void TxtSalesAbove_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void TxtSalesAbove_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void TxtSalesAbove_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtSalesAbove, 'L');
    }

    private void BtnTerm_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetSalesTermList("SAVE", "OTHER");
        if (description.IsValueExits())
        {
            TxtVatTerm.Text = description;
            _vatTermId = id;
        }
        TxtVatTerm.Focus();
    }
    private bool GetLedgerId()
    {
        var frm2 = new FrmTagList
        {
            ReportDesc = @"GENERALLEDGER",
            Category = "Customer",
            BranchId = _branchId,
            CompanyUnitId = _companyUnitId,
            FiscalYearId = _fiscalYearId,
        };
        frm2.ShowDialog();

        _ledgerId = ClsTagList.PlValue1;
        if (!string.IsNullOrEmpty(_ledgerId))
        {
            return true;
        }
        MessageBox.Show(@"LEDGER NOT SELECTED..!!", ObjGlobal.Caption);
        return false;
    }
    private void BtnShow_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TxtVatTerm.Text))
        {
            MessageBox.Show(@"SALES VAT TERM IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtVatTerm.Focus();
            return;
        }

        _rptType = "Normal";
        _fromAdDate = ObjGlobal.SysDateType switch
        {
            "M" => ObjGlobal.ReturnEnglishDate(MskFrom.Text),
            _ => MskFrom.Text
        };
        _fromBsDate = ObjGlobal.SysDateType switch
        {
            "M" => MskFrom.Text,
            _ => ObjGlobal.ReturnNepaliDate(MskFrom.Text)
        };
        _toAdDate = ObjGlobal.SysDateType switch
        {
            "M" => ObjGlobal.ReturnEnglishDate(MskToDate.Text),
            _ => MskToDate.Text
        };
        _toBsDate = ObjGlobal.SysDateType switch
        {
            "M" => MskToDate.Text,
            _ => ObjGlobal.ReturnNepaliDate(MskToDate.Text)
        };

        _reportMode = rBtnNormal.Checked ? "DATE WISE"
            : rBtnCustomer.Checked ? "CUSTOMER WISE"
            : rbtnVoucherWise.Checked ? "VOUCHER WISE"
            : rBtnProductWise.Checked ? "PRODUCT WISE"
            : rBtnMonthly.Checked ? "MONTH WISE"
            : "NORMAL";

        _rptDate = ObjGlobal.SysDateType == "M" && ChkDate.Checked == false ||
                   ObjGlobal.SysDateType == "D" && ChkDate.Checked
            ? $"From Date {_fromBsDate} To {_toBsDate}"
            : $"From Date {Convert.ToDateTime(_fromAdDate).ToShortDateString()} To {Convert.ToDateTime(_toAdDate).ToShortDateString()}";
        if (rBtnCustomer.Checked)
        {
            GetLedgerId();
        }
        if (ChkDaynamicReport.Checked)
        {
            var result = new FrmDevReportGenerator("SALES_VAT_REGISTER_DATE_WISE")
            {
                VatTermId = [_vatTermId.ToString()],
                FromDate = _fromAdDate,
                ToDate = _toAdDate,
            };
            result.ShowDialog();
        }
        else
        {
            var dr = new DisplayRegisterReports
            {
                RptName = @"SALES VAT REGISTER",
                RptType = _rptType,
                RptMode = _reportMode,
                FromAdDate = _fromAdDate,
                ToAdDate = _toAdDate,
                RptDate = _rptDate,
                LedgerId = _ledgerId,
                IsDate = ChkDate.Checked,
                NepaliColumn = ChkNepaliDesc.Checked,
                IsSummary = ChkSummary.Checked,
                IncludeSalesReturn = ChkIncludeReturn.Checked,
                FilterValue = TxtSalesAbove.Text,
                Module = _module,
                VatTermId =
                [
                    _vatTermId.ToString(),
                    0.ToString()
                ]
            };
            dr.Show();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void CmbSysDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", CmbDateType.SelectedIndex, MskFrom, MskToDate);
    }

    private void ChkSummary_CheckedChanged(object sender, EventArgs e)
    {
        if (rBtnCustomer.Checked)
        {
            TxtSalesAbove.Enabled = ChkSummary.Checked;
        }
        if (!ChkSummary.Checked)
        {
            TxtSalesAbove.Clear();
        }
    }

    // OBJECT FOR THIS FORM
    private int _vatTermId;

    private string _module;
    private string _fromAdDate;
    private string _fromBsDate;
    private string _reportMode = string.Empty;
    private string _rptDate;
    private string _rptType;
    private string _toAdDate;
    private string _toBsDate;
    private string _ledgerId;
    private readonly IMasterSetup _setup;
    private string _branchId;
    private string _companyUnitId;
    private string _currencyId;
    private string _fiscalYearId;
}