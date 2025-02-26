using MrBLL.Master.LedgerSetup;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Utility.Server;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Master;

public partial class FrmStoreCustomer : MrForm
{
    private void btnCancel_Click(object sender, EventArgs e)
    {
    }

    #region --------------- Global Value ---------------

    private string _PaymentType { get; }
    private decimal _InvoiceAmount { get; }
    private string _ActionTag { get; }
    public bool SaveResult { get; set; }
    public string _TxtAdvance { get; set; }
    public string _TxtCustomer { get; set; }
    public string _CustomerId { get; set; }
    public string _TxtPrintingDesc { get; set; }
    public string _TxtAddress { get; set; }
    public string _TxtPanNo { get; set; }
    public string _TxtBillAmount { get; set; }
    public string _TxtTenderAmount { get; set; }
    public string _TxtChangeAmount { get; set; }
    public string TxtNetAmount { get; set; }
    public long LedgerId { get; set; }
    public long partyLedgerId { get; set; }
    public long _partyLedgerId;

    #endregion --------------- Global Value ---------------

    #region --------------- Store Customer ---------------

    public FrmStoreCustomer(string paymentType, decimal invoiceAmount, string actionTag)
    {
        InitializeComponent();
        _PaymentType = paymentType;
        _InvoiceAmount = invoiceAmount;
        _ActionTag = actionTag;
        var ledgerId = GetConnection
            .GetQueryData($" Select Ledger_Id from Master.AMS.UserInfo where User_Name= '{ObjGlobal.LogInUser}')")
            .GetLong();
        if (ledgerId is 0) ledgerId = ObjGlobal.FinanceCashLedgerId;
        TxtCustomer.Text = paymentType switch
        {
            "CASH" => GetConnection.GetQueryData($"Select GLName from AMS.GeneralLedger where GlId = {ledgerId}"),
            "BANK" => GetConnection.GetQueryData(
                $"Select GLName from AMS.GeneralLedger where GlId ={ObjGlobal.FinanceBankLedgerId} "),
            _ => GetConnection.GetQueryData(
                $"Select GLName from AMS.GeneralLedger where GlId ={ObjGlobal.FinanceCashLedgerId} ")
        };
        LedgerId = ledgerId;
    }

    private void FrmStoreCustomer_Load(object sender, EventArgs e)
    {
        TxtChangeAmount.Clear();
        TxtBillAmount.Text = _InvoiceAmount.ToString(ObjGlobal.SysAmountFormat);
        ControlEnable();
        TxtCustomer.Focus();
    }

    private void TxtCustomer_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnCustomer_Click(sender, e);
        }
        else if (e.Control && e.KeyCode == Keys.N)

        {
            using var frm = new FrmGeneralLedger("Customer", true);
            frm.ShowDialog();
            TxtCustomer.Text = frm.LedgerDesc;
            LedgerId = frm.LedgerId;
            frm.Dispose();
        }
        else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
        {
            if (TxtCustomer.Text.Trim() == string.Empty) BtnCustomer_Click(sender, e);
        }
        else if (e.KeyCode == Keys.F2 || e.KeyCode == Keys.Tab)
        {
            TxtCustomer.Focus();
            var frm = new FrmCustomerList("CustomerList", "Customer List", TxtCustomer.Text, "('Both','Customer')");
            frm.ShowDialog();
            if (frm.SelectList.Count > 0)
            {
                TxtCustomer.Text = frm.SelectList[0]["GLName"].ToString().Trim();
                LedgerId = ObjGlobal.ReturnLong(frm.SelectList[0]["GLID"].ToString().Trim());
                TxtCustomer.Focus();
            }

            frm.Dispose();
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtCustomer, btnCustomer);
        }
    }

    private void Global_Enter(object sender, EventArgs e)
    {
        var txt = (TextBox)sender;
        ObjGlobal.TxtBackColor(txt, 'E');
    }

    private void Global_Leave(object sender, EventArgs e)
    {
        var txt = (TextBox)sender;
        ObjGlobal.TxtBackColor(txt, 'L');
        if (txt.Name.Equals("TxtCustomer") && string.IsNullOrEmpty(txt.Text))
        {
            MessageBox.Show(@"CUSTOMER LEDGER IS BLANK PLEASE SELECT CUSTOMER..!!", ObjGlobal.Caption,
                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            txt.Focus();
        }
    }

    #endregion --------------- Store Customer ---------------

    #region --------------- Event Control ---------------

    private void BtnCustomer_Click(object sender, EventArgs e)
    {
        var category = _PaymentType != null && _PaymentType.ToUpper() is @"CASH" ? "CASH" :
            _PaymentType?.ToUpper() is "BANK" ? "BANK" : "CUSTOMER";
        var frmPickList = new FrmAutoPopList("MAX", @"GENERALLEDGER", ObjGlobal.SearchText, _ActionTag, category,
            "MASTER");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtCustomer.Text = frmPickList.SelectedList[0]["Particular"].ToString().Trim();
                LedgerId = ObjGlobal.ReturnLong(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
                GetCustomerBalance();
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"CUSTOMER LEDGER ARE NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtCustomer.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtCustomer.Focus();
    }

    private void BtnSavePrint_Click(object sender, EventArgs e)
    {
        SaveResult = ValueAssign();
        Close();
    }

    private bool ValueAssign()
    {
        if (LedgerId is 0) return false;
        _TxtCustomer = TxtCustomer.Text.Trim();
        _TxtPrintingDesc = TxtPrintingDesc.Text.Trim();
        _TxtAddress = TxtAddress.Text.Trim();
        _TxtPanNo = TxtPanNo.Text.Trim();
        _TxtBillAmount = TxtBillAmount.Text.Trim();
        _TxtTenderAmount = TxtTenderAmount.Text.Trim();
        _TxtChangeAmount = TxtChangeAmount.Text.Trim();
        _CustomerId = LedgerId.ToString();
        _partyLedgerId = partyLedgerId;
        return true;
    }

    private void FrmStoreCustomer_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
            SendKeys.Send("{Tab}");
    }

    private void TxtTenderAmount_Validating(object sender, CancelEventArgs e)
    {
        decimal.TryParse(TxtTenderAmount.Text, out var understatement);
        if (understatement < _InvoiceAmount)
        {
            MessageBox.Show(@"TENDER AMOUNT MUST BE GREATER THEN OR EQUAL TO INVOICE AMOUNT..!!", ObjGlobal.Caption,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            TxtTenderAmount.Text = string.Empty;
            e.Cancel = true;
            return;
        }

        TxtChangeAmount.Text = (understatement - _InvoiceAmount).ToString(ObjGlobal.SysAmountFormat);
        TxtTenderAmount.Text = ObjGlobal.ReturnDecimal(TxtTenderAmount.Text).ToString(ObjGlobal.SysAmountFormat);
    }

    private void TxtTenderAmount_TextChanged(object sender, EventArgs e)
    {
        var tenderAmount = ObjGlobal.ReturnDouble(TxtTenderAmount.Text);
        var basicAmount = ObjGlobal.ReturnDouble(TxtBillAmount.Text);
        var changeAmount = Math.Abs(basicAmount - tenderAmount);
        TxtChangeAmount.Text = changeAmount.ToString(ObjGlobal.SysAmountFormat);
    }

    private void TxtCustomer_Validating(object sender, CancelEventArgs e)
    {
        if (ObjGlobal.ReturnDouble(TxtBalance.Text) is 0) GetCustomerBalance();
    }

    private void TxtPrintingDesc_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1) BtnPartyName_Click(sender, e);
    }

    private void BtnPartyName_Click(object sender, EventArgs e)
    {
        using var frmPickList =
            new FrmAutoPopList("MAX", @"PARTYLEDGER", ObjGlobal.SearchText, _ActionTag, "", "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtPrintingDesc.Text = frmPickList.SelectedList[0]["Party_Name"].ToString().Trim();
                partyLedgerId = ObjGlobal.ReturnLong(frmPickList.SelectedList[0]["LedgerId"].ToString());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"PRINTING DESCRIPTION NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtPrintingDesc.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtPrintingDesc.Focus();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        ValueAssign();
        SaveResult = true;
        Close();
    }

    #endregion --------------- Event Control ---------------

    #region --------------- Method ---------------

    private void ControlEnable()
    {
        TxtPrintingDesc.Enabled = _PaymentType.Equals("CASH") || _PaymentType.Equals("BANK");
        TxtPanNo.Enabled = TxtPrintingDesc.Enabled;
        TxtPhoneNo.Enabled = TxtPrintingDesc.Enabled;
        TxtAddress.Enabled = TxtPrintingDesc.Enabled;
        TxtAdvance.Enabled = !_PaymentType.Equals("CASH");
    }

    private void GetCustomerBalance()
    {
        var cmdString = GetConnection.GetQueryData(
            $"SELECT SUM(LocalDebit_Amt -LocalCredit_Amt) Amount FROM AMS.accountDetails WHERE Ledger_id ={LedgerId} AND Voucher_Date <=GETDATE();");
        TxtBalance.Text = ObjGlobal.ReturnDecimal(cmdString).ToString(ObjGlobal.SysAmountFormat);
    }

    #endregion --------------- Method ---------------
}