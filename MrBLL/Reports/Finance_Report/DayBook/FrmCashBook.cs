using MrBLL.Utility.Common.Class;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using System;
using System.Windows.Forms;

namespace MrBLL.Reports.Finance_Report.DayBook;

public partial class FrmCashBook : MrForm
{
    #region -------------- CASH BOOK --------------

    public FrmCashBook()
    {
        InitializeComponent();
    }

    private void FrmCashBook_Load(object sender, EventArgs e)
    {
        ChkIsDate.Text = ObjGlobal.SysDateType == "M" ? "Date" : "Miti";
        CmbDateType.SelectedIndex = 8;
        CmbDateType.Focus();
    }

    private void FrmCashBook_KeyPress(object sender, KeyPressEventArgs e)
    {
        switch (e.KeyChar)
        {
            case (char)Keys.Escape:
            {
                if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
                {
                    Close();
                }

                break;
            }
            case (char)Keys.Enter:
            {
                SendKeys.Send("{TAB}");
                break;
            }
        }
    }

    #endregion -------------- CASH BOOK --------------

    #region -------------- EVENT CLICK --------------

    private void BtnLedger_Click(object sender, EventArgs e)
    {
        long ledgerId = 0;
        (TxtCashLedger.Text, ledgerId) = GetMasterList.GetGeneralLedger("SAVE", "CASH");
        LedgerId = ledgerId.ToString();
    }

    private void TxtCashLedger_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtCashLedger, 'E');
    }

    private void TxtCashLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnLedger_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Back)
        {
            LedgerId = string.Empty;
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                ObjGlobal.SearchText, TxtCashLedger, BtnLedger);
        }
    }

    private void TxtCashLedger_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void TxtCashLedger_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtCashLedger, 'L');
    }

    private void ChkSummary_CheckedChanged(object sender, EventArgs e)
    {
        ChkIncludeSubledger.Enabled = !ChkSummary.Checked;
        ChkIncludeSubledger.Checked = ChkIncludeSubledger.Enabled && ChkIncludeSubledger.Checked;

        ChkRemarks.Enabled = !ChkSummary.Checked;
        ChkRemarks.Checked = ChkRemarks.Enabled && ChkRemarks.Checked;

        ChkCombineSales.Enabled = !ChkSummary.Checked;
        ChkCombineSales.Checked = ChkCombineSales.Enabled && ChkCombineSales.Checked;

        ChkTFormat.Enabled = !ChkSummary.Checked;
        ChkTFormat.Checked = ChkTFormat.Enabled && ChkTFormat.Checked;
    }

    private void CmbDateType_Enter(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(CmbDateType, 'E');
    }

    private void CmbDateType_Leave(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(CmbDateType, 'L');
    }

    private void MskFrom_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskFrom, 'E');
    }

    private void MskFrom_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(MskToDate, 'L');
    }

    private void CmbDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", CmbDateType.SelectedIndex, MskFrom, MskToDate);
    }

    #endregion -------------- EVENT CLICK --------------

    #region -------------- BUTTON EVENT --------------

    private bool GetLedgerId()
    {
        LedgerId = GetMasterList.GetTagMasterList("GENERALLEDGER", "CASH");
        if (!string.IsNullOrEmpty(LedgerId)) return true;
        MessageBox.Show(@"LEDGER NOT SELECTED..!!", ObjGlobal.Caption);
        return false;
    }

    private void BtnShow_Click(object sender, EventArgs e)
    {
        if (!MskFrom.MaskCompleted)
        {
            MessageBox.Show(@"FROM DATE CAN'T BE BLANK..!!", ObjGlobal.Caption);
            MskFrom.Focus();
            return;
        }

        if (!MskToDate.MaskCompleted)
        {
            MessageBox.Show(@"FROM DATE CAN'T BE BLANK..!!", ObjGlobal.Caption);
            MskToDate.Focus();
            return;
        }

        FromADDate = ObjGlobal.SysDateType == "M" ? ObjGlobal.ReturnEnglishDate(MskFrom.Text) : MskFrom.Text;
        FromBSDate = ObjGlobal.SysDateType == "M" ? MskFrom.Text : ObjGlobal.ReturnNepaliDate(MskFrom.Text);
        ToADDate = ObjGlobal.SysDateType == "M" ? ObjGlobal.ReturnEnglishDate(MskToDate.Text) : MskToDate.Text;
        ToBSDate = ObjGlobal.SysDateType == "M" ? MskToDate.Text : ObjGlobal.ReturnNepaliDate(MskToDate.Text);

        RptDate = ChkIsDate.Checked && ChkIsDate.Text is @"Miti"
            ? $"From Date {FromBSDate} To {ToBSDate}"
            : $"From Date {FromADDate.GetDateString()} To {ToADDate.GetDateString()}";
        if (LedgerId.IsBlankOrEmpty() && !GetLedgerId())
        {
            TxtCashLedger.Focus();
            return;
        }

        var display = new DisplayFinanceReports
        {
            RptType = "NORMAL",
            RptName = "CASH/BANK BOOK",
            RptDate = RptDate,
            FromAdDate = FromADDate,
            ToAdDate = ToADDate,
            IsDetails = !ChkSummary.Checked,
            CurrencyId = ObjGlobal.SysCurrencyId.ToString(),
            IsCombineSales = ChkCombineSales.Checked,
            IsSubLedger = ChkIncludeSubledger.Checked,
            IncludeNarration = ChkNarration.Checked,
            IncludeVoucherTotal = ChkVoucherTotal.Checked,
            IncludeRemarks = ChkRemarks.Checked,
            IsDate = ChkIsDate.Checked,
            LedgerId = LedgerId,
            IsTFormat = ChkTFormat.Checked,
            BranchId = ObjGlobal.SysBranchId.ToString(),
            FiscalYearId = ObjGlobal.SysFiscalYearId.ToString()
        };
        display.Show();
        display.BringToFront();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        var dialogResult = MessageBox.Show(@"ARE YOU SURE WANT TO CLOSE FORM..??", ObjGlobal.Caption,
            MessageBoxButtons.YesNo);
        if (dialogResult == DialogResult.Yes) Close();
    }

    #endregion -------------- BUTTON EVENT --------------

    #region -------------- OBJECT --------------

    private string FromADDate;
    private string FromBSDate;
    private string LedgerId = string.Empty;
    private string Query = string.Empty;
    private string RptDate;
    private string RptName;

    private string RptType;
    private string ToADDate;
    private string ToBSDate;

    #endregion -------------- OBJECT --------------
}