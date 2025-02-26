using MrBLL.Utility.Common.Class;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;
using System.Windows.Forms;

namespace MrBLL.Reports.Register_Report.Vat_Report;

public partial class FrmPurchaseVatRegister : MrForm
{
    public FrmPurchaseVatRegister(string module)
    {
        InitializeComponent();
        ObjGlobal.BindDateType(CmbDateType);
        ObjGlobal.BindPeriodicDate(MskFrom, MskToDate);
        _module = module;
    }

    private void PurchaseVatRegister_Load(object sender, EventArgs e)
    {
        Text = _module.Equals("PB") ? "PURCHASE VAT REGISTER" : "PURCHASE RETURN VAT REGISTER";
        TxtPurchaseAbove.Enabled = false;
        ChkIncludeReturn.Enabled = ChkIncludeReturn.Visible = _module.Equals("PB");
        lbl_SalesAbove.Text = _reportMode.Equals("PB") ? "Purchase Above" : "Return Above";
    }

    private void FrmPurchaseVatRegister_Shown(object sender, EventArgs e)
    {
        var dtTerm = _masterSetup.GetMasterSystemTagVatTerm("PB");
        if (dtTerm.Rows.Count > 0)
        {
            _vatTermId = Convert.ToInt32(dtTerm.Rows[0]["PT_Id"].ToString());
            TxtVatTerm.Text = dtTerm.Rows[0]["PT_Name"].ToString();
        }
        ChkDate.Text = ObjGlobal.SysDateType switch
        {
            "M" => "Date",
            _ => "Miti"
        };
        CmbDateType.SelectedIndex = 8;
        CmbDateType.Focus();
    }

    private void PurchaseVatRegister_KeyPress(object sender, KeyPressEventArgs e)
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

    private void CmbSysDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", CmbDateType.SelectedIndex, MskFrom, MskToDate);
    }

    private void BtnShow_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TxtVatTerm.Text))
        {
            CustomMessageBox.Warning("PURCHASE VAT TERM IS BLANK..!!");
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

        _reportMode = rbtnVoucherWise.Checked ? "VOUCHER WISE"
            : rBtnProductWise.Checked ? "PRODUCT WISE"
            : rBtnMonthly.Checked ? "MONTH WISE"
            : rBtnCustomer.Checked ? "VENDOR WISE"
            : "DATE WISE";

        _rptDate = ObjGlobal.SysDateType == "M" && ChkDate.Checked == false ||
                   ObjGlobal.SysDateType == "D" && ChkDate.Checked
            ? $"FROM DATE {_fromBsDate} TO {_toBsDate}"
            : $"FROM DATE {_fromAdDate.GetDateString()} TO {_toAdDate.GetDateString()}";

        var dr = new DisplayRegisterReports
        {
            RptName = @"PURCHASE VAT REGISTER",
            RptType = _rptType,
            RptMode = _reportMode,
            FromAdDate = _fromAdDate,
            ToAdDate = _toAdDate,
            RptDate = _rptDate,
            IsDate = ChkDate.Checked,
            NepaliColumn = ChkNepaliDesc.Checked,
            IsSummary = ChkSummary.Checked,
            FilterValue = TxtPurchaseAbove.Text,
            Module = _module,
            IncludeSalesReturn = ChkIncludeReturn.Checked,
            VatTermId =
            [
                0.ToString(), _vatTermId.ToString()
            ]
        };
        dr.Show();
    }

    private void BtnTerm_Click(object sender, EventArgs e)
    {
        (TxtVatTerm.Text, _vatTermId) = GetMasterList.GetPurchaseTermList("SAVE", "");
        TxtVatTerm.Focus();
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

    private void MskToDate_Validated(object sender, EventArgs e)
    {
        if (ActiveControl != null && MskToDate.MaskFull)
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

    private void TxtVatTerm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnTerm_Click(sender, e);
        }
    }

    private void TxtSalesAbove_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void TxtSalesAbove_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void ChkSummary_CheckedChanged(object sender, EventArgs e)
    {
        if (rBtnCustomer.Checked)
        {
            TxtPurchaseAbove.Enabled = ChkSummary.Checked;
        }

        if (!ChkSummary.Checked)
        {
            TxtPurchaseAbove.Clear();
        }
    }

    // OBJECT FOR THIS FORM
    private int _vatTermId;

    private string _fromAdDate;
    private string _fromBsDate;
    private string _module;
    private string _reportMode = string.Empty;
    private string _rptDate;
    private string _rptType;
    private string _toAdDate;
    private string _toBsDate;

    private readonly IMasterSetup _masterSetup = new ClsMasterSetup();
}