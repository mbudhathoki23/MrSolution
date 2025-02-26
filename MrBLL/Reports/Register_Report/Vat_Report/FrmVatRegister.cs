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

public partial class FrmVatRegister : MrForm
{
    #region --------------- VAT REGISTER ---------------

    public FrmVatRegister()
    {
        InitializeComponent();
        ObjGlobal.BindDateType(CmbDateType);
        ObjGlobal.BindPeriodicDate(MskFrom, MskToDate);
        ChkDate.Text = ObjGlobal.SysDateType switch
        {
            "M" => "Date",
            _ => "Miti"
        };
        var dtPurchaseTerm = _masterSetup.GetMasterSystemTagVatTerm("PB");
        if (dtPurchaseTerm.Rows.Count > 0)
        {
            _purchaseVatTermId = dtPurchaseTerm.Rows[0]["PT_Id"].GetInt();
            TxtPurchaseVatTerm.Text = dtPurchaseTerm.Rows[0]["PT_Name"].ToString();
        }
        var dtSalesTerm = _masterSetup.GetMasterSystemTagVatTerm("SB");
        if (dtSalesTerm.Rows.Count > 0)
        {
            _salesVatTermId = dtSalesTerm.Rows[0]["ST_Id"].GetInt();
            TxtSalesVatTerm.Text = dtSalesTerm.Rows[0]["ST_Name"].ToString();
        }
        CmbDateType.SelectedIndex = 8;
    }

    private void FrmVatRegister_Load(object sender, EventArgs e)
    {
        CmbSysDateType_SelectedIndexChanged(this, EventArgs.Empty);
        RBtnNormal_CheckedChanged(this, EventArgs.Empty);
        ChkTransactionAbove_CheckedChanged(this, EventArgs.Empty);
        CmbDateType.Focus();
    }

    private void FrmVatRegister_KeyPress(object sender, KeyPressEventArgs e)
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
                return;
            }
        }
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

    private void TxtSalesVatTerm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnSalesTerm_Click(sender, e);
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtSalesVatTerm, BtnSalesTerm);
        }
    }

    private void BtnSalesTerm_Click(object sender, EventArgs e)
    {
        (TxtSalesVatTerm.Text, _salesVatTermId) = GetMasterList.GetSalesTermList("SAVE", string.Empty);
        TxtSalesVatTerm.Focus();
    }

    private void TxtPurchaseVatTerm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnPurchaseTerm_Click(sender, e);
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtPurchaseVatTerm, BtnPurchaseTerm);
        }
    }

    private void BtnPurchaseTerm_Click(object sender, EventArgs e)
    {
        (TxtPurchaseVatTerm.Text, _purchaseVatTermId) = GetMasterList.GetPurchaseTermList("SAVE", string.Empty);
        TxtPurchaseVatTerm.Focus();
    }

    private void BtnShow_Click(object sender, EventArgs e)
    {
        if (TxtSalesVatTerm.IsBlankOrEmpty())
        {
            CustomMessageBox.Warning(@"SALES VAT TERM IS BLANK..!!");
            TxtSalesVatTerm.Focus();
            return;
        }
        if (TxtPurchaseVatTerm.IsBlankOrEmpty())
        {
            CustomMessageBox.Warning(@"PURCHASE VAT TERM IS BLANK..!!");
            TxtPurchaseVatTerm.Focus();
            return;
        }
        _rptType = "NORMAL";
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

        _reportMode = "NORMAL";

        _rptDate = ObjGlobal.SysDateType == "M" && ChkDate.Checked == false || ObjGlobal.SysDateType == "D" && ChkDate.Checked
            ? $"From Date {_fromBsDate} To {_toBsDate}"
            : $"From Date {Convert.ToDateTime(_fromAdDate).ToShortDateString()} To {Convert.ToDateTime(_toAdDate).ToShortDateString()}";

        var dr = new DisplayRegisterReports
        {
            RptName = @"VAT REGISTER",
            RptType = _rptType,
            RptMode = _reportMode,
            FromAdDate = _fromAdDate,
            ToAdDate = _toAdDate,
            RptDate = _rptDate,
            IsDate = ChkDate.Checked,
            NepaliColumn = ChkNepaliDesc.Checked,
            IsSummary = ChkTransactionAbove.Checked,
            IncludeSalesReturn = ChkIncludeReturn.Checked,
            FilterValue = string.Empty,
            Module = _module,
            VatTermId =
            [
                _salesVatTermId.ToString(),
                _purchaseVatTermId.ToString()
            ]
        };
        dr.Show();
        dr.BringToFront();
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
        return;
    }

    private void CmbSysDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", CmbDateType.SelectedIndex, MskFrom, MskToDate);
    }

    private void RBtnNormal_CheckedChanged(object sender, EventArgs e)
    {
        ChkTransactionAbove.Enabled = rBtnNormal.Checked;
        if (ChkTransactionAbove.Checked && !rBtnNormal.Checked)
        {
            ChkTransactionAbove.Checked = rBtnNormal.Checked;
        }
    }

    private void ChkTransactionAbove_CheckedChanged(object sender, EventArgs e)
    {
        TxtTransactionAboveAbove.Enabled = ChkTransactionAbove.Checked;
    }

    #endregion --------------- VAT REGISTER ---------------

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT FOR THIS FORM ---------------

    private int _salesVatTermId;
    private int _purchaseVatTermId;
    private string _fromAdDate;
    private string _fromBsDate;
    private string _module;
    private string _reportMode = string.Empty;
    private string _rptDate;
    private string _rptType;
    private string _toAdDate;
    private string _toBsDate;
    private readonly IMasterSetup _masterSetup = new ClsMasterSetup();

    #endregion --------------- OBJECT FOR THIS FORM ---------------
}