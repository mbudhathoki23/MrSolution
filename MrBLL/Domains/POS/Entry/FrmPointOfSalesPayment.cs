using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTab;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Domains.Billing;
using MrDAL.Global.Common;
using MrDAL.Global.Dialogs;
using MrDAL.Models.Custom;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Entry;

public partial class FrmPointOfSalesPayment : MrForm
{
    private readonly decimal _grandTotal;
    private readonly long _initCashLedgerId;
    private readonly SalesMembershipModel _initMembership;
    private readonly SalesInvoicePaymentMode _initPaymentMode;
    private readonly SalesInvoiceService _invoiceService;
    private bool _accepted;
    private SalesInvoicePaymentModel _model;

    public FrmPointOfSalesPayment(decimal grandTotal, SalesMembershipModel initMembership, SalesInvoicePaymentMode initPaymentMode, long initCashLedgerId)
    {
        InitializeComponent();

        _grandTotal = grandTotal;
        _initMembership = initMembership;
        _initPaymentMode = initPaymentMode;
        _initCashLedgerId = initCashLedgerId;
        _invoiceService = new SalesInvoiceService();

        TxtGrandTotal.Text = grandTotal.ToString(ObjGlobal.SysAmountFormat);
        glkupPartyLedgers.Properties.SearchMode = glkupCreditLedgers.Properties.SearchMode =
            glkupCardLedgers.Properties.SearchMode = GridLookUpSearchMode.AutoSearch;
        glkupPartyLedgers.Properties.PopupFormWidth = 800;
        glkupCreditLedgers.Properties.PopupFormWidth = glkupCardLedgers.Properties.PopupFormWidth = 900;
        chkCustomerManual_CheckedChanged(chkCustomerManual, EventArgs.Empty);
    }

    private async void FrmPointOfSalesPayment_Load(object sender, EventArgs e)
    {
        if (_grandTotal <= 0)
        {
            MessageBox.Show(@"Invalid grand total amount for sales invoice.", @"Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            Close();
            return;
        }

        await LoadInvoiceCustomerLedgersAsync();
        xtraTabControl.SelectedTabPage = _initPaymentMode switch
        {
            SalesInvoicePaymentMode.Cash => tabCash,
            SalesInvoicePaymentMode.Credit => tabCredit,
            SalesInvoicePaymentMode.Card => tabCard,
            _ => throw new ArgumentOutOfRangeException()
        };
        if (_initMembership == null) return;
        TxtInvoiceTo.Text = _initMembership.MShipDesc;
        TxtPhoneNo.Text = _initMembership.PhoneNo;
    }

    private SalesInvoicePaymentMode GetSelectedPaymentMode()
    {
        if (xtraTabControl.SelectedTabPage == tabCash) return SalesInvoicePaymentMode.Cash;
        if (xtraTabControl.SelectedTabPage == tabCard) return SalesInvoicePaymentMode.Card;
        if (xtraTabControl.SelectedTabPage == tabCredit) return SalesInvoicePaymentMode.Credit;

        throw new InvalidOperationException("The selected payment tab is invalid.");
    }

    private async Task<bool> LoadInvoiceCustomerLedgersAsync()
    {
        bsPartyLedgers.DataSource = null;

        var response = await _invoiceService.GetPartyLedgersAsync();
        if (!response.Success)
        {
            response.ShowErrorDialog("Error fetching customers ledgers.");
            return false;
        }

        glkupPartyLedgers.Properties.DataSource = response.List;
        glkupPartyLedgers.Properties.DisplayMember = "PartyName";
        glkupPartyLedgers.Properties.ValueMember = "LedgerId";
        return true;
    }

    private async Task<bool> LoadGeneralLedgersAsync()
    {
        bsCreditLedgers.DataSource = bsCardLedgers.DataSource = null;

        var paymentMode = GetSelectedPaymentMode();
        var response = await _invoiceService.GetGeneralLedgersAsync(paymentMode);
        if (!response.Success)
        {
            response.ShowErrorDialog("Error fetching general ledgers.");
            return false;
        }

        switch (paymentMode)
        {
            case SalesInvoicePaymentMode.Credit:
                bsCreditLedgers.DataSource = response.List;
                break;

            case SalesInvoicePaymentMode.Card:
                bsCardLedgers.DataSource = response.List;
                break;
        }

        return true;
    }

    private void glkupPartyLedgers_EditValueChanged(object sender, EventArgs e)
    {
        TxtInvoiceTo.Clear();
        TxtAddress.Clear();
        TxtPanNo.Clear();
        TxtPhoneNo.Clear();

        // auto populate customer detail
        if (glkupPartyLedgers.EditValue == null ||
            !(glkupPartyLedgers.GetSelectedDataRow() is SalesPartyLedgerModel model)) return;
        TxtInvoiceTo.Text = model.PartyName;
        TxtAddress.Text = model.Address;
        TxtPhoneNo.Text = model.MobileNo;
        TxtPanNo.Text = model.VatNo;
    }

    private async void glkupGeneralLedgers_EditValueChanged(object sender, EventArgs e)
    {
        TxtLedgerBalance.Clear();

        // load balance for the selected ledger
        if (glkupCreditLedgers.EditValue == null ||
            !(glkupCreditLedgers.GetSelectedDataRow() is SalesGeneralLedgerModel model)) return;
        var response = await _invoiceService.GetBalanceAsync(model.LedgerId);
        if (!response.Success)
        {
            response.ShowErrorDialog("Error fetching selected ledger balance.");
            return;
        }

        TxtLedgerBalance.Text = response.Model.ToString(ObjGlobal.SysCurrencyFormat);
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private bool InputFieldsValid()
    {
        var paymentMode = GetSelectedPaymentMode();
        switch (paymentMode)
        {
            case SalesInvoicePaymentMode.Cash:
                var tender = TxtTenderAmount.Text.GetPositiveDecimalOrZero();
                var change = TxtChangeAmount.Text.GetPositiveDecimalOrZero();

                if (tender == 0)
                {
                    this.NotifyValidationError(TxtTenderAmount, @"The tender amount is not valid.");
                    return false;
                }

                if (tender - change != _grandTotal)
                {
                    this.NotifyError(
                        @"The sum of tender and return amount is not valid for given grand total amount.", 4);
                    return false;
                }

                break;

            case SalesInvoicePaymentMode.Credit:
                if (glkupCreditLedgers.GetSelectedDataRow() is not SalesGeneralLedgerModel)
                {
                    this.NotifyValidationError(glkupCreditLedgers, @"No credit customer ledger selected");
                    return false;
                }

                break;

            case SalesInvoicePaymentMode.Card:
                if (glkupCardLedgers.GetSelectedDataRow() is not SalesGeneralLedgerModel)
                {
                    this.NotifyValidationError(glkupCardLedgers,
                        @"No card ledger or relevant record for selected payment mode specified.", 4);
                    return false;
                }

                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        return true;
    }

    private void BtnAccept_Click(object sender, EventArgs e)
    {
        if (!InputFieldsValid()) return;

        var party = glkupPartyLedgers.GetSelectedDataRow() as SalesPartyLedgerModel;
        var paymentMode = GetSelectedPaymentMode();

        long ledgerId;
        switch (paymentMode)
        {
            case SalesInvoicePaymentMode.Cash:
                ledgerId = _initCashLedgerId;
                break;

            case SalesInvoicePaymentMode.Credit:
                ledgerId = ((SalesGeneralLedgerModel)glkupCreditLedgers.GetSelectedDataRow()).LedgerId;
                break;

            case SalesInvoicePaymentMode.Card:
                ledgerId = ((SalesGeneralLedgerModel)glkupCardLedgers.GetSelectedDataRow()).LedgerId;
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        var model = new SalesInvoicePaymentModel
        {
            LedgerId = ledgerId,
            InvoiceToName = TxtInvoiceTo.Text.NullableTrimmedText(),
            InvoiceToAddress = TxtAddress.Text.NullableTrimmedText(),
            InvoiceToPhone = TxtPhoneNo.Text.NullableTrimmedText(),
            InvoiceToPan = TxtPanNo.Text.NullableTrimmedText(),
            PaymentMode = paymentMode,
            PrintInvoice = chkPrintInvoice.Checked,
            CreditLedgerId = (glkupCreditLedgers.GetSelectedDataRow() as SalesGeneralLedgerModel)?.LedgerId,
            CardLedgerId = (glkupCardLedgers.GetSelectedDataRow() as SalesGeneralLedgerModel)?.LedgerId,
            ContactPerson = null,
            Remarks = txtRemarks.Text.NullableTrimmedText(),
            ReturnAmount = paymentMode == SalesInvoicePaymentMode.Cash
                ? TxtChangeAmount.Text.GetPositiveDecimalOrZero()
                : 0,
            TenderAmount = paymentMode == SalesInvoicePaymentMode.Cash
                ? TxtTenderAmount.Text.GetPositiveDecimalOrZero()
                : 0
        };
        _model = model;
        _accepted = true;
        Close();
    }

    private void chkCustomerManual_CheckedChanged(object sender, EventArgs e)
    {
        glkupPartyLedgers.EditValue = null;
        TxtInvoiceTo.Clear();
        TxtAddress.Clear();
        TxtPhoneNo.Clear();
        TxtPanNo.Clear();

        if (chkCustomerManual.Checked)
        {
            TxtInvoiceTo.ReadOnly = TxtAddress.ReadOnly = TxtPhoneNo.ReadOnly = TxtPanNo.ReadOnly = false;
            glkupPartyLedgers.EditValue = null;
            glkupPartyLedgers.Enabled = false;
            TxtInvoiceTo.Focus();
        }
        else
        {
            TxtInvoiceTo.ReadOnly = TxtAddress.ReadOnly = TxtPhoneNo.ReadOnly = TxtPanNo.ReadOnly = true;
            glkupPartyLedgers.Enabled = true;
            glkupPartyLedgers.Focus();
        }
    }

    private void TxtTenderAmount_TextChanged(object sender, EventArgs e)
    {
        var billAmount = TxtGrandTotal.Text.GetDecimal();
        if (billAmount <= 0)
        {
            MessageBox.Show(@"Invalid bill amount.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var tenderAmount = TxtTenderAmount.Text.GetDecimal();
        TxtChangeAmount.Text = tenderAmount > billAmount ? (tenderAmount - billAmount).ToString("0.00") : @"0";
    }

    private void glkupPartyLedgers_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            e.Handled = true;
            glkupPartyLedgers.EditValue = null;
            TxtInvoiceTo.Clear();
            TxtAddress.Clear();
            TxtPhoneNo.Clear();
            TxtPanNo.Clear();
            glkupPartyLedgers.Focus();
        }
    }

    private void glkupGeneralLedgers_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            e.Handled = true;
            glkupCreditLedgers.EditValue = null;
            glkupCreditLedgers.Focus();
        }
    }

    private async void xtraTabControl_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
    {
        TxtTenderAmount.Clear();
        TxtChangeAmount.Clear();
        await LoadGeneralLedgersAsync();
    }

    public static (bool Accepted, SalesInvoicePaymentModel Model) GetPayment(decimal grandTotal, SalesMembershipModel membership, SalesInvoicePaymentMode initPaymentMode, long initCashLedgerId)
    {
        var form = new FrmPointOfSalesPayment(grandTotal, membership, initPaymentMode, initCashLedgerId);
        form.ShowDialog();
        return (form._accepted, form._model);
    }
}