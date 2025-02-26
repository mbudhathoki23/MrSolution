using DevExpress.XtraEditors;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.FinanceTransaction.DayClosing;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.Interface.FinanceTransaction.DayClosing;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Utility.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PrintControl.PrintClass.DirectPrint;

namespace MrBLL.DataEntry.Common;

public partial class FrmCashClosing : MrForm
{
    // CASH CLOSING

    #region --------------- Form ---------------

    public FrmCashClosing()
    {
        InitializeComponent();
    }

    private void FrmCashClosing_Load(object sender, EventArgs e)
    {
        var table = new DataTable();
        _userId = ObjGlobal.LogInUserId;
        table = table.GetLoginUserInfo();
        if (table.Rows.Count > 0)
        {
            TxtLoginUser.Text = table.Rows[0]["User_Name"].ToString();
            _ledgerId = table.Rows[0]["Ledger_Id"].GetLong();
        }

        table = table.GetLoginUserInfo(true);
        if (table.Rows.Count > 0)
        {
            CmbTransferUser.DataSource = table;
            CmbTransferUser.DisplayMember = "User_Name";
            CmbTransferUser.ValueMember = "User_Id";
            CmbTransferUser.SelectedIndex = 0;
        }
        CmbType.SelectedIndex = 0;
        EnableControl();
        ClearControl();

        _ledgerId = _ledgerId is 0 ? ObjGlobal.FinanceCashLedgerId : _ledgerId;
        if (_ledgerId != 0)
        {
            LedgerCurrentBalance(_ledgerId.ToString());
        }
        BindDayBookReport();
        BindSalesType();
        btnNew.Focus();
        ObjGlobal.GetFormAccessControl([btnNew, BtnView], this.Tag);
    }

    private void FrmCashClosing_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)27)
        {
            if (btnNew.Enabled == false)
            {
                if (CustomMessageBox.ClearVoucherDetails("CASH DENOMINATION") == DialogResult.Yes)
                {
                    _actionTag = string.Empty;
                    EnableControl();
                    ClearControl();
                    btnNew.Focus();
                }
            }
            else
            {
                if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
                {
                    Close();
                }
            }
        }
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        EnableControl(true, false);
        ReturnVoucherNumber();
        ClearControl();
        btnNew.Enabled = false;
        CmbType.Focus();
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        var amount = lblTotalAmount.Text.GetDecimal();
        if (amount > 0)
        {
            if (IudCashClosing() <= 0)
            {
                return;
            }
            IudCashBankVoucherDetails();
            if (_actionTag == "SAVE")
            {
                var cmdString = @$" UPDATE AMS.DocumentNumbering SET DocCurr=isNull(DocCurr,0)+1  WHERE DocModule='CV' AND  DocDesc='{_docDesc}'";
                var update = SqlExtensions.ExecuteNonQuery(cmdString);
            }
            ClearControl();
            var result = "DO YOU WANT TO PRINT DAY CLOSING..??";
            if (CustomMessageBox.Question(result) == DialogResult.Yes)
            {
                var data = GetConnection.GetQueryData("SELECT Max(CC_ID) FROM AMS.CashClosing");
                new PrintSalesBill
                {
                    BillNo = data,
                    Printdesign = "DayClosing",
                    PrintedBy = ObjGlobal.LogInUser,
                    NoofPrint = 1
                }.PrintDocumentDesign();
                Close();
            }
            CustomMessageBox.Information("DAY CLOSING SAVE SUCCESSFULLY..??");
        }
        else
        {
            MessageBox.Show(@"Cash Cannot Be Zero..!!", ObjGlobal.Caption);
            Txt1000.Focus();
        }
    }

    private void Txt1000_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(Txt1000, 'E');
    }

    private void Txt1000_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(Txt1000, 'L');
        Txt1000.Text = Txt1000.GetDecimalString();
        Txt1000Value.Text = Txt1000Value.GetDecimalString();
    }

    private void Txt1000_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) Txt500.Focus();
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains("."))
        {
            return;
        }
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void Txt1000_TextChanged(object sender, EventArgs e)
    {
        Txt1000Value.Text = (lbl1000.GetDecimal() * Txt1000.GetDecimal()).GetDecimalString();
        TotalCalc();
    }

    private void Txt500_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(Txt500, 'E');
    }

    private void Txt500_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(Txt500, 'L');
        Txt500.Text = Txt500.GetDecimalString();
        Txt500Value.Text = Txt500Value.GetDecimalString();
    }

    private void Txt500_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) Txt100.Focus();
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains("."))
        {
            return;
        }
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void Txt500_TextChanged(object sender, EventArgs e)
    {
        Txt500Value.Text = (lbl500.GetDecimal() * Txt500.GetDecimal()).GetDecimalString();
        TotalCalc();
    }

    private void Txt100_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(Txt100, 'E');
    }

    private void Txt100_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(Txt100, 'L');
        Txt100.Text = Txt100.GetDecimalString();
        Txt100Value.Text = Txt100Value.GetDecimalString();
    }

    private void Txt100_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) Txt50.Focus();
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void Txt100_TextChanged(object sender, EventArgs e)
    {
        Txt100Value.Text = (lbl100.GetDecimal() * Txt100.GetDecimal()).GetDecimalString();
        TotalCalc();
    }

    private void Txt50_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(Txt50, 'E');
    }

    private void Txt50_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(Txt50, 'L');
        Txt50.Text = Txt50.GetDecimalString();
        Txt50Value.Text = Txt50Value.GetDecimalString();
    }

    private void Txt50_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) Txt25.Focus();
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains("."))
        {
            return;
        }
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void Txt50_TextChanged(object sender, EventArgs e)
    {
        Txt50Value.Text =
            (lbl50.GetDecimal() * Txt50.GetDecimal()).GetDecimalString();
        TotalCalc();
    }

    private void Txt25_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(Txt25, 'E');
    }

    private void Txt25_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(Txt25, 'L');
        Txt25.Text = Txt25.GetDecimalString();
        Txt25Value.Text = Txt25Value.GetDecimalString();
    }

    private void Txt25_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            Txt20.Focus();
        }
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains("."))
        {
            return;
        }
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void Txt25_TextChanged(object sender, EventArgs e)
    {
        Txt25Value.Text = (lbl25.GetDecimal() * Txt25.GetDecimal()).GetDecimalString();
        TotalCalc();
    }

    private void Txt20_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(Txt20, 'E');
    }

    private void Txt20_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(Txt20, 'L');
        Txt20.Text = Txt20.GetDecimalString();
        Txt20Value.Text = Txt20Value.GetDecimalString();
    }

    private void Txt20_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) Txt10.Focus();
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains("."))
        {
            return;
        }
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void Txt20_TextChanged(object sender, EventArgs e)
    {
        Txt20Value.Text = (lbl20.GetDecimal() * Txt20.GetDecimal()).GetDecimalString();
        TotalCalc();
    }

    private void Txt10_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(Txt10, 'E');
    }

    private void Txt10_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(Txt10, 'L');
        Txt10.Text = Txt10.GetDecimalString();
        Txt10Value.Text = Txt10Value.GetDecimalString();
    }

    private void Txt10_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) Txt5.Focus();
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains("."))
        {
            return;
        }
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void Txt10_TextChanged(object sender, EventArgs e)
    {
        Txt10Value.Text = (lbl10.GetDecimal() * Txt10.GetDecimal()).GetDecimalString();
        TotalCalc();
    }

    private void Txt5_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(Txt5, 'E');
    }

    private void Txt5_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(Txt5, 'L');
        Txt5.Text = Txt5.GetDecimalString();
        Txt5Value.Text = Txt5Value.GetDecimalString();
    }

    private void Txt5_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            Txt2.Focus();
        }
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains("."))
        {
            return;
        }
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void Txt5_TextChanged(object sender, EventArgs e)
    {
        Txt5Value.Text = (lbl5.GetDecimal() * Txt5.GetDecimal()).GetDecimalString();
        TotalCalc();
    }

    private void Txt2_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(Txt2, 'E');
    }

    private void Txt2_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(Txt2, 'L');
        Txt2.Text = Txt2.GetDecimalString();
        Txt2Value.Text = Txt2Value.GetDecimalString();
    }

    private void Txt2_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) Txt1.Focus();
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains("."))
        {
            return;
        }
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void Txt2_TextChanged(object sender, EventArgs e)
    {
        Txt2Value.Text = (lbl2.GetDecimal() * Txt2.GetDecimal()).GetDecimalString();
        TotalCalc();
    }

    private void Txt1_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(Txt1, 'E');
    }

    private void Txt1_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(Txt1, 'L');
        Txt1.Text = Txt1.GetDecimalString();
        Txt1Value.Text = Txt1Value.GetDecimalString();
    }

    private void Txt1_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) btnSave.Focus();
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void Cmb_Mode_Enter(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(CmbType, 'E');
    }

    private void Cmb_Mode_Leave(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(CmbType, 'L');
    }

    private void Cmb_Mode_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) CmbTransferUser.Focus();
    }

    private void cmb_User_Enter(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(CmbTransferUser, 'E');
    }

    private void Cmb_User_Leave(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(CmbTransferUser, 'L');
    }

    private void Cmb_User_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            Txt1000.Focus();
        }
    }

    private void Txt1_TextChanged(object sender, EventArgs e)
    {
        Txt1Value.Text = (lbl1.GetDecimal() * Txt1.GetDecimal()).GetDecimalString();
        TotalCalc();
    }

    private void LblTotalAmount_Click(object sender, EventArgs e)
    {
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList("MAX", "TSALES", ObjGlobal.SearchText, _actionTag, "Normal", "TRANSACTION");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"USER SALES REPORT NOT FOUNDS..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        ObjGlobal.SearchText = string.Empty;
    }

    #endregion --------------- Form ---------------

    #region --------------- METHOD  ---------------

    private void LedgerCurrentBalance(string ledgerId)
    {
        _dtTemp.Clear();
        _query = @$"
            SELECT SUM(sm.LN_Amount)-ISNULL(s.ReturnAmount, 0) SalesAmount
            FROM AMS.SB_Master sm
                 LEFT OUTER JOIN(SELECT Customer_Id LedgerId, SUM(LN_Amount) ReturnAmount
                                 FROM AMS.SR_Master sr
                                 WHERE sr.Enter_By='{TxtLoginUser.Text}' AND sr.Payment_Mode='CASH' AND sr.Invoice_Date='{DateTime.Now.GetSystemDate()}' and sr.CBranch_Id = {ObjGlobal.SysBranchId}
                                 GROUP BY sr.Customer_Id) AS s ON s.LedgerId=sm.Customer_Id
            WHERE sm.Enter_By='{TxtLoginUser.Text}' AND sm.Payment_Mode='CASH' AND sm.Invoice_Date='{DateTime.Now.GetSystemDate()}' AND sm.CBranch_Id = {ObjGlobal.SysBranchId}
            GROUP BY sm.Customer_Id, ISNULL(s.ReturnAmount, 0);";
        LblTotalSales.Text = GetConnection.GetQueryData(_query).GetDecimalString();

        _query = $@"
            SELECT ISNULL((SUM(LocalDebit_Amt)-SUM(LocalCredit_Amt)), 0) AS Amount
            FROM AMS.AccountDetails
            WHERE Ledger_Id={ledgerId} AND Branch_ID={ObjGlobal.SysBranchId};";
        _dtTemp = _query.GetQueryDataTable();
        if (_dtTemp.Rows.Count > 0)
        {
            if (_dtTemp.Rows[0]["Amount"] != null)
            {
                var stringAmount = _dtTemp.Rows[0]["Amount"].ToString();
                decimal.TryParse(stringAmount, out var amount);
                LblLedgerCash.Text = amount switch
                {
                    > 0 => $@"{amount.GetDecimalString()} Dr",
                    < 0 => $@"{amount.GetDecimalString()} Cr",
                    _ => $@"{0.GetDecimalString()}"
                };
            }
            else
            {
                LblLedgerCash.Text = $@"{0.GetDecimalString()}";
            }
        }
        else
        {
            LblLedgerCash.Text = $@"{0.GetDecimalString()}";
        }
    }

    private void ClearControl()
    {
        IList list = PanelHeader.Controls;
        foreach (var t in list)
        {
            var control = (Control)t;
            if (control is not TextBox)
            {
                continue;
            }
            control.Text = string.Empty;
            control.BackColor = SystemColors.Window;
        }
        TxtLoginUser.Text = ObjGlobal.LogInUser;
        LedgerCurrentBalance(_ledgerId.ToString());
    }

    private void EnableControl(bool Txt = false, bool Btn = true)
    {
        CmbTransferUser.Enabled = CmbType.Enabled = Txt;
        Txt1.Enabled = Txt1Value.Enabled = Txt;
        Txt2.Enabled = Txt2Value.Enabled = Txt;
        Txt5.Enabled = Txt5Value.Enabled = Txt;
        Txt10.Enabled = Txt10Value.Enabled = Txt;
        Txt20.Enabled = Txt20Value.Enabled = Txt;
        Txt25.Enabled = Txt25Value.Enabled = Txt;
        Txt50.Enabled = Txt50Value.Enabled = Txt;
        Txt100.Enabled = Txt100Value.Enabled = Txt;
        Txt500.Enabled = Txt500Value.Enabled = Txt;
        Txt1000.Enabled = Txt1000Value.Enabled = Txt;

        btnNew.Enabled = Btn;
    }

    private void TotalCalc()
    {
        try
        {
            var thousand = Txt1000Value.GetDecimal();
            var fiveHundred = Txt500Value.GetDecimal();
            var hundred = Txt100Value.GetDecimal();
            var fifty = Txt50Value.GetDecimal();
            var twentyFive = Txt25Value.GetDecimal();
            var twenty = Txt20Value.GetDecimal();
            var ten = Txt10Value.GetDecimal();
            var five = Txt5Value.GetDecimal();
            var two = Txt2Value.GetDecimal();
            var one = Txt1Value.GetDecimal();

            var amount = thousand + fiveHundred;
            amount += hundred + fifty;
            amount += twentyFive + twenty;
            amount += ten + five;
            amount += two + one;

            lblTotalAmount.Text = amount.GetDecimalString();
        }
        catch
        {
            // ignored
        }
    }

    private int IudCashClosing()
    {
        _entry.CashMaster.OneQty = Txt1.GetDecimal();
        _entry.CashMaster.OneVal = Txt1Value.GetDecimal();

        _entry.CashMaster.TwoQty = Txt2.GetDecimal();
        _entry.CashMaster.TwoVal = Txt2Value.GetDecimal();

        _entry.CashMaster.FiveQty = Txt5.GetDecimal();
        _entry.CashMaster.FiveVal = Txt5Value.GetDecimal();

        _entry.CashMaster.TenQty = Txt10.GetDecimal();
        _entry.CashMaster.TenVal = Txt10Value.GetDecimal();

        _entry.CashMaster.TwentyQty = Txt20.GetDecimal();
        _entry.CashMaster.TwentyVal = Txt20Value.GetDecimal();

        _entry.CashMaster.TwenteyFiveQty = Txt25.GetDecimal();
        _entry.CashMaster.TwentyFiveVal = Txt25Value.GetDecimal();

        _entry.CashMaster.FiFtyQty = Txt50.GetDecimal();
        _entry.CashMaster.FiftyVal = Txt50Value.GetDecimal();

        _entry.CashMaster.HunQty = Txt100.GetDecimal();
        _entry.CashMaster.HunVal = Txt100Value.GetDecimal();

        _entry.CashMaster.FivHunQty = Txt500.GetDecimal();
        _entry.CashMaster.FivHunVal = Txt500Value.GetDecimal();

        _entry.CashMaster.ThauQty = Txt1000.GetDecimal();
        _entry.CashMaster.ThouVal = Txt1000Value.GetDecimal();

        _entry.CashMaster.TotalCash = LblLedgerCash.GetDecimal();
        _entry.CashMaster.Cash_Diff = Math.Abs(LblLedgerCash.GetDecimal() - LblTotalSales.GetDecimal());

        _entry.CashMaster.HandOverUser = CmbTransferUser.Text.Trim();

        _entry.CashMaster.EnterBy = TxtLoginUser.Text.Trim();
        _entry.CashMaster.EnterMiti = DateTime.Now.GetNepaliDate();
        _entry.CashMaster.Module = CmbType.Text.Trim();
        return _entry.SaveCashClosing(_actionTag);
    }

    private string ReturnVoucherNumber()
    {
        var voucherNo = new TextBox();
        var numbering = _cashClosingRepository.IsExitsCheckDocumentNumbering("CV");
        if (numbering?.Rows.Count is 1)
        {
            _docDesc = numbering.Rows[0]["DocDesc"].ToString();
            voucherNo.Text = voucherNo.GetCurrentVoucherNo("CV", _docDesc);
        }
        else if (numbering is { Rows: { Count: > 1 } })
        {
            using var wnd = new FrmNumberingScheme("CV", "AMS.CB_Master", "Voucher_No");
            if (wnd.ShowDialog() != DialogResult.OK || wnd.VNo.IsBlankOrEmpty())
            {
                return string.Empty;
            }
            _docDesc = wnd.Description;
            voucherNo.Text = wnd.VNo;
        }

        return voucherNo.Text;
    }

    private int IudCashBankVoucherDetails()
    {
        try
        {
            var txtVno = ReturnVoucherNumber();
            var dpDate = new MaskedTextBox();
            var mskMiti = new MaskedTextBox();
            if (txtVno.IsBlankOrEmpty()) return 0;

            var cmdString = new StringBuilder();
            var SLedgerId = _cashClosingRepository.GetUserLedgerIdFromUser(TxtLoginUser.Text);
            var TLedgerId = _cashClosingRepository.GetUserLedgerIdFromUser(CmbTransferUser.Text);

            dpDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            mskMiti.Text = dpDate.Text;
            mskMiti.Text = mskMiti.GetNepaliDate(dpDate.Text);
            _entry.CbMaster.VoucherMode = CmbType.Text.ToUpper().Equals("PAYMENT") ? "PV" : "RV";
            _entry.CbMaster.Voucher_No = txtVno;
            _entry.CbMaster.Voucher_Date = dpDate.Text.GetDateTime();
            _entry.CbMaster.Voucher_Miti = mskMiti.Text;
            _entry.CbMaster.Voucher_Time = DateTime.Now;
            _entry.CbMaster.Ref_VNo = string.Empty;
            _entry.CbMaster.Ref_VDate = DateTime.Now;
            _entry.CbMaster.VoucherType = CmbType.Text.ToUpper();
            _entry.CbMaster.Ledger_Id = SLedgerId > 0 ? SLedgerId : ObjGlobal.FinanceCashLedgerId.GetLong();
            _entry.CbMaster.CheqNo = string.Empty;
            _entry.CbMaster.CheqDate = DateTime.Now;
            _entry.CbMaster.CheqMiti = string.Empty;
            _entry.CbMaster.Currency_Id = ObjGlobal.SysCurrencyId;
            _entry.CbMaster.Currency_Rate = 1;
            _entry.CbMaster.Cls1 = 0;
            _entry.CbMaster.Remarks = $@"BEING CASH TRANSFER FROM {TxtLoginUser.Text.ToUpper()} TO {CmbTransferUser.Text}";
            _entry.CbMaster.Action_Type = "SAVE";
            _entry.CbMaster.EnterBy = ObjGlobal.LogInUser;
            _entry.CbMaster.SyncRowVersion = 1;
            _entry.CbMaster.In_Words = ClsMoneyConversion.MoneyConversion(lblTotalAmount.Text);

            _entry.CbDetails.Voucher_No = txtVno;
            _entry.CbDetails.SNo = 1;
            _entry.CbDetails.Ledger_Id = TLedgerId > 0 ? TLedgerId : ObjGlobal.FinanceCashLedgerId.GetLong();
            _entry.CbDetails.Subledger_Id = 0;
            _entry.CbDetails.Agent_Id = 0;
            _entry.CbDetails.Cls1 = 0;

            if (CmbType.Text == @"Payment")
            {
                _entry.CbDetails.Debit = lblTotalAmount.Text.GetDecimal();
                _entry.CbDetails.LocalDebit = lblTotalAmount.Text.GetDecimal();
                _entry.CbDetails.Credit = 0;
                _entry.CbDetails.LocalCredit = 0;
            }
            else if (CmbType.Text == @"Receipt")
            {
                _entry.CbDetails.Credit = lblTotalAmount.Text.GetDecimal();
                _entry.CbDetails.LocalCredit = lblTotalAmount.Text.GetDecimal();
                _entry.CbDetails.Debit = 0;
                _entry.CbDetails.LocalDebit = 0;
            }

            _entry.CbDetails.Narration = string.Empty;
            _entry.CbDetails.Tbl_Amount = 0;
            _entry.CbDetails.V_Amount = 0;
            _entry.CbDetails.Party_No = string.Empty;
            _entry.CbDetails.Invoice_Date = DateTime.Now;
            _entry.CbDetails.Invoice_Miti = string.Empty;
            _entry.CbDetails.VatLedger_Id = 0;
            _entry.CbDetails.PanNo = 0;
            _entry.CbDetails.Vat_Reg = false;
            _entry.CbDetails.CBLedgerId = SLedgerId > 0 ? SLedgerId : ObjGlobal.FinanceCashLedgerId.GetInt();
            _entry.CbDetails.CurrencyId = ObjGlobal.SysCurrencyId;
            _entry.CbDetails.CurrencyRate = 1;
            _entry.CbDetails.SyncRowVersion = 1;
            return _entry.SaveCashBankVoucher(_actionTag);
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return Result1;
        }
    }

    private void BindDayBookReport()
    {
        RGrid.AutoGenerateColumns = false;
        var cmdString = $@"
            WITH DAY_CASH AS
            (
                SELECT ad.Voucher_Miti VoucherMiti, ad.Voucher_No VoucherNo, ad.Ledger_ID, gl.GLName Ledger, ad.LocalCredit_Amt Receipt, ad.LocalDebit_Amt Payment
                FROM AMS.AccountDetails           ad
                      INNER JOIN AMS.GeneralLedger gl ON gl.GLID = ad.Ledger_ID
                WHERE ad.Module = 'CB' AND ad.Ledger_ID = {_ledgerId} AND ad.Branch_ID = {ObjGlobal.SysBranchId} AND ad.Voucher_Date = '{DateTime.Now:yyyy-MM-dd}' AND ad.FiscalYearId = {ObjGlobal.SysFiscalYearId} AND ad.EnterBy = '{ObjGlobal.LogInUser}'
                UNION ALL
                SELECT '{DateTime.Now.GetNepaliDate()}' VoucherMiti,CASE WHEN ad.Module = 'SR' THEN 'RETURN' ELSE 'TODAYS_SALES' END VoucherNo, ad.Ledger_ID, gl.GLName Ledger,  SUM(ad.LocalDebit_Amt) Receipt,SUM (ad.LocalCredit_Amt)  Payment
                FROM AMS.AccountDetails           ad
                      INNER JOIN AMS.GeneralLedger gl ON gl.GLID = ad.Ledger_ID
                WHERE ad.Module IN ('SB','SR') AND ad.Ledger_ID = {_ledgerId} AND ad.Branch_ID = {ObjGlobal.SysBranchId} AND ad.Voucher_Date = '{DateTime.Now:yyyy-MM-dd}' AND ad.FiscalYearId = {ObjGlobal.SysFiscalYearId} AND ad.EnterBy = '{ObjGlobal.LogInUser}'
                GROUP BY ad.Ledger_ID, gl.GLName,ad.Module
             )
             SELECT dc.VoucherMiti, 'TOTAL_CASH' VoucherNo, dc.Ledger_ID, dc.Ledger,FORMAT(SUM(dc.Receipt),'{ObjGlobal.SysAmountCommaFormat}') Receipt,FORMAT(SUM( dc.Payment),'{ObjGlobal.SysAmountCommaFormat}') Payment,0 IsGroup
              FROM DAY_CASH dc
              GROUP BY dc.VoucherMiti,dc.Ledger_ID, dc.Ledger
              ORDER BY dc.Ledger;    ";
        var dtData = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        if (dtData.Rows.Count > 0)
        {
            var dataRow = dtData.NewRow();
            dataRow["Ledger"] = "[TOTAL AMOUNT] => ";
        }
        RGrid.DataSource = dtData;
    }

    private void BindSalesType()
    {
        var cmdString = $@"
            SELECT  '[' + RTRIM(Payment_Mode) + ']' + '-' + FORMAT( SUM(ISNULL(LN_Amount,0)),'{ObjGlobal.SysAmountCommaFormat}') [NET SALES] FROM AMS.SB_Master sm
            WHERE sm.Invoice_Date = '{DateTime.Now.GetSystemDate()}' AND sm.CBranch_Id = '{ObjGlobal.SysBranchId}'
            GROUP BY sm.Payment_Mode; ";
        var dtData = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        if (dtData.Rows.Count > 0)
        {
            listBox1.DataSource = dtData;
            listBox1.DisplayMember = "NET SALES";
            listBox1.ValueMember = "NET SALES";
            listBox1.ClearSelected();
        }
    }

    #endregion --------------- METHOD  ---------------

    #region --------------- Global ---------------

    public int Result1 = 0;
    private long _userId;
    private long _ledgerId;
    private string _query = string.Empty;
    private string _actionTag = string.Empty;
    private string _docType = string.Empty;
    private string _docDesc = string.Empty;
    private DataTable _dt = new(string.Empty);
    private DataTable _dtTemp = new(string.Empty);
    private readonly IFinanceEntry _entry = new ClsFinanceEntry();
    private readonly ICashClosingRepository _cashClosingRepository = new CashClosingRepository();

    #endregion --------------- Global ---------------
}